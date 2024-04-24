using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class Program {
        static void Main(string[] args) {

            int opcion = 0;
            
            Console.WriteLine();
            Console.WriteLine("███████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine("█▄─▀█▀─▄██─▄─██─▄▄▄─█▄─██─▄█▄─▄█▄─▀█▄─▄██─▄─████▄─▄▄─█▄─▀─▄█▄─▄▄─█▄─▄▄─█▄─▀█▄─▄█▄─▄▄▀█▄─▄▄─█▄─▄▄▀█─▄▄─█▄─▄▄▀██─▄─██");
            Console.WriteLine("██─█▄█─███─▀─██─██▀▄██─██─███─███─█▄▀─███─▀─█████─▄█▀██▀─▀███─▄▄▄██─▄█▀██─█▄▀─███─██─██─▄█▀██─██─█─██─██─▄─▄██─▀─██");
            Console.WriteLine("█▄▄▄█▄▄▄█▄▄█▄▄█▄▄▄█▄██▄▄▄▄██▄▄▄█▄▄▄██▄▄█▄▄█▄▄███▄▄▄▄▄█▄▄█▄▄█▄▄▄███▄▄▄▄▄█▄▄▄██▄▄█▄▄▄▄██▄▄▄▄▄█▄▄▄▄██▄▄▄▄█▄▄█▄▄█▄▄█▄▄█");
            Console.WriteLine("▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
            Console.WriteLine();

            Console.WriteLine(" ###################################");
            Console.WriteLine(" ##  1.Comprar Productos          ##");
            Console.WriteLine(" ##  2.Información de Productos   ##");
            Console.WriteLine(" ##  3.Salir                      ##");
            Console.WriteLine(" ##  4.Opciones Admin             ##");
            Console.WriteLine(" ###################################");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion) {
                case 1:
                    string otro = "S";

                    do {
                        Console.WriteLine("Introduce la Id del producto: ");
                        int idProducto_compra = int.Parse(Console.ReadLine());
                        InterfazUsuario productoElegido_compra = new InterfazUsuario();
                        Producto elegido_compra = productoElegido_compra.ElegirProducto(idProducto_compra);

                        if (elegido_compra != null) {
                            Console.WriteLine($"Precio: {elegido_compra.PrecioUnitario}");
                            InterfazUsuario compra = new InterfazUsuario();
                            compra.ComprarProducto(idProducto_compra);
                        }
                        else {
                            Console.WriteLine("Producto no encontrado");
                        }

                        Console.Write("Quieres elegir otro producto? (S/N)");
                        otro = Console.ReadLine().ToUpper();
                    } while (otro != "N");
                    break;

                case 2:
                    Console.WriteLine("Introduce la Id del producto: ");
                    int idProducto_info = int.Parse(Console.ReadLine());
                    InterfazUsuario productoElegido_info = new InterfazUsuario();
                    Producto elegido_info = productoElegido_info.ElegirProducto(idProducto_info);

                    if (elegido_info != null) {
                        elegido_info.MostrarInformacion();
                    }
                    break;
                case 3:
                    InterfazUsuario noAdmin = new InterfazUsuario();
                    noAdmin.Salir(false);
                    break;
                case 4:
                    int opcionAdmin = 0;
                    Console.WriteLine("1.Cargar Producto");
                    Console.WriteLine("2.Cargar lista de Productos");
                    Console.WriteLine("3.Salir");
                    opcionAdmin = int.Parse(Console.ReadLine());

                    switch (opcionAdmin) {
                        case 1:
                            Usuario u = new Usuario();
                            Console.WriteLine("Introduce la Id del producto: ");
                            int idProducto_cargar = int.Parse(Console.ReadLine());
                            InterfazUsuario productoElegido_cargar = new InterfazUsuario();
                            Producto elegido_cargar = productoElegido_cargar.ElegirProducto(idProducto_cargar);
                            break;
                        case 2:
                            Console.WriteLine("TODOS LOS PRODUCTOS:");
                            InterfazUsuario TodosProductos = new InterfazUsuario();
                            Producto p = new Producto();
                            p.MostrarInformacion();
                            TodosProductos.CargaTodosLosProductos();
                            break;
                        case 3:
                            InterfazUsuario serAdmin = new InterfazUsuario();
                            serAdmin.Salir(true);
                            break;
                    }
                    break;
            }
        }
    }
}

