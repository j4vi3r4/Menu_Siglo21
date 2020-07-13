using Menu_Siglo21.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace Menu_Siglo21.Model
{
    public class Origen : BaseViewModel
    {
        [Key]
        public int Id_Origen { get; set; }

        [Display(Name ="Descripcion")]
        public string Descripcion { get; set; }


        public override string ToString()
        {
            return this.Id_Origen.ToString();
           // return this.IdOrigen.ToString() + this.Descripcion;           
        }

    }
}
