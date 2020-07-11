using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_region
    {
        public List<string> listarRegion()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> lista = new List<string>();
                var consulta = entidades.Region.ToList();
                foreach (Region Re in consulta)
                {
                    lista.Add(Re.NombreRegion.ToUpper());
                }
                return lista;
            }
        }

        public void asegurarVivienda(Vivienda vivienda)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                entidades.Vivienda.Add(vivienda);
                entidades.SaveChanges();
            }
        }
        public int idRegion(string nombreRegion)//OJOOOO
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.Region.Where(x => x.NombreRegion.ToUpper().Equals(nombreRegion.ToUpper())).FirstOrDefault();
                return consulta.IdRegion;
            }
        }
    }
}
