using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class UsuarioCliente : Usuario {

        // PROPIEDADES
        protected List<Producto> Listaproductos;
        private List<UsuarioCliente> ListaUsuariosCliente;
        InterfazUsuario InterfazUsuario = new InterfazUsuario();

        // CONTRUCTORES
        public UsuarioCliente() { }
        public UsuarioCliente(int id, string nickname, string nombre, string ape1, string ape2, string password, MaquinaExpendedora maquina)
            : base(id, nickname, nombre, ape1, ape2, password, false, maquina) { }

        // METODOS
        public override void Menu() {
            int opcion;
            do {
                Console.WriteLine("1. Comprar Listaproductos. ");
                Console.WriteLine("2. Mostrar todos los Listaproductos de la maquina.  ");
                Console.WriteLine("3. Salir de la maquina.  ");
                Console.WriteLine("Seleccione una opcion: ");
                opcion = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (opcion) {
                    case 1:
                        Console.Write("Introduce el ID del contenido: ");
                        int idProductoAComprar;
                        try {
                            idProductoAComprar = int.Parse(Console.ReadLine());
                            Producto producto = null;
                            foreach (Producto p in Listaproductos) {
                                if (p.Id == idProductoAComprar) {
                                    producto = p;
                                    break;
                                }
                            }
                            if (producto != null) {
                                producto.ComprarProducto(idProductoAComprar);
                            }
                            else {
                                Console.WriteLine("El producto seleccionado no esta en stock.");
                            }
                        }
                        catch (FormatException) {
                            Console.WriteLine("ID de producto invalido. Introduce un nuevo ID.");
                        }
                        break;
                    case 2:
                        InterfazUsuario.MostrarProductos();
                        break;
                    case 3:
                        InterfazUsuario.Salir();
                        break;
                    default:
                        Console.WriteLine("Intenta de nuevo.");
                        break;
                }
            } while (opcion != 3);
        }

        public override void MostrarProductos() {
            if (Listaproductos.Count == 0) {
                Console.WriteLine("no hay productos disponibles en nuestra maquina expendedora. ");
                return;
            }
            foreach (Producto p in Listaproductos) {
                p.MostrarInformacion();
                Console.WriteLine();
            }
        }

        public override void Salir() {
            string filePath = "usuarios_cliente.txt";
            try {
                using (StreamWriter sw = new StreamWriter(filePath)) {
                    foreach (UsuarioCliente cliente in ListaUsuariosCliente) {
                        sw.WriteLine(cliente.Id);
                    }
                    sw.Close();
                }
                Console.WriteLine("Los IDs de los usuarios cliente han sido guardados en el archivo.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al guardar los IDs de los usuarios cliente: {ex.Message}");
            }
        }
    }
}
