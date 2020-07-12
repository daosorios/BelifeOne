using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.Negocio
{
    public class PersistenciaMemento
    {
        private EstadoAnterior _memento;

        /// <summary>
        /// Retorna o asigna el memento de respaldo
        /// </summary>
        public EstadoAnterior Memento
        {
            get { return _memento; }
            set { _memento = value; }
        }

    }
}
