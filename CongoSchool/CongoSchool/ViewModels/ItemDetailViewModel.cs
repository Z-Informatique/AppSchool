using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CongoSchool.Models;
using CongoSchool.Views;
using Xamarin.Forms;

namespace CongoSchool.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Command EditItemCommand { get; }
        public Command DeleteAsyncCommand { get; }

        private int itemId;
        public int Id { get; set; }

        private Item item1;

        public Item Item
        {
            get => item1;
            set => SetProperty(ref item1, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
                OnPropertyChanged(nameof(item1));
            }
        }

        public ItemDetailViewModel()
        {
            EditItemCommand = new Command(OnEditItem);
            DeleteAsyncCommand = new Command(OnDeleteItem);
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                Item = await App.MockDataStore.GetItemAsync(itemId);
                Id = Item.Id;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        async void OnEditItem()
        {
            if (Item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(NewItemPage)}?{nameof(NewItemViewModel.ItemId)}={Item.Id}");
        }
        async void OnDeleteItem()
        {
            if (Item == null)
                return;

            var resultAlerte = await Shell.Current.DisplayAlert("Confirmer ?", "Souhaitez-vous supprimer cette entree ?", "OUI", "NON");
            if (!resultAlerte)
                return;

            var result = await App.MockDataStore.DeleteItemAsync(Item);
            if (result > 1)
            {
                await Shell.Current.DisplayAlert("Info", "Suppression effectuee avec success", "Ok");
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}

