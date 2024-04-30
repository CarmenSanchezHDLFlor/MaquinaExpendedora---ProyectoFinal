using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class GestorCompra {

        // CONSTRUCTORES

        /// <summary>
        /// Contructor por defecto de GestorCompra
        /// </summary>
        public GestorCompra() { }

        // MÉTODOS 

        /// <summary>
        /// Método público para seleccionar el método de pago (efectivo o tarjeta)
        /// </summary>
        /// <param name="total"></param>
        public void SeleccionarMetodoPago(double total) {
            Console.WriteLine($" Total a pagar: Euros {total}");
            Console.WriteLine(" Seleccione el metodo de pago:");
            Console.WriteLine(" 1. Efectivo");
            Console.WriteLine(" 2. Tarjeta");

            try {
                int opcionPago = int.Parse(Console.ReadLine());

                if (opcionPago != 1 && opcionPago != 2) {
                    Console.WriteLine(" Opcion invalida. Intente de nuevo.");
                    return;
                }

                switch (opcionPago) {
                    case 1:
                        PagoEfectivo(total); 
                        break;
                    case 2:
                        PagoTarjeta(total);
                        break;
                }

            }
            catch (FormatException) {
                Console.WriteLine(" Opcion invalida. Intente de nuevo.");
            }
            catch (Exception e) {
                Console.WriteLine($" Error: {e.Message}");
            }
        }

        /// <summary>
        /// Método público con parametro 'double' para realizar el PagoEfectivo de la compra de un producto 
        /// </summary>
        /// <param name="total"></param>
        public void PagoEfectivo(double total) {
            double cantidadPagada = 0;
            Console.WriteLine($" Total a pagar: Euros {total}");

            while (cantidadPagada < total) {
                Console.WriteLine($" Introduzca la cantidad en efectivo (Total restante: Euros {total - cantidadPagada}):");

                try {
                    double moneda = double.Parse(Console.ReadLine());

                    if (moneda <= 0) {
                        Console.WriteLine(" Cantidad invalida. Intente de nuevo.");
                        continue;
                    }

                    cantidadPagada += moneda;

                }
                catch (FormatException) {
                    Console.WriteLine(" Cantidad invalida. Intente de nuevo.");
                }
                catch (Exception e) {
                    Console.WriteLine($" Error: {e.Message}");
                }
            }

            double cambio = cantidadPagada - total;
            Console.WriteLine($" Cambio: ${cambio}");
        }

        /// <summary>
        /// Método público con parametro 'double' para realizar el PagoTarjeta de la compra de un producto 
        /// Validamos el numero de tarjeta, la fecha de caducidad y el CVV
        /// </summary>
        /// <param name="total"></param>
        public void PagoTarjeta(double total) {
            Console.WriteLine(" Ingrese los detalles de la tarjeta:");

            // Validar número de tarjeta
            string numeroTarjeta;
            while (true) {
                Console.Write(" Numero de tarjeta (16 digitos): ");
                numeroTarjeta = Console.ReadLine();

                try {
                    long.Parse(numeroTarjeta);  // Usamos long para evitar desbordamiento
                    if (numeroTarjeta.Length != 16) {
                        throw new FormatException();
                    }
                    break;
                }
                catch (FormatException) {
                    Console.WriteLine(" Numero de tarjeta invalido. Intente de nuevo.");
                }
                catch (Exception e) {
                    Console.WriteLine($" Error: {e.Message}");
                }
            }

            // Validar fecha de caducidad
            string fechaCaducidad;
            while (true) {
                Console.Write(" Fecha de caducidad (MM/YY): ");
                fechaCaducidad = Console.ReadLine();

                try {
                    DateTime.ParseExact(fechaCaducidad, "MM/yy", null);
                    break;
                }
                catch (FormatException) {
                    Console.WriteLine(" Fecha de caducidad invalida. Intente de nuevo.");
                }
                catch (Exception e) {
                    Console.WriteLine($" Error: {e.Message}");
                }
            }

            // Validar CVV
            string cvv;
            while (true) {
                Console.Write(" CVV (3 digitos): ");
                cvv = Console.ReadLine();

                try {
                    int.Parse(cvv);
                    if (cvv.Length != 3) {
                        throw new FormatException();
                    }
                    break;
                }
                catch (FormatException) {
                    Console.WriteLine(" CVV invalido. Intente de nuevo.");
                }
                catch (Exception e) {
                    Console.WriteLine($" Error: {e.Message}");
                }
            }

            Console.WriteLine(" Procesando pago...");
            Console.WriteLine($" Pago de ${total} realizado con exito.");
        }

    }
 } 
