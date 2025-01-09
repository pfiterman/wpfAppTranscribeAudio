using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Transcription;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranscribeAudioWpfApp
{
    public sealed class TranscribeAudioSource
    {
        private readonly SpeechConfig _speechConfig;
        private readonly String _audioFile;
        private readonly String _path;
        private IProgress<ReportModel> _reportProgress;

        public TranscribeAudioSource(SpeechConfig speechConfig, String audioFile, String path, IProgress<ReportModel> reportProgress)
        {
            _speechConfig = speechConfig;
            _audioFile = audioFile;
            _path = path;
            _reportProgress = reportProgress;
        }

        async public Task TranscribeAudioFileDiarization()
        {
            using StreamWriter outputFile = new(_path);
            var stopRecognition = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
            ReportModel report = new();

            using (var audioConfig = AudioConfig.FromWavFileInput(_audioFile))
            {
                using (var conversationTranscriber = new ConversationTranscriber(_speechConfig, audioConfig))
                {
                    conversationTranscriber.Transcribing += (s, e) =>
                    {
                        WriteTranscriptionResultToFile(outputFile, e.Result);
                        report.NumRecognizingLines ++;
                        _reportProgress.Report(report);
                    };

                    conversationTranscriber.Transcribed += (s, e) =>
                    {
                        WriteTranscriptionResultToFile(outputFile, e.Result);
                        report.NumRecognizedLines++;
                        _reportProgress.Report(report);
                    };

                    conversationTranscriber.Canceled += (s, e) =>
                    {
                        WriteTranscriptionResultToFile(outputFile, e.Result);
                        stopRecognition.TrySetResult(0);
                    };

                    conversationTranscriber.SessionStopped += (s, e) =>
                    {
                        stopRecognition.TrySetResult(0);
                    };

                    await conversationTranscriber.StartTranscribingAsync();
                    Task.WaitAny(new[] { stopRecognition.Task });
                    await conversationTranscriber.StopTranscribingAsync();
                }
            }
        }

        async public Task TranscribeAudioFileContinuousRecognition()
        {
            using var audioConfig = AudioConfig.FromWavFileInput(_audioFile);
            using var speechRecognizer = new SpeechRecognizer(_speechConfig, audioConfig);
            using StreamWriter outputFile = new(_path);
            var stopRecognition = new TaskCompletionSource<int>();
            ReportModel reportModel = new ();
            
            speechRecognizer.Recognizing += (s, e) =>
            {
                WriteSpeechRecognitionResultToFile(outputFile, e.Result);
                reportModel.NumRecognizingLines++;
                _reportProgress.Report(reportModel);
            };

            speechRecognizer.Recognized += (s, e) =>
            {
                WriteSpeechRecognitionResultToFile(outputFile, e.Result);
                reportModel.NumRecognizedLines++; ;
                _reportProgress.Report(reportModel);
            };

            speechRecognizer.Canceled += (s, e) =>
            {
                stopRecognition.TrySetResult(0);
            };

            speechRecognizer.SessionStopped += (s, e) =>
            {
                stopRecognition.TrySetResult(0);
            };

            await speechRecognizer.StartContinuousRecognitionAsync();
            Task.WaitAny(new[] { stopRecognition.Task });
        }

        private static void WriteTranscriptionResultToFile(StreamWriter outputFile, ConversationTranscriptionResult conversationTranscriptionResult)
        {
            switch (conversationTranscriptionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    outputFile.WriteLine($"{conversationTranscriptionResult.SpeakerId} {conversationTranscriptionResult.Text}");
                    break;
                case ResultReason.NoMatch:
                    outputFile.WriteLine($"NOMATCH: Speech could not be recognized. ");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(conversationTranscriptionResult);
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        outputFile.WriteLine($"CANCELLED: ErrorCode={cancellation.ErrorCode}");
                        outputFile.WriteLine($"CANCELLED: ErrorCode={cancellation.ErrorDetails}");
                        outputFile.WriteLine($"CANCELLED: Did you set the speech resouce key and region values?");
                    }
                    break;
            }
        }

        private static void WriteSpeechRecognitionResultToFile(StreamWriter outputFile, SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    outputFile.WriteLine(speechRecognitionResult.Text);
                    break;
                case ResultReason.NoMatch:
                    outputFile.WriteLine($"NOMATCH: Speech could not be recognized. ");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    outputFile.WriteLine($"CANCELLED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        outputFile.WriteLine($"CANCELLED: ErrorCode={cancellation.ErrorCode}");
                        outputFile.WriteLine($"CANCELLED: ErrorCode={cancellation.ErrorDetails}");
                        outputFile.WriteLine($"CANCELLED: Did you set the speech resouce key and region values?");
                    }
                    break;
            }
        }
    }
}
