using MaquinaExpendedora_ProyectoFinal;
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
        // Método para iniciar el proceso de compra
        public void IniciarCompra(double precio, int idProducto) {
            SeleccionarMetodoPago(precio, idProducto);
        }

        // Método para seleccionar el método de pago (efectivo o tarjeta)
        private void SeleccionarMetodoPago(double precio, int idProducto) {
            Console.WriteLine("Selecciona el metodo de pago:");
            Console.WriteLine("1. Pago en efectivo");
            Console.WriteLine("2. Pago con tarjeta");
            int opcionPago = int.Parse(Console.ReadLine());

            switch (opcionPago) {
                case 1:
                    PagoEfectivo(precio, idProducto); // modo de pago en efectivo 
                    break;
                case 2:
                    PagoTarjeta(precio, idProducto); // modo de pago con tarjeta 
                    break;
                default:
                    Console.WriteLine("Opcion de pago invalido.");
                    break;
            }
        }

        // metodo para leer la cantidad de dinero introducido 
        private double LeerCantidad(string mensaje) { 
            Console.WriteLine(mensaje); // indicación para el usuario sobre que cantidad debe ingresar
            double cantidad; // variable para almacenar el valor ingresado 
            while (true) {
                try {
                    cantidad = double.Parse(Console.ReadLine());
                    break;  // Salir del bucle si la conversión es exitosa
                }
                catch (FormatException) {
                    Console.WriteLine("Cantidad invalida. Por favor, ingrese un numero.");
                    Console.WriteLine(mensaje);
                }
            }
            return cantidad; // devuelve la cantidad ingresada correcta 
        }

        // metodo de pago en efectivo 
        public void PagoEfectivo(double precio, int idProducto) {
            double cantidadPagada = LeerCantidad($"Inserte ${precio} en efectivo:");
            // llama al método LeerCantidad para solicitar al usuario que ingrese la cantidad de dinero en efectivo que desea pagar

            if (cantidadPagada < precio) { // se verifica si la cantidad pagada por el usuario es menor que el precio del producto
                Console.WriteLine("La cantidad introducida no es suficiente. El procedimiento ha sido cancelado.");
                // si es así, se imprime un mensaje indicando que la cantidad introducida no es suficiente
                return;
            }

            double cambio = CalcularCambio(cantidadPagada, precio); // llamada al método CalcularCambio para determinar cuánto cambio se debe devolver al usuario
            MostrarCambio(cambio); // llamada al método MostrarCambio para mostrar el cambio que se debe devolver al usuario
            ProcesarPago(precio, idProducto); // llamada al ProcesarPago para completar el pago y realizar cualquier otra acción necesaria
        }

        // Método para calcular el cambio
        private double CalcularCambio(double cantidadPagada, double precio) {
            //  calcula el cambio restando el precio del producto de la cantidad pagada 
            return cantidadPagada - precio; // devuelve la diferencia, que es el cambio que se debe devolver al cliente
        }

        // Método para mostrar el cambio devuelto al cliente
        private void MostrarCambio(double cambio) {
            Console.WriteLine($"Gracias por su compra. Su cambio es ${cambio}.");
            // imprime un mensaje agradeciendo al cliente por su compra y mostrando el cambio total que se le devuelve

            int[] especificacionesMonedas = { 25, 10, 5, 1 }; // variable 'array' de enteros que representa las denominaciones de las monedas

            Console.WriteLine("Su cambio incluye:");
            int cambioCentimos = Convert.ToInt32(cambio * 100);  // convierte el cambio total a centavos para facilitar el cálculo de las monedas
            foreach (int monedaEspecifica in especificacionesMonedas) { // por cada moneda en el grupo de monedas especificas... 
                int cantidadMonedas = cambioCentimos / monedaEspecifica; // calcula cuántas monedas de una denominación específica pueden formarse con el cambio restante
                if (cantidadMonedas > 0) { // si la cantidad de monedas es mayor que 0,...
                    Console.WriteLine($"{cantidadMonedas} monedas de {monedaEspecifica} centimos"); // imprime un mensaje indicando la cantidad de monedas de esa especificacion
                    cambioCentimos %= monedaEspecifica; // se actualiza el cambio restante utilizando el operador %=
                }
            }
        }

        // Método para realizar el pago con tarjeta
        public void PagoTarjeta(double precio, int idProducto) { 
            Console.WriteLine("Pago con tarjeta seleccionado."); // el usuario ha seleccionado el método de pago con tarjeta

            Console.WriteLine("Ingrese el numero de tarjeta:"); // solicita al usuario que ingrese el número de su tarjeta
            string numeroTarjeta = Console.ReadLine();

            Console.WriteLine("Ingrese la fecha de vencimiento (MM/YY):"); // solicita al usuario que ingrese la fecha de vencimiento de su tarjeta
            string fechaVencimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el codigo de seguridad:"); // solicita al usuario que ingrese el código de seguridad de su tarjeta 
            string codigoSeguridad = Console.ReadLine();

            Console.WriteLine("¡Pago con tarjeta completado!"); // informa al usuario que el proceso de pago con tarjeta ha sido completado

            ProcesarPago(precio, idProducto); // llamada al método ProcesarPago para completar el pago y realizar cualquier otra acción necesaria
        }

        // Método para procesar el pago y actualizar el stock de productos
        private void ProcesarPago(double precio, int idProducto) {
            Producto productoVendido = ListaProductos.Find(p => p.Id == idProducto); 
            // busca el producto en la lista ListaProductos que coincida con el idProducto proporcionado
            if (productoVendido != null) { // verifica si se encontró un producto con el idProducto proporcionado
                productoVendido.Vendido = true; // Vendido se coloca a true, indicando que el producto ha sido vendido
                productoVendido.Unidades--; // decrementa el número de unidades disponibles del producto en uno, indicando que una unidad ha sido vendida
                RegistrarVenta(productoVendido, precio); // llamada al método RegistrarVenta para registrar la venta del producto con su precio
                Console.WriteLine("Compra completada."); // mensaje indicando que la compra ha sido completada 
            }
            else { // Se ejecuta si no se encuentra el producto con el idProducto proporcionado
                Console.WriteLine("Error: No hay stock."); // mensaje indicando que no hay productos 
            }
        }

        // Método para registrar la venta de un producto
        private void RegistrarVenta(Producto producto, double precio) {
            Console.WriteLine($"Venta registrada: {producto.Nombre} - ${precio}");
        }

    }
 } 
