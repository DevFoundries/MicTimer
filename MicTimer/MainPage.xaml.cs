using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MicTimer.ViewModel;

namespace MicTimer
{
    public sealed partial class MainPage
    {
        public MainViewModel Vm => (MainViewModel)DataContext;

        public MainPage()
        {
            InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManagerBackRequested;

            Loaded += (s, e) =>
            {
//                Vm.RunClock();
            };
        }

        private void SystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Vm.StopClock();
            base.OnNavigatingFrom(e);
        }

        private void StopTimer(object sender, RoutedEventArgs e)
        {
            Vm.StopClock();
        }

		private void StartTimer(object sender, RoutedEventArgs e)
        {
            Vm.RunClock();
        }

        private void AddDurationOption(object sender, RoutedEventArgs e)
        {
	        var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
			navigationService.NavigateTo(ViewModelLocator.DurationOptionPageKey);

        }

        private void Help(object sender, RoutedEventArgs e)
        {
	        var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
	        navigationService.NavigateTo(ViewModelLocator.AboutPageKey);

		}

        private void MainPage_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetCounterSize();
        }

        private void SetCounterSize()
        {
            double height = this.CounterRow.ActualHeight;
            // Get the ratio of the TextBlock's height to that of the TextBox’s 
            double fontsizeMultiplier = Math.Sqrt(height / this.CounterBlock.ActualHeight);

            // Set the new FontSize 
            this.CounterBlock.FontSize = Math.Floor(this.CounterBlock.FontSize * fontsizeMultiplier);
            this.CounterBlock.HorizontalAlignment = HorizontalAlignment.Center;

        }
    }
}
