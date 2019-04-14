using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;

namespace MicTimer
{
    public sealed partial class AboutPage
    {
        public AboutPage()
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
    }
}
