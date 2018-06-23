using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Application = System.Windows.Application;
using Cursors = System.Windows.Input.Cursors;
using Timer = System.Threading.Timer;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm = new MainWindowViewModel();
        private Timer tm;
        private int Amount = 0, oldAmount = 0;
        private string _folderPath = @"D:\Videos\Psycho-Pass 2 [Dual Audio][10bit 720p][BD 720p][MeGaTroN]\";
        private int index = 0;
        private double _time = 0;
        public MainWindow()
        {
            //TODO: Put most of code in viewmodel where possible
            //TODO: make menu fade away when nothing happend for 2 seconds
            InitializeComponent();
            base.DataContext = vm;
            ShowList();
            //Console.WriteLine(vm.Files[0]);
            ReadProgress();


            CreateClock();

            tm = new Timer((e) =>
            {
                Application.Current.Dispatcher.Invoke((Action)(() =>
               {
                   // update collection here
                   if (MediaElement.Clock.CurrentTime.HasValue)
                   {
                       Amount = (int)MediaElement.Clock.CurrentTime.Value.TotalSeconds;
                       togoLabel.Content = Slider.Maximum - Amount;
                       if (oldAmount != Amount)
                       {
                           Slider.Value = Amount;
                           oldAmount = Amount;
                       }

                   }
               }));
            }, null, 0, 1);



        }

        private void ReadProgress()
        {
            _time = 0;
            index = 0;
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(_folderPath + "Progress.txt");

                //Read the first line of text
                line = sr.ReadLine();
                int i = 0;
                //Continue to read until you reach end of file
                while (line != null)
                {
                    if (i == 0)
                        index = Int32.Parse(line);
                    else if (i == 1)
                        _time = Double.Parse(line);
                    i++;
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            Console.WriteLine(index);
            Console.WriteLine(_time);
        }

        private void CreateClock()
        {
            vm.Playing = @"" + files[index];
            Console.WriteLine(vm.Playing);
            var tl = new MediaTimeline(new Uri(vm.Playing));

            MediaElement.Clock = tl.CreateClock(true) as MediaClock;


            MediaElement.MediaOpened += (o, e) =>
            {
                Slider.Maximum = MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                Slider.Value = _time;
                Window_PreviewMouseUp(null, null);
            };


            //TODO: wait 5 seconds before going further, or less
            MediaElement.Clock.Completed += NextVideo;
            MediaElement.Clock.Controller.Resume();
        }

        private void NextVideo(object sender, EventArgs e)
        {
            NextVideo();
        }

        private void NextVideo()
        {
            OtherVideo(1);
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            //TODO: save where stopped last time for confortable continue
            // safe to local file where the video is - 5-10 seconds
            //safe which episode , next episode if 1 min left
            Console.WriteLine("closing so I could do this");

            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(_folderPath + "Progress.txt");

                //Write a line of text
                sw.WriteLine(index);
                Console.WriteLine(index);

                //Write a second line of text
                _time = MediaElement.Clock.CurrentTime.Value.TotalSeconds;
                if (_time > 5)
                    _time -= 5;
                sw.WriteLine(_time);
                Console.WriteLine(_time);
                //Close the file
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            tm.Dispose();
            tm = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {//TODO: probably unnecesary
            MediaElement.Clock.Controller.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FullScreen();
        }



        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A://0
                    OpenFolder();
                    break;
                case Key.L://l
                    ShowList();
                    break;
                case Key.Escape://esc
                    FullScreen();
                    break;
                case Key.P://p
                    PlayPause();
                    break;
                case Key.Space://spacebar
                    PlayPause();
                    break;
                case Key.Left://left arrow
                    MoveTime(-1);
                    break;
                case Key.Right://right arrow
                    MoveTime(1);
                    break;
                case Key.Up://up arrow
                    Volume(1);
                    break;
                case Key.Down://down arrow
                    Volume(-1);
                    break;
                default://else
                    HideBars();
                    break;
            }
        }

        private void HideBars()
        {
            vm.VisOpa = 0;
        }

        private void ShowBars()
        {
            vm.VisOpa = 0.5;
        }

        private void Volume(int p0)
        {
            var up = 0.1 * p0;
            if (up < 0)
                up = 0;
            else if (up > 1)
                up = 0;
            MediaElement.Volume += up;
            Console.WriteLine(MediaElement.Volume);
        }

        private void MoveTime(int p0)
        {
            Slider.Value += 10 * p0;
            Window_PreviewMouseUp(null, null);
        }

        private void PlayPause()
        {
            if (MediaElement.Clock.IsPaused)
            {
                MediaElement.Clock.Controller.Resume();
                HideBars();
            }
            else
            {
                MediaElement.Clock.Controller.Pause();
                ShowBars();
            }
        }

        private void FullScreen()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                ShowBars();
            }
            else
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                HideBars();

            }
            vm.FullScreenCommand.Execute(this);
        }

        private string[] files;

        private void ShowList()
        {
            //TODO: 
            files = Directory.GetFiles(_folderPath, "*.mkv");
            vm.Files = files;
            Console.WriteLine(files);
        }

        private void OpenFolder()
        {
            //TODO: pauze video and open folder for videos. only mp4 allowed so far.
            throw new NotImplementedException();
        }


        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Amount != Slider.Value)
                MediaElement.Clock.Controller.Seek(TimeSpan.FromSeconds(Slider.Value), TimeSeekOrigin.BeginTime);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NextVideo();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PreviousVideo();
        }

        private void PreviousVideo()
        {
            OtherVideo(-1);
        }

        private void OtherVideo(int i)
        {
            //TODO: check if index is in range
            index += i;
            vm.Playing = @"" + files[index];
            _time = 0;
            CreateClock();
        }

        private void MediaElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            HideBars();
        }

        private void MediaElement_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            ShowBars();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayPause();
        }
    }
}
