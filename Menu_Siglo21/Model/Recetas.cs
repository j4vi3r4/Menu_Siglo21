using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Menu_Siglo21.Model
{
    public partial class Recetas
    {
        [JsonProperty("id_receta")]
        public long IdReceta { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("precio")]
        public long Precio { get; set; }

        [JsonProperty("disponibilidad")]
        public string Disponibilidad { get; set; }

        [JsonProperty("origen")]
        public Origen Origen { get; set; }

        [JsonProperty("eleminado")]
        public long Eleminado { get; set; }

    }
    //SI ES COCINA O BAR
    public partial class Origen
    {
        [JsonProperty("id_origen")]
        public long IdOrigen { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
    }

}
