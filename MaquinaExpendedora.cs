using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class MaquinaExpendedora {

        // PROPIEDADES
        private List<Producto> productos;

        // CONSTRUCTORES
        public MaquinaExpendedora() { productos = new List<Producto>(); }


        // METODOS 
        // metodo para mostrar productos
        public void MostrarProductos() {
            foreach(Producto p in productos) {
                p.MostrarInformacion();
                Console.WriteLine();
            }
        }


    }
}

