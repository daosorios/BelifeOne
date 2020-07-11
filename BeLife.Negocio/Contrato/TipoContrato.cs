using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.Negocio
{
    public class TipoContrato
    {
        public int IdTipoContrato { get; set; }
        public string Descripcion { get; set; }


        public TipoContrato()
        {
            Init();
        }

        private void Init()
        {
            IdTipoContrato = 0;
            Descripcion = string.Empty;
        }


        public bool Read()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            Datos.TipoContrato tipoContrato = new Datos.TipoContrato();
            try
            {
                CommonBC.Syncronize(tipoContrato, this);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<TipoContrato> ReadAll()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            List<Datos.TipoContrato> tipoContratoDatos = bbdd.TipoContrato.ToList<Datos.TipoContrato>();
            return GenerarListado(tipoContratoDatos);
        }


        private List<TipoContrato> GenerarListado(List<Datos.TipoContrato> tipoContratoDatos)
        {
            List<TipoContrato> tipoContratos = new List<TipoContrato>();

            foreach (var tip in tipoContratoDatos)
            {
                TipoContrato tipo = new TipoContrato();
                CommonBC.Syncronize(tip, tipo);
                tipoContratos.Add(tipo);
            }
            return tipoContratos;
        }
    }
}
