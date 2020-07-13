namespace Menu_Siglo21.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Menu_Siglo21.Model;
    using Menu_Siglo21.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class BarViewModel : BaseViewModel
    {
        #region Atributos
        bool isRefreshing; // para refrescar la lista
        private ApiService apiService;
        private ObservableCollection<Receta> bebidas;
        //      private ObservableCollection<Receta> origenes;
        #endregion

        #region Propiedades
        public ObservableCollection<Receta> Bebidas //PROPIEDAD ItemsSource="{Binding Platos}" DEL XAML
        {
            get { return this.bebidas; }
            set { this.SetValue(ref this.bebidas, value); }
        }

        public ObservableCollection<Receta> Origenes
        {
            get { return this.bebidas; }
            set { this.SetValue(ref this.bebidas, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public BarViewModel()
        {
            this.apiService = new ApiService();
            this.LoadBebidas();
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadBebidas);
            }
        }
        #endregion
        #region Methods
        private async void LoadBebidas()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection(); // validación de conexión a internet 
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["Prefix"].ToString();
            var response = await this.apiService.GetList<Receta>(url, prefix, "/listarrecetas");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            var listBebidas = (List<Receta>)response.Result;
            Bebidas = new ObservableCollection<Receta>();
            for (int i = 0; i < listBebidas.Count; i++)
            {
                if (listBebidas[i].Origen.Id_Origen == 2 && listBebidas[i].Disponibilidad == "D")
                {                    
                    Bebidas.Add(listBebidas[i]);                      
                    this.IsRefreshing = false;
                }
            }
        }
    }

    #endregion


}

