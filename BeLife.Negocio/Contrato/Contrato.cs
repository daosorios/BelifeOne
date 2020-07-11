using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.Negocio
{
    public class Contrato
    {
        public string Numero { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaTermino { get; set; }
        public string RutCliente { get; set; }
        public string CodigoPlan { get; set; } 
        public int IdTipoContrato { get; set; }
        public System.DateTime FechaInicioVigencia { get; set; }
        public System.DateTime FechaFinVigencia { get; set; }
        public bool Vigente { get; set; }
        public bool DeclaracionSalud { get; set; }
        public double PrimaAnual { get; set; }
        public double PrimaMensual { get; set; }
        public string Observaciones { get; set; }


        public Contrato()
        {
            this.Init();
        }

        void Init()
        {
            Numero              = string.Empty;
            FechaCreacion       = DateTime.Today;
            FechaTermino        = DateTime.Today;
            RutCliente          = string.Empty;
            CodigoPlan          = string.Empty;  
            IdTipoContrato      = 0; 
            FechaInicioVigencia = DateTime.Today;
            FechaFinVigencia    = DateTime.Today;
            Vigente             = true;
            DeclaracionSalud    = true;
            PrimaAnual          = 0.0;
            PrimaMensual        = 0.0;
            Observaciones       = string.Empty;
        }

        //C.R.U.D. Crear contrato

        public bool CreateContrato()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            Datos.Contrato con = new Datos.Contrato();

            try
            {
                CommonBC.Syncronize(this, con);
                bbdd.Contrato.Add(con);
                bbdd.SaveChanges();
                return true;
            }

            catch (Exception)
            {
                bbdd.Contrato.Remove(con);
                return false;
            }
            
        }

        //C.R.U.D. Leer Contrato
        public bool ReadContrato()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            try
            {

                if (!RutCliente.Equals(""))
                {
                    Datos.Contrato contrato = bbdd.Contrato.First(e => e.RutCliente == RutCliente);
                    CommonBC.Syncronize(contrato, this);
                }
                else
                {
                    Datos.Contrato contrato = bbdd.Contrato.First(e => e.Numero == Numero);
                    CommonBC.Syncronize(contrato, this);
                }

                
                
                

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        //C.R.U.D. Actualizar Contrato

        public bool UpdateContrato()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            Datos.Contrato contrato = bbdd.Contrato.First(e => e.Numero == Numero);
            try
            {
                CommonBC.Syncronize(this, contrato);

                bbdd.SaveChanges();

                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //C.R.U.D. Borrar contrato

        public bool DeleteContrato()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();

            try
            {
                Datos.Contrato contrato = bbdd.Contrato.First(e => e.RutCliente == RutCliente);

                CommonBC.Syncronize(this, contrato);

                bbdd.SaveChanges();

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //ReadAll para el DataGrid

        public List<Contrato> ReadAll()
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();

            try
            {
                List<Datos.Contrato> listadoDatos = bbdd.Contrato.ToList<Datos.Contrato>();
                
                List<Contrato> listadoContrato = GenerarListado(listadoDatos);
                
                return listadoContrato;
            }
            catch (Exception)
            {
                return new List<Contrato>();
            }
        }



        private List<Contrato> GenerarListado(List<Datos.Contrato> listadoDatos)
        {
            List<Contrato> listadoContrato = new List<Contrato>();

            foreach (Datos.Contrato dato in listadoDatos)
            {
                Contrato contra = new Contrato();

                CommonBC.Syncronize(dato, contra);

                listadoContrato.Add(contra);
            }

            return listadoContrato;
        }

        public IEnumerable<Contrato> ReadS(string numero, String rut, string poliza)
        {
            Datos.BeLifeEntities bbdd = new Datos.BeLifeEntities();
            
            Datos.Plan pl = bbdd.Plan.First(e => e.PolizaActual == poliza);



            if (rut.Length > 0 && numero.Length > 0 && poliza.Length > 0)
            {
                IEnumerable<Contrato> abc = from d in ReadAll()
                                            where d.RutCliente == rut && d.Numero == numero.ToString() && d.CodigoPlan == pl.IdPlan.ToString()
                                            select d;
                return abc;
            }

            else if (rut.Length > 0 && numero.Length > 0 && poliza.Length == 0)
            {
                IEnumerable<Contrato> ab = from d in ReadAll()
                                           where d.RutCliente == rut && d.Numero == numero.ToString()
                                           select d;
                return ab;
            }


            else if (rut.Length > 0 && numero.Length == 0 && poliza.Length > 0)
            {
                IEnumerable<Contrato> ac = from d in ReadAll()
                                           where d.RutCliente == rut && d.CodigoPlan == pl.IdPlan.ToString()
                                           select d;
                return ac;
            }

            else if (rut.Length > 0 && numero.Length == 0 && poliza.Length == 0)
            {
                IEnumerable<Contrato> a = from d in ReadAll()
                                          where d.RutCliente == rut
                                          select d;
                return a;
            }

            else if (numero.Length > 0 && poliza.Length > 0)
            {
                IEnumerable<Contrato> ac = from d in ReadAll()
                                           where d.Numero == numero.ToString() && d.CodigoPlan == pl.IdPlan.ToString()
                                           select d;
                return ac;
            }
            else if (numero.Length > 0 && poliza.Length == 0)
            {
                IEnumerable<Contrato> a = from d in ReadAll()
                                          where d.Numero == numero.ToString()
                                          select d;
                return a;
            }
            else if (numero.Length == 0 && poliza.Length > 0)
            {
                IEnumerable<Contrato> c = from d in ReadAll()
                                          where d.CodigoPlan == pl.IdPlan.ToString()
                                          select d;
                return c;

            }
            else
            {
                return ReadAll();
            }

        }
        

        public IEnumerable<Contrato> FilCon(string numero)
        {

            if (numero.Length > 0)
            {
                IEnumerable<Contrato> a = from d in ReadAll()
                                          where d.Numero == numero.ToString()
                                          select d;
                return a;
            }
            else
            {
                return ReadAll();
            }
        }

    }
}
