using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class Usuario {

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
        protected List<Producto> ListaProductos;
        protected InterfazUsuario InterfazUsuario { get; set; }

        // PROPIEDAD PARA EL ADMIN
        public string ClaveAdmin { get; private set; }

        // CONSTRUCTOR
        public Usuario() { }

        // CONTRUCTOR PARAMETRIZADO
        public Usuario(string nickName, string nombre, string ape1, string ape2, string password,
            bool esAdmin, MaquinaExpendedora maquina, string claveAdmin) {
            NickName = nickName;
            Nombre = nombre;
            Ape1 = ape1;
            Ape2 = ape2;
            Password = password;
            EsAdmin = esAdmin;
            Maquina = maquina;
            ClaveAdmin = claveAdmin;
            ListaProductos = new List<Producto>();
        }

        // METODOS
        public string GetRealNombre() {
            return $"{Nombre} {Ape1} {Ape2}";
        }

        // metodo para que el ADMIN pueda acceder 
        public bool Login(string nickname, string password) {
            return NickName == nickname && Password == password;
        }

        // metodo MENU para adminisitrar las acciones de ADMIN y CLIENTE
        public void Menu(bool esAdmin) {
            if (esAdmin) {
                Console.WriteLine("Ingrese la clave de administrador:");
                string claveIngresada = Console.ReadLine();

                if (claveIngresada == ClaveAdmin) {
                    int opcion;
                    do {
                        Console.WriteLine("1. Comprar productos. ");
                        Console.WriteLine("2. Cargar la maquina producto a producto.  ");
                        Console.WriteLine("3. Cargar la maquina al completo  ");
                        Console.WriteLine("4. Mostrar todos los productos de la maquina.  ");
                        Console.WriteLine("5. Salir. ");
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
                                    foreach (Producto p in ListaProductos) {
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
                                    Console.WriteLine("ID de producto invalido. Introduce un nuevo ID");
                                }
                                break;
                            case 2:
                                InterfazUsuario.CargaIndividualProductos();
                                break;
                            case 3:
                                InterfazUsuario.CargaCompletaProductos();
                                break;
                            case 4:
                                InterfazUsuario.MostrarProductos();
                                break;
                            case 5:
                                InterfazUsuario.Salir(esAdmin);
                                Console.WriteLine("Saliendo...");
                                break;
                            default:
                                Console.WriteLine("Intenta de nuevo.");
                                break;
                        }
                    } while (opcion != 5);
                }
                else {
                    Console.WriteLine("Clave incorrecta. Acceso denegado.");
                }
            }
            else {
                int opcion;
                do {
                    Console.WriteLine("1. Comprar productos.");
                    Console.WriteLine("2. Mostrar todos los productos de la maquina.");
                    Console.WriteLine("3. Salir de la maquina.");
                    Console.WriteLine("Seleccione una opcion: ");
                    opcion = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (opcion) {
                        case 1:
                            Console.Write("Introduce el ID del producto: ");
                            int idProductoAComprar;
                            try {
                                idProductoAComprar = int.Parse(Console.ReadLine());
                                Producto producto = null;
                                foreach (Producto p in ListaProductos) {
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
                            InterfazUsuario.Salir(!esAdmin);
                            break;
                        default:
                            Console.WriteLine("Intenta de nuevo.");
                            break;
                    }
                } while (opcion != 3);
            }
        }

    }
}