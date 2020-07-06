namespace Menu_Siglo21.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Menu_Siglo21.Model;
    using Menu_Siglo21.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Cache;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PlatosViewModel : BaseViewModel
    {
        private ApiService apiService;
      //  private Origen origen;
        private ObservableCollection<Receta> platos;

        public ObservableCollection<Receta> Platos
        {
            get { return this.platos; }
            set => this.SetValue(ref this.platos, value);
        }

        public PlatosViewModel()
        {
         //   this.origen = new Origen();
            this.apiService = new ApiService();
            this.LoadPlatos();
        }

        private async void LoadPlatos()
        {
            var response = await this.apiService.GetList<Receta>("http://34.230.13.150:4567", "/app", "/listarrecetas");/*http://51.143.4.69:4567*//* - http://10.0.2.2:4567*/
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Aceptar");
                return;
            }
           // var idOrigen = (List<Receta>)response.Result;
            
            var list = (List<Receta>)response.Result;
            this.Platos = new ObservableCollection<Receta>(list);
        }
    }
}
