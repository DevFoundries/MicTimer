using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using MicTimer.Model;
using MicTimer.ViewModel;

namespace MicTimer
{
    public sealed partial class DurationOptionPage
    {

        public MainViewModel Vm => (MainViewModel)DataContext;

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
	        Vm.AddDurationOption(new DurationOption(){ Minutes = Convert.ToInt32(this.newMinutes.Text), Label = this.newLabel.Text});
            this.newLabel.Text = "";
            this.newMinutes.Text = "";
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
	        
        }

        private void DeleteOption(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Vm.DeleteDurationOption(b.CommandParameter as DurationOption);
        }
    }
}
