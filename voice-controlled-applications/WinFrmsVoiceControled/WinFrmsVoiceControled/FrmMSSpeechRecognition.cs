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

namespace WindowsFormsApp1
{
    public partial class FrmMSSpeechRecognition : Form
    {
        public FrmMSSpeechRecognition()
        {
            InitializeComponent();
        }

        private SpeechRecognitionEngine sr = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            // Create a new SpeechRecognitionEngine instance.
            //SpeechRecognizer sr = new SpeechRecognizer();
            sr = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));


            //sr.SetInputToDefaultAudioDevice();
            sr.SetInputToWaveFile(@"c:\sound\Colors.wav");

            // Create a simple grammar that recognizes "red", "green", or "blue".
            Choices colors = new Choices();
            colors.Add(new string[] { "yellow", "blue", "green", "orange", "white", "black", "purple", "red", "Lime", "tomato", "RoyalBlue"});

            // Create a GrammarBuilder object and append the Choices object.
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            Grammar g = new Grammar(gb);
            sr.LoadGrammarAsync(g);

            // Create a default dictation grammar.
            DictationGrammar defaultDictationGrammar = new DictationGrammar();
            defaultDictationGrammar.Name = "default dictation";
            defaultDictationGrammar.Enabled = true;

            // Create the spelling dictation grammar.
            DictationGrammar spellingDictationGrammar = new DictationGrammar("grammar:dictation#spelling");
            spellingDictationGrammar.Name = "spelling dictation";
            spellingDictationGrammar.Enabled = true;

            // Create the question dictation grammar.
            DictationGrammar customDictationGrammar = new DictationGrammar("grammar:dictation");
            customDictationGrammar.Name = "question dictation";
            customDictationGrammar.Enabled = true;

            sr.LoadGrammarAsync(defaultDictationGrammar);
            sr.LoadGrammarAsync(spellingDictationGrammar);
            sr.LoadGrammarAsync(customDictationGrammar);


            sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);
            sr.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);

            sr.SpeechDetected += Sr_SpeechDetected;

            //sr.RecognizeAsync();
        }

        private void Sr_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            //MessageBox.Show("Speech detected " + e.AudioPosition.Ticks.ToString());
        }

        // Create a simple handler for the SpeechRecognized event.
        private void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {            
            if (e.Result.Confidence >= 0.5)
            {
                label2.Text = e.Result.Text;
                //high enough confidence, use result
                MessageBox.Show(e.Result.Text);
                switch (e.Result.Text)
                {
                    case "red":
                        this.BackColor = Color.Red;
                        break;
                    case "blue":
                        this.BackColor = Color.Blue;
                        break;
                    case "green":
                        this.BackColor = Color.Green;
                        break;
                    case "orange":
                        this.BackColor = Color.Orange;
                        break;
                    case "white":
                        this.BackColor = Color.White;
                        break;
                    case "black":
                        this.BackColor = Color.Black;
                        break;
                    case "purple":
                        this.BackColor = Color.Purple;
                        break;
                    case "yellow":
                        this.BackColor = Color.Yellow;
                        break;
                    case "lime":
                        this.BackColor = Color.Lime;
                        break;
                    case "tomato":
                        this.BackColor = Color.Tomato;
                        break;
                    case "RoyalBlue":
                        this.BackColor = Color.RoyalBlue;
                        break;

                }
            }
            else
            {
                //reject result
            }           
        }

        bool completed = true;
        // Handle the RecognizeCompleted event.
        void recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
            {
                if (e.Error != null)
                {
                    MessageBox.Show(string.Format("  Error encountered, {0}: {1}",e.Error.GetType().Name, e.Error.Message));
                }
                if (e.Cancelled)
                {
                    MessageBox.Show("  Operation cancelled.");
                }
                if (e.InputStreamEnded)
                {
                    completed = true;
                    MessageBox.Show("  End of stream encountered.");
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            if (completed)
            {
                if (rdbAudioFile.Checked && sr != null)
                {
                    sr.RecognizeAsync(RecognizeMode.Single);
                }
                else
                {
                    sr.RecognizeAsync(RecognizeMode.Multiple);
                }
            }
            else
            {
                completed = false;
            }
        }

        private void rdbAudioFile_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbAudioFile.Checked && sr != null)
            {
                sr.SetInputToWaveFile(@"c:\sound\Colors.wav");
            }
        }

        private void rdbMic_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMic.Checked && sr != null)
            {
                sr.SetInputToDefaultAudioDevice();
            }
        }
    }
}
