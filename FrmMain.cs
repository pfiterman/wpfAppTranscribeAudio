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

        private void FrmMain_Load(object sender, EventArgs e)
        {
            CbOptionTranscribeAudio.SelectedIndex = 1;
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
            // Disabling and Enabling warning message Converting null literal or possible null value to non-nullable type.
            #pragma warning disable CS8600
            string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
            string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");
            #pragma warning restore CS8600

            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechRecognitionLanguage = CbRecognitionLanguage.SelectedItem.ToString();

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