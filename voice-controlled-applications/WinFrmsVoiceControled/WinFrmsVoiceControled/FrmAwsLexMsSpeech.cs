using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

using System.IO;
using System.Media;
using NAudio;
using NAudio.Utils;
using NAudio.Wave;
using NAudio.MediaFoundation;
using Amazon.Lex;
using Amazon.Lex.Model;
using WMPLib;

namespace WinFrmsVoiceControled
{
    public partial class FrmAwsLexMsSpeech : Form
    {
        public FrmAwsLexMsSpeech()
        {
            InitializeComponent();
        }

        private SpeechRecognitionEngine sr = null;

        MemoryStream memoryStream = null;
        WaveIn waveIn = null;
        WaveOut waveOut = null;
        WaveInProvider waveInProvider = null;
        WaveFileWriter waveWriter = null;
        BufferedWaveProvider buff = null;
        SoundPlayer myPlayer = null;
        WindowsMediaPlayerClass mediaPlayer = null;
        WindowsMediaPlayerClass EngageMediaPlayer = null;



        string BotName { get; set; }
        string BotAlias { get; set; }

        private void FrmAwsLexMsSpeech_Load(object sender, EventArgs e)
        {

            // Create a new SpeechRecognitionEngine instance.
            sr = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-GB"));
            // Configure mic to be used.
            sr.SetInputToDefaultAudioDevice();
            // Create a simple grammar that recognizes "Engage".
            Choices wakeWord = new Choices();
            wakeWord.Add("Engage");
            // Create a GrammarBuilder object and append the Choices object.
            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = new System.Globalization.CultureInfo("en-GB");
            gb.Append(wakeWord);
            // Create Grammar with grammar builder.
            Grammar g = new Grammar(gb);
            // Load the Grammar to speech recognition engine.
            sr.LoadGrammarAsync(g);
            // Initialize speech recognized event handler.
            sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);



            sr.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);
            sr.SpeechDetected += Sr_SpeechDetected;

            this.BotName = "BookTrip";
            this.BotAlias = "myTripBot";

            EngageMediaPlayer = new WindowsMediaPlayerClass();
        }

        private enum MsSpeechState { Started, Stoped };
        MsSpeechState state;
        private void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence >= 0.5)
            {
                if (e.Result.Text.ToUpper() == "ENGAGE")
                {
                    EngageMediaPlayer.URL = @"Notification Sound\notification11.mp3";
                    EngageMediaPlayer.play();
                    EngageMediaPlayer.EndOfStream += EngageMediaPlayer_EndOfStream;
                    EngageMediaPlayer.PlayStateChange += EngageMediaPlayer_PlayStateChange;
                    state = MsSpeechState.Started;


                }
                else if (e.Result.Text.ToUpper() == "STOP")
                {
                    EngageMediaPlayer.URL = @"C:\to-the-point.mp3";
                    EngageMediaPlayer.play();
                    sr.RecognizeAsyncStop();
                    StopRecord();
                    state = MsSpeechState.Stoped;
                }
            }
        }

        private void EngageMediaPlayer_PlayStateChange(int NewState)
        {
            if (WMPPlayState.wmppsMediaEnded == EngageMediaPlayer.IWMPPlayer2_playState)
            {
                EngageMediaPlayer.stop();
                label2.Text = "Start speaking.";
                StartRecord();
            }
        }

        private void EngageMediaPlayer_EndOfStream(int Result)
        {
            if (state == MsSpeechState.Started)
                StartRecord();
        }

        private void Sr_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(string.Format("  Error encountered, {0}: {1}", e.Error.GetType().Name, e.Error.Message));
            }
            if (e.Cancelled)
            {
                MessageBox.Show("  Operation cancelled.");
            }
            if (e.InputStreamEnded)
            {
                MessageBox.Show("  End of stream encountered.");
            }
            //sr.RecognizeAsyncStop();            
        }

        private void btnRecognise_Click(object sender, EventArgs e)
        {
            sr.SetInputToDefaultAudioDevice();
            sr.RecognizeAsync();
        }

        private void StartRecord()
        {
            if (memoryStream == null)
                memoryStream = new MemoryStream();

            if (waveIn == null)
            {
                waveIn = new NAudio.Wave.WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
                waveIn.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(waveIn_DataAvailable);
                waveIn.RecordingStopped += WaveIn_RecordingStopped;
            }

            if (waveWriter == null)
                waveWriter = new WaveFileWriter(new IgnoreDisposeStream(memoryStream), waveIn.WaveFormat);

            waveIn.StartRecording();
        }

        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {

        }

        int counter = 0;
        sbyte threshold = 70;        
        private void waveIn_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            // Write wave form data to memoryStream as bytes.
            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();

            // Sum up the value of data recorded in a frame
            double sum = 0;
            for (int n = 0; n < e.BytesRecorded - 1; n++)
            {
                double sample = BitConverter.ToInt16(e.Buffer, n) / System.Int16.MaxValue;
                sum += (sample * sample);
            }

            double rms = Math.Sqrt(sum / e.Buffer.Length);
            double decibel = 92.8 + 20 * Math.Log10(rms);

            if (decibel < threshold)
                counter++;

            double silenceSamples = (double)counter / waveIn.WaveFormat.Channels;
            double silenceDuration = (silenceSamples / waveIn.WaveFormat.SampleRate) * 1000;
            if (TimeSpan.FromSeconds(silenceDuration).Seconds > 3)
            {
                if (memoryStream != null)
                {
                    SendReq(memoryStream);
                }
                counter = 0;
            }
        }

        private void StopRecord()
        {
            memoryStream = null;
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
        }

        bool stopRecording = false;

        private void SendReq(Stream stream)
        {
            FileStream fs = null;
            //try
            //{
            var amazonLexClient = new AmazonLexClient(Amazon.RegionEndpoint.EUWest1);


            PostContentRequest postContentRequest = new PostContentRequest();

            postContentRequest.BotAlias = this.BotAlias;
            postContentRequest.BotName = this.BotName;
            postContentRequest.ContentType = "audio/l16; rate=16000; channels=1";
            //postContentRequest.ContentType = "audio/mpeg;";
            stream.Position = 0;
            postContentRequest.InputStream = stream;
            postContentRequest.UserId = "user";
            //File.WriteAllBytes()
            Task<PostContentResponse> task = amazonLexClient.PostContentAsync(postContentRequest);

            task.Wait();

            PostContentResponse postContentResponse = task.Result;

            //MessageBox.Show(postContentResponse.Message);

            var filename = string.Format(@"C:\Test Lex\Test_{0}.mpg", DateTime.Now.ToFileTime());
            fs = new FileStream(filename, FileMode.Create);
            postContentResponse.AudioStream.CopyTo(fs);
            fs.Close();

            richTextBox1.SelectionColor = Color.OrangeRed;
            richTextBox1.AppendText("\n" + postContentResponse.InputTranscript);
            richTextBox1.SelectionColor = Color.Indigo;
            richTextBox1.AppendText("\n" + "\t" + postContentResponse.Message);

            mediaPlayer = new WindowsMediaPlayerClass();
            mediaPlayer.URL = filename;
            mediaPlayer.play();
            mediaPlayer.EndOfStream += MediaPlayer_EndOfStream;
            mediaPlayer.PlayStateChange += MediaPlayer_PlayStateChange;

            if (postContentResponse != null)
            {
                label3.Text = postContentResponse.DialogState.Value;
                if (postContentResponse.DialogState == DialogState.ReadyForFulfillment || postContentResponse.InputTranscript.ToUpper().Contains("STOP"))
                {
                    stopRecording = true;
                    StopRecord();
                }
            }
        }

        private void MediaPlayer_PlayStateChange(int NewState)
        {
            if (WMPPlayState.wmppsPlaying != mediaPlayer.IWMPPlayer2_playState)
            {
                //mediaPlayer.stop();
                memoryStream = null;
                StopRecord();
                
            }
            if (WMPPlayState.wmppsStopped == mediaPlayer.IWMPPlayer2_playState && !stopRecording)
            {
                StartRecord();
            }

        }

        private void MediaPlayer_EndOfStream(int Result)
        {
            mediaPlayer.stop();
            //this.btnRecognise.PerformClick();      
        }

    }
}