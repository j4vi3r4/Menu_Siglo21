using System;
using System.Collections.Generic;
using System.Text;

namespace Menu_Siglo21.Model
{
    public class Orden
    {
        public int Id_Comensal { get; set; }

        public string Platos { get; set; }

        public string Cantidad { get; set; }


        public override string ToString()
        {
            return this.Id_Comensal.ToString() + Platos + Cantidad;            
        }
    }
}
