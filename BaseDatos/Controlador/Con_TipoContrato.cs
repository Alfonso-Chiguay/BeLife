using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_TipoContrato
    {
        public List<string> listarTiposContrato()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.TipoContrato.ToList();
                List<string> lista = new List<string>();
                foreach(TipoContrato tContrato in consulta)
                {
                    lista.Add(tContrato.Descripcion.ToUpper());
                }
                return lista;
            }
        }

        public int idTipoContrato(string descripcion)
        {
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.TipoContrato.Where(x => x.Descripcion.ToUpper().Equals(descripcion.ToUpper())).FirstOrDefault();
                return consulta.IdTipoContrato;
            }
        }
    }
}
