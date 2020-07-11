using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.Negocio
{
    public class Plan
    {
        public String IdPlan { get; set; }
        public String Nombre { get; set; }
        public float PrimaBase { get; set; }
        public String PolizaActual { get; set; }
        public int IdTipoContrato { get; set; }

        public Plan()
        {
            this.Init();
        }

        private void Init()
        {
            IdPlan = String.Empty;
            Nombre = String.Empty;
            PrimaBase = 0;
            PolizaActual = String.Empty;
            IdTipoContrato = 0;
        }


        public bool Read()
        {
            Datos.BeLifeEntities BBDD = new Datos.BeLifeEntities();
            Datos.Plan plan = BBDD.Plan.First(p => p.IdPlan == IdPlan);
            try
            {

                CommonBC.Syncronize(plan, this);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /// <summary>
        /// Obtiene una lista de todos los planes que pertenezcan a un determinado tipo contrato
        /// </summary>
        /// <returns></returns>
        public List<Plan> ReadAllPlan(String idTipo)
        {

            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();

            try
            {
                Console.WriteLine("dtao");
                /* Se obtiene todos los registro desde la tabla */
                //var listadoDatos = bbdd.Plan.ToList();

                var listadoDatos = (from d in bbdd.Plan
                                    where d.IdPlan != "HOG01" && d.IdPlan != "HOG02" && d.IdPlan != "HOG03"
                                    select d).ToList();



                /* Se convierte el listado de datos en un listado de negocio */
                List<Plan> listadoPlan = GenerarListado(listadoDatos, idTipo);


                /* Se retorna la lista */
                return listadoPlan;
            }
            catch (Exception)
            {
                return new List<Plan>();

            }

        }

        private List<Plan> GenerarListado(List<BeLife.Datos.Plan> listadoDatos, String idTipo)
        {
            List<Plan> listadoPlan = new List<Plan>();
            Console.WriteLine(idTipo+"compara");
            foreach (Datos.Plan dato in listadoDatos)
            {
                Console.WriteLine(dato.IdTipoContrato);
     
                if (dato.IdTipoContrato==int.Parse(idTipo))
                {
                    Plan plan = new Plan();
                    CommonBC.Syncronize(dato, plan);
                    Console.WriteLine(plan.ToString()+"sddsfsfsd");
                    listadoPlan.Add(plan);
                }

            }

            return listadoPlan;
        }
    }
}
