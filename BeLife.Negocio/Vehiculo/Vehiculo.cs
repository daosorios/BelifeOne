using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using System.Threading.Tasks;

namespace BeLife.Negocio
{
    public class Vehiculo
    {
        public string Patente { get; set; } 
        public int IdMarca { get; set; } 
        public int IdModelo { get; set; }
        public int Anho { get; set; }
        public string NroContrato { get; set; }

        public Vehiculo()
        {
            Init();
        }

        private void Init()
        {
            Patente = string.Empty;
            IdMarca = 0;
            IdModelo = 0;
            Anho = 0;
            NroContrato = string.Empty;
        }

        public void Carga() 
        { 
        
            
        }
    }
}
