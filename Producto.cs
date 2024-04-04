using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class Producto {

        // PROPIEDADES
        public string Nombre { get; set; }
        public int Unidades { get; set; }
        public double PrecioUnitario { get; set; }
        public string Descripcion { get; set; }


        // CONTRUCTORES 
        public Producto() { }
        public Producto(string nombre, int unidades, double precioUnitario, string descripcion) {
            Nombre = nombre;
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
            Descripcion = descripcion;
        }

        // METODOS
        // metodo para mostrar informacion de cada producto
        public virtual void MostrarInformacion() {
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Unidades: {Unidades}");
            Console.WriteLine($"Precio Unitario: {PrecioUnitario}");
            Console.WriteLine($"Descripcion: {Descripcion}");
        }


    }
}
