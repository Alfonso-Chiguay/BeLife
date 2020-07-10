using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeLife.memento
{
    public class CareTakerUtil
    {
        List<Memento> estadosGuardados = new List<Memento>();
        public void agregarMemento(Memento memento)
        {
            estadosGuardados.Add(memento);
        }

        public Memento getMemento(int indice)
        {
            return estadosGuardados.ElementAt(indice);
        }

        public int Contar()
        {
            return estadosGuardados.Count();
        }

        internal IEnumerable<Memento> getMemento()
        {
            throw new NotImplementedException();
        }
    }
}
