using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class InterfazUsuario {

        // PROPIEDADES 
        private MaquinaExpendedora Maquina;
        private Usuario Usuario;
        public List<Producto> ListaProductos { get; private set; }

        private GestorCompra GestorCompra { get; set; }

        // PROPIEDAD PARA ADMIN
        private List<Usuario> ListaUsuariosAdmin = new List<Usuario>();

        // PROPIEDADES PARA CLIENTE
        private List<Usuario> ListaUsuariosCliente = new List<Usuario>();

        // CONTRUCTORES
        public InterfazUsuario() { }

        // CONTRUCTOR PARAMETRIZADO
        public InterfazUsuario(MaquinaExpendedora maquina, Usuario usuario) {
            Maquina = maquina;
            Usuario = usuario;
            ListaProductos = Maquina.ListaProductos;
        }

        // CONTRUCTOR PARAMETRIZADO 2
        public InterfazUsuario(MaquinaExpendedora maquina, Usuario usuario, List<Producto> listaProductos) {
            Maquina = maquina;
            Usuario = usuario;
            ListaProductos = listaProductos;
            GestorCompra = new GestorCompra(ListaProductos);
        }

        // METODOS 
        public Producto ElegirProducto(int idSeleccionado) { 
            if (ListaProductos.Count == 0) {
                Console.WriteLine("No hay productos disponibles en nuestra maquina expendedora.");
                return null;
            }
        
            MostrarProductos(Usuario.EsAdmin);
        
            Console.Write("Seleccione el ID del producto: ");
            
        
            try {
                idSeleccionado = int.Parse(Console.ReadLine());
            }
            catch (FormatException) {
                Console.WriteLine("Por favor, ingrese un numero valido.");
                return null;  // Retorna null en caso de error
            }
        
            Producto productoSeleccionado = ListaProductos.Find(p => p.Id == idSeleccionado);
            if (productoSeleccionado == null) {
                Console.WriteLine("Producto no encontrado.");
                return null;
            }
        
            return productoSeleccionado;
        }

        // metodo para mostrar los productos 
        public void MostrarProductos(bool esAdmin) {
            if (ListaProductos.Count == 0) {
                Console.WriteLine(esAdmin ? "No hay productos disponibles en nuestra maquina expendedora." : "No hay productos disponibles para su cuenta de cliente.");
                return;
            }

            foreach (Producto p in ListaProductos) {
                p.MostrarInformacion();
                Console.WriteLine();
            }
        }

        // metodo para comprar producto
        public void ComprarProducto(int idProducto) {
            Producto producto = ElegirProducto(idProducto);
            foreach (Producto p in ListaProductos) {
                if (p.Id == idProducto) {
                    producto = p;
                    break;
                }
            }

            if (producto == null) {
                Console.WriteLine("No tenemos ese producto en nuestra maquina.");
                return;
            }

            if (producto.Unidades == 0) {
                Console.WriteLine("No tenemos el producto en stock.");
                return;
            }

            //Console.WriteLine($"Has comprado {producto.Nombre} por {producto.PrecioUnitario}.");
            //producto.Unidades--;

            Console.WriteLine($"Has seleccionado {producto.Nombre}.");
            Console.WriteLine($"Precio Unitario: {producto.PrecioUnitario}");

            Console.WriteLine("Selecciona el metodo de pago:");
            Console.WriteLine("1. Pago en efectivo");
            Console.WriteLine("2. Pago con tarjeta");
            int opcionPago = int.Parse(Console.ReadLine());

            switch (opcionPago) {
                case 1:
                    PagoEfectivo(producto.PrecioUnitario, producto.Id);
                    break;
                case 2:
                    PagoTarjeta(producto.PrecioUnitario, producto.Id);
                    break;
                default:
                    Console.WriteLine("Opcion de pago invalida.");
                    break;
            }
        }

        // metodo aux para realizar el pago efectivo de la compra
        private void PagoEfectivo(double precio, int idProducto) {
            GestorCompra.PagoEfectivo(precio, idProducto);
        }

        // metodo aux para realizar el pago con tarjeta de la compra
        private void PagoTarjeta(double precio, int idProducto) {
            GestorCompra.PagoTarjeta(precio, idProducto);
        }

        // metodo para cargar individualmente los productos de la maquina 
        public void CargaIndividualProductos() {
            // Si es un administrador...
            if (Usuario.EsAdmin) {
                Console.WriteLine("Selecciona una opcion:");
                Console.WriteLine("1. Añadir existencias a un producto existente.");
                Console.WriteLine("2. Añadir un nuevo producto.");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion) {
                    case 1:
                        int idProductoExistente;
                        string inputIdProducto = Console.ReadLine();
                        try {
                            idProductoExistente = int.Parse(inputIdProducto);
                        }
                        catch (FormatException) {
                            Console.WriteLine("ID de producto invalido.");
                            return;
                        }

                        // Buscar el producto por su ID
                        Producto productoExistente = ListaProductos.Find(p => p.Id == idProductoExistente);

                        if (productoExistente == null) {
                            Console.WriteLine("No se ha encontrado ningun producto con el ID introducido.");
                            return;
                        }

                        // Solicitar la cantidad de unidades a añadir
                        Console.Write("Ingrese la cantidad de unidades a añadir: ");
                        string inputUnidadesAAnadir = Console.ReadLine();
                        int unidadesAAnadir;

                        try {
                            unidadesAAnadir = int.Parse(inputUnidadesAAnadir);
                        }
                        catch (FormatException) {
                            Console.WriteLine("Cantidad invalida.");
                            return;
                        }

                        // Añadir las unidades al producto existente
                        productoExistente.Unidades += unidadesAAnadir;
                        Console.WriteLine($"Se han añadido {unidadesAAnadir} unidades al producto '{productoExistente.Nombre}'.");
                        break;

                    case 2:
                        // Añadir un nuevo producto
                        Console.WriteLine("Ingrese los detalles del nuevo producto:");
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();

                        int unidades;
                        while (true) {
                            Console.Write("Unidades: ");
                            try {
                                unidades = int.Parse(Console.ReadLine());
                                break;
                            }
                            catch (FormatException) {
                                Console.WriteLine("Cantidad invalida. Por favor, ingrese un numero entero.");
                            }
                        }
                        double precioUnitario;
                        while (true) {
                            Console.Write("Precio Unitario: ");
                            try {
                                precioUnitario = double.Parse(Console.ReadLine());
                                break;
                            }
                            catch (FormatException) {
                                Console.WriteLine("Precio invalido. Por favor, ingrese un numero decimal.");
                            }
                        }
                        Console.Write("Descripcion: ");
                        string descripcion = Console.ReadLine();

                        // Generar un nuevo ID para el producto
                        int nuevoId = ListaProductos.Any() ? ListaProductos.Max(p => p.Id) + 1 : 1;

                        // Crear el nuevo producto y agregarlo a la lista de productos
                        Producto nuevoProducto = new Producto(nombre, unidades, precioUnitario, descripcion) { Id = nuevoId };
                        ListaProductos.Add(nuevoProducto);
                        Console.WriteLine("Nuevo producto ha sido agregado correctamente.");
                        break;

                    default:
                        Console.WriteLine("Opcion invalida");
                        break;
                }
            }
            else {
                Console.WriteLine("No tiene permisos de administrador para realizar esta accion.");
            }
        }

        // metood para cargar todos los productos de la maquina 
        public void CargarTodosLosProductos() {
            if (File.Exists("productos.txt")) {
                try {
                    using (StreamReader sr = new StreamReader("productos.txt")) {
                        string linea;
                        while ((linea = sr.ReadLine()) != null) {
                            string[] datosProducto = linea.Split(';'); // Suponiendo que los datos estn separados por punto y coma (;)
                            if (datosProducto.Length == 6) {
                                string nombre = datosProducto[0];
                                int unidades;
                                double precioUnitario;
                                string descripcion;

                                try {
                                    unidades = int.Parse(datosProducto[1]);
                                }
                                catch (FormatException) {
                                    Console.WriteLine("Error en el formato de datos del archivo. El número de unidades no es válido.");
                                    continue;
                                }

                                try {
                                    precioUnitario = double.Parse(datosProducto[2]);
                                }
                                catch (FormatException) {
                                    Console.WriteLine("Error en el formato de datos del archivo. El precio unitario no es válido.");
                                    continue;
                                }

                                descripcion = datosProducto[3];
                                // Crear un nuevo producto y agregarlo a la lista de productos
                                Producto nuevoProducto = new Producto(nombre, unidades, precioUnitario, descripcion);
                                ListaProductos.Add(nuevoProducto);
                            }
                            else {
                                Console.WriteLine("Error en el formato de datos del archivo. No se pudo cargar el producto.");
                            }
                        }
                        Console.WriteLine("Carga de productos completada correctamente.");
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error al cargar los productos desde el archivo: {ex.Message}");
                }
            }
            else {
                Console.WriteLine("El archivo 'productos.txt' no existe.");
            }
        }

        // metodo para salir de la maquina ADMIN Y CLIENTE 
        public void Salir(bool esAdmin)
        {
            if (esAdmin)
            {
                string filePath = "usuarios_admin.txt";
                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (Usuario a in ListaUsuariosAdmin)
                        {
                            sw.WriteLine(a.Nombre);
                        }
                    }
                    Console.WriteLine("Los IDs de los usuarios admin han sido guardados en el archivo.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar los IDs de los usuarios admin: {ex.Message}");
                }
            }
            else
            {
                string filePath = "usuarios_cliente.txt";
                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (Usuario cliente in ListaUsuariosCliente)
                        {
                            sw.WriteLine(cliente.Nombre);
                        }
                    }
                    Console.WriteLine("Los IDs de los usuarios cliente han sido guardados en el archivo.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar los IDs de los usuarios cliente: {ex.Message}");
                }
            }
        }
    }
}

