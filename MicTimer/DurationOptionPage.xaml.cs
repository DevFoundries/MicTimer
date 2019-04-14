using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;

namespace MicTimer
{
    public sealed partial class DurationOptionPage
    {
        public DurationOptionPage()
        {
            InitializeComponent();
        }

        private void GoBackButtonClick(object sender, RoutedEventArgs e)
        {
            var nav = ServiceLocator.Current.GetInstance<INavigationService>();
            nav.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
	        
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
	        
        }
    }
}
