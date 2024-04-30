using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class Producto {

        // TIPO ENUM 

        /// <summary>
        /// Propiedad pública tipo Enum de TipoProducto
        /// Nos ayuda a saber que tipos de productos tenemos 
        /// </summary>
        public enum TipoProducto {
            MaterialesPreciosos,
            ProductosAlimenticios,
            ProductosElectronicos
        }

        // PROPIEDADES

        /// <summary>
        /// Propiedad pública Id de un producto 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Propiedad pública Nombre de un producto 
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Propiedad pública Unidades
        /// Cuantas unidades de cada producto tenemos 
        /// </summary>
        public int Unidades { get; set; }

        /// <summary>
        /// Propiedad pública PrecioUnitario
        /// El precio de cada producto 
        /// </summary>
        public double PrecioUnitario { get; set; }

        /// <summary>
        /// Propiedad pública de Descripcion 
        /// La descripcion de cada producto que tenemos en la máquina 
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Propiedad pública de Vendido
        /// Nos ayuda a comprobar si un producto ha sido vendido, no tenemos stock o no 
        /// </summary>
        public bool Vendido {  get; set; }

        /// <summary>
        /// Propiedad pública de TipoProducto 
        /// </summary>
        public TipoProducto Tipo;

        // CONTRUCTORES 

        /// <summary>
        /// Contructor por defecto de Producto 
        /// </summary>
        public Producto() { }

        // CONTRUCTOR PARAMETRIZADO
        /// <summary>
        /// Constructor parametrizado de Producto 
        /// Inicializa las propiedades de Producto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="tipo"></param>
        /// <param name="descripcion"></param>
        /// <param name="unidades"></param>
        /// <param name="precioUnitario"></param>
        public Producto(int id, string nombre, TipoProducto tipo, string descripcion, int unidades, double precioUnitario) {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
            Descripcion = descripcion;
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
        }

        // METODOS

        /// <summary>
        /// Método público para mostrar informacion de cada producto
        /// Este método es el que se va a sobreescribir en sus hijas 'ProductosElectronico', 'ProductosAlimenticios' y 'MaterialesPreciosos'
        /// Nos da la información común de cada producto 
        /// </summary>
        public virtual void MostrarInformacion() {
            Console.WriteLine($" Nombre: {Nombre}");
            Console.WriteLine($" Unidades: {Unidades}");
            Console.WriteLine($" Precio Unitario: {PrecioUnitario}");
            Console.WriteLine($" Descripcion: {Descripcion}");
        }

        /// <summary>
        /// Método público para consultar si el producto está vendido
        /// </summary>
        /// <returns>Vendido</returns>
        public bool EstaVendido() {
            return Vendido;
        }

        /// <summary>
        /// Método público para marcar el producto como vendido
        /// </summary>
        public void MarcarComoVendido() {
            Vendido = true;
        }

    }
}
