using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.Negocio
{
    public class Marca
    {
        public int IdMarca { get; set; }     
        public string Descripcion { get; set; }


        public Marca()
        {
            Init();
        }
        private void Init()
        {
            IdMarca = 0;
            Descripcion = string.Empty;
        }


        //Lee un registro de marca de la BBDD
        public bool Read()
        {
            Datos.BeLifeEntities BBDD = new Datos.BeLifeEntities();
            Datos.MarcaVehiculo marca = BBDD.MarcaVehiculo.First(m => m.IdMarca == IdMarca);
            try
            {
                CommonBC.Syncronize(marca, this);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Entrega un listado con todas las marcas de la BBDD

        public List<Marca> ReadAll()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            List<Datos.MarcaVehiculo> MarcaDatos = bbdd.MarcaVehiculo.ToList<Datos.MarcaVehiculo>();
            return GenerarListado(MarcaDatos);
        }

        // Genera un listado de marcas de negocio a partir de un listado de marcas de datos
        private List<Marca> GenerarListado(List<Datos.MarcaVehiculo> MarcaDatos)
        {
            List<Marca> marcas = new List<Marca>();

            foreach (var md in MarcaDatos)
            {
                Marca marca = new Marca();
                CommonBC.Syncronize(md, marca);
                marcas.Add(marca);
            }
            return marcas;
        }



    }
}
