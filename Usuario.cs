using MaquinaExpendedora_ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora___ProyectoFinal {
    internal abstract class Usuario {

        // PROPIEDADES
        public int Id { get; private set; }
        public string NickName { get; private set; }
        public string Nombre { get; private set; }
        public string Ape1 { get; private set; }
        public string Ape2 { get; private set; }
        private string Password { get; set; }

        // PROPIEDAD PARA SABER SI ES CLIENTE O ADMIN
        public bool EsAdmin { get; protected set; }

        // ATRIBUTO PARA LA MAQUINA EXPENDEDORA
        protected MaquinaExpendedora Maquina { get; set; }

        // CONSTRUCTOR
        public Usuario() { }

        // CONTRUCTOR PARAMETRIZADO
        public Usuario(int id, string nickName, string nombre, string ape1, string ape2, string password,
            bool esAdmin, MaquinaExpendedora maquina) {
            Id = id;
            NickName = nickName;
            Nombre = nombre;
            Ape1 = ape1;
            Ape2 = ape2;
            Password = password;
            EsAdmin = esAdmin;
            Maquina = maquina;
        }

        // METODOS
        public string GetRealNombre() {
            return $"{Nombre} {Ape1} {Ape2}";
        }

        public bool Login(string nickname, string password) {
            return NickName == nickname && Password == password;
        }

        public abstract void Menu();

        public abstract void MostrarProductos();
        public abstract void Salir();

    }
}
