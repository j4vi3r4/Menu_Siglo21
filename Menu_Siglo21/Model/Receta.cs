using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Menu_Siglo21.Model
{
    public class Receta
    {
        [Key]
        [Display(Name = "Id_receta")]
        public int Id_receta { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Precio")]
        public int Precio { get; set; }

        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Display(Name = "Disponibilidad")]
        public string Disponibilidad { get; set; }

        public Origen Origen { get; set; }

        //public int Eleminado { get; set; }

        public override string ToString()
        {
            return this.Origen.Id_Origen.ToString() + Disponibilidad;
        }
    } 
}
