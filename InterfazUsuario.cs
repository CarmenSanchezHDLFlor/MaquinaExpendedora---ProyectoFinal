using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class InterfazUsuario {

        private MaquinaExpendedora Maquina;
        private Usuario Usuario;
        public List<Producto> Listaproductos { get; private set; }

        public InterfazUsuario() { }

        public InterfazUsuario(MaquinaExpendedora maquina, Usuario usuario) {
            Maquina = maquina;
            Usuario = usuario;
        }

        public void MostrarMenuPrincipal() {
            Console.WriteLine("--- Menu ---");
            if (Maquina.Usuario.EsAdmin) {
                Console.WriteLine("1. Carga individual la maquina con productos.");
                Console.WriteLine("2. Carga completa de los productos de la maquina.");
                Console.WriteLine("3. Mostrar todos los productos de la maquina.");
                Console.WriteLine("4. Salir.");
            }
            else {
                Console.WriteLine("1. Comprar productos.");
                Console.WriteLine("2. Mostrar los productos disponibles.");
                Console.WriteLine("3. Salir de la maquina.");
            }
        }

        // METODOS 
        public void MostrarProductos() {
            if (Listaproductos.Count == 0) {
                Console.WriteLine("no hay productos disponibles en nuestra maquina expendedora. ");
                return;
            }
            foreach (Producto p in Listaproductos) {
                p.MostrarInformacion();
                Console.WriteLine();
            }
        }

        public void ComprarProducto(int idProducto) {
            Producto producto = null;
            foreach (Producto p in Listaproductos) {
                if (p.Id == idProducto) {
                    producto = p;
                    break;
                }
            }
            if (producto == null) {
                Console.WriteLine("No tenemos ese producto en nuestra maquina.");
                return;
            }
            if (producto.Unidades == 0) {
                Console.WriteLine("No tenemos el producto en stock.");
                return;
            }
            Console.WriteLine($"Has comprado {producto.Nombre} por {producto.PrecioUnitario}.");
            producto.Unidades--;
        }

        public void CargaIndividualProductos() {
            
        }

        public void CargarTodosLosProductos() {
            try {
                Console.Write("Ingrese el nombre del archivo de carga de productos: ");
                string nombreArchivo = Console.ReadLine();

                using (StreamReader sr = new StreamReader(nombreArchivo)) {
                    string linea;
                    while ((linea = sr.ReadLine()) != null) {

                    }
                }

                Console.WriteLine("Carga de productos completada correctamente.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al cargar los productos desde el archivo: {ex.Message}");
            }
        }

        public void Salir() {
            string filePath = "productos.txt";
            try {
                using (StreamWriter sw = new StreamWriter(filePath)) {
                    foreach (Producto producto in Listaproductos) {
                        sw.WriteLine(producto.Nombre);
                    }
                }
                Console.WriteLine("Los productos han sido guardados en el archivo.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al guardar los productos: {ex.Message}");
            }
        }

    }
}

