using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebRefresher.Models;
using WebRefresher.Views;
using Xamarin.Forms;

namespace WebRefresher.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditItemViewModel : BaseViewModel
    {
        private string itemId;
        private string name;
        private string url;
        private int refreshInterval;
        private bool enabled;

        public EditItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(url);
        }

        public string Id { get; set; }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public int RefreshInterval
        {
            get { return refreshInterval; }
            set { SetProperty(ref refreshInterval, value); }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { SetProperty(ref enabled, value); }
        }

        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value); }
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // await Shell.Current.GoToAsync($"..?{nameof(ItemDetailViewModel.ItemId)}={Id}", true);
            await Shell.Current.GoToAsync("../../", true);
        }

        private async void OnSave()
        {
            WebPage item = new WebPage()
            {
                Id = Id,
                Name = Name,
                Url = Url,
                RefreshInterval = RefreshInterval,
                Enabled = Enabled
            };

            await DataStore.UpdateItemAsync(item);

            await Shell.Current.GoToAsync($"..?{nameof(ItemDetailViewModel.ItemId)}={Id}", true);
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
    }
}
