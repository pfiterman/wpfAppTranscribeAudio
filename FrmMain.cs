using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Transcription;
using NAudio.Wave;
using System;
using TranscribeAudioWpfApp;

namespace TranscribeAudio
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    TxtFileSavingFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void LoadComboRecognitionLanguages()
        {
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Danish - Denmark", "da-DK"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("German - Germany", "de-DE"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Australia", "en-AU"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Canada", "en-CA"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Great Britain", "en-GB"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Hong Kong", "en-HK"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Ireland", "en-IE"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - India", "en-IN"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Nigeria", "en-NG"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - New Zealand", "en-NZ"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Philippines", "en-PH"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - Singapore", "en-SG"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("English - United States", "en-US"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Spanish - Spain", "es-ES"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Spanish - Mexico", "es-MX"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Finnish - Finland", "fi-FI"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("French - Canada", "fr-CA"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("French - France", "fr-FR"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Hindi - India", "hi-IN"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Italian - Italy", "it-IT"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Japanese - Japan", "ja-JP"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Korean - South Korea", "ko-KR"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Norwegian - Norway", "nb-NO"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Dutch - Netherlands", "nl-NL"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Polish - Poland", "pl-PL"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Portuguese - Brazil", "pt-BR"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Portuguese - Portugal", "pt-PT"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Swedish - Sweden", "sv-SE"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Turkish - Turkey", "tr-TR"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Chinese - China", "zh-CN"));
            CbRecognitionLanguage.Items.Add(new RecognitionLanguageItem("Chinese - Hong Kong", "zh-HK"));
        }

        private void LoadComboTranscribeMode()
        {
            CbTranscribeMode.Items.Add("Continuous Recognition");
            CbTranscribeMode.Items.Add("Diarization");
            CbTranscribeMode.Items.Add("Single - Shot");

        }   

        private void SelectRecognitionLanguageItem(string displayName)
        {
            foreach (var item in CbRecognitionLanguage.Items)
            {
              if (item is RecognitionLanguageItem recognitionLanguageItem &&  recognitionLanguageItem.DisplayName == displayName )
                {
                    CbRecognitionLanguage.SelectedItem = item;
                    break;
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadComboRecognitionLanguages();
            LoadComboTranscribeMode();
            SelectRecognitionLanguageItem("English - Canada");
            CbTranscribeMode.SelectedItem = "Continuous Recognition";
            TxtFileSavingFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private bool CheckRequiredFields()
        {
            if ((CbOptionTranscribeAudio.SelectedIndex == -1) || (TxtAudioFile.Text == "") || (TxtFileSavingFolder.Text == "") || (TxtFileName.Text == "") || (CbRecognitionLanguage.SelectedIndex == -1))
            {
                if (CbOptionTranscribeAudio.SelectedIndex == -1)
                {
                    MessageBox.Show("Select how do you want transcribe the audio?", "Validation Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CbOptionTranscribeAudio.Focus();
                }
                if (TxtAudioFile.Text.Trim() == "")
                {
                    MessageBox.Show("Select the wav audio file.", "Validation Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtAudioFile.Focus();
                }
                if (TxtFileSavingFolder.Text.Trim() == "")
                {
                    MessageBox.Show("Select the destination folder.", "Validation Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtFileSavingFolder.Focus();
                }
                if (TxtFileName.Text.Trim() == "")
                {
                    MessageBox.Show("Type the file name.", "Validation Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtFileName.Focus();
                }
                if (CbRecognitionLanguage.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the recognition language.", "Validation Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CbRecognitionLanguage.Focus();
                }
                return false;
            }
            return true;
        }        

        private void OnFrameChanged(object o, EventArgs e)
        {

            //Force a call to the Paint event handler.
            Invalidate();
        }

        private static void ConvertMp3ToWav(string _inPath_, string _outPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                }
            }
        }

        private void ShowProgressBarProcessing(bool show)
        {
            if (show)
            {
                ProgressTranscribingReport.Style = ProgressBarStyle.Marquee;
                ProgressTranscribingReport.MarqueeAnimationSpeed = 30;
            }else
            {
                ProgressTranscribingReport.Style= ProgressBarStyle.Blocks;
            }

        }

        private void UpdateProgressUI(ReportModel progess)
        {
            LblNumRecognizing.Text = progess.NumRecognizingLines.ToString();
            LblNumRecognized.Text = progess.NumRecognizedLines.ToString();
        }

        private async void BtnTranscribeAudio_Click(object sender, EventArgs e)
        {
            if (Environment.GetEnvironmentVariable("SPEECH_KEY") == null || Environment.GetEnvironmentVariable("SPEECH_REGION") == null)
            {
                MessageBox.Show("Please set the environment variables SPEECH_KEY and SPEECH_REGION.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
            string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

            speechConfig.SpeechRecognitionLanguage = ((RecognitionLanguageItem)CbRecognitionLanguage.SelectedItem).LanguageCode;

            if (CheckRequiredFields())
            {
                string file = TxtFileName.Text;
                string fileOutput = Path.Combine(TxtFileSavingFolder.Text, file);
                string audioFileMP3 = TxtAudioFile.Text;
                string audioFileWAV = audioFileMP3.Split('.')[0] + ".wav";

                if (!File.Exists(audioFileWAV)) ConvertMp3ToWav(audioFileMP3, audioFileWAV);

                TranscribeAudioSource transcribeAudioSource = new(speechConfig, audioFileWAV, fileOutput, new Progress<ReportModel>(UpdateProgressUI));                             

                try
                {                    
                    if (CbTranscribeMode.SelectedItem.ToString() == "Continuous Recognition")
                    {
                        ShowProgressBarProcessing(true);                        
                        await Task
                            .Run(async () => 
                            { 
                                await transcribeAudioSource.TranscribeAudioFileContinuousRecognition(); 
                            });
                    }
                    if (CbTranscribeMode.SelectedItem.ToString() == "Diarization")
                    {
                        await Task
                            .Run(async () =>
                            {
                                await transcribeAudioSource.TranscribeAudioFileDiarization();
                            });                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ShowProgressBarProcessing(false);
                    LblNumRecognizing.Text = "0";
                    LblNumRecognized.Text = "0";
                    string[] fileData = TxtAudioFile.Text.Split('\\');
                    string audiofile = fileData[fileData.Length - 1];
                    if (File.Exists(audioFileWAV)) File.Delete(audioFileWAV);
                    MessageBox.Show("The file " + audiofile + " was transcribed!","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);                    
                }
            }
        }

        private void BtnAudioFile_Click(object sender, EventArgs e)
        {
            OfdAudioFile = new OpenFileDialog()
            {
                FileName = "Select a mp3 file",
                Filter = "Text files (*.mp3)|*.mp3",
                Title = "Open mp3 file"
            };

            if (OfdAudioFile.ShowDialog() == DialogResult.OK)
            {
                TxtAudioFile.Text = OfdAudioFile.FileName;
                string[] splitFullPath = OfdAudioFile.FileName.Split('\\');
                TxtFileName.Text = splitFullPath[splitFullPath.Length - 1].Split('.')[0] + ".txt";
            }
        }
    }
}