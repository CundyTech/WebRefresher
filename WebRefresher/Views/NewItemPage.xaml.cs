using System;
using System.Collections.Generic;
using System.ComponentModel;
using WebRefresher.Models;
using WebRefresher.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebRefresher.Views
{
    public partial class NewItemPage : ContentPage
    {
        public WebPage WebPage { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}