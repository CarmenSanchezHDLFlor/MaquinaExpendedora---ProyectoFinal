using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachinePropia {
    internal class Usuario {

        // PROPIEDADES

        /// <summary>
        /// Propiedad pública de un Enum TipoUsuario
        /// </summary>
        public enum TipoUsuario {
            Cliente,
            Admin
        }

        /// <summary>
        /// Propiedad público de TipoUsuario
        /// </summary>
        public TipoUsuario Tipo { get; set; }

        // CONTRUCTORES 

        /// <summary>
        /// Contructor por defecto de Usuario
        /// </summary>
        public Usuario() { }

        /// <summary>
        /// Constructor Usuario con parametro de TipoUsuario
        /// </summary>
        /// <param name="tipo"></param>
        public Usuario(TipoUsuario tipo) {
            Tipo = tipo;
        }

    }
}
