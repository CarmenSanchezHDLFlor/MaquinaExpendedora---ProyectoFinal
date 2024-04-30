using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachinePropia;
using static MaquinaExpendedora_ProyectoFinal.Producto;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class InterfazUsuario {

        // PROPIEDADES 
        /// <summary>
        /// Propiedad privada de MaquinaExpendedora 
        /// </summary>

        private MaquinaExpendedora Maquina;

        /// <summary>
        /// Propiedad privada de Usuario
        /// </summary>
        private Usuario Usuario { get; set; }

        /// <summary>
        /// Propiedad público de List<Producto> ListaProductos inicializada con 12 productos 
        /// </summary>
        public List<Producto> ListaProductos { get; set; } = new List<Producto>(12);

        // PROPIEDAD PARA LA GESTION DE COMPRA
        /// <summary>
        /// Propiedad privada de GestorCompra 
        /// </summary>
        private GestorCompra GestorCompra { get; set; }

        // PROPIEDAD PARA SALIR DE LA APLICACIÓN 
        /// <summary>
        /// Propiedad privada para salir de la aplicacion
        /// Empleada en el metodo Salir()
        /// </summary>

        private bool Continuar = true;

        // CONTRUCTORES

        /// <summary>
        /// Constructor por defecto de InterfazUsuario
        /// </summary>
        public InterfazUsuario() { }

        // CONTRUCTOR PARAMETRIZADO
        /// <summary>
        /// Constructor parametrizado con MaquinaExpendedora y Usuario de InterfazUsuario 
        /// Inicializamos el usuario con el actual, maquina, ListaProductos y GestorCompra
        /// </summary>
        /// <param name="maquina"></param>
        /// <param name="usuario"></param>
        public InterfazUsuario(MaquinaExpendedora maquina, Usuario usuario) {
            Maquina = maquina;
            this.Usuario = usuario;
            ListaProductos = new List<Producto>();
            GestorCompra = new GestorCompra();
        }

        // METODOS 

        /// <summary>
        /// Método público para mostrar los productos 
        /// </summary>
        public void MostrarProductos() {
            Console.WriteLine("----- PRODUCTOS DISPONIBLES -----");
            foreach (Producto producto in ListaProductos) {
                producto.MostrarInformacion();
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------");
        }

        /// <summary>
        /// Método público parametrizado 'int' y 'int' 
        /// Método ComprarProducto, comprueba si eres cliente o admin, gestiona como está la lista de productos, 
        /// gestiona también el método de pago
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cantidad"></param>
        public void ComprarProducto(int id, int cantidad) {
            if (Usuario.Tipo != Usuario.TipoUsuario.Cliente) {
                Console.WriteLine("Acceso denegado. Solo los clientes pueden comprar productos.");
                return;
            }

            Producto producto = ListaProductos.FirstOrDefault(p => p.Id == id && !p.EstaVendido());

            if (producto == null) {
                Console.WriteLine("Producto no encontrado o ya vendido.");
                return;
            }

            // Verificar si el producto no es null antes de acceder a sus propiedades
            if (producto != null && producto.Unidades < cantidad) {
                Console.WriteLine("No hay suficientes unidades disponibles.");
                return;
            }

            double total = producto.PrecioUnitario * cantidad;
            producto.MarcarComoVendido();
            producto.Unidades -= cantidad;

            // Usar el GestorCompra inicializado para gestionar el pago
            if (GestorCompra != null) {
                GestorCompra.SeleccionarMetodoPago(total);
                Console.WriteLine($"Producto(s) comprado(s) con exito: {producto.Nombre} x {cantidad}");
            }
            else {
                Console.WriteLine("Error: El producto no ha sido comprado con exito.");
            }
        }

        /// <summary>
        /// Método público para cargar individualmente los productos de la maquina 
        /// Hace una diferencia de productos ya sean materiales preociosos, productos alimenticios o productos electronicos 
        /// Y los añade a la lista 
        /// </summary>
        /// <param name="producto"></param>
        public bool AgregarProducto(Producto producto, int tipoProducto) {
            if (ListaProductos.Count >= 12) {
                Console.WriteLine("La máquina expendedora está llena. No se pueden agregar más productos.");
                return false;
            }
            switch (tipoProducto) {
                case 1:
                    if (producto is MaterialesPreciosos) {
                        ListaProductos.Add(producto);
                        Console.WriteLine($"Producto {producto.Nombre} de tipo Materiales Preciosos agregado correctamente.");
                        return true;
                    } else {
                        Console.WriteLine("Tipo de producto no coincidente.");
                        return false;
                    }
                case 2:
                    if (producto is ProductosAlimenticios) {
                        
                        ListaProductos.Add(producto);
                        Console.WriteLine($"Producto {producto.Nombre} de tipo Productos Alimenticios agregado correctamente.");
                        return true;
                    } else {
                        Console.WriteLine("Tipo de producto no coincidente.");
                        return false;
                    }
                case 3:
                    if (producto is ProductosElectronicos) {
                        ListaProductos.Add(producto);
                        Console.WriteLine($"Producto {producto.Nombre} de tipo Productos Electronicos agregado correctamente.");
                        return true;
                    } else {
                        Console.WriteLine("Tipo de producto no coincidente.");
                        return false;
                    }
                default:
                    Console.WriteLine("Opcion invalida.");
                    return false;
            }
        }

        /// <summary>
        /// Método público para buscar el producto por el ID
        /// Recorre la lista en busca del ID seleccionado 
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns>producto</returns>
        public Producto BuscarProductoPorId(int idProducto) {
            foreach (Producto producto in ListaProductos) {
                if (producto.Id == idProducto) {
                    return producto;
                }
            }
            return null; // Si no se encuentra el producto con el ID dado
        }

        /// <summary>
        /// Método público para salir de la maquina ADMIN Y CLIENTE 
        /// Empleamos la propiedad creada anteriormente de 'Continuar'
        /// </summary>
        public void Salir() {
            Console.WriteLine("Saliendo de la aplicacion...");
            Continuar = false;
        }

        /// <summary>
        /// Método público para eliminar un producto de nuestra máquina expendedora 
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns>true si elimina dicho producto y false sino es asi</returns>
        public bool EliminarProducto(int idProducto) {
            Producto productoAEliminar = BuscarProductoPorId(idProducto);

            if (productoAEliminar != null) {
                ListaProductos.Remove(productoAEliminar);
                Console.WriteLine($"Producto con ID {idProducto} ha sido eliminado correctamente.");
                return true;
            }
            else {
                Console.WriteLine("Producto no encontrado.");
                return false;
            }
        }

        public void EliminarProducto(string nombreProducto) {
            Producto productoAEliminar = ListaProductos.FirstOrDefault(p => p.Nombre.Equals(nombreProducto));
            if (productoAEliminar != null) {
                ListaProductos.Remove(productoAEliminar);
                Console.WriteLine($"Producto '{nombreProducto}' eliminado correctamente.");
            }
            else {
                Console.WriteLine($"El producto '{nombreProducto}' no existe en la máquina expendedora.");
            }
        }
    }
}
