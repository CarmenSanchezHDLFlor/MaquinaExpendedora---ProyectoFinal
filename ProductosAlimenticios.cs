using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class ProductosAlimenticios : Producto {

        // PROPIEDADES
        public string InformacionNutricional { get; set; }

        // CONSTRUCTORES
        public ProductosAlimenticios() { }
        public ProductosAlimenticios(string nombre, int unidades, double precioUnitario, string descripcion, string informacionNutricional)
            : base(nombre, unidades, precioUnitario, descripcion) {
            InformacionNutricional = informacionNutricional;
        }

        // METODOS
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($"Informacion nutricional: {InformacionNutricional}");
        }
    }
}
