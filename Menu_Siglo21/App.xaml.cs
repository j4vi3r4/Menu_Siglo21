using Menu_Siglo21.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Menu_Siglo21
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MenuPedidos();
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
