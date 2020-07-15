namespace Menu_Siglo21.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Menu_Siglo21.Model;
    using Menu_Siglo21.Services;
    using Menu_Siglo21.Views;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public class PlatosViewModel : BaseViewModel
    {
        #region Atributos
        bool isRefreshing; // para refrescar la lista
        private ApiService apiService;
        private ObservableCollection<Receta> platos;
        private string txtCantidadCount;

        #endregion


        #region Propiedades

        public string Cantidad { get; set; }
        
        public ObservableCollection<Receta> Platos //PROPIEDAD ItemsSource="{Binding Platos}" DEL XAML
        {
            get { return this.platos; }
            set { this.SetValue(ref this.platos, value); }
        }
        public string TxtCantidadCount
        {
            get { return this.txtCantidadCount; }
            set { this.SetValue(ref this.txtCantidadCount, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public PlatosViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPlatos();            
        }
        #endregion


        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPlatos);
            }
        }

       public ICommand AgregarPedidoCommand
        {
            get
            {
                return new RelayCommand(AgregarPlatoPedido);
            }
        }
                

        #endregion

        #region Methods
        private async void LoadPlatos()
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
            var listPlatos = (List<Receta>)response.Result;
            Platos = new ObservableCollection<Receta>();

            
                for (int i = 0; i < listPlatos.Count; i++)
                {
                    if (listPlatos[i].Origen.Id_Origen == 1 && listPlatos[i].Disponibilidad == "D")
                    {
                        Platos.Add(listPlatos[i]);
                        //Debug.WriteLine("------>" + listPlatos[i].Nombre);                    
                        this.IsRefreshing = false;
                    }
                }                      
        }

        private async void AgregarPlatoPedido()
        {
           
        }


    }
    #endregion
}

