using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachinePropia;
using static MaquinaExpendedora_ProyectoFinal.Producto;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class MaquinaExpendedora {

        // PROPIEDADES 

        /// <summary>
        /// Propiedad pública estatica de List<Producto> ListaProductos inicializado con 12 productos 
        /// </summary>
        public static List<Producto> ListaProductos { get; set; } = new List<Producto>(12);

        /// <summary>
        /// Propiedad privada de GestorCompra 
        /// </summary>
        private GestorCompra GestorCompra;

        /// <summary>
        /// Propiedad privada de Usuario
        /// </summary>
        private Usuario Usuario;

        // CONSTRUCTOR 

        /// <summary>
        /// Contructor por defecto de MaquinaExpendedor
        /// </summary>
        public MaquinaExpendedora() { }

        // CONTRUCTOR PARAMETRIZADO 

        /// <summary>
        /// Contructor parametrizado de MaquinaExpendedora con parametro Usuario para poder inicializarlo con el usuario actual 
        /// También inicializamos el gestor de compra 
        /// </summary>
        /// <param name="usuario"></param>
        public MaquinaExpendedora(Usuario usuario) {
            this.Usuario = usuario;
            GestorCompra = new GestorCompra();
        }
        /// <summary>
        /// Método público para cargar los productos desde un archivo CSV
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo CSV</param>
        public void CargarProductosDesdeCSV(string rutaArchivo) {
            using (StreamReader reader = new StreamReader(rutaArchivo)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    // Dividir la línea en campos utilizando el delimitador "|"
                    string[] fields = line.Split('|');

                    // Parsear los campos comunes a todos los productos
                    int id = int.Parse(fields[0]);
                    string nombre = fields[1];
                    int unidades = int.Parse(fields[2]);
                    double precio = double.Parse(fields[3]);

                    // Determinar el tipo de producto en función del contenido de los campos adicionales
                    if (fields.Length >= 5) {
                        string descripcion = fields[4];
                        if (fields.Length >= 6) {
                            // Si hay al menos seis campos, es un producto alimenticio
                            string informacionNutricional = fields[5];
                            ListaProductos.Add(new ProductosAlimenticios(id, nombre, unidades, precio, descripcion, informacionNutricional));
                        }
                        else if (fields.Length == 7 && fields[6].ToLower() == "true") {
                            // Si hay siete campos y el último campo es "true", es un producto electrónico
                            string tipoMaterial = fields[5];
                            bool tieneBateria = bool.Parse(fields[6]);
                            ListaProductos.Add(new ProductosElectronicos(id, nombre, unidades, precio, descripcion, tipoMaterial, tieneBateria, false));
                        }
                        else if (fields.Length == 8 && fields[6].ToLower() == "true" && fields[7].ToLower() == "true") {
                            // Si hay ocho campos y los dos últimos campos son "true", es un producto electrónico con batería precargada
                            string tipoMaterial = fields[5];
                            bool tieneBateria = bool.Parse(fields[6]);
                            bool precargado = bool.Parse(fields[7]);
                            ListaProductos.Add(new ProductosElectronicos(id, nombre, unidades, precio, descripcion, tipoMaterial, tieneBateria, precargado));
                        }
                        else if (fields.Length == 7) {
                            // Si hay siete campos pero no cumple con las condiciones anteriores, es un material precioso
                            string tipoMaterial = fields[5];
                            double peso = double.Parse(fields[6]);
                            ListaProductos.Add(new MaterialesPreciosos(id, nombre, unidades, precio, descripcion, tipoMaterial, peso));
                        }
                        else {
                            // Si no coincide con ninguno de los casos anteriores, mostrar un error
                            Console.WriteLine($" Error: La línea del archivo CSV no tiene el formato correcto para un producto: {line}");
                        }
                    }
                    else {
                        // Si no hay suficientes campos, mostrar un error
                        Console.WriteLine($" Error: La línea del archivo CSV no tiene el formato correcto para un producto: {line}");
                    }
                }
            }
        }

        /// <summary>
        /// Método público para guardar productos en CSV
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="rutaArchivo"></param>
        public void GuardarProductoEnCSV(Producto producto, string rutaArchivo) {
            using (StreamWriter writer = new StreamWriter(rutaArchivo, true)) {
                if (producto is ProductosAlimenticios) {
                    ProductosAlimenticios productoAlimenticio = (ProductosAlimenticios)producto;
                    writer.WriteLine($"{producto.Id}|{productoAlimenticio.Nombre}|{productoAlimenticio.Descripcion}|{productoAlimenticio.Unidades}|{productoAlimenticio.PrecioUnitario}|{productoAlimenticio.InformacionNutricional}");
                }
                else if (producto is ProductosElectronicos) {
                    ProductosElectronicos productoElectronico = (ProductosElectronicos)producto;
                    string tieneBateria = productoElectronico.TieneBateria ? "true" : "false";
                    string precargado = productoElectronico.Precargado ? "true" : "false";
                    writer.WriteLine($"{producto.Id}|{productoElectronico.Nombre}|{productoElectronico.Descripcion}|{productoElectronico.Unidades}|{productoElectronico.PrecioUnitario}|{productoElectronico.TipoMaterial}|{tieneBateria}|{precargado}");
                }
                else if (producto is MaterialesPreciosos) {
                    MaterialesPreciosos materialPrecioso = (MaterialesPreciosos)producto;
                    writer.WriteLine($"{producto.Id}|{materialPrecioso.Nombre}|{materialPrecioso.Descripcion}|{materialPrecioso.Unidades}|{materialPrecioso.PrecioUnitario}|{materialPrecioso.TipoMaterial}|{materialPrecioso.Peso}");
                }
            }
        }


    }
}


