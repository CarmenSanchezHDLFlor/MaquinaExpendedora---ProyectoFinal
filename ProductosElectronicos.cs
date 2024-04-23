using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class ProductosElectronicos : Producto {

        // PROPIEDADES
        public string TipoMaterial {  get; set; }
        public bool TieneBateria { get; set; }
        public bool Precargado { get; set; }

        // CONSTRUCTORES
        public ProductosElectronicos() { }

        // CONTRUCTOR PARAMETRIZADO
        public ProductosElectronicos(int id, string nombre, int unidades, double precioUnitario, string descripcion, string tipoMaterial, bool tieneBateria, bool precargado)
    :    base(id, nombre, TipoProducto.ProductosElectronicos, descripcion, unidades, false) {
            TipoMaterial = tipoMaterial;
            TieneBateria = tieneBateria;
            Precargado = precargado;
        }

        // METODOS
        // metodo para mostrar la info de productos electronicos 
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($"Tipo Material: {TipoMaterial}");
            Console.WriteLine($"Tiene batería: {TieneBateria}");
            Console.WriteLine($"Precargado: {(Precargado ? "Si" : "No")}");
        }

        
    }
}
