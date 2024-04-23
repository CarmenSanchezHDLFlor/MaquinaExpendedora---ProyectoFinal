﻿using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class Producto : InterfazUsuario {

        public enum TipoProducto {
            MaterialesPreciosos,
            ProductosAlimenticios,
            ProductosElectronicos
        }

        // PROPIEDADES
        public string Nombre { get; set; }
        public int Unidades { get; set; }
        public double PrecioUnitario { get; set; }
        public string Descripcion { get; set; }

        public int Id {  get; set; }
        public bool Vendido {  get; set; }

        public TipoProducto Tipo;

        // CONTRUCTORES 
        public Producto() { }

        // CONTRUCTOR PARAMETRIZADO
        public Producto(string nombre, int unidades, double precioUnitario, string descripcion) {
            Nombre = nombre;
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
            Descripcion = descripcion;
        }

        // CONTRUCTOR PARAMETRIZADO 2
        public Producto(string nombre, int unidades, double precioUnitario, string descripcion, bool vendido) {
            Nombre = nombre;
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
            Descripcion = descripcion;
            Vendido = vendido;
        }

        public Producto(int id, string nombre,TipoProducto tipo, string descripcion, int unidades, bool vendido) {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
            Descripcion = descripcion;
            Unidades = unidades;
            Vendido = vendido;
        }

        public Producto(int id, string nombre, int unidades, double precioUnitario, string descripcion) {
            Id = id;
            Nombre = nombre;
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
            Descripcion = descripcion;
        }

        // este se usa en interfazUsuario para cargar los productos 
        public Producto(int id, string nombre, TipoProducto tipo, string descripcion, int unidades, double precioUnitario) {
            Id = id;
            Nombre = nombre;
            Tipo = tipo;
            Descripcion = descripcion;
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
        }


        // METODOS
        // metodo para mostrar informacion de cada producto
        public virtual void MostrarInformacion() {
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Unidades: {Unidades}");
            Console.WriteLine($"Precio Unitario: {PrecioUnitario}");
            Console.WriteLine($"Descripcion: {Descripcion}");
        }

        // metodo para consultar si el producto esta vendido
        public bool EstaVendido() {
            return Vendido;
        }

        // metodo para marcar el producto como vendido
        public void MarcarComoVendido() {
            Vendido = true;
        }

        public override string ToString() {
            return $"ID: {Id}, Nombre: {Nombre}, Tipo: {Tipo}, Unidades: {Unidades}, Precio: ${PrecioUnitario}, Descripción: {Descripcion}, Vendido: {(Vendido ? "Sí" : "No")}";
        }
    }
}
