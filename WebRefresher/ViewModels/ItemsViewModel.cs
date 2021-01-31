using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using WebRefresher.Models;
using WebRefresher.Views;
using Xamarin.Forms;

namespace WebRefresher.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private WebPage _selectedItem;

        public ObservableCollection<WebPage> WebPages { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<WebPage> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            WebPages = new ObservableCollection<WebPage>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<WebPage>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        private bool _switchValue;
        public bool SwitchValue
        {
            get { return _switchValue; }
            set { SetProperty(ref _switchValue, value); }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                WebPages.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    WebPages.Add(item);
                }
            }
            catch (Exception ex)
            {
                await DataStore.DeleteAllItemsAsync();
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

        public WebPage SelectedItem
        {
            get { return _selectedItem; }
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

        async void OnItemSelected(WebPage item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}", true);
        }
    }
}