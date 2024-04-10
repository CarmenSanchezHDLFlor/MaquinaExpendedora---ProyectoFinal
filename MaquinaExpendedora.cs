using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class MaquinaExpendedora {

        // PROPIEDADES
        public List<Producto> Listaproductos { get; private set; }
        public MaquinaExpendedora Maquina;


        public Usuario Usuario { get; private set; }

        // CONSTRUCTORES
        public MaquinaExpendedora(Usuario usuario) {
            Usuario = usuario;
            Listaproductos = new List<Producto>();
        }

        // METODOS 
        public void MostrarProductos() {
            if (Listaproductos.Count == 0) {
                Console.WriteLine("no hay productos disponibles en nuestra maquina expendedora. ");
                return;
            }
            foreach (Producto p in Listaproductos) {
                p.MostrarInformacion();
                Console.WriteLine();
            }
        }


    }
}


