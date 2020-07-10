using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.memento
{
    public class Memento
    {
        private object estado;
        public Memento(object estado)
        {
            this.estado = estado;
        }

        public object obtenerEstadoGuardado()
        {
            return estado;
        }
    }
}
