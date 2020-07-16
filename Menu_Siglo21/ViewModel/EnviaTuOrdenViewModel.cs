using GalaSoft.MvvmLight.Command;
using Menu_Siglo21.Model;
using Menu_Siglo21.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public ICommand EnviarOrdenCommand { private set; get; }

        private void RefreshCanExecutes()
        {
            ((Command)EnviarOrdenCommand).ChangeCanExecute();
        } 

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(CargarPedidos);
            }
        }

        #endregion

        #region Methods
        
        public EnviaTuOrdenViewModel() {
            EnviarOrdenCommand = new Command<string>(
                execute: async (string arg) =>
                {
                    if (RecetasArray.Count > 0)
                    {
                        await SendOrdenAsync();
                    }
                }
                );
        }

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

        private async System.Threading.Tasks.Task SendOrdenAsync()
        {
            var connection = await this.apiService.CheckConnection(); // validación de conexión a internet 
            Debug.WriteLine("------> connection " + connection);
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }

            // Aquí le pasamos la ID de mesa en bruto hasta que no podamos guardar en el setting
            int id_mesa = 5;

            // >>> Este código concatena los platos y las cantidades para 
            string platosJson = "[";
            string cantidadJson = "[";

            int lenght = RecetasArray.Count;

            for (int i = 0; i < lenght; i++)
            {
                RecetaObject item = (RecetaObject)RecetasArray[i];

                platosJson += "\"" + item.id_receta + "\"";
                cantidadJson += "\"" + item.cantidad + "\"";

                if (i < (lenght - 1)) {
                    platosJson += ",";
                    cantidadJson += ",";
                }
            }

            platosJson += "]";
            cantidadJson += "]";

            string json = "{ \"id_mesa\" : \"" + id_mesa + "\", \"platos\" :" + platosJson + ", \"cantidad\" :" + cantidadJson + " }";
            Debug.WriteLine("-----> JSON Query :" + json);

            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            var response = await this.apiService.PostUpdate<string>(json, url, prefix, "/crearPreorden"); //acá lista de mesa

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            if (response.Result.ToString().Equals("0"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Erro al mandar orden, intente nuevamente", "Accept");
                return;
            }

            await Application.Current.MainPage.DisplayAlert("OK", "Pedido enviado correctamente", "Accept");

            RecetasArray.Clear();
            RecetasArray.TrimToSize();
        }

        /*private void SendOrden()
        {
            //agregar cantidad id_receta
        }*/

        #endregion



    }
}
