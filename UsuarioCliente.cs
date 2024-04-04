using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora___ProyectoFinal {
    internal class UsuarioCliente : Usuario {

        // CONTRUCTORES
        public UsuarioCliente() { }
        public UsuarioCliente(int id, string nickname, string nombre, string ape1, string ape2, string password)
            : base(id, nickname, nombre, ape1, ape2, password) { }

        // METODOS
        public override void Menu() {
            int opcion;
            do {
                Console.WriteLine("1.  ");
                Console.WriteLine("2.  ");
                Console.WriteLine("3.  ");
                Console.WriteLine("4.  ");
                Console.WriteLine("5. Salir. ");
                Console.WriteLine("Opcion: ");
                opcion = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (opcion) {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Intenta de nuevo.");
                        break;
                }
            } while (opcion != 5);
        }

        public override void ComprarProducto() {

        }

        public override void MostrarProducto() {

        }

        public override void Salir() {

        }
    }
}
