using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BeLife.Negocio;

namespace BeLife.Negocio
{
    public class EstadoAnterior
    {
        private Cliente _mementoCliente;
        private Contrato _mementoContrato;


        public Cliente MementoCliente
        {
            get { return _mementoCliente; }
            set { _mementoCliente = value; }
        }

        public Contrato MementoContrato
        {
            get { return _mementoContrato; }
            set { _mementoContrato = value; }
        }

        public EstadoAnterior()
        {
            Init();
        }

        private void Init()
        {
            MementoCliente = new Cliente();
            MementoContrato = new Contrato();
        }

        public void SerializarXml()
        {
            StreamWriter MyFile = new StreamWriter("Source\\../../../Resources/Save.txt");

            System.Xml.Serialization.XmlSerializer serializador = new System.Xml.Serialization.XmlSerializer(this.GetType());

            serializador.Serialize(MyFile, this);
            MyFile.Close();

        }


        public bool DeserializarXML()
        {

            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(EstadoAnterior));

                FileStream myFileStream = new FileStream("Source\\../../../Resources/Save.txt", FileMode.Open);

                EstadoAnterior myObject = (EstadoAnterior)mySerializer.Deserialize(myFileStream);

                if (myObject._mementoContrato.IdTipoContrato >= 1)
                {

                    CommonBC.Syncronize(myObject, this);
                    myFileStream.Close();
                }
                else
                {
                    return false;
                }


                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
