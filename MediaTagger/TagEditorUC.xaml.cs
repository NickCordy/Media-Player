using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for TagEditorUC.xaml
    /// </summary>
    public partial class TagEditorUC : UserControl
    {
        private TagLib.File file;

        public delegate void TagsUpdatedHandler();
        public event TagsUpdatedHandler TagsUpdated;

        public TagEditorUC(TagLib.File file)
        {
            InitializeComponent();

            try
            {
                this.file = file;

                UpdateText();

                Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("File error!");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // If media is playing, there is an error

            if (int.TryParse(YearTextBox.Text, out int year)) {

                try
                {
                    UpdateTags();
                    file.Save();
                    TagsUpdated?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not save while media is playing!");
                }
            }
            else
            {
                MessageBox.Show($"Invalid Year Entered!");
            }
        }

        public void UpdateFile(TagLib.File file)
        {
            // Change to newly opened file with new media

            this.file = file;
            UpdateText();
        }

        public void UpdateTags()
        {
            file.Tag.Title = TitleTextBox.Text;
            file.Tag.AlbumArtists = new[] { ArtistTextBox.Text };
            file.Tag.Album = AlbumTextBox.Text;
            file.Tag.Year = uint.Parse(YearTextBox.Text);
        }

        public void UpdateText()
        {
            TitleTextBox.Text = file.Tag.Title;
            ArtistTextBox.Text = file.Tag.AlbumArtists.FirstOrDefault();
            AlbumTextBox.Text = file.Tag.Album;
            YearTextBox.Text = file.Tag.Year.ToString();
        }
    }
}
