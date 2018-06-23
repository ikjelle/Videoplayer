using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoPlayer.Annotations;

namespace VideoPlayer
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _title = "HOUND HOUND AND MORE HOUNDS";

        private ICommand _fullScreenCommand;
        public ICommand FullScreenCommand
        {
            get { return _fullScreenCommand ?? (_fullScreenCommand = new CommandHandler(() => GoFullScreen(), true)); }
        }

        private int _rowSpan = 1;
        public int RowSpan
        {
            get { return _rowSpan; }
            set
            {
                _rowSpan = value;
                OnPropertyChanged();
            }
        }


        private double _visOpa = 0.5;
        public double VisOpa
        {
            get { return _visOpa; }
            set
            {
                _visOpa = value;
                OnPropertyChanged();
            }
        }
        private int _columnSpan = 5;
        public int ColumnSpan
        {
            get { return _columnSpan; }
            set
            {
                _columnSpan = value;
                
            }
        }
        private string[] _files;
        public string[] Files
        {
            get { return _files; }
            set
            {
                _files = value;
                OnPropertyChanged();
            }
        }

        private string _playing = @"D:\Videos\12.mp4";

        public string Playing
        {
            get
            {
                return _playing;

            }
            set
            {
                _playing = value;
                OnPropertyChanged();
            }
        }

        public void VideoDone()
        {
            // load next vid
            Console.WriteLine("done video is done");
        }

        private void GoFullScreen()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

    }
}
