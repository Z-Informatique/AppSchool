using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CongoSchool.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Accueil";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://infracheck.infraone.fr"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
