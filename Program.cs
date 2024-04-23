/* AUTORES: CARMEN SÁNCHEZ HERNÁNDEZ 
 *          MARIO MARTINEZ LOZANO 
 *          ALBA GARCÍA RIVAS
 * 
 * PROYECTO FINAL PROGRAMACIÓN ORIENTADA A OBJETOS 
 *  
 */

using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Producto p = new Producto();
            GestorCompra g = new GestorCompra();
            InterfazUsuario usuario = new InterfazUsuario();

            int opcion = 0; // Variable para almacenar la opción seleccionada por el usuario

            // Bucle principal del programa, se ejecuta hasta que el usuario selecciona la opción para salir
            do
            {

                Console.WriteLine();
                Console.WriteLine("███████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
                Console.WriteLine("█▄─▀█▀─▄██▀▄─██─▄▄▄─█▄─██─▄█▄─▄█▄─▀█▄─▄██─▄─████▄─▄▄─█▄─▀─▄█▄─▄▄─█▄─▄▄─█▄─▀█▄─▄█▄─▄▄▀█▄─▄▄─█▄─▄▄▀█─▄▄─█▄─▄▄▀██▀▄─██");
                Console.WriteLine("██─█▄█─███─▀─██─██▀▄██─██─███─███─█▄▀─███─▀─█████─▄█▀██▀─▀███─▄▄▄██─▄█▀██─█▄▀─███─██─██─▄█▀██─██─█─██─██─▄─▄██─▀─██");
                Console.WriteLine("█▄▄▄█▄▄▄█▄▄█▄▄█▄▄▄█▄██▄▄▄▄██▄▄▄█▄▄▄██▄▄█▄▄█▄▄███▄▄▄▄▄█▄▄█▄▄█▄▄▄███▄▄▄▄▄█▄▄▄██▄▄█▄▄▄▄██▄▄▄▄▄█▄▄▄▄██▄▄▄▄█▄▄█▄▄█▄▄█▄▄█");

                Console.WriteLine();

                Console.WriteLine("1. Comprar Productos");
                Console.WriteLine("2. Mostrar Información de Productos");
                Console.WriteLine("3. Cargar Productos Individuales");
                Console.WriteLine("4. Cargar Todos los Productos");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion) // Evaluar la opción seleccionada
                {
                    case 1: // Opción para comprar productos
                        string otro = "S";

                        do
                        {
                            Console.WriteLine("Introduce la Id del producto: ");
                            int idProducto_compra = int.Parse(Console.ReadLine());
                            InterfazUsuario productoElegido_compra = new InterfazUsuario();
                            Producto elegido_compra = productoElegido_compra.ElegirProducto(idProducto_compra);

                            if (elegido_compra != null)
                            {
                                Console.WriteLine($"Precio: {elegido_compra.PrecioUnitario}");
                                InterfazUsuario compra = new InterfazUsuario();
                                compra.ComprarProducto(idProducto_compra);
                            }
                            else
                            {
                                Console.WriteLine("Producto no encontrado");
                            }

                            Console.Write("¿Quieres elegir otro producto? (S/N)");
                            otro = Console.ReadLine().ToUpper();
                        } while (otro != "N");

                        break;

                    case 2: // Opción para mostrar información detallada de un producto
                        Console.WriteLine("Introduce la Id del producto: ");
                        int idProducto_info = int.Parse(Console.ReadLine());
                        InterfazUsuario productoElegido_info = new InterfazUsuario();
                        Producto elegido_info = productoElegido_info.ElegirProducto(idProducto_info);

                        if (elegido_info != null)
                        {
                            elegido_info.MostrarInformacion();
                        }
                        else
                        {
                            Console.WriteLine("Producto no encontrado");
                        }
                        break;

                    case 3: // Opción para cargar productos individuales en la máquina expendedora
                        Console.WriteLine("Introduce la Id del producto: ");
                        int idProducto_cargar = int.Parse(Console.ReadLine());
                        InterfazUsuario productoElegido_cargar = new InterfazUsuario();
                        Producto elegido_cargar = productoElegido_cargar.ElegirProducto(idProducto_cargar);

                        if (elegido_cargar != null)
                        {
                            elegido_cargar.CargaIndividualProductos();
                        }
                        else
                        {
                            Console.WriteLine("Producto no encontrado");
                        }
                        break;

                    case 4: // Opción para cargar todos los productos en la máquina expendedora
                        Console.WriteLine("Cargando todos los productos...");
                        InterfazUsuario cargarProductos = new InterfazUsuario();
                        cargarProductos.CargarTodosLosProductos();
                        break;

                    case 5: // Opción para salir del programa
                        Console.WriteLine("Saliendo...");
                        break;

                    default: // Opción por defecto
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
                Console.WriteLine();
            } while (opcion != 5);
        }
    }
}

