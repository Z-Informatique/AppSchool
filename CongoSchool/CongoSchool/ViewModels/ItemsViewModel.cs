using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CongoSchool.Models;
using CongoSchool.Views;
using System.Collections.Generic;

namespace CongoSchool.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get;  }
        public Command SendData { get; }
        public Command<Item> ItemTapped { get; }

        bool show;
        public bool Show
        {
            get => show;
            set
            {
                show = value;
                OnPropertyChanged(nameof(Show));
            }
        }

        public ItemsViewModel()
        {
            Title = "Liste des eleves";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            SendData = new Command(SendDataAsync);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await App.MockDataStore.GetItemsAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
                }

                if (Items.Count > 0)
                    Show = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void SendDataAsync(object obj)
        {
            await Shell.Current.DisplayAlert("Succes", "Cette option est encours de maintenance.\n Revenez plus-tard ou contactez l'administrateur.", "OK");
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
