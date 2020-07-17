﻿using GalaSoft.MvvmLight.Command;
using Menu_Siglo21.Model;
using Menu_Siglo21.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Menu_Siglo21.ViewModel
{
    public class EnviaTuOrdenViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        private bool isRefreshing;
        private ObservableCollection<Receta> listaOrden;
        
        #endregion

        #region Propiedades
          
       // public static ArrayList RecetasArray { get; set; }
        public ObservableCollection<Receta> ListaOrden // CON este hace binding al xaml enviatuorden.xaml en la parte del listview 
        {
            get { return this.listaOrden; }
            set { this.SetValue(ref this.listaOrden, value); }
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

            EnviarOrdenCommand = new Command<string>(
                execute: async (string arg) =>
                {
                    if (RecetasArray.Count > 0)
                    {
                        await SendOrdenAsync();
                        RefreshCanExecutes();
                    }
                }
                );
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

            ListaOrden = new ObservableCollection<Receta>(listRecetas);

            this.IsRefreshing = false;         
        }

        private async Task SendOrdenAsync()
        {
            var connection = await this.apiService.CheckConnection(); // validación de conexión a internet 
            //Debug.WriteLine("------> connection " + connection);
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }

            // Aquí le pasamos la ID de mesa en bruto hasta que no podamos guardar en el setting
            string id_mesa = "25";

            string id_comensal = await getComensal(id_mesa);

            if (id_comensal.Equals("-1"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha registrado en el totem", "Accept");
                return;
            }

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

            string json = "{ \"id_comensal\" : \"" + id_comensal + "\", \"platos\" :" + platosJson + ", \"cantidad\" :" + cantidadJson + " }";
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

        // TODO: Agregar el buscar comensal primero antes de hacer la agregación

        private async Task<string> getComensal(string id_mesa)
        {
            var connection = await this.apiService.CheckConnection(); // validación de conexión a internet 
            //Debug.WriteLine("------> connection " + connection);
            if (!connection.IsSuccess)
            {
                return "-1";
            }

            string json = "{ \"id_mesa\" : \"" + id_mesa + "\" }";
            //Debug.WriteLine("-----> JSON Query :" + json);

            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            var response = await this.apiService.PostUpdate<string>(json, url, prefix, "/getComensal"); //acá lista de mesa

            if (!response.IsSuccess)
                return "-1";

            if (response.Result.ToString().Equals("0"))
                return "-1";

            return response.Result.ToString();
        }

        #endregion



    }
}
