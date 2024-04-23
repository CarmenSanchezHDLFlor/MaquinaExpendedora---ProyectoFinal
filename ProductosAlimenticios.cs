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

        // CONTRUCTOR PARAMETRIZADO
        public ProductosAlimenticios(int id, string nombre, int unidades, double precioUnitario, string descripcion, 
            string informacionNutricional) : base(id, nombre, TipoProducto.ProductosAlimenticios, descripcion, unidades, false) {
            InformacionNutricional = informacionNutricional;
        }

        // METODOS
        // metodo para mostrar la info de productos alimenticios
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($"Informacion nutricional: {InformacionNutricional}");
        }
    }
}
