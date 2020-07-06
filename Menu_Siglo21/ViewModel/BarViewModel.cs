
namespace Menu_Siglo21.ViewModel
{
    using Menu_Siglo21.Model;
    using Menu_Siglo21.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Xamarin.Forms;

    public class BarViewModel : BaseViewModel
    {
     /*   private ApiService apiService;
        private ObservableCollection<Receta> bebidas;

        public ObservableCollection<Receta> Bebidas
        {
            get { return this.bebidas; }
            set => this.SetValue(ref this.bebidas, value);
        }

        public BarViewModel()
        {
            this.apiService = new ApiService();
            this.LoadBebidas();
        }

        private async void LoadBebidas()
        {
            var response = await this.apiService.GetList<Receta>("://10.0.2.2:4567", "/app", "/listarrecetas");ttp://51.143.4.69:4567
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var list = (List<Receta>)response.Result;
            this.bebidas= new ObservableCollection<Receta>(list);
        }*/
    }
}
