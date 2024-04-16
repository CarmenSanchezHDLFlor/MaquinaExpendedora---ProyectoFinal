﻿using MaquinaExpendedora___ProyectoFinal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaExpendedora_ProyectoFinal {
    internal class MaquinaExpendedora {

        // PROPIEDADES
        public List<Producto> Listaproductos { get; private set; }
        public Usuario Usuario { get; private set; }

        // CONSTRUCTORES
        public MaquinaExpendedora() { }

        // CONTRUCTOR PARAMETRIZADO
        public MaquinaExpendedora(Usuario usuario) {
            Usuario = usuario;
            Listaproductos = new List<Producto>();
        }

    }
}


