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
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Microsoft.Win32;
using System.IO;

namespace Mp3Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private OpenFileDialog openFileDialog = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();
            UC1.UCButtonClicked += new EventHandler(Hide_Click);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {            
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mePlayer.Source = new Uri(openFileDialog.FileName);

                ShowSongInfo();
            }                
        }

        private void ShowSongInfo()
        {
            var file = TagLib.File.Create(openFileDialog.FileName); // Change file path accordingly.
            songTitle.Text = file.Tag.Title;
            songArtist.Text = file.Tag.FirstPerformer;
            songAlbum.Text = file.Tag.Album;
            songYear.Text = file.Tag.Year.ToString();

            //title.Text = "Title";
            //artist.Text = "Artist";
            //album.Text = "Album";
            //year.Text = "Year";

            MemoryStream ms = new MemoryStream(file.Tag.Pictures[0].Data.Data);
            ms.Seek(0, SeekOrigin.Begin);

            // ImageSource for System.Windows.Controls.Image
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.EndInit();

            songImage.Source = bitmap;

            Description.Visibility = Visibility.Visible;
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void EditSong_Click(object sender, RoutedEventArgs e)
        {
            etitle.Text = "Title";
            eartist.Text = "Artist";
            ealbum.Text = "Album";
            eyear.Text = "Year";            

            try
            {
                var file = TagLib.File.Create(openFileDialog.FileName); // Change file path accordingly.
                editsongTitle.Text = file.Tag.Title;
                editsongArtist.Text = file.Tag.FirstPerformer;
                editsongAlbum.Text = file.Tag.Album;
                editsongYear.Text = file.Tag.Year.ToString();

                EditSongInfo.Visibility = Visibility.Visible;
                ShowSongInfo();
            }
            catch (Exception err)
            {
                UC1.Errormsg.Text = "Error !!!";                
                MyPopup.IsOpen = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = TagLib.File.Create(openFileDialog.FileName); // Change file path accordingly.
                file.Tag.Title = editsongTitle.Text;
                file.Tag.Performers[0] = editsongArtist.Text;
                file.Tag.Album = editsongAlbum.Text;
                file.Tag.Year = Convert.ToUInt32(editsongYear.Text);
                file.Save();
                ShowSongInfo();
            }
            catch(Exception err)
            {
                UC1.Errormsg.Text = "Could not Save changes.";
                MyPopup.IsOpen = true;
            }
            
            EditSongInfo.Visibility = Visibility.Hidden;
        }

        private void Hide_Click(object sender, EventArgs e)
        {           
            MyPopup.IsOpen = false;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditSongInfo.Visibility = Visibility.Hidden;
        }             
    }
}
