using System;
using WebRefresher.Services;
using WebRefresher.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WebRefresher
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("EditItemPage", typeof(EditItemPage));
            DependencyService.Register<DataStoreManager>();
            DependencyService.Register<FileHandler>();
            DependencyService.Register<HttpManager>();
            DependencyService.Register<RefreshManager>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
