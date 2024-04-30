using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachinePropia;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class Program {

        private static string claveSecreta = "POOC#";

        private static bool ValidarClaveSecreta(string clave) {
            return clave == claveSecreta;
        }

        static void Main(string[] args) {

            // Ruta del archivo CSV
            string filePath = "productos.csv";

            // Lista para almacenar los productos leídos del archivo
            List<Producto> listaProductos = new List<Producto>();

            Usuario usuario = new Usuario();
            MaquinaExpendedora maquina = new MaquinaExpendedora(usuario);
            InterfazUsuario interfazUsuario = new InterfazUsuario(maquina, usuario);
            bool stop = false;

            do {
                do {

                    Console.WriteLine();
                    Console.WriteLine("███████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
                    Console.WriteLine("█▄─▀█▀─▄██─▄─██─▄▄▄─█▄─██─▄█▄─▄█▄─▀█▄─▄██─▄─████▄─▄▄─█▄─▀─▄█▄─▄▄─█▄─▄▄─█▄─▀█▄─▄█▄─▄▄▀█▄─▄▄─█▄─▄▄▀█─▄▄─█▄─▄▄▀██─▄─██");
                    Console.WriteLine("██─█▄█─███─▀─██─██▀▄██─██─███─███─█▄▀─███─▀─█████─▄█▀██▀─▀███─▄▄▄██─▄█▀██─█▄▀─███─██─██─▄█▀██─██─█─██─██─▄─▄██─▀─██");
                    Console.WriteLine("█▄▄▄█▄▄▄█▄▄█▄▄█▄▄▄█▄██▄▄▄▄██▄▄▄█▄▄▄██▄▄█▄▄█▄▄███▄▄▄▄▄█▄▄█▄▄█▄▄▄███▄▄▄▄▄█▄▄▄██▄▄█▄▄▄▄██▄▄▄▄▄█▄▄▄▄██▄▄▄▄█▄▄█▄▄█▄▄█▄▄█");
                    Console.WriteLine("▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
                    Console.WriteLine();


                    // Preguntar al usuario el tipo (Admin o Cliente)
                    Console.WriteLine("¿Eres un administrador? (S/N)");
                    string respuesta = Console.ReadLine().ToLower();

                    if (respuesta == "s") {
                        usuario.Tipo = Usuario.TipoUsuario.Admin;
                    }
                    else {
                        usuario.Tipo = Usuario.TipoUsuario.Cliente;
                    }

                    while (true) {
                        Console.WriteLine("----- MAQUINA EXPENDEDORA -----");
                        Console.WriteLine("Elige una opcion: ");
                        Console.WriteLine("1. Comprar productos");
                        Console.WriteLine("2. Mostrar informacion del producto");
                        Console.WriteLine("3. Salir");

                        // Si el usuario es un administrador, mostrar las opciones adicionales
                        if (usuario.Tipo == Usuario.TipoUsuario.Admin) {
                            Console.WriteLine("4. Carga individual de productos");
                            Console.WriteLine("5. Carga completa de productos desde archivo");
                            Console.WriteLine("6. Eliminar producto de la maquina ");
                        }

                        int opcion = int.Parse(Console.ReadLine());

                        switch (opcion) {
                            case 1:
                                // Código para comprar productos
                                interfazUsuario.MostrarProductos();

                                int idProductoCompra;
                                int cantidadCompra;

                                try {
                                    Console.Write("Introduce el ID del producto que quieres comprar: ");
                                    idProductoCompra = int.Parse(Console.ReadLine());

                                    Console.Write("Introduce la cantidad que deseas: ");
                                    cantidadCompra = int.Parse(Console.ReadLine());

                                    interfazUsuario.ComprarProducto(idProductoCompra, cantidadCompra);
                                }
                                catch (FormatException) {
                                    Console.WriteLine("Entrada invalida. Introduce un numero valido.");
                                }
                                break;
                            case 2:
                                // Código para mostrar información del producto
                                Console.WriteLine("Introduce el ID del producto: ");
                                string input = Console.ReadLine();
                                int idProducto;

                                try {

                                    // Código para mostrar información del producto
                                    idProducto = int.Parse(input);
                                    Console.WriteLine("Introduce el ID del producto: ");

                                    try {
                                        idProducto = int.Parse(input);

                                        // Buscar el producto por su ID
                                        Producto producto = interfazUsuario.BuscarProductoPorId(idProducto);

                                        if (producto != null) { // Mostrar información del producto
                                            producto.MostrarInformacion();
                                        }
                                        else {
                                            // Mostrar mensaje de que la información no está disponible
                                            Console.WriteLine("La informacion del producto no esta disponible.");
                                        }
                                    }
                                    catch (FormatException) {
                                        Console.WriteLine("Introduce un ID valido.");
                                    }
                                    break;

                                }
                                catch (FormatException) {
                                    Console.WriteLine("Introduce un ID valido.");
                                }
                                break;
                            case 3:
                                interfazUsuario.Salir();
                                break;
                            case 4:
                                // Código para cargar productos individualmente
                                if (usuario.Tipo == Usuario.TipoUsuario.Admin) {
                                    Console.WriteLine();
                                    Console.Write(" Ingrese la contraseña de administrador: ");
                                    StringBuilder contraseñaIngresada = new StringBuilder();
                                    ConsoleKeyInfo tecla;
                                    do {
                                        tecla = Console.ReadKey(true); // Lee la tecla sin mostrarla en la consola
                                        if (tecla.Key != ConsoleKey.Enter) {
                                            if (tecla.Key == ConsoleKey.Backspace && contraseñaIngresada.Length > 0) {
                                                Console.Write("\b \b"); // Borra el último carácter mostrado
                                                contraseñaIngresada.Remove(contraseñaIngresada.Length - 1, 1); // Elimina el último carácter de la contraseña ingresada
                                            }
                                            else {
                                                contraseñaIngresada.Append(tecla.KeyChar);
                                                Console.Write("*"); // Muestra un asterisco en lugar del carácter real
                                            }
                                        }
                                    } while (tecla.Key != ConsoleKey.Enter);

                                    if (!ValidarClaveSecreta(contraseñaIngresada.ToString())) {
                                        Console.WriteLine("\nClave incorrecta. Acceso denegado.");
                                        break;
                                    }
                                    Console.WriteLine();

                                    // Código para cargar productos individualmente
                                    try {
                                        Console.WriteLine("Introduce los detalles del producto:");
                                        Console.Write("Nombre: ");
                                        string nombre = Console.ReadLine();
                                        Console.Write("Unidades: ");
                                        int unidades = int.Parse(Console.ReadLine());
                                        Console.Write("Precio unitario: ");
                                        double precioUnitario = double.Parse(Console.ReadLine());
                                        Console.Write("Descripción: ");
                                        string descripcion = Console.ReadLine();
                                        int id;
                                        do {
                                            Console.WriteLine("ID: ");
                                            id = int.Parse(Console.ReadLine());
                                            if (MaquinaExpendedora.ListaProductos.Any(p => p.Id == id)) {
                                                Console.WriteLine($"Ya existe un producto con el ID {id}. Introduce un ID unico.");
                                            }
                                        } while (MaquinaExpendedora.ListaProductos.Any(p => p.Id == id));

                                        // Crear una instancia del tipo correcto de producto
                                        Producto nuevoProducto = null;

                                        Console.WriteLine("Selecciona el tipo de producto:");
                                        Console.WriteLine("1. Materiales Preciosos");
                                        Console.WriteLine("2. Productos Alimenticios");
                                        Console.WriteLine("3. Productos Electronicos");
                                        int tipo = int.Parse(Console.ReadLine());

                                        switch (tipo) {
                                            case 1:
                                                Console.Write("Tipo de material: ");
                                                string tipoMaterial = Console.ReadLine();
                                                Console.Write("Peso (en gramos): ");
                                                double peso = double.Parse(Console.ReadLine());
                                                nuevoProducto = new MaterialesPreciosos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterial, peso);
                                                break;
                                            case 2:
                                                Console.Write("Informacion nutricional: ");
                                                string informacionNutricional = Console.ReadLine();
                                                nuevoProducto = new ProductosAlimenticios(id, nombre, unidades, precioUnitario, descripcion, informacionNutricional);
                                                break;
                                            case 3:
                                                Console.Write("Tipo de material: ");
                                                string tipoMaterialE = Console.ReadLine();
                                                Console.Write("¿Tiene bateria? (Si/No): ");
                                                bool tieneBateria = Console.ReadLine().ToLower() == "si";
                                                Console.Write("¿Precargado? (Si/No): ");
                                                bool precargado = Console.ReadLine().ToLower() == "si";
                                                nuevoProducto = new ProductosElectronicos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterialE, tieneBateria, precargado);
                                                break;
                                            default:
                                                Console.WriteLine("Opcion invalida.");
                                                break;
                                        }

                                        interfazUsuario.AgregarProducto(nuevoProducto, tipo);
                                        maquina.GuardarProductoEnCSV(nuevoProducto, "productos.csv");
                                    }
                                    catch (FormatException) {
                                        Console.WriteLine("Entrada invalida. Introduce un formato valido.");
                                    }
                                }
                                else {
                                    Console.WriteLine("Opcion invalida para usuarios cliente.");
                                }
                                break;
                            case 5:
                                // Código para cargar productos desde un archivo
                                if (usuario.Tipo == Usuario.TipoUsuario.Admin) {
                                    try {
                                        //Console.Write("Introduce la ruta del archivo de texto: ");
                                        //string rutaArchivo = Console.ReadLine();

                                        // Llama al método para cargar productos desde el archivo CSV
                                        maquina.CargarProductosDesdeCSV(filePath);

                                        Console.WriteLine("Productos guardados exitosamente.");

                                    }
                                    catch (Exception e) {
                                        Console.WriteLine($"Error: {e.Message}");
                                    }
                                }
                                else {
                                    Console.WriteLine("Opcion invalida para usuarios cliente.");
                                }
                                break;
                            case 6:
                                // Código para eliminar producto de la máquina
                                Console.WriteLine("Introduce el ID del producto que deseas eliminar: ");
                                int idProductoEliminar = int.Parse(Console.ReadLine());
                                interfazUsuario.EliminarProducto(idProductoEliminar);
                                break;
                            default:
                                Console.WriteLine("Opcion invalida. Por favor, selecciona una opcion valida.");
                                break;
                        }
                        // Si se selecciona la opción de stop, te permite cambiar el usuario
                        if (opcion == 3) {
                            break;
                        }
                    }

                    // Si el usuario es administrador, preguntar si desea salir o seguir siendo cliente
                    if (usuario.Tipo == Usuario.TipoUsuario.Admin) {
                        Console.WriteLine("¿Desea salir de la aplicacion? (S/N)");
                        string respuesta2 = Console.ReadLine().ToLower();
                        if (respuesta2 == "s") {
                            stop = true;
                            break;
                        }
                    }
                    else {
                        stop = true;
                    }

                } while (!stop); // Continuar preguntando hasta que se decida stop de la aplicación

                // Preguntar si se desea reiniciar la aplicación
                Console.WriteLine("¿Desea reiniciar la aplicacion? (S/N)");
            } while (Console.ReadLine().ToLower() == "s"); // Si se ingresa "s", reiniciar la aplicación
        }
    }
 }
