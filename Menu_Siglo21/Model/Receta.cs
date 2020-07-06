using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Menu_Siglo21.Model
{
    public class Receta
    {
        
        public int IdReceta { get; set; }

        
        public string Nombre { get; set; }

        
        public string Descripcion { get; set; }

        
        public int Precio { get; set; }
       
        public string Disponibilidad { get; set; }

        
        public Origen Origen { get; set; }

      
        public int Eleminado { get; set; }

        public override string ToString()
        {
            return this.Descripcion;
        }
    }
   

    
    /*en caso que ocupe esto en el xaml para la url de la imagen
     * <Image Source="{Binding ImageUrl}"
                               Aspect="AspectFill"
                               HeightRequest="150"
                               WidthRequest="150"
                               HorizontalOptions="Center" />*/
}
