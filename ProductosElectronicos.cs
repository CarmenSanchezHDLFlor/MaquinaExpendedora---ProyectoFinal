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
        public ProductosElectronicos(string nombre, int unidades, double precioUnitario, string descripcion, string tipoMaterial, 
            bool tieneBateria, bool precargado)
            : base(nombre, unidades, precioUnitario, descripcion) {
            TipoMaterial = tipoMaterial;
            TieneBateria = tieneBateria;
            Precargado = precargado;
        }

        // METODOS
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($"Tipo Material: {TipoMaterial}");
            Console.WriteLine($"Tiene batería: {TieneBateria}");
            Console.WriteLine($"Precargado: {(Precargado ? "Sí" : "No")}");
        }

        
    }
}
