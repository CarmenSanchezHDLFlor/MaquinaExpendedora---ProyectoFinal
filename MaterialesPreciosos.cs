using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class MaterialesPreciosos : Producto {

        // PROPIEDADES
        public string TipoMaterial {  get; set; }
        public double Peso { get; set; }

        // CONSTRUCTORES
        public MaterialesPreciosos() { }

        // CONTRUCTOR PARAMETRIZADO
        public MaterialesPreciosos(int id, string nombre, int unidades, double precioUnitario, string descripcion, string tipoMaterial, double peso)
            : base(id, nombre, TipoProducto.MaterialesPreciosos, descripcion, unidades, false) {
            TipoMaterial = tipoMaterial;
            Peso = peso;
        }

        // METODOS
        // metodo para mostrar la info de materiales preciosos
        public override void MostrarInformacion() {
            base.MostrarInformacion();
            Console.WriteLine($"TipoMaterial: {TipoMaterial}");
            Console.WriteLine($"Peso: {Peso} gramos");
        }


    }
}
