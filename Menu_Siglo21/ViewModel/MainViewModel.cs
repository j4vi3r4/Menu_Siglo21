
using System.Collections;

namespace Menu_Siglo21.ViewModel
{
    public class MainViewModel : BaseViewModel
    {      
        public PlatosViewModel Platos { get; set; } // PLATOS binding context de la page 
        public BarViewModel Bebidas { get; set; } //debe ser cambiado a bebidas
        public EnviaTuOrdenViewModel EnviarOrden { get; set; }

       
        public MainViewModel()
        {
            RecetasArray = new ArrayList();

            this.EnviarOrden = new EnviaTuOrdenViewModel();
            this.Platos = new PlatosViewModel();
            this.Bebidas= new BarViewModel();         
        }
    }
}
