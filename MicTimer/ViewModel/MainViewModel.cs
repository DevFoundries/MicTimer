using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using MicTimer.Model;

namespace MicTimer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private string _clock = "00:00";
        private RelayCommand<string> _navigateCommand;
        private bool _runClock;
        private string _welcomeTitle = string.Empty;

        public string Clock
        {
            get
            {
                return _clock;
            }
            set
            {
                Set(ref _clock, value);
            }
        }

        public RelayCommand<string> NavigateCommand
        {
            get
            {
                return _navigateCommand
                       ?? (_navigateCommand = new RelayCommand<string>(
                           p => _navigationService.NavigateTo(ViewModelLocator.SecondPageKey, p),
                           p => !string.IsNullOrEmpty(p)));
            }
        }

        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        public MainViewModel(
            IDataService dataService,
            INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            Initialize();
        }

        private int tenMinutes = 10*60;
        public void RunClock()
        {
            _runClock = true;

            Task.Run(async () =>
            {
                while (_runClock)
                {
                    try
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            Clock = TimeSpan.FromSeconds(tenMinutes).ToString("mm\\:ss");
                            if (tenMinutes <= 150 && tenMinutes > 0)
                            {
                                BackgroundColor = Colors.DarkGoldenrod;

                            }
                            if (tenMinutes <= 0)
                            {
                                Clock = "- " + Clock;
                                BackgroundColor = Colors.Red;
                            }

                            tenMinutes--;
                        });

                        await Task.Delay(1000);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }

        private Color _solidColor = Colors.Black;
        public Color BackgroundColor
        {
            get
            {
                return _solidColor;
            }
            set
            {
                Set(ref _solidColor, value);
            }
        }    

        public void ResetClock()
        {
            BackgroundColor = Colors.Black;
            tenMinutes = 60 * 10;
        }

        public void StopClock()
        {
            _runClock = false;
        }

        private async Task Initialize()
        {
            try
            {
                var item = await _dataService.GetData();
                WelcomeTitle = item.Title;
            }
            catch (Exception ex)
            {
                // Report error here
                WelcomeTitle = ex.Message;
            }
        }
    }
}