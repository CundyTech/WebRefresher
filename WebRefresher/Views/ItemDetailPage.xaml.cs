using System.ComponentModel;
using WebRefresher.ViewModels;
using Xamarin.Forms;

namespace WebRefresher.Views
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