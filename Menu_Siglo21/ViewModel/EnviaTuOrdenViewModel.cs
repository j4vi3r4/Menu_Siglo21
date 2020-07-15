using GalaSoft.MvvmLight.Command;
using Menu_Siglo21.Model;
using Menu_Siglo21.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Menu_Siglo21.ViewModel
{
    public class EnviaTuOrdenViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;
        
        private ObservableCollection<Orden> enviarOrden;
        #endregion

        #region Propiedades
        public ObservableCollection<Orden> EnviarOrden
        {
            get { return this.enviarOrden; }
            set { this.SetValue(ref this.enviarOrden, value); }
        }
        #endregion

        #region Commands

        public ICommand EnviarOrdenCommand => new RelayCommand(SendOrden);




        #endregion

        #region Methods
        private void SendOrden()
        {
            //agregar cantidad id_receta
        }
        
        #endregion


    }
}
