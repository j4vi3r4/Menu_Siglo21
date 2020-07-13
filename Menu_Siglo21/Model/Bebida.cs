using System;
using System.Collections.Generic;
using System.Text;

namespace Menu_Siglo21.Model
{
    public class Bebida
    {
        public int IdReceta { get; set; }

        public string Nombre { get; set; }


        public string Descripcion { get; set; }


        public int Precio { get; set; }

        //public string ImageUrl { get; set; }
        public string Disponibilidad { get; set; }

        public Origen Origen { get; set; }


        public int Eleminado { get; set; }
    }
}
