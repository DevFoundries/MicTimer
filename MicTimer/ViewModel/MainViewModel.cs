using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Search;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using MicTimer.Messages;
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
        private bool isRunning;
        private string _welcomeTitle = string.Empty;
		private ObservableCollection<DurationOption> durationOptions;
        private Settings settings;
        private TimerState currentTimerState = TimerState.Normal;

		public bool IsRunning
		{
			get => isRunning;
			set => Set(ref isRunning, value);
		}

        public Settings Settings
        {
            get { return this.settings; }
            set { Set(ref settings, value); }
        }

		public ObservableCollection<DurationOption> DurationOptions
		{
			get { return new ObservableCollection<DurationOption>(this.durationOptions);}
			set { Set(ref durationOptions, value); }
		}

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

        private RelayCommand<int> _timerCommand;
        public RelayCommand<int> TimerCommand
        {
	        get
            {
                return _timerCommand
                       ?? (_timerCommand = new RelayCommand<int>(
                           p =>
                           {
                               timerMinutes = p;
                               currentTimer = timerMinutes * 60;
                               Clock = TimeSpan.FromSeconds(currentTimer).ToString("mm\\:ss");
                               BackgroundColor = new SolidColorBrush(Colors.Black);
                           }
                       ));
            }
	        set => throw new NotImplementedException();
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
	        set => throw new NotImplementedException();
        }

        public MainViewModel(
            IDataService dataService,
            INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            Initialize();
        }

        private int timerMinutes = 10;
        private int currentTimer = 0;
        public void RunClock()
        {
	        if (IsRunning) return;
            currentTimer = timerMinutes * 60;
            IsRunning = true;

            Task.Run(async () =>
            {
                while (IsRunning)
                {
                    try
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            Clock = TimeSpan.FromSeconds(currentTimer).ToString("mm\\:ss");
                            if (currentTimer <= settings.WarnAtSeconds && currentTimer > settings.AlertAtSeconds)
                            {
                                BackgroundColor = new SolidColorBrush(Colors.DarkGoldenrod);
                                if (this.currentTimerState != TimerState.Warn)
                                {
                                    this.currentTimerState = TimerState.Warn;
                                    Messenger.Default.Send<TimerStateMessage>(new TimerStateMessage()
                                        {TimerState = this.currentTimerState});
                                }
                            }
                            if (currentTimer <= settings.AlertAtSeconds)
                            {
                                Clock = "- " + Clock;
                                BackgroundColor = new SolidColorBrush(Colors.Red);
                                if (this.currentTimerState != TimerState.Alert)
                                {
                                    this.currentTimerState = TimerState.Alert;
                                    Messenger.Default.Send<TimerStateMessage>(new TimerStateMessage()
                                        {TimerState = this.currentTimerState});
                                }
                            }

                            currentTimer--;
                        });

                        await Task.Delay(1000);
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }

        private SolidColorBrush _solidColor = new SolidColorBrush(Colors.Black);
        public SolidColorBrush BackgroundColor
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
            BackgroundColor = new SolidColorBrush(Colors.Black);
            currentTimer = timerMinutes * 60;
            this.currentTimerState = TimerState.Normal;
        }

        public void StopClock()
        {
			this.IsRunning = false;
        }

        public void AddDurationOption(DurationOption newOption)
        {
            if (newOption.Minutes == 0 || newOption.Label == "") return;
            this.durationOptions.Add(newOption);
            this.DurationOptions = this.durationOptions;
            SaveDurationOptions();
            RaisePropertyChanged(nameof(DurationOptions));
        }

        public void DeleteDurationOption(DurationOption toDelete)
        {
            if (toDelete == null) return;
            this.durationOptions.Remove(toDelete);
            this.DurationOptions = this.durationOptions;
            SaveDurationOptions();
            RaisePropertyChanged(nameof(DurationOptions));
        }

        private void SaveDurationOptions()
        {
            _dataService.SaveDurationOptions(this.durationOptions.ToList());
        }

        public void SaveSettings()
        {
            _dataService.SaveSettings(this.settings);
        }

        private void Initialize()
        {
            try
            {
                DurationOptions = new ObservableCollection<DurationOption>(_dataService.GetDurationOptions());
                Settings = _dataService.GetSettings();
            }
            catch (Exception ex)
            {
                // Report error here
            }
        }


    }
}