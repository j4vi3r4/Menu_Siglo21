using GalaSoft.MvvmLight.Command;
using Menu_Siglo21.Model;
using Menu_Siglo21.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Menu_Siglo21.ViewModel
{
    public class EnviaTuOrdenViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private bool isRefreshing;
        private ObservableCollection<Receta> enviarOrden;
        private static ArrayList recetaArray;


        // private BarViewModel barViewModel;

        //   private ObservableCollection<Orden> enviarOrden;

        
        #endregion

        #region Propiedades
          
       // public static ArrayList RecetasArray { get; set; }
        public ObservableCollection<Receta> EnviarOrden // CON este hace binding al xaml enviatuorden.xaml en la parte del listview 
        {
            get { return this.enviarOrden; }
            set { this.SetValue(ref this.enviarOrden, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public EnviaTuOrdenViewModel()
        {
            this.apiService = new ApiService();
            this.CargarPedidos();
        }
        #endregion

        #region Commands

        
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(CargarPedidos);
            }
        }




        #endregion

        #region Methods

        private async void CargarPedidos()
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
            var listRecetas = (List<Receta>)response.Result;
         var listArray = (List<Receta>)response.Result;
            

            EnviarOrden = new ObservableCollection<Receta>(listRecetas);
           // RecetasArray = new ArrayList(recetaArray);
            RecetasArray = new ArrayList(listArray);
            this.IsRefreshing = false;         

        }


        /*private void SendOrden()
        {
            //agregar cantidad id_receta
        }*/

        #endregion



    }
}
