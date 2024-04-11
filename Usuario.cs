using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora___ProyectoFinal {
    internal abstract class Usuario {

        // PROPIEDADES
        public string NickName { get; private set; }
        public string Nombre { get; private set; }
        public string Ape1 { get; private set; }
        public string Ape2 { get; private set; }
        private string Password { get; set; }

        // PROPIEDAD PARA SABER SI ES CLIENTE O ADMIN
        public bool EsAdmin { get; protected set; }

        // ATRIBUTO PARA LA MAQUINA EXPENDEDORA
        protected MaquinaExpendedora Maquina { get; set; }

        // PROPIEDADES PARA ADMIN 
        protected List<Producto> Listaproductos;
        InterfazUsuario InterfazUsuario = new InterfazUsuario();
        private List<Usuario> ListaUsuariosAdmin = new List<Usuario>();


        // PROPIEDADES PARA CLIENTE
        private List<Usuario> ListaUsuariosCliente = new List<Usuario>();

        // CONSTRUCTOR
        public Usuario() { }

        // CONTRUCTOR PARAMETRIZADO
        public Usuario(string nickName, string nombre, string ape1, string ape2, string password,
            bool esAdmin, MaquinaExpendedora maquina) {
            NickName = nickName;
            Nombre = nombre;
            Ape1 = ape1;
            Ape2 = ape2;
            Password = password;
            EsAdmin = esAdmin;
            Maquina = maquina;
        }

        // METODOS
        public string GetRealNombre() {
            return $"{Nombre} {Ape1} {Ape2}";
        }

        public bool Login(string nickname, string password) {
            return NickName == nickname && Password == password;
        }

        public void Menu(bool esAdmin) {
            if (esAdmin) {
                int opcion;
                do {
                    Console.WriteLine("1. Comprar productos. ");
                    Console.WriteLine("2. Cargar la máquina producto a producto.  ");
                    Console.WriteLine("3. Cargar la máquina al completo  ");
                    Console.WriteLine("4. Mostrar todos los productos de la máquina.  ");
                    Console.WriteLine("5. Salir. ");
                    Console.WriteLine("Seleccione una opción: ");
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
                                    Console.WriteLine("El producto seleccionado no está en stock.");
                                }
                            }
                            catch (FormatException) {
                                Console.WriteLine("ID de producto inválido. Introduce un nuevo ID");
                            }
                            break;
                        case 2:
                            InterfazUsuario.CargaIndividualProductos();
                            break;
                        case 3:
                            InterfazUsuario.CargarTodosLosProductos();
                            break;
                        case 4:
                            MostrarProductos(esAdmin);
                            break;
                        case 5:
                            InterfazUsuario.Salir();
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            Console.WriteLine("Intenta de nuevo.");
                            break;
                    }
                } while (opcion != 5);
            }
            else {
                int opcion;
                do {
                    Console.WriteLine("1. Comprar productos.");
                    Console.WriteLine("2. Mostrar todos los productos de la máquina.");
                    Console.WriteLine("3. Salir de la máquina.");
                    Console.WriteLine("Seleccione una opción: ");
                    opcion = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (opcion) {
                        case 1:
                            Console.Write("Introduce el ID del producto: ");
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
                                    Console.WriteLine("El producto seleccionado no está en stock.");
                                }
                            }
                            catch (FormatException) {
                                Console.WriteLine("ID de producto inválido. Introduce un nuevo ID.");
                            }
                            break;
                        case 2:
                            MostrarProductos(esAdmin);
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
        }

        public void Salir(bool esAdmin) {
            if (esAdmin) {
                string filePath = "usuarios_admin.txt";
                try {
                    using (StreamWriter sw = new StreamWriter(filePath)) {
                        foreach (Usuario a in ListaUsuariosAdmin) {
                            sw.WriteLine(a.Nombre);
                        }
                    }
                    Console.WriteLine("Los IDs de los usuarios admin han sido guardados en el archivo.");
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error al guardar los IDs de los usuarios admin: {ex.Message}");
                }
            }
            else {
                string filePath = "usuarios_cliente.txt";
                try {
                    using (StreamWriter sw = new StreamWriter(filePath)) {
                        foreach (Usuario cliente in ListaUsuariosCliente) {
                            sw.WriteLine(cliente.Nombre);
                        }
                    }
                    Console.WriteLine("Los IDs de los usuarios cliente han sido guardados en el archivo.");
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error al guardar los IDs de los usuarios cliente: {ex.Message}");
                }
            }
        }
        public void MostrarProductos(bool esAdmin) {
            if (esAdmin) {
                if (Listaproductos.Count == 0) {
                    Console.WriteLine("No hay productos disponibles en nuestra máquina expendedora.");
                    return;
                }
                foreach (Producto p in Listaproductos) {
                    p.MostrarInformacion();
                    Console.WriteLine();
                }
            }
            else {
                if (Listaproductos.Count == 0) {
                    Console.WriteLine("No hay productos disponibles para su cuenta de cliente.");
                    return;
                }
                foreach (Producto p in Listaproductos) {
                    p.MostrarInformacion();
                    Console.WriteLine();
                }
            }
        }

    }
}
