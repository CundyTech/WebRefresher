using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WebRefresher.Models;
using Xamarin.Forms;

namespace WebRefresher.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string name;
        private string url;
        private int refreshInterval;
        private bool enabled;

        public NewItemViewModel()
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

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value); }
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..", true);
        }

        private async void OnSave()
        {
            WebPage newItem = new WebPage()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Url = Url, 
                RefreshInterval = RefreshInterval,
                Enabled =  Enabled
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
