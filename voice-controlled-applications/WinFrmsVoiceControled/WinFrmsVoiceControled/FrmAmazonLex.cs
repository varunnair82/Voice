using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using NAudio;
using NAudio.Utils;
using NAudio.Wave;
using NAudio.MediaFoundation;
using Amazon.Lex;
using Amazon.Lex.Model;
using WMPLib;

namespace WindowsFormsApp1
{
    public partial class FrmAmazonLex : Form
    {
        public FrmAmazonLex()
        {
            InitializeComponent();
        }

        private void FrmAmazonLex_Load(object sender, EventArgs e)
        {
            //mytimer.Start();


        }

        MemoryStream memoryStream = null;
        WaveIn waveIn = null;
        WaveOut waveOut = null;
        WaveInProvider waveInProvider = null;
        WaveFileWriter waveWriter = null;
        BufferedWaveProvider buff = null;
        SoundPlayer myPlayer = null;
        WindowsMediaPlayerClass mediaPlayer = null;

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
            }

            if (waveWriter == null)
                waveWriter = new WaveFileWriter(new IgnoreDisposeStream(memoryStream), waveIn.WaveFormat);

            waveIn.StartRecording();

            //waveInProvider = new NAudio.Wave.WaveInProvider(waveIn);
            //buff = new BufferedWaveProvider(waveIn.WaveFormat);
        }

        private void waveIn_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void StopRecord()
        {

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

            SendReq(memoryStream);
        }

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
            

            //using (WaveStream blockAlignedStream = new BlockAlignReductionStream( WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(postContentResponse.AudioStream))))
            //{
            //    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
            //    {
            //        waveOut.Init(blockAlignedStream);
            //        waveOut.Play();
            //        while (waveOut.PlaybackState == PlaybackState.Playing)
            //        {
            //            System.Threading.Thread.Sleep(100);
            //        }
            //    }
            //}


            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (fs != null)
            //    {
            //        fs.Close();
            //        fs.Dispose();
            //    }                
            //}

        }

        private void MediaPlayer_PlayStateChange(int NewState)
        {
            if (WMPPlayState.wmppsPlaying != mediaPlayer.IWMPPlayer2_playState)
            {
                mediaPlayer.stop();
            }

        }

        private void MediaPlayer_EndOfStream(int Result)
        {
            mediaPlayer.stop();
        }

        private void PlayFile(String url)
        {
            //Player = new WMPLib.WindowsMediaPlayer();
            //Player.PlayStateChange += Player_PlayStateChange;
            //Player.URL = url;
            //Player.controls.play();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            memoryStream = null;
            StartRecord();
        }

        private void bthStop_Click(object sender, EventArgs e)
        {
            StopRecord();
        }


        private void mytimer_Tick(object sender, EventArgs e)
        {
            //if (sourceStream != null)
            //{
            //    sourceStream.StopRecording();
            //    waveWriter.Flush();

            //    var amazonLexClient = new AmazonLexClient(Amazon.RegionEndpoint.EUWest1);
            //    var amazonPostRequest = new Amazon.Lex.Model.PostContentRequest();
            //    var amazonPostResponse = new Amazon.Lex.Model.PostContentResponse();
            //    amazonPostRequest.BotAlias = "myBot";
            //    amazonPostRequest.BotName = "VarunsBot";
            //    amazonPostRequest.ContentType = "audio/l16; rate=16000; channels=1";
            //    amazonPostRequest.UserId = "user1";
            //    memoryStream.Position = 0;
            //    amazonPostRequest.InputStream = memoryStream;
            //    try
            //    {
            //        amazonPostResponse = amazonLexClient.PostContent(amazonPostRequest);
            //        if (amazonPostResponse.ContentLength > 0)
            //            MessageBox.Show(amazonPostResponse.Message);
            //    }

            //    catch (Exception w)
            //    {
            //        sourceStream = null;
            //        mytimer.Enabled = false;
            //        MessageBox.Show(string.Format("Exception caught : {0}", w.Message));
            //        MessageBox.Show(w.Message);

            //    }
            //}
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            WaveFileReader waveFileReader = new WaveFileReader("C:\\Test.wav");

            WaveOut waveOut = new WaveOut();
            waveOut.Init(waveFileReader);
            waveOut.Play();

            //using (var inputStream = File.OpenRead("C:\\Test.wav"))
            //{
            //    using (var reader = new WaveFileReader(inputStream))
            //    {
            //        SoundPlayer myPlayer = new SoundPlayer(reader);
            //        myPlayer.Load();
            //        myPlayer.Play();
            //    }
            //}

            //SoundPlayer myPlayer = new SoundPlayer(streamIn);
            //myPlayer.Load();
            //myPlayer.Play();
        }

        string BotName { get; set; }
        string BotAlias { get; set; }

        private void radioBtnBotSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnBookHotel.Checked)
            {
                this.BotName = "BookHotel";
                this.BotAlias = "myTripBot";
            }
            if(radioBtnEngage.Checked)
            {
                this.BotName = "Engage";
                this.BotAlias = "Engage";
            }
        }        
    }
}
