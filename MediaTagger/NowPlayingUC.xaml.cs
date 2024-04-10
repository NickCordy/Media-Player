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
    /// Interaction logic for NowPlayingUC.xaml
    /// </summary>
    public partial class NowPlayingUC : UserControl
    {
        private TagLib.File file;

        public NowPlayingUC(TagLib.File file)
        {
            InitializeComponent();

            try
            {
                this.file = file;

                Title.Content = "Title: " + file.Tag.Title;
                Artist.Content = "Artist: " + file.Tag.AlbumArtists.FirstOrDefault();
                Album.Content = "Album: " + file.Tag.Album;
                Year.Content = "Year: " + file.Tag.Year;
            }
            catch (Exception ex)
            {
                MessageBox.Show("File error!");
            }
        }

        public void UpdateFile(TagLib.File file)
        {
            // Change to newly opened file with new media

            this.file = file;
            UpdateTags();
        }

        public void UpdateTags()
        {
            Title.Content = "Title: " + file.Tag.Title;
            Artist.Content = "Artist: " + file.Tag.AlbumArtists.FirstOrDefault();
            Album.Content = "Album: " + file.Tag.Album;
            Year.Content = "Year: " + file.Tag.Year;
        }
    }
}
