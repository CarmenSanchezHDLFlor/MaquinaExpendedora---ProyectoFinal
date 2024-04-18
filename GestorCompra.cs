﻿using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class GestorCompra {

        // PROPIEDADES 
        private List<Producto> ListaProductos;

        // CONSTRUCTORES
        public GestorCompra() { }

        // CONTRUCTOR PARAMETRIZADO
        public GestorCompra(List<Producto> productos) {
            ListaProductos = productos;
        }

        // METODOS
        public void IniciarCompra(double precio, int idProducto) {
            SeleccionarMetodoPago(precio, idProducto);
        }

        private void SeleccionarMetodoPago(double precio, int idProducto) {
            Console.WriteLine("Selecciona el metodo de pago:");
            Console.WriteLine("1. Pago en efectivo");
            Console.WriteLine("2. Pago con tarjeta");
            int opcionPago = int.Parse(Console.ReadLine());

            switch (opcionPago) {
                case 1:
                    PagoEfectivo(precio, idProducto);
                    break;
                case 2:
                    PagoTarjeta(precio, idProducto);
                    break;
                default:
                    Console.WriteLine("Opcion de pago invalido.");
                    break;
            }
        }

        public void PagoEfectivo(double precio, int idProducto) {
            Console.WriteLine("Pago en efectivo seleccionado.");

            double cantidadPagada = PedirCantidadPagada(precio);

            if (cantidadPagada < precio) {
                Console.WriteLine("La cantidad introducida no es suficiente. El procedimiento ha sido cancelado.");
                return;
            }

            double cambio = CalcularCambio(cantidadPagada, precio);
            MostrarCambio(cambio);
            ProcesarPago(precio, idProducto);
        }

        private double PedirCantidadPagada(double precio) {
            Console.WriteLine($"Inserte ${precio} en efectivo:");
            return double.Parse(Console.ReadLine());
        }

        private double CalcularCambio(double cantidadPagada, double precio) {
            return cantidadPagada - precio;
        }

        private void MostrarCambio(double cambio) {
            Console.WriteLine($"Gracias por su compra. Su cambio es ${cambio}.");

            int[] especificacionesMonedas = { 25, 10, 5, 1 };

            Console.WriteLine("Su cambio incluye:");
            int cambioCentimos = Convert.ToInt32(cambio * 100); 
            foreach (int monedaEspecifica in especificacionesMonedas) {
                int cantidadMonedas = cambioCentimos / monedaEspecifica;
                if (cantidadMonedas > 0) {
                    Console.WriteLine($"{cantidadMonedas} monedas de {monedaEspecifica} centimos");
                    cambioCentimos %= monedaEspecifica;
                }
            }
        }

        public void PagoTarjeta(double precio, int idProducto) { 
            Console.WriteLine("Pago con tarjeta seleccionado.");

            Console.WriteLine("Pago con tarjeta seleccionado.");

            Console.WriteLine("Ingrese el número de tarjeta:");
            string numeroTarjeta = Console.ReadLine();

            Console.WriteLine("Ingrese la fecha de vencimiento (MM/YY):");
            string fechaVencimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el código de seguridad:");
            string codigoSeguridad = Console.ReadLine();

            Console.WriteLine("¡Pago con tarjeta completado!");

            ProcesarPago(precio, idProducto);
        }


        private void ProcesarPago(double precio, int idProducto) {
            Producto productoVendido = ListaProductos.Find(p => p.Id == idProducto);
            if (productoVendido != null) {
                productoVendido.Vendido = true;
                productoVendido.Unidades--;
                RegistrarVenta(productoVendido, precio);
                Console.WriteLine("Compra completada.");
            }
            else {
                Console.WriteLine("Error: No hay stock.");
            }
        }

        private void RegistrarVenta(Producto producto, double precio) {
            Console.WriteLine($"Venta registrada: {producto.Nombre} - ${precio}");
        }

    }
 } 
