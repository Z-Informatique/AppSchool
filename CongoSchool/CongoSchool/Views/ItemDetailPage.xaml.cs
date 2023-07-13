using System.ComponentModel;
using Xamarin.Forms;
using CongoSchool.ViewModels;

namespace CongoSchool.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
