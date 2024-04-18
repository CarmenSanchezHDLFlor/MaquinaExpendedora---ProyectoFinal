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

namespace MaquinaExpendedora_ProyectoFinal {
    internal class Program {
        static void Main(string[] args) {

            Producto p = new Producto();
            GestorCompra g = new GestorCompra();
            InterfazUsuario usuario = new InterfazUsuario();

            
            int opcion = 0;
            //hay un método de menú, ver como ajustar
            Console.WriteLine("1.Comprar Productos");
            Console.WriteLine("2.Información de Productos");
            Console.WriteLine("3.Cargar Producto");
            Console.WriteLine("4.Cargar lista de Productos");
            Console.WriteLine("5.Salir");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion) {
                case 1: //No está hecho exactamente igual a como se pide en el diagrama de flujo del guión
                    string otro="S";
                    
                    do
                    {                 
                        Console.WriteLine("Introduce la Id del producto: ");
                        int idProducto_compra=int.Parse(Console.ReadLine());
                        InterfazUsuario productoElegido_compra= new InterfazUsuario();
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

                        Console.Write("Quieres elegir otro producto? (S/N)");
                        otro = Console.ReadLine().ToUpper();
                    } while (otro != "N");
                    
                    break;
                case 2:
                    Console.WriteLine("Introduce la Id del producto: ");
                    int idProducto_info = int.Parse(Console.ReadLine());
                    InterfazUsuario productoElegido_info = new InterfazUsuario();
                    Producto elegido_info = productoElegido_info.ElegirProducto(idProducto_info);

                    if (elegido_info != null)
                    {
                        if (elegido_info is ProductosAlimenticios)
                        {
                            elegido_info.MostrarInformacion();
                        }
                        else if(elegido_info is ProductosElectronicos)
                        {
                            elegido_info.MostrarInformacion();
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine("Introduce la Id del producto: ");
                    int idProducto_cargar = int.Parse(Console.ReadLine());
                    InterfazUsuario productoElegido_cargar = new InterfazUsuario();
                    Producto elegido_cargar = productoElegido_cargar.ElegirProducto(idProducto_cargar);

                    elegido_cargar.CargaIndividualProductos();
                    break;
                case 4:
                    Console.WriteLine("TODOS LOS PRODUCTOS:");
                    InterfazUsuario TodosProductos= new InterfazUsuario();
                    TodosProductos.CargarTodosLosProductos();
                    break;
                case 5: //¿por qué se pregunta si es admin al salir?
                    string respuesta = "N";
                    Console.WriteLine("Eres Admin (S/N): ");
                    respuesta = Console.ReadLine().ToUpper();
                    bool admin = false;
                    if(respuesta != "N")
                    {
                        admin = true;
                        InterfazUsuario serAdmin = new InterfazUsuario();
                        serAdmin.Salir(admin);

                    }
                    else
                    {
                        admin = false;
                        InterfazUsuario serAdmin = new InterfazUsuario();
                        serAdmin.Salir(admin);
                    }

                    break;
            }
        }

    }
}
