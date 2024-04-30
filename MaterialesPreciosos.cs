using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class MaterialesPreciosos : Producto {

        // PROPIEDADES

        /// <summary>
        /// Propiedad pública de TipoMaterial 
        /// </summary>
        public string TipoMaterial {  get; set; }

        /// <summary>
        /// Propiedad pública de Peso
        /// </summary>
        public double Peso { get; set; }

        // CONSTRUCTORES

        /// <summary>
        /// Constructor por defecto de MaterialesPreciosos
        /// </summary>
        public MaterialesPreciosos() { }

        // CONTRUCTOR PARAMETRIZADO

        /// <summary>
        /// Constructor parametrizado inicializando las propiedades de la clase 
        /// Y heredando las de su padre 'Producto'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="unidades"></param>
        /// <param name="precioUnitario"></param>
        /// <param name="descripcion"></param>
        /// <param name="tipoMaterial"></param>
        /// <param name="peso"></param>
        public MaterialesPreciosos(int id, string nombre, int unidades, double precioUnitario, string descripcion,
            string tipoMaterial, double peso)
            : base(id, nombre, TipoProducto.MaterialesPreciosos, descripcion, unidades, precioUnitario) {
            TipoMaterial = tipoMaterial;
            Peso = peso;
        }

        // METODOS

        /// <summary>
        /// Método público sobreescrito para mostrar la info de materiales preciosos
        /// Hereda la informacion de su padre 'Producto'
        /// </summary>
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($" TipoMaterial: {TipoMaterial}");
            Console.WriteLine($" Peso: {Peso} gramos");
        }

    }
}
