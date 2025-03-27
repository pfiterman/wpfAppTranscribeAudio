namespace TranscribeAudio
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            FbdFileDestination = new FolderBrowserDialog();
            BtnSelectFolder = new Button();
            TxtFileName = new TextBox();
            LblFileName = new Label();
            TxtFileSavingFolder = new TextBox();
            LblFolderDestination = new Label();
            CbOptionTranscribeAudio = new ComboBox();
            LblOption = new Label();
            BtnTranscribeAudio = new Button();
            LblSelectAudioFile = new Label();
            BtnAudioFile = new Button();
            TxtAudioFile = new TextBox();
            OfdAudioFile = new OpenFileDialog();
            LblRecognitionLanguage = new Label();
            CbRecognitionLanguage = new ComboBox();
            GbStatistics = new GroupBox();
            LblNumRecognized = new Label();
            LblRecognized = new Label();
            ProgressTranscribingReport = new ProgressBar();
            LblNumRecognizing = new Label();
            LblRecognizing = new Label();
            LblTranscribeMode = new Label();
            CbTranscribeMode = new ComboBox();
            Imlimages = new ImageList(components);
            GbStatistics.SuspendLayout();
            SuspendLayout();
            // 
            // BtnSelectFolder
            // 
            BtnSelectFolder.Location = new Point(231, 84);
            BtnSelectFolder.Name = "BtnSelectFolder";
            BtnSelectFolder.Size = new Size(65, 23);
            BtnSelectFolder.TabIndex = 18;
            BtnSelectFolder.Text = "Select";
            BtnSelectFolder.UseVisualStyleBackColor = true;
            BtnSelectFolder.Click += BtnSelectFolder_Click;
            // 
            // TxtFileName
            // 
            TxtFileName.Location = new Point(232, 121);
            TxtFileName.Name = "TxtFileName";
            TxtFileName.Size = new Size(362, 23);
            TxtFileName.TabIndex = 17;
            // 
            // LblFileName
            // 
            LblFileName.AutoSize = true;
            LblFileName.Location = new Point(12, 126);
            LblFileName.Name = "LblFileName";
            LblFileName.Size = new Size(100, 15);
            LblFileName.TabIndex = 16;
            LblFileName.Text = "Output file name:";
            // 
            // TxtFileSavingFolder
            // 
            TxtFileSavingFolder.Location = new Point(301, 83);
            TxtFileSavingFolder.Name = "TxtFileSavingFolder";
            TxtFileSavingFolder.Size = new Size(293, 23);
            TxtFileSavingFolder.TabIndex = 15;
            // 
            // LblFolderDestination
            // 
            LblFolderDestination.AutoSize = true;
            LblFolderDestination.Location = new Point(12, 87);
            LblFolderDestination.Name = "LblFolderDestination";
            LblFolderDestination.Size = new Size(194, 15);
            LblFolderDestination.TabIndex = 14;
            LblFolderDestination.Text = "Where do you want to save the file?";
            // 
            // CbOptionTranscribeAudio
            // 
            CbOptionTranscribeAudio.FormattingEnabled = true;
            CbOptionTranscribeAudio.Items.AddRange(new object[] { "From your voice (speaking on the microphone)", "From an audio file (.mp3)" });
            CbOptionTranscribeAudio.Location = new Point(232, 19);
            CbOptionTranscribeAudio.Name = "CbOptionTranscribeAudio";
            CbOptionTranscribeAudio.Size = new Size(362, 23);
            CbOptionTranscribeAudio.TabIndex = 13;
            CbOptionTranscribeAudio.Text = "From an audio file (.mp3)";
            // 
            // LblOption
            // 
            LblOption.AutoSize = true;
            LblOption.Location = new Point(12, 22);
            LblOption.Name = "LblOption";
            LblOption.Size = new Size(158, 15);
            LblOption.TabIndex = 12;
            LblOption.Text = "Transcribe audio file method";
            // 
            // BtnTranscribeAudio
            // 
            BtnTranscribeAudio.Location = new Point(239, 374);
            BtnTranscribeAudio.Name = "BtnTranscribeAudio";
            BtnTranscribeAudio.Size = new Size(129, 23);
            BtnTranscribeAudio.TabIndex = 22;
            BtnTranscribeAudio.Text = "Transcribe Audio";
            BtnTranscribeAudio.UseVisualStyleBackColor = true;
            BtnTranscribeAudio.Click += BtnTranscribeAudio_Click;
            // 
            // LblSelectAudioFile
            // 
            LblSelectAudioFile.AutoSize = true;
            LblSelectAudioFile.Location = new Point(12, 54);
            LblSelectAudioFile.Name = "LblSelectAudioFile";
            LblSelectAudioFile.Size = new Size(58, 15);
            LblSelectAudioFile.TabIndex = 23;
            LblSelectAudioFile.Text = "Audio file";
            // 
            // BtnAudioFile
            // 
            BtnAudioFile.Location = new Point(231, 51);
            BtnAudioFile.Name = "BtnAudioFile";
            BtnAudioFile.Size = new Size(65, 23);
            BtnAudioFile.TabIndex = 25;
            BtnAudioFile.Text = "Select";
            BtnAudioFile.UseVisualStyleBackColor = true;
            BtnAudioFile.Click += BtnAudioFile_Click;
            // 
            // TxtAudioFile
            // 
            TxtAudioFile.Location = new Point(301, 50);
            TxtAudioFile.Name = "TxtAudioFile";
            TxtAudioFile.Size = new Size(293, 23);
            TxtAudioFile.TabIndex = 24;
            // 
            // LblRecognitionLanguage
            // 
            LblRecognitionLanguage.AutoSize = true;
            LblRecognitionLanguage.Location = new Point(12, 161);
            LblRecognitionLanguage.Name = "LblRecognitionLanguage";
            LblRecognitionLanguage.Size = new Size(177, 15);
            LblRecognitionLanguage.TabIndex = 26;
            LblRecognitionLanguage.Text = "Select the recognition language:";
            // 
            // CbRecognitionLanguage
            // 
            CbRecognitionLanguage.FormattingEnabled = true;
            CbRecognitionLanguage.Location = new Point(231, 155);
            CbRecognitionLanguage.Name = "CbRecognitionLanguage";
            CbRecognitionLanguage.Size = new Size(172, 23);
            CbRecognitionLanguage.TabIndex = 27;
            // 
            // GbStatistics
            // 
            GbStatistics.Controls.Add(LblNumRecognized);
            GbStatistics.Controls.Add(LblRecognized);
            GbStatistics.Controls.Add(ProgressTranscribingReport);
            GbStatistics.Controls.Add(LblNumRecognizing);
            GbStatistics.Controls.Add(LblRecognizing);
            GbStatistics.Location = new Point(18, 239);
            GbStatistics.Name = "GbStatistics";
            GbStatistics.Size = new Size(576, 112);
            GbStatistics.TabIndex = 28;
            GbStatistics.TabStop = false;
            GbStatistics.Text = "Statistics";
            // 
            // LblNumRecognized
            // 
            LblNumRecognized.AutoSize = true;
            LblNumRecognized.Location = new Point(97, 48);
            LblNumRecognized.Name = "LblNumRecognized";
            LblNumRecognized.Size = new Size(13, 15);
            LblNumRecognized.TabIndex = 34;
            LblNumRecognized.Text = "0";
            // 
            // LblRecognized
            // 
            LblRecognized.AutoSize = true;
            LblRecognized.Location = new Point(17, 47);
            LblRecognized.Name = "LblRecognized";
            LblRecognized.Size = new Size(74, 15);
            LblRecognized.TabIndex = 33;
            LblRecognized.Text = "Transcribed :";
            // 
            // ProgressTranscribingReport
            // 
            ProgressTranscribingReport.Location = new Point(16, 76);
            ProgressTranscribingReport.Maximum = 350;
            ProgressTranscribingReport.Name = "ProgressTranscribingReport";
            ProgressTranscribingReport.Size = new Size(537, 23);
            ProgressTranscribingReport.TabIndex = 32;
            // 
            // LblNumRecognizing
            // 
            LblNumRecognizing.AutoSize = true;
            LblNumRecognizing.Location = new Point(97, 26);
            LblNumRecognizing.Name = "LblNumRecognizing";
            LblNumRecognizing.Size = new Size(13, 15);
            LblNumRecognizing.TabIndex = 31;
            LblNumRecognizing.Text = "0";
            // 
            // LblRecognizing
            // 
            LblRecognizing.AutoSize = true;
            LblRecognizing.Location = new Point(16, 25);
            LblRecognizing.Name = "LblRecognizing";
            LblRecognizing.Size = new Size(78, 15);
            LblRecognizing.TabIndex = 29;
            LblRecognizing.Text = "Recognizing :";
            // 
            // LblTranscribeMode
            // 
            LblTranscribeMode.AutoSize = true;
            LblTranscribeMode.Location = new Point(12, 197);
            LblTranscribeMode.Name = "LblTranscribeMode";
            LblTranscribeMode.Size = new Size(138, 15);
            LblTranscribeMode.TabIndex = 29;
            LblTranscribeMode.Text = "Select transcribing mode";
            // 
            // CbTranscribeMode
            // 
            CbTranscribeMode.FormattingEnabled = true;
            CbTranscribeMode.Location = new Point(231, 192);
            CbTranscribeMode.Name = "CbTranscribeMode";
            CbTranscribeMode.Size = new Size(172, 23);
            CbTranscribeMode.TabIndex = 30;
            // 
            // Imlimages
            // 
            Imlimages.ColorDepth = ColorDepth.Depth8Bit;
            Imlimages.ImageStream = (ImageListStreamer)resources.GetObject("Imlimages.ImageStream");
            Imlimages.TransparentColor = Color.Transparent;
            Imlimages.Images.SetKeyName(0, "gears.gif");
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(610, 411);
            Controls.Add(CbTranscribeMode);
            Controls.Add(LblTranscribeMode);
            Controls.Add(GbStatistics);
            Controls.Add(CbRecognitionLanguage);
            Controls.Add(LblRecognitionLanguage);
            Controls.Add(BtnAudioFile);
            Controls.Add(TxtAudioFile);
            Controls.Add(LblSelectAudioFile);
            Controls.Add(BtnTranscribeAudio);
            Controls.Add(BtnSelectFolder);
            Controls.Add(TxtFileName);
            Controls.Add(LblFileName);
            Controls.Add(TxtFileSavingFolder);
            Controls.Add(LblFolderDestination);
            Controls.Add(CbOptionTranscribeAudio);
            Controls.Add(LblOption);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transcribe Audio";
            Load += FrmMain_Load;
            GbStatistics.ResumeLayout(false);
            GbStatistics.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FolderBrowserDialog FbdFileDestination;
        private Button BtnSelectFolder;
        private TextBox TxtFileName;
        private Label LblFileName;
        private TextBox TxtFileSavingFolder;
        private Label LblFolderDestination;
        private ComboBox CbOptionTranscribeAudio;
        private Label LblOption;
        private Button BtnTranscribeAudio;
        private Label LblSelectAudioFile;
        private Button BtnAudioFile;
        private TextBox TxtAudioFile;
        private OpenFileDialog OfdAudioFile;
        private Label LblRecognitionLanguage;
        private ComboBox CbRecognitionLanguage;
        private GroupBox GbStatistics;
        private Label LblNumRecognizing;
        private Label LblRecognizing;
        private Label LblTranscribeMode;
        private ComboBox CbTranscribeMode;
        private ImageList Imlimages;
        private ProgressBar ProgressTranscribingReport;
        private Label LblNumRecognized;
        private Label LblRecognized;
    }
}