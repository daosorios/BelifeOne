using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.Negocio
{
    public class Modelo
    {
        public int IdModelo { get; set; }    
        public string Descripcion { get; set; }


        public Modelo()
        {
            Init();
        }    
        private void Init()
        {
            IdModelo = 0;
            Descripcion = string.Empty;
        }

        // Lee un registro de modelo vehiculo de la BBDD
        public bool Read()
        {
            Datos.BeLifeEntities BBDD = new Datos.BeLifeEntities();
            Datos.ModeloVehiculo modelo = BBDD.ModeloVehiculo.First(m => m.IdModelo == IdModelo);
            try
            {
                CommonBC.Syncronize(modelo, this);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Entrega una lista con todos los modelos de vehiculos de la BBDD
        public List<Modelo> ReadAll()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            List<Datos.ModeloVehiculo> modeloDatos = bbdd.ModeloVehiculo.ToList<Datos.ModeloVehiculo>();
            return GenerarListado(modeloDatos);
        }

        //Segun la id marca entregada, retorna un listado con todos los modelos respectivos
        public List<Modelo> ReadAllByMarca(int idmarca)
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            List<Datos.MarcaModeloVehiculo> marcaModelos =
                bbdd.MarcaModeloVehiculo.Where(m => m.IdMarca == idmarca).ToList<Datos.MarcaModeloVehiculo>();

            List<Datos.ModeloVehiculo> modeloDatos = new List<Datos.ModeloVehiculo>();
            foreach (var marMod in marcaModelos)
            {
                Datos.ModeloVehiculo modelo = bbdd.ModeloVehiculo.First(m => m.IdModelo == marMod.IdModelo);
                modeloDatos.Add(modelo);
            }

            return GenerarListado(modeloDatos);
        }


        // Crea un listado de modelo vehiculo de negocio a partir de una lista de modelo vehiculo de datos
        private List<Modelo> GenerarListado(List<Datos.ModeloVehiculo> modeloDatos)
        {
            List<Modelo> modelos = new List<Modelo>();

            foreach (var md in modeloDatos)
            {
                Modelo modelo = new Modelo();
                CommonBC.Syncronize(md, modelo);
                modelos.Add(modelo);
            }
            return modelos;
        }

    }
}
