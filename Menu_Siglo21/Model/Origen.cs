using System;
using System.Collections.Generic;
using System.Text;

namespace Menu_Siglo21.Model
{
    public class Origen
    {
        public int IdOrigen { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}
