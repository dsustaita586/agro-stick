using InvernaderoApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvernaderoApp
{
    public partial class App : Application
    {
        public App()
        {
            // Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Njc0NjUyQDMyMzAyZTMyMmUzMGVBdHhldHpCTWI1MGRtWjVWVzd5MTEvbkZ3UXUvc2JXS3lxVUgzblFvaEk9");

            InitializeComponent();

            XF.Material.Forms.Material.Init(this);

            if (Application.Current.Properties.ContainsKey("usuario"))
                MainPage = new MenuShell();
            else
                MainPage = new LoginPage();
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
