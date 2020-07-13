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

        public bool ReadByNroContrato()
        {
            Console.WriteLine("lees el vehiculo entro");
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            try
            {
                Datos.Vehiculo vehiculo = bbdd.Vehiculo.First(v => v.Contrato.FirstOrDefault(c => c.Numero == Numero).Numero == Numero);

                CommonBC.Syncronize(vehiculo, this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Read()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            Datos.Vehiculo vehiculo = bbdd.Vehiculo.First(v => v.Patente == Patente);
            try
            {
                CommonBC.Syncronize(vehiculo, this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 


        public void SetMemento(EstadoAnterior memento)
        {
            CommonBC.Syncronize(this, memento.MementoVehiculo);

        }


        public EstadoAnterior CrearMemento(Vehiculo veh)
        {

            Console.WriteLine("entrando a memento");
            EstadoAnterior estadoAnterior = new EstadoAnterior();
            CommonBC.Syncronize(this, estadoAnterior.MementoVehiculo);
            CommonBC.Syncronize(veh, estadoAnterior.MementoVehiculo);


            return estadoAnterior;

        }

    }
}
