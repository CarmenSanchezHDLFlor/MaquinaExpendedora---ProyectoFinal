using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaquinaExpendedora_ProyectoFinal.Producto;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class InterfazUsuario {

        // PROPIEDADES 
        private MaquinaExpendedora Maquina;
        private Usuario Usuario;
        public List<Producto> ListaProductos { get; set; } = new List<Producto>();

        // PROPIEDAD PARA LA GESTION DE COMPRA
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
        // método para elegir un producto de la maquina 
        public Producto ElegirProducto(int idSeleccionado) {
            CargaIndividualProductos(); // llamada para cargar individualmente los productos 
            MostrarProductos(); // llamada para mostrar los productos 

            Console.WriteLine($"Seleccionando producto con ID: {idSeleccionado}");
            // mensaje indicando que se esta seleccionando un producto con el ID proporcionado

            Producto productoSeleccionado = ListaProductos.Find(p => p.Id == idSeleccionado);
            // un producto en una ListaProductos que tenga el mismo ID que el proporcionado

            if (productoSeleccionado == null) { // Se verifica si el producto seleccionado es nulo, si es asi... 
                Console.WriteLine("Producto no encontrado."); // mensaje indicando que no se ha encontrado el producto 
                return null;
            }

            return productoSeleccionado; // devuelve el producto seleccionado
        }

        // metodo para mostrar los productos 
        public void MostrarProductos() {
            foreach (Producto p in ListaProductos) { // Para cada producto en la lista,... 
                p.MostrarInformacion(); // muestra la información del producto
                Console.WriteLine();
            }
        }

        // metodo para comprar producto
        public void ComprarProducto(int idProducto) {
            Producto producto = ElegirProducto(idProducto); // llamada al metodo de ElegirProducto para seleccionar un producto basado en el ID 
            foreach (Producto p in ListaProductos) { // para cada producto de la ListaProductos
                if (p.Id == idProducto) { // si el ID proporcionado es igual al idProducto...
                    producto = p; // asigna ese producto a la variable producto
                    break; // se rompe el bucle 
                }
            }

            if (producto == null) { // se verifica si el producto seleccionado es nulo
                Console.WriteLine("No tenemos ese producto en nuestra maquina.");
                return;
            }

            if (producto.Unidades == 0) { // se verifica si el producto está agotado 
                Console.WriteLine("No tenemos el producto en stock.");
                return;
            }

            Console.WriteLine($"Has seleccionado {producto.Nombre}.");
            Console.WriteLine($"Precio Unitario: {producto.PrecioUnitario}");

            // le pide al usuario que seleccione un método de pago: 
            Console.WriteLine("Selecciona el metodo de pago:");
            Console.WriteLine("1. Pago en efectivo");
            Console.WriteLine("2. Pago con tarjeta");
            int opcionPago = int.Parse(Console.ReadLine());

            switch (opcionPago) {
                case 1: // pago en efectivo
                    PagoEfectivo(producto.PrecioUnitario, producto.Id);
                    break;
                case 2: // pago mediante tarjeta 
                    PagoTarjeta(producto.PrecioUnitario, producto.Id);
                    break;
                default:
                    Console.WriteLine("Opcion de pago invalida.");
                    break;
            }
        }

        // metodo aux para realizar el pago efectivo de la compra
        private void PagoEfectivo(double precio, int idProducto) {
            GestorCompra.PagoEfectivo(precio, idProducto); // llama al método PagoEfectivo del GestorCompra, pasándole el precio y el ID del producto como argumentos
        }

        // metodo aux para realizar el pago con tarjeta de la compra
        private void PagoTarjeta(double precio, int idProducto) {
            GestorCompra.PagoTarjeta(precio, idProducto); // llama al método PagoTarjeta del GestorCompra, pasándole el precio y el ID del producto como argumentos
        }

        // metodo para cargar individualmente los productos de la maquina 
        public bool CargaIndividualProductos() {
            bool productosCargados = false;

            if (File.Exists("productos.txt")) {
                StreamReader sr = new StreamReader("productos.txt");
                string linea;

                while ((linea = sr.ReadLine()) != null) {
                    productosCargados = true;
                    string[] datos = linea.Split('|');

                    int id = int.Parse(datos[0]);
                    string nombre = datos[1];
                    string tipoString = datos[2];
                    double precioUnitario = double.Parse(datos[5]);
                    string descripcion = datos[3];
                    int unidades = int.Parse(datos[4]);

                    switch (tipoString) {
                        case "Productos Alimenticios":
                            string informacionNutricional = datos[6];
                            ProductosAlimenticios productoA = new ProductosAlimenticios(id, nombre, unidades, precioUnitario, descripcion, informacionNutricional);
                            ListaProductos.Add(productoA);
                            break;
                        case "Productos Electrónicos":
                            string tipoMaterial = datos[7];
                            bool tieneBateria = bool.Parse(datos[8]);
                            bool precargado = bool.Parse(datos[9]);
                            ProductosElectronicos productoE = new ProductosElectronicos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterial, tieneBateria, precargado);
                            ListaProductos.Add(productoE);
                            break;
                        case "Materiales Preciosos":
                            // 13|Diamante|Materiales Preciosos|Gemas|1|3000.00|Diamante|0.5
                            string tipoMaterialM = datos[2];
                            double peso = double.Parse(datos[7]);
                            MaterialesPreciosos productoM = new MaterialesPreciosos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterialM, peso);
                            ListaProductos.Add(productoM);
                            break;
                    }


                    
                }
                sr.Close();
            }
            else {
                Console.WriteLine("El archivo 'productos.txt' no existe.");
            }

            return productosCargados;
        }

        public bool CargaTodosLosProductos() {
            bool productosCargados = false;

            if (File.Exists("productos.txt")) {
                StreamReader sr = new StreamReader("productos.txt");
                string linea;

                while ((linea = sr.ReadLine()) != null) {
                    productosCargados = true;
                    string[] datos = linea.Split('|');

                    int id;
                    string nombre;
                    string descripcion;
                    int unidades;
                    double precioUnitario;

                    try {
                        id = int.Parse(datos[0]);
                        nombre = datos[1];
                        string tipoProducto = datos[2];
                        descripcion = datos[3];
                        unidades = int.Parse(datos[4]);
                        precioUnitario = double.Parse(datos[5]);

                        switch (tipoProducto) {
                            case "Productos Alimenticios":
                                if (datos.Length >= 7) {
                                    string informacionNutricional = datos[6];
                                    ProductosAlimenticios productoA = new ProductosAlimenticios(id, nombre, unidades, precioUnitario, descripcion, informacionNutricional);
                                    ListaProductos.Add(productoA);
                                }
                                else {
                                    Console.WriteLine($"Error: Informacion incompleta para el producto alimenticio con ID {id}.");
                                }
                                break;
                            case "Productos Electrónicos":
                                if (datos.Length >= 9) {
                                    string tipoMaterial = datos[3];
                                    try {
                                        bool tieneBateria = bool.Parse(datos[6]);
                                        bool precargado = bool.Parse(datos[7]);

                                        ProductosElectronicos productoE = new ProductosElectronicos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterial, tieneBateria, precargado);
                                        ListaProductos.Add(productoE);
                                    }
                                    catch (FormatException) {
                                        Console.WriteLine($"Error: Formato incorrecto para los campos de productos electronicos con ID {id}.");
                                    }
                                }
                                else {
                                    Console.WriteLine($"Error: Informacion incompleta para el producto electronico con ID {id}.");
                                }
                                break;
                            case "Materiales Preciosos":
                                string tipoMaterialM = datos[3];
                                if (datos.Length >= 7) {
                                    try {
                                        double peso = double.Parse(datos[6]);
                                        MaterialesPreciosos productoM = new MaterialesPreciosos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterialM, peso);
                                        ListaProductos.Add(productoM);
                                    }
                                    catch (FormatException) {
                                        Console.WriteLine($"Error: Formato incorrecto para el peso del material precioso con ID {id}.");
                                    }
                                }
                                else {
                                    Console.WriteLine($"Error: Informacion incompleta para el material precioso con ID {id}.");
                                }
                                break;
                            default:
                                Console.WriteLine($"Error: Tipo de producto no reconocido para el producto con ID {id}.");
                                break;
                        }
                    }
                    catch (FormatException) {
                        Console.WriteLine($"Error: Formato incorrecto en la linea {linea}");
                        continue;
                    }
                }

                sr.Close();
                Console.WriteLine("Productos cargados exitosamente.");
            }
            else {
                Console.WriteLine("El archivo 'productos.txt' no existe.");
            }

            return productosCargados;
        }

        // metodo para salir de la maquina ADMIN Y CLIENTE 
        public void Salir(bool esAdmin) {
            if (esAdmin) { // si eres administrador... 
                string filePath = "usuarios_admin.txt"; // nombre del archivo donde se guardarsn los nombres de los usuarios administradores
                try {
                    using (StreamWriter sw = new StreamWriter(filePath)) { //  bloque using para garantizar que el recurso StreamWriter se libere correctamente después de su uso
                        foreach (Usuario a in ListaUsuariosAdmin) { // recorre la lista ListaUsuariosAdmin que contiene los usuarios administradores
                            sw.WriteLine(a.Nombre); // escribe el nombre de cada usuario administrador en el archivo de texto
                        }
                        sw.Close(); // cierra el archivo
                    }
                    Console.WriteLine("Los IDs de los usuarios admin han sido guardados en el archivo.");
                }
                catch (Exception ex) { // Captura cualquier excepción que pueda ocurrir durante la escritura en el archivo para los usuarios administradores
                    Console.WriteLine($"Error al guardar los IDs de los usuarios admin: {ex.Message}");
                }
            }
            else { // si eres cliente... 
                string filePath = "usuarios_cliente.txt"; // nombre del archivo donde se guardarán los nombres de los usuarios clientes
                try {
                    using (StreamWriter sw = new StreamWriter(filePath)) { //  bloque using para garantizar que el recurso StreamWriter se libere correctamente después de su uso
                        foreach (Usuario cliente in ListaUsuariosCliente) { // Recorre la lista ListaUsuariosCliente que contiene los usuarios cliente
                            sw.WriteLine(cliente.Nombre); // escribe el nombre de cada usuario cliente en el archivo de texto
                        }
                        sw.Close(); // cierra el archivo
                    }
                    Console.WriteLine("Los IDs de los usuarios cliente han sido guardados en el archivo.");
                }
                catch (Exception ex) { // Captura cualquier excepción que pueda ocurrir durante la escritura en el archivo para los usuarios clientes
                    Console.WriteLine($"Error al guardar los IDs de los usuarios cliente: {ex.Message}");
                }
            }
        }
    }
}
