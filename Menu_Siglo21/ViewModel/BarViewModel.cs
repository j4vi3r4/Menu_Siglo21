namespace Menu_Siglo21.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Menu_Siglo21.Model;
    using Menu_Siglo21.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class BarViewModel : BaseViewModel
    {
        #region Atributos
        bool isRefreshing; // para refrescar la lista
        private ApiService apiService;
        private ObservableCollection<Receta> bebidas;
        int txtCantidadCount;
        //      private ObservableCollection<Receta> origenes;
        #endregion

        #region Propiedades
        public int TxtCantidadCount
        {
            get { return this.txtCantidadCount; }
            set
            {
                txtCantidadCount = value;
                OnPropertyChanged("TxtCantidadCount");
            }
        }
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

            AddElementCommand = new Command<string>(
                execute: (string arg) =>
                {
                    if (txtCantidadCount > 0)
                    {
                        RecetaObject recetaObject = new RecetaObject();

                        recetaObject.cantidad = txtCantidadCount;
                        recetaObject.id_receta = int.Parse(arg);

                        //Agregamos a la lista de insumos
                        RecetasArray.Add(recetaObject);

                        //Seteamos el setteper a 0
                        txtCantidadCount = 0;

                        RefreshCanExecutes();
                        //Debug.WriteLine("-------> String Arg: " + arg);
                        //Debug.WriteLine("-------> Text Count: " + txtCantidadCount + " / " + Value );
                        Debug.WriteLine("-------> Cantidad Elementos: " + RecetasArray.Count);
                    }
                }
                );
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
        public ICommand AddElementCommand { private set; get; }

        private void RefreshCanExecutes()
        {
            ((Command)AddElementCommand).ChangeCanExecute();
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
            try
            {
                for (int i = 0; i < listBebidas.Count; i++)
                {
                    if (listBebidas[i].Origen.Id_Origen == 2 && listBebidas[i].Disponibilidad == "D")
                    {
                        
                        Bebidas.Add(listBebidas[i]);
                        //Debug.WriteLine("------>" + listPlatos[i].Nombre);                    
                        this.IsRefreshing = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine( ex.Message);   
            }
        }
    }
    #endregion


}

