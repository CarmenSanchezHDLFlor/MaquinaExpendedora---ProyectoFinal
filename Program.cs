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
                    Console.WriteLine("▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
                    Console.WriteLine();

                    // Preguntar al usuario el tipo (Admin o Cliente)
                    Console.Write(" ¿Eres un administrador? (S/N): ");
                    string respuesta = Console.ReadLine().ToLower();

                    if (respuesta == "s") {
                        usuario.Tipo = Usuario.TipoUsuario.Admin;
                        // Solicitar contraseña solo si el usuario es administrador
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
                    }
                    else {
                        usuario.Tipo = Usuario.TipoUsuario.Cliente;
                    }

                    while (true) {

                        Console.WriteLine();
                        Console.WriteLine(" ###################################");
                        Console.WriteLine(" ##          C L I E N T E        ##");
                        Console.WriteLine(" ###################################");
                        Console.WriteLine(" ##  1.Comprar Productos          ##");
                        Console.WriteLine(" ##  2.Información de Productos   ##");
                        Console.WriteLine(" ##  3.Salir                      ##");
                        Console.WriteLine(" ###################################");


                        // Si el usuario es un administrador, mostrar las opciones adicionales
                        if (usuario.Tipo == Usuario.TipoUsuario.Admin) {

                            Console.WriteLine(" ##   A D M I N I S T R A D O R   ##");
                            Console.WriteLine(" ###################################");
                            Console.WriteLine(" ## 4. Cargar producto            ##");
                            Console.WriteLine(" ## 5. Cargar lista de productos  ##");
                            Console.WriteLine(" ## 6. Eliminar producto          ##");
                            Console.WriteLine(" ###################################");
                        }

                        Console.WriteLine();
                        Console.Write(" Selecciona una opcion: ");
                        int opcion = int.Parse(Console.ReadLine());

                        switch (opcion) {
                            case 1:
                                // Código para comprar productos
                                interfazUsuario.MostrarProductos();

                                int idProductoCompra;
                                int cantidadCompra;

                                try {
                                    Console.Write(" Introduce el ID del producto que quieres comprar: ");
                                    idProductoCompra = int.Parse(Console.ReadLine());

                                    Console.Write(" Introduce la cantidad que deseas: ");
                                    cantidadCompra = int.Parse(Console.ReadLine());

                                    interfazUsuario.ComprarProducto(idProductoCompra, cantidadCompra);
                                }
                                catch (FormatException) {
                                    Console.WriteLine(" Entrada invalida. Introduce un numero valido.");
                                }
                                break;
                            case 2:
                                // Código para mostrar información del producto
                                // Código para mostrar información de todos los productos
                                Console.WriteLine(" Información de todos los productos:");
                                foreach (Producto producto in interfazUsuario.ListaProductos) {
                                    producto.MostrarInformacion();
                                    Console.WriteLine();
                                }
                                break;
                            case 3:
                                interfazUsuario.Salir();
                                break;
                            case 4:
                                // Código para cargar productos individualmente
                                if (usuario.Tipo == Usuario.TipoUsuario.Admin) {
                                    try {
                                        Console.WriteLine(" Introduce los detalles del producto:");
                                        Console.Write(" Nombre: ");
                                        string nombre = Console.ReadLine();
                                        Console.Write(" Unidades: ");
                                        int unidades = int.Parse(Console.ReadLine());
                                        Console.Write(" Precio unitario: ");
                                        double precioUnitario = double.Parse(Console.ReadLine());
                                        Console.Write(" Descripción: ");
                                        string descripcion = Console.ReadLine();
                                        int id;
                                        do {
                                            Console.Write(" ID: ");
                                            id = int.Parse(Console.ReadLine());
                                            if (MaquinaExpendedora.ListaProductos.Any(p => p.Id == id)) {
                                                Console.WriteLine($" Ya existe un producto con el ID {id}. Introduce un ID único.");
                                            }
                                        } while (MaquinaExpendedora.ListaProductos.Any(p => p.Id == id));

                                        // Crear una instancia del tipo correcto de producto
                                        Producto nuevoProducto = null;

                                        Console.WriteLine();
                                        Console.WriteLine(" ###################################");
                                        Console.WriteLine(" ##   A D M I N I S T R A D O R   ##");
                                        Console.WriteLine(" ###################################");
                                        Console.WriteLine(" ## 1. Material Precioso          ##");
                                        Console.WriteLine(" ## 2. Producto Alimenticio       ##");
                                        Console.WriteLine(" ## 3. Producto Electrónico       ##");
                                        Console.WriteLine(" ###################################");
                                        Console.WriteLine();
                                        Console.Write(" Seleccione el tipo de producto: ");

                                        int tipo = int.Parse(Console.ReadLine());

                                        switch (tipo) {
                                            case 1:
                                                Console.Write(" Tipo de material: ");
                                                string tipoMaterial = Console.ReadLine();
                                                Console.Write(" Peso (en gramos): ");
                                                double peso = double.Parse(Console.ReadLine());
                                                nuevoProducto = new MaterialesPreciosos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterial, peso);
                                                break;
                                            case 2:
                                                Console.Write(" Información nutricional: ");
                                                string informacionNutricional = Console.ReadLine();
                                                nuevoProducto = new ProductosAlimenticios(id, nombre, unidades, precioUnitario, descripcion, informacionNutricional);
                                                break;
                                            case 3:
                                                Console.Write(" Tipo de material: ");
                                                string tipoMaterialE = Console.ReadLine();
                                                Console.Write(" ¿Tiene batería? (Si/No): ");
                                                bool tieneBateria = Console.ReadLine().ToLower() == "si";
                                                Console.Write(" ¿Precargado? (Si/No): ");
                                                bool precargado = Console.ReadLine().ToLower() == "si";
                                                nuevoProducto = new ProductosElectronicos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterialE, tieneBateria, precargado);
                                                break;
                                            default:
                                                Console.WriteLine(" Opción inválida.");
                                                break;
                                        }

                                        interfazUsuario.AgregarProducto(nuevoProducto, tipo);
                                        maquina.GuardarProductoEnCSV(nuevoProducto, "productos.csv");
                                    }
                                    catch (FormatException) {
                                        Console.WriteLine(" Entrada inválida. Introduce un formato válido.");
                                    }
                                }
                                else {
                                    Console.WriteLine("\nClave incorrecta. Acceso denegado.");
                                }
                                break;
                            case 5:
                                // Código para cargar productos desde un archivo
                                if (usuario.Tipo == Usuario.TipoUsuario.Admin) {
                                    try {
                                        // Llama al método para cargar productos desde el archivo CSV
                                        maquina.CargarProductosDesdeCSV(filePath);
                                        Console.WriteLine(" Productos guardados exitosamente.");
                                    }
                                    catch (Exception e) {
                                        Console.WriteLine($" Error: {e.Message}");
                                    }
                                }
                                else {
                                    Console.WriteLine(" Opcion invalida para usuarios cliente.");
                                }
                                break;
                            case 6:
                                // Código para eliminar producto de la máquina
                                Console.WriteLine(" Introduce el ID del producto que deseas eliminar: ");
                                int idProductoEliminar = int.Parse(Console.ReadLine());
                                interfazUsuario.EliminarProducto(idProductoEliminar);
                                break;
                            default:
                                Console.WriteLine(" Opcion invalida. Por favor, selecciona una opcion valida.");
                                break;
                        }
                        // Si se selecciona la opción de stop, te permite cambiar el usuario
                        if (opcion == 3) {
                            break;
                        }
                    }

                    // Si el usuario es administrador, preguntar si desea salir o seguir siendo cliente
                    if (usuario.Tipo == Usuario.TipoUsuario.Admin) {
                        Console.WriteLine(" ¿Desea salir de la aplicacion? (S/N)");
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
                Console.WriteLine(" ¿Desea reiniciar la aplicacion? (S/N)");
            } while (Console.ReadLine().ToLower() == "s"); // Si se ingresa "s", reiniciar la aplicación
        }
    }
}
