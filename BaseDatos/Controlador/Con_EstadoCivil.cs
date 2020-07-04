using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_EstadoCivil
    {
        public List<string> listarEstadoCivil()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> lista = new List<string>();
                var consulta = entidades.EstadoCivil.ToList();
                foreach (EstadoCivil estCiv in consulta)
                {
                    lista.Add(estCiv.Descripcion.ToUpper());
                }
                return lista;
            }
        }
    }
}
