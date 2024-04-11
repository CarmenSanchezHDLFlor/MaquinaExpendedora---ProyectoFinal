﻿using MaquinaExpendedora_ProyectoFinal;
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
        public List<Producto> Listaproductos { get; private set; }

        private GestorCompra GestorCompra { get; set; }

        // CONTRUCTORES
        public InterfazUsuario() { }

        public InterfazUsuario(MaquinaExpendedora maquina, Usuario usuario) {
            Maquina = maquina;
            Usuario = usuario;
            Listaproductos = Maquina.Listaproductos;
        }

        public InterfazUsuario(MaquinaExpendedora maquina, Usuario usuario, List<Producto> listaProductos) {
            Maquina = maquina;
            Usuario = usuario;
            Listaproductos = maquina.Listaproductos;
            GestorCompra = new GestorCompra(Listaproductos);
        }

        // METODOS 
        public void MostrarMenuPrincipal() {
            Console.WriteLine("--- Menu ---");
            if (Maquina.Usuario.EsAdmin) {
                Console.WriteLine("1. Carga individual la maquina con productos.");
                Console.WriteLine("2. Carga completa de los productos de la maquina.");
                Console.WriteLine("3. Mostrar todos los productos de la maquina.");
                Console.WriteLine("4. Salir.");
            }
            else {
                Console.WriteLine("1. Comprar productos.");
                Console.WriteLine("2. Mostrar los productos disponibles.");
                Console.WriteLine("3. Salir de la maquina.");
            }
        }

        public void MostrarProductos() {
            if (Listaproductos.Count == 0) {
                Console.WriteLine("no hay productos disponibles en nuestra maquina expendedora. ");
                return;
            }
            foreach (Producto p in Listaproductos) {
                p.MostrarInformacion();
                Console.WriteLine();
            }
        }

        public void ComprarProducto(int idProducto) {
            Producto producto = null;
            foreach (Producto p in Listaproductos) {
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

        public void PagoEfectivo(double precio, int idProducto) {
            GestorCompra.PagoEfectivo(precio, idProducto);
        }

        public void PagoTarjeta(double precio, int idProducto) {
            GestorCompra.PagoTarjeta(precio, idProducto);
        }

        public void CargaIndividualProductos() {
            // Si es un administrador...
            if (Usuario.EsAdmin) {
                Console.WriteLine("Selecciona una opción:");
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
                        Producto productoExistente = Listaproductos.Find(p => p.Id == idProductoExistente);

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
                            Console.WriteLine("Cantidad inválida.");
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
                        int nuevoId = Listaproductos.Any() ? Listaproductos.Max(p => p.Id) + 1 : 1;

                        // Crear el nuevo producto y agregarlo a la lista de productos
                        Producto nuevoProducto = new Producto(nombre, unidades, precioUnitario, descripcion) { Id = nuevoId };
                        Listaproductos.Add(nuevoProducto);
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

        public void CargarTodosLosProductos() { // TERMINAR ESTE METODO
            try {
                Console.Write("Ingrese el nombre del archivo de carga de productos: ");
                string nombreArchivo = Console.ReadLine();

                using (StreamReader sr = new StreamReader(nombreArchivo)) {
                    string linea;
                    while ((linea = sr.ReadLine()) != null) {





                    }
                }

                Console.WriteLine("Carga de productos completada correctamente.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al cargar los productos desde el archivo: {ex.Message}");
            }
        }

        public void Salir() {
            string filePath = "productos.txt";
            try {
                using (StreamWriter sw = new StreamWriter(filePath)) {
                    foreach (Producto producto in Listaproductos) {
                        sw.WriteLine(producto.Nombre);
                    }
                }
                Console.WriteLine("Los productos han sido guardados en el archivo.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al guardar los productos: {ex.Message}");
            }
        }

    }
}

