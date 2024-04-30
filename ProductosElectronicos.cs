using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class ProductosElectronicos : Producto {

        // PROPIEDADES

        /// <summary>
        /// Propiedad pública de TipoMaterial 
        /// </summary>
        public string TipoMaterial {  get; set; }

        /// <summary>
        /// Propiedad pública de TieneBateria
        /// </summary>
        public bool TieneBateria { get; set; }

        /// <summary>
        /// Propiedad pública de Precargado
        /// </summary>
        public bool Precargado { get; set; }

        // CONSTRUCTORES

        /// <summary>
        /// Contructor por defecto de Productos Electronicos 
        /// </summary>
        public ProductosElectronicos() { }

        // CONTRUCTOR PARAMETRIZADO

        /// <summary>
        /// Contructor parametrizado de Productos Electronicos inicializado cada propiedad 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="unidades"></param>
        /// <param name="precioUnitario"></param>
        /// <param name="descripcion"></param>
        /// <param name="tipoMaterial"></param>
        /// <param name="tieneBateria"></param>
        /// <param name="precargado"></param>
        public ProductosElectronicos(int id, string nombre, int unidades, double precioUnitario, string descripcion, string tipoMaterial, 
            bool tieneBateria, bool precargado) : base(id, nombre, TipoProducto.ProductosElectronicos, descripcion, unidades, precioUnitario) {
            TipoMaterial = tipoMaterial;
            TieneBateria = tieneBateria;
            Precargado = precargado;
        }

        // METODOS

        /// <summary>
        /// Método público sobreescrito de MostrarInformacion
        /// Lo tiene heredado de su padre 'Producto'
        /// </summary>
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($"Tipo Material: {TipoMaterial}");
            Console.WriteLine($"Tiene bateria: {TieneBateria}");
            Console.WriteLine($"Precargado: {(Precargado ? "Si" : "No")}");
        }

    }
}
