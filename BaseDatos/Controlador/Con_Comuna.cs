using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Comuna
    {
        public List<string> listarComunas()
        {
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> lista = new List<string>();
                var consulta = entidades.Comuna.ToList();
                foreach(Comuna comuna in consulta)
                {
                    lista.Add(comuna.NombreComuna.ToString());
                }
                return lista;
            }
        }
    }
}
