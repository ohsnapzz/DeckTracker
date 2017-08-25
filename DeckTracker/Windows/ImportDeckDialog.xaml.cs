using DeckTracker.LowLevel;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace DeckTracker.Windows
{
    public partial class ImportDeckDialog
    {
        private readonly TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        public string DeckList { get; set; }

        public ImportDeckDialog()
        {
            InitializeComponent();
            DeckListTextBox.Focus();
        }

        private void ImportButton_OnClick(object sender, RoutedEventArgs e)
        {
            ImportButton.IsEnabled = false;
            tcs.SetResult(DeckList);
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            CancelButton.IsEnabled = false;
            tcs.SetResult(null);
        }

        internal Task<string> WaitForButtonPressAsync() => tcs.Task;

        private void ImportFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            ImportFileButton.IsEnabled = false;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Import Deck",
                InitialDirectory = Logger.GameDataDirectory,
                Filter = "Deck tracker deck file (*.dtdeck)|*.dtdeck",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
                DeckListTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            tcs.SetResult(DeckList);
        }
    }
}
