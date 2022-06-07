namespace WindowsFormsApp1
{
    partial class FrmAmazonLex
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
            this.components = new System.ComponentModel.Container();
            this.mytimer = new System.Windows.Forms.Timer(this.components);
            this.btnRecord = new System.Windows.Forms.Button();
            this.bthStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBoxSelectBot = new System.Windows.Forms.GroupBox();
            this.radioBtnBookHotel = new System.Windows.Forms.RadioButton();
            this.radioBtnEngage = new System.Windows.Forms.RadioButton();
            this.groupBoxSelectBot.SuspendLayout();
            this.SuspendLayout();
            // 
            // mytimer
            // 
            this.mytimer.Interval = 5000;
            this.mytimer.Tick += new System.EventHandler(this.mytimer_Tick);
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(32, 136);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(79, 29);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // bthStop
            // 
            this.bthStop.Location = new System.Drawing.Point(32, 193);
            this.bthStop.Name = "bthStop";
            this.bthStop.Size = new System.Drawing.Size(79, 29);
            this.bthStop.TabIndex = 1;
            this.bthStop.Text = "Stop";
            this.bthStop.UseVisualStyleBackColor = true;
            this.bthStop.Click += new System.EventHandler(this.bthStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(32, 251);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(79, 29);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(135, 67);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(395, 352);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // groupBoxSelectBot
            // 
            this.groupBoxSelectBot.Controls.Add(this.radioBtnBookHotel);
            this.groupBoxSelectBot.Controls.Add(this.radioBtnEngage);
            this.groupBoxSelectBot.Location = new System.Drawing.Point(192, 10);
            this.groupBoxSelectBot.Name = "groupBoxSelectBot";
            this.groupBoxSelectBot.Size = new System.Drawing.Size(267, 51);
            this.groupBoxSelectBot.TabIndex = 4;
            this.groupBoxSelectBot.TabStop = false;
            this.groupBoxSelectBot.Text = "Select Bot";
            // 
            // radioBtnBookHotel
            // 
            this.radioBtnBookHotel.AutoSize = true;
            this.radioBtnBookHotel.Checked = true;
            this.radioBtnBookHotel.Location = new System.Drawing.Point(157, 19);
            this.radioBtnBookHotel.Name = "radioBtnBookHotel";
            this.radioBtnBookHotel.Size = new System.Drawing.Size(78, 17);
            this.radioBtnBookHotel.TabIndex = 1;
            this.radioBtnBookHotel.TabStop = true;
            this.radioBtnBookHotel.Text = "Book Hotel";
            this.radioBtnBookHotel.UseVisualStyleBackColor = true;
            this.radioBtnBookHotel.CheckedChanged += new System.EventHandler(this.radioBtnBotSelect_CheckedChanged);
            // 
            // radioBtnEngage
            // 
            this.radioBtnEngage.AutoSize = true;
            this.radioBtnEngage.Location = new System.Drawing.Point(38, 20);
            this.radioBtnEngage.Name = "radioBtnEngage";
            this.radioBtnEngage.Size = new System.Drawing.Size(62, 17);
            this.radioBtnEngage.TabIndex = 0;
            this.radioBtnEngage.Text = "Engage";
            this.radioBtnEngage.UseVisualStyleBackColor = true;
            this.radioBtnEngage.CheckedChanged += new System.EventHandler(this.radioBtnBotSelect_CheckedChanged);
            // 
            // FrmAmazonLex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 431);
            this.Controls.Add(this.groupBoxSelectBot);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.bthStop);
            this.Controls.Add(this.btnRecord);
            this.Name = "FrmAmazonLex";
            this.Text = "Amazon Lex Example";
            this.Load += new System.EventHandler(this.FrmAmazonLex_Load);
            this.groupBoxSelectBot.ResumeLayout(false);
            this.groupBoxSelectBot.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer mytimer;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button bthStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBoxSelectBot;
        private System.Windows.Forms.RadioButton radioBtnBookHotel;
        private System.Windows.Forms.RadioButton radioBtnEngage;
    }
}