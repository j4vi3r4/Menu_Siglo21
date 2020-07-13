using Menu_Siglo21.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Menu_Siglo21.ViewModel
{
    public class MainViewModel
    {
      //  public RecetaViewModel Recetas { get; set; }
        public PlatosViewModel Platos { get; set; } // PLATOS binding context de la page 
        public BarViewModel Bebidas { get; set; } //debe ser cambiado a bebidas
        //public RecetaViewModel Origenes { get; set; }


        public MainViewModel()
        {
          //  this.Recetas = new RecetaViewModel(); 
            this.Platos = new PlatosViewModel();
            this.Bebidas= new BarViewModel();
         //   this.Origenes = new RecetaViewModel();
        }
    }
}
