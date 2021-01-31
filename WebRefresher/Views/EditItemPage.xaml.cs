using System;
using System.Collections.Generic;
using System.ComponentModel;
using WebRefresher.Models;
using WebRefresher.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebRefresher.Views
{
    public partial class EditItemPage : ContentPage
    {
        public EditItemPage()
        {
            InitializeComponent();
            BindingContext = new EditItemViewModel();
        }

        private void Slider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            DisplayLabel.Text = String.Format($"Web page will refresh every {0} seconds", value);
        }
    }
}