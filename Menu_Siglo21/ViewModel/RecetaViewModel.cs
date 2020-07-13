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
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class RecetaViewModel : BaseViewModel
    {
     /*   bool isRefreshing;
        private ApiService apiService;

        private ObservableCollection<Receta> recetas;
        private ObservableCollection<Origen> origenes;

        public ObservableCollection<Receta> Recetas
        {
            get { return this.recetas; } // devuelve el atributo privado de mesas 
            set { this.SetValue(ref this.recetas, value); } // se encarga de asignar y refrescar la viewmodel
        }
        public ObservableCollection<Origen> Origenes
        {
            get { return this.origenes ; }
            set { this.SetValue(ref this.origenes, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                this.SetValue(ref this.isRefreshing, value);
            }
        }
        public RecetaViewModel()
        {
            this.apiService = new ApiService();
            //this.GetOrigen();
            this.LoadRecetas();
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadRecetas);
            }
        }

        private async void LoadRecetas()
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
            if (true)
            {

            }
            var list = (List<Receta>)response.Result;
            // list.Where(i => i.Origen.Id_Origen == 2);
            this.Recetas = new ObservableCollection<Receta>(list);
            Debug.WriteLine("---" + recetas);
            this.IsRefreshing = false;
        }*/

    }
}
