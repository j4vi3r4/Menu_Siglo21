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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Transactions;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    public class PlatosViewModel : BaseViewModel
    {
        #region Atributos

        bool isRefreshing; // para refrescar la lista
        private ApiService apiService;
        private ObservableCollection<Receta> platos;
        int txtCantidadCount;
        //public string Value { get; set; }

        #endregion


        #region Propiedades

        public string Cantidad { get; set; }

        public int TxtCantidadCount
        {
            get { return this.txtCantidadCount; }
            set
            {
                txtCantidadCount = value;
                OnPropertyChanged("TxtCantidadCount");
            }
        }

        public ObservableCollection<Receta> Platos //PROPIEDAD ItemsSource="{Binding Platos}" DEL XAML
        {
            get { return this.platos; }
            set { this.SetValue(ref this.platos, value); }
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

            AddElementCommand = new Command<string>(
                execute: (string arg) =>
                {
                    RecetaObject recetaObject = new RecetaObject();

                    recetaObject.cantidad = txtCantidadCount;
                    recetaObject.id_receta = int.Parse(arg);

                    //Agregamos a la lista de insumos
                    RecetasArray.Add(recetaObject);
                    
                    //Seteamos el setteper a 0
                    txtCantidadCount = 0;


                    //Debug.WriteLine("-------> String Arg: " + arg);
                    //Debug.WriteLine("-------> Text Count: " + txtCantidadCount + " / " + Value );
                }
                );
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

        public ICommand AddElementCommand { private set; get; }

        private void RefreshCanExecutes()
        {
            ((Command)AddElementCommand).ChangeCanExecute();
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

        private  void AgregarPlatoPedido()
        {
           
        }

    }
    #endregion
}

