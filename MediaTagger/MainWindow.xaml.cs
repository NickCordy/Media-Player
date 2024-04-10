using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TagLib.File? File {  get; set; }
        public OpenFileDialog? Ofd { get; set; }
        public NowPlayingUC? NowPlayingUC { get; set; }
        public TagEditorUC? TagEditorUC { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                // Instantiate and show a file dialog
                Ofd = new();

                if (Ofd.ShowDialog() == true)
                {

                    File = TagLib.File.Create(Ofd.FileName);

                    MainMediaPlayer.Source = new Uri(Ofd.FileName);

                    if (NowPlayingUC == null)
                    {
                        // First time media setup

                        NowPlayingUC = new(File);
                        TagEditorUC = new(File);

                        MainPanel.Children.Add(NowPlayingUC);
                        MainPanel.Children.Add(TagEditorUC);

                        TagEditorUC.TagsUpdated += NowPlayingUC.UpdateTags;
                    }
                    else
                    {
                        NowPlayingUC.UpdateFile(File);
                        TagEditorUC.UpdateFile(File);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File error!");
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Media_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = File != null;
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainMediaPlayer.Source = new Uri(Ofd.FileName);
            MainMediaPlayer.Play();
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainMediaPlayer.Pause();
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainMediaPlayer.Stop();
            MainMediaPlayer.Source = null; // Set to null to avoid saving error
        }

        private void NowPlayingButton_Click(object sender, RoutedEventArgs e)
        {
            if (File != null)
            {
                NowPlayingUC.Visibility = Visibility.Visible;
                TagEditorUC.Visibility = Visibility.Collapsed;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (File != null)
            {
                NowPlayingUC.Visibility = Visibility.Collapsed;
                TagEditorUC.Visibility = Visibility.Visible;
            }
        }
    }
}