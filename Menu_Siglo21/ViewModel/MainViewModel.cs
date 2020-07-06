namespace Menu_Siglo21.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    public class MainViewModel
    {        
        public PlatosViewModel Platos { get; set; }
        public BarViewModel Bebidas { get; set; }

        public MainViewModel()
        {
            this.Platos = new PlatosViewModel();
            this.Bebidas = new BarViewModel();
        }
    }
}
