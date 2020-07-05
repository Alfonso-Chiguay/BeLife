using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Sexo
    {
        public List<string> listarSexos()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> lista = new List<string>();
                var consulta = entidades.Sexo.ToList();
                foreach(Sexo sexo in consulta)
                {
                    lista.Add(sexo.Descripcion.ToUpper());
                }
                return lista;
            }
        }

        public string sexoPorId(int id)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.Sexo.Where(x => x.IdSexo == id).FirstOrDefault();
                return consulta.Descripcion.ToUpper();
            }
        }

        public int obtenerId(string descripcion)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.Sexo.Where(x => x.Descripcion.ToUpper().Equals(descripcion.ToUpper())).FirstOrDefault();
                return consulta.IdSexo;
            }

        }
    }
}
