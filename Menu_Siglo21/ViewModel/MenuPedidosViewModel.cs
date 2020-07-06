namespace Menu_Siglo21.ViewModel
{
    using Menu_Siglo21.Model;
    using Menu_Siglo21.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Xamarin.Forms;

    public class MenuPedidosViewModel : BaseViewModel
    {
        private ApiService apiService;
        


        private ObservableCollection<Receta> recetaItems = new ObservableCollection<Receta>();

        public ObservableCollection<Receta> RecetaItems
        {
            get { return recetaItems; }
            set { recetaItems = value; OnPropertyChanged(); }
        }

        public MenuPedidosViewModel()
        {
            this.apiService = new ApiService();
            
            this.FiltroReceta();
        }

        private async void FiltroReceta()
        {
            var response = await this.apiService.GetList<Receta>("http://10.0.2.2:4567", "/app", "/listarrecetas");/*http://51.143.4.69:4567*/            

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            
            
            //var list = (List<Receta>)response.Result;
            //this.RecetaItems = new ObservableCollection<Receta>(list);        
        }
    }
}
