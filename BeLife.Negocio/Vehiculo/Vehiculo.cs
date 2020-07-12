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
        public string Numero { get; set; }




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
            Numero = string.Empty;
        }

        public bool CreateContratoVehiculo()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            Datos.Contrato contrato = bbdd.Contrato.First(c => c.Numero == Numero);
            Datos.Vehiculo vehiculo = new Datos.Vehiculo();

            try
            {
                CommonBC.Syncronize(this, vehiculo);
                vehiculo.Contrato.Add(contrato);
                bbdd.Vehiculo.Add(vehiculo);
                bbdd.SaveChanges();
                return true;
            }

            catch (Exception)
            {
                bbdd.Vehiculo.Remove(vehiculo);
                return false;
            }

        }




        public void SetMemento(EstadoAnterior memento)
        {
            //////////CommonBC.Syncronize(this, memento.MementoVehiculo);
            //this.Patente = memento.MementoVehiculo.Patente;
            //this.Anho = memento.MementoVehiculo.Anho;
            //this.IdMarca = memento.MementoVehiculo.IdMarca;
            //this.IdModelo = memento.MementoVehiculo.IdModelo;

        }

        public EstadoAnterior CrearMemento(Contrato contrato)
        {
            EstadoAnterior estadoAnterior = new EstadoAnterior();
            //CommonBC.Syncronize(this, estadoAnterior.MementoVehiculo);
            CommonBC.Syncronize(contrato, estadoAnterior.MementoContrato);


            return estadoAnterior;

        }

    }
}
