namespace Menu_Siglo21.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MainViewModel
    {
        public NumeroViewModel NumeroMesa { get; set; }


        public MainViewModel()
        {
            this.NumeroMesa = new NumeroViewModel();
        }
    }
}
