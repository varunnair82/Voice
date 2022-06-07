namespace WindowsFormsApp1
{
    partial class FrmMSSpeechRecognition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSpeechRecognized = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRecognise = new System.Windows.Forms.Button();
            this.rdbMic = new System.Windows.Forms.RadioButton();
            this.grp_VoiceInputMethod = new System.Windows.Forms.GroupBox();
            this.rdbAudioFile = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grp_VoiceInputMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSpeechRecognized
            // 
            this.lblSpeechRecognized.AutoSize = true;
            this.lblSpeechRecognized.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeechRecognized.Location = new System.Drawing.Point(12, 69);
            this.lblSpeechRecognized.Name = "lblSpeechRecognized";
            this.lblSpeechRecognized.Size = new System.Drawing.Size(279, 31);
            this.lblSpeechRecognized.TabIndex = 0;
            this.lblSpeechRecognized.Text = "Speech Recognized : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(279, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = " ";
            // 
            // btnRecognise
            // 
            this.btnRecognise.Location = new System.Drawing.Point(388, 13);
            this.btnRecognise.Name = "btnRecognise";
            this.btnRecognise.Size = new System.Drawing.Size(75, 23);
            this.btnRecognise.TabIndex = 2;
            this.btnRecognise.Text = "Recognise";
            this.btnRecognise.UseVisualStyleBackColor = true;
            this.btnRecognise.Click += new System.EventHandler(this.button1_Click);
            // 
            // rdbMic
            // 
            this.rdbMic.AutoSize = true;
            this.rdbMic.Location = new System.Drawing.Point(126, 19);
            this.rdbMic.Name = "rdbMic";
            this.rdbMic.Size = new System.Drawing.Size(42, 17);
            this.rdbMic.TabIndex = 3;
            this.rdbMic.TabStop = true;
            this.rdbMic.Text = "Mic";
            this.rdbMic.UseVisualStyleBackColor = true;
            this.rdbMic.CheckedChanged += new System.EventHandler(this.rdbMic_CheckedChanged);
            // 
            // grp_VoiceInputMethod
            // 
            this.grp_VoiceInputMethod.Controls.Add(this.rdbAudioFile);
            this.grp_VoiceInputMethod.Controls.Add(this.rdbMic);
            this.grp_VoiceInputMethod.Location = new System.Drawing.Point(18, 12);
            this.grp_VoiceInputMethod.Name = "grp_VoiceInputMethod";
            this.grp_VoiceInputMethod.Size = new System.Drawing.Size(186, 49);
            this.grp_VoiceInputMethod.TabIndex = 4;
            this.grp_VoiceInputMethod.TabStop = false;
            this.grp_VoiceInputMethod.Text = "Voice Input Method";
            // 
            // rdbAudioFile
            // 
            this.rdbAudioFile.AutoSize = true;
            this.rdbAudioFile.Checked = true;
            this.rdbAudioFile.Location = new System.Drawing.Point(15, 19);
            this.rdbAudioFile.Name = "rdbAudioFile";
            this.rdbAudioFile.Size = new System.Drawing.Size(68, 17);
            this.rdbAudioFile.TabIndex = 4;
            this.rdbAudioFile.TabStop = true;
            this.rdbAudioFile.Text = "Audio file";
            this.rdbAudioFile.UseVisualStyleBackColor = true;
            this.rdbAudioFile.CheckedChanged += new System.EventHandler(this.rdbAudioFile_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(239, 168);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 31);
            this.textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 213);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.grp_VoiceInputMethod);
            this.Controls.Add(this.btnRecognise);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSpeechRecognized);
            this.Name = "Form1";
            this.Text = "MS Speech Recognition";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grp_VoiceInputMethod.ResumeLayout(false);
            this.grp_VoiceInputMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSpeechRecognized;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRecognise;
        private System.Windows.Forms.RadioButton rdbMic;
        private System.Windows.Forms.GroupBox grp_VoiceInputMethod;
        private System.Windows.Forms.RadioButton rdbAudioFile;
        private System.Windows.Forms.TextBox textBox1;
    }
}

