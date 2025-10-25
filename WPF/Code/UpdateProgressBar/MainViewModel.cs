using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows.Input;

namespace UpdateProgressBar
{
    public class MainViewModel : BaseViewModel
    {
        private int _progressValue;
        private string _statusText;
        private string _parsedContent;
        private bool _isParsing;

        public int ProgressValue
        {
            get => _progressValue;
            set { _progressValue = value; OnPropertyChanged(); }
        }

        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        public string ParsedContent
        {
            get => _parsedContent;
            set { _parsedContent = value; OnPropertyChanged(); }
        }

        public bool IsParsing
        {
            get => _isParsing;
            set { _isParsing = value; OnPropertyChanged(); }
        }

        public ICommand ParseFileCommand { get; }

        public MainViewModel()
        {
            // Init command
            ParseFileCommand = new RelayCommand(async (param) => await ExecuteParseFileCommand(), (param) => !IsParsing);
            StatusText = "Ready";
        }

        private async Task ExecuteParseFileCommand()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Txt (*.txt)|*.txt|All files (*.*)|*.*"
                };

                if (openFileDialog.ShowDialog() != true)
                {
                    StatusText = "Operation cancelled";
                    return;
                }

                // Reset status
                IsParsing = true;
                StatusText = "Parsing...";
                ParsedContent = string.Empty;
                ProgressValue = 0;

                // Create IProgress<T> object, which is key to safely updating the UI
                // The Action (p => ...) in the constructor will be executed on the UI thread automatically
                var progressReporter = new Progress<int>((percentCompleted) =>
                {
                    ProgressValue = percentCompleted;
                });

                // Call the parsing logic and await its completion
                string result = await ParseFileAsync(openFileDialog.FileName, progressReporter);

                // Update UI with the result
                ParsedContent = result;
                StatusText = "Finished parsing!";
            }
            catch (Exception ex)
            {
                StatusText = $"Parsed unsuccessfully: {ex.Message}";
                ParsedContent = string.Empty;
            }
            finally
            {
                IsParsing = false;
                ProgressValue = 0;
            }
        }

        private async Task<string> ParseFileAsync(string filePath, IProgress<int> progress)
        {
            // Use Task.Run to run CPU-bound work (file IO and parsing)
            // on a background thread pool thread to free up the UI thread.
            return await Task.Run(() =>
            {
                long fileSize = new FileInfo(filePath).Length;
                if (fileSize == 0) return "The file is empty.";

                StringBuilder sb = new StringBuilder();

                // Mock reading in chunks
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(50);
                    sb.AppendLine($"Handling the Part {i}");

                    // 'progress.Report' safely marshals the 'i' value
                    // to the 'Progress<T>' object we created on the UI thread
                    progress?.Report(i);
                }

                string content = File.ReadAllText(filePath);
                sb.AppendLine(content[..Math.Min(1000, content.Length)]);

                return sb.ToString();
            });
        }
    }
}