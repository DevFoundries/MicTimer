using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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
            this.StartButton.Visibility = Visibility.Visible;
            this.StopButton.Visibility = Visibility.Collapsed;
            Vm.StopClock();
        }

        private void StartTimer(object sender, RoutedEventArgs e)
        {
            this.StopButton.Visibility = Visibility.Visible;
            this.StartButton.Visibility = Visibility.Collapsed;
            Vm.ResetClock();
            Vm.RunClock();
        }

        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (textBlock != null)
            {
                textBlock.HorizontalTextAlignment = TextAlignment.Center;
            }
        }
    }
}
