using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebRefresher.Models;
using WebRefresher.Views;
using Xamarin.Forms;

namespace WebRefresher.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Command DeleteItemCommand { get; }
        public Command EditItemCommand { get; }

        public string Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string url;
        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value); }
        }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { SetProperty(ref enabled, value); }
        }

        private string itemId;
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        private int refreshInterval;

        public int RefreshInterval
        {
            get { return refreshInterval; }
            set { SetProperty(ref refreshInterval, value); }
        }

        public ItemDetailViewModel()
        {
            DeleteItemCommand = new Command(OnDeleteItem);
            EditItemCommand = new Command(OnEditItem);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Url = item.Url;
                RefreshInterval = item.RefreshInterval;
                Enabled = item.Enabled;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private async void OnDeleteItem()
        {
            await DataStore.DeleteItemAsync((string)Id);
            await Shell.Current.GoToAsync("..", true);
        }

        private async void OnEditItem()
        {
            await Shell.Current.GoToAsync($"{nameof(EditItemPage)}?{nameof(EditItemViewModel.ItemId)}={ItemId}", true);
        }
    }
}
