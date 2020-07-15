using Menu_Siglo21.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Menu_Siglo21.ViewModel
{
    public class MainViewModel
    {      
        public PlatosViewModel Platos { get; set; } // PLATOS binding context de la page 
        public BarViewModel Bebidas { get; set; } //debe ser cambiado a bebidas
        public EnviaTuOrdenViewModel EnviarOrden { get; set; }

       
        public MainViewModel()
        {
            this.EnviarOrden = new EnviaTuOrdenViewModel();
            this.Platos = new PlatosViewModel();
            this.Bebidas= new BarViewModel();         
        }
    }
}
