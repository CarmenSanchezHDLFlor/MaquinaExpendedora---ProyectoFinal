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
        //public List<Producto> ListaProductos { get; private set; }
        public List<Producto> ListaProductos { get; set; } = new List<Producto>();


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
            CargaIndividualProductos();
            MostrarProductos();

            Console.WriteLine($"Seleccionando producto con ID: {idSeleccionado}");

            Producto productoSeleccionado = ListaProductos.Find(p => p.Id == idSeleccionado);

            if (productoSeleccionado == null) {
                Console.WriteLine("Producto no encontrado.");
                return null;
            }
        
            return productoSeleccionado;
        }

        // metodo para mostrar los productos 
        public void MostrarProductos() {
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
        public bool CargaIndividualProductos() {
            bool productosCargados = false;

            if (File.Exists("productos.txt")) {
                StreamReader sr = new StreamReader("productos.txt");
                string linea;

                while ((linea = sr.ReadLine()) != null) {
                    productosCargados = true;
                    string[] datos = linea.Split('|');

                    if (datos.Length >= 7) {
                        int id = int.Parse(datos[0]);
                        string nombre = datos[1];
                        string tipoString = datos[2];
                        double precioUnitario = double.Parse(datos[5]);
                        string descripcion = datos[3];
                        int unidades = int.Parse(datos[4]);

                        if (Enum.TryParse(tipoString, out TipoProducto tipoProducto)) {
                            switch (tipoProducto) {
                                case TipoProducto.ProductosAlimenticios:
                                    string informacionNutricional = datos[6];
                                    ProductosAlimenticios productoA = new ProductosAlimenticios(id, nombre, unidades, precioUnitario, descripcion, informacionNutricional);
                                    ListaProductos.Add(productoA);
                                    break;
                                case TipoProducto.ProductosElectronicos:
                                    string tipoMaterial = datos[6];
                                    bool tieneBateria = bool.Parse(datos[7]);
                                    bool precargado = bool.Parse(datos[8]);
                                    ProductosElectronicos productoE = new ProductosElectronicos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterial, tieneBateria, precargado);
                                    ListaProductos.Add(productoE);
                                    break;
                                case TipoProducto.MaterialesPreciosos:
                                    string tipoMaterialM = datos[3]; 
                                    double peso = double.Parse(datos[6]);
                                    MaterialesPreciosos productoM = new MaterialesPreciosos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterialM, peso);
                                    ListaProductos.Add(productoM);
                                    break;
                            }
                        }
                        else {
                            Console.WriteLine("Error: tipo de producto no reconocido.");
                        }
                    }
                    else {
                        Console.WriteLine("Error: linea con formato incorrecto.");
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
                    TipoProducto tipoProducto;
                    string descripcion;
                    int unidades;
                    double precioUnitario;

                    try {
                        id = int.Parse(datos[0]);
                        nombre = datos[1];
                        Enum.TryParse(datos[2], out tipoProducto);
                        descripcion = datos[3];
                        unidades = int.Parse(datos[4]);
                        precioUnitario = double.Parse(datos[5]);

                        switch (tipoProducto) {
                            case TipoProducto.ProductosAlimenticios:
                                if (datos.Length >= 7) {
                                    string informacionNutricional = datos[6];
                                    ProductosAlimenticios productoA = new ProductosAlimenticios(id, nombre, unidades, precioUnitario, descripcion, informacionNutricional);
                                    ListaProductos.Add(productoA);
                                }
                                else {
                                    Console.WriteLine($"Error: Informacion incompleta para el producto alimenticio con ID {id}.");
                                }
                                break;
                            case TipoProducto.ProductosElectronicos:
                                if (datos.Length >= 9) {
                                    string tipoMaterial = datos[3];
                                    bool tieneBateria;
                                    bool precargado;

                                    if (bool.TryParse(datos[6], out tieneBateria) && bool.TryParse(datos[7], out precargado)) {
                                        ProductosElectronicos productoE = new ProductosElectronicos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterial, tieneBateria, precargado);
                                        ListaProductos.Add(productoE);
                                    }
                                    else {
                                        Console.WriteLine($"Error: Formato incorrecto para los campos de productos electronicos con ID {id}.");
                                    }
                                }
                                else {
                                    Console.WriteLine($"Error: Informacion incompleta para el producto electronico con ID {id}.");
                                }
                                break;
                            case TipoProducto.MaterialesPreciosos:
                                string tipoMaterialM = datos[3]; 
                                if (datos.Length >= 7) {
                                    double peso;

                                    if (double.TryParse(datos[6], out peso)) {
                                        MaterialesPreciosos productoM = new MaterialesPreciosos(id, nombre, unidades, precioUnitario, descripcion, tipoMaterialM, peso);
                                        ListaProductos.Add(productoM);
                                    }
                                    else {
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

        // Llama al método que carga todos los productos
        public void CargaCompletaProductos()
        {
            CargaTodosLosProductos();
        }

        // metodo para salir de la maquina ADMIN Y CLIENTE 
        public void Salir(bool esAdmin) {
        if (esAdmin) {
            string filePath = "usuarios_admin.txt";
            try {
                using (StreamWriter sw = new StreamWriter(filePath)) {
                    foreach (Usuario a in ListaUsuariosAdmin) {
                        sw.WriteLine(a.Nombre);
                    }
                    sw.Close();
                }
                Console.WriteLine("Los IDs de los usuarios admin han sido guardados en el archivo.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al guardar los IDs de los usuarios admin: {ex.Message}");
            }
        }
        else {
            string filePath = "usuarios_cliente.txt";
            try {
                using (StreamWriter sw = new StreamWriter(filePath)) {
                    foreach (Usuario cliente in ListaUsuariosCliente) {
                        sw.WriteLine(cliente.Nombre);
                    }
                    sw.Close();
                }
                Console.WriteLine("Los IDs de los usuarios cliente han sido guardados en el archivo.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al guardar los IDs de los usuarios cliente: {ex.Message}");
            }
        }
    }
}
}
