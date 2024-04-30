using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class ProductosAlimenticios : Producto {

        // PROPIEDADES

        /// <summary>
        /// Propiedad pública de InformacionNutricional 
        /// </summary>
        public string InformacionNutricional { get; set; }

        // CONSTRUCTORES

        /// <summary>
        /// Contructor por defecto 
        /// </summary>
        public ProductosAlimenticios() { }

        // CONTRUCTOR PARAMETRIZADO

        /// <summary>
        /// Contructor parametrizado de Productos Alimenticios 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="unidades"></param>
        /// <param name="precioUnitario"></param>
        /// <param name="descripcion"></param>
        /// <param name="informacionNutricional"></param>
        public ProductosAlimenticios(int id, string nombre, int unidades, double precioUnitario, string descripcion,
            string informacionNutricional) : base(id, nombre, TipoProducto.ProductosAlimenticios, descripcion, unidades, precioUnitario) {
            InformacionNutricional = informacionNutricional;
        }

        // METODOS

        /// <summary>
        ///  Metodo público sobreescrito heredado de su padre 'Producto'
        /// </summary>
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($"Informacion nutricional: {InformacionNutricional}");
        }

    }
}
