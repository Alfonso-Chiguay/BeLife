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

        public List<string> listaComunaPorRegion(string region)
        {
            Con_region controlador_region = new Con_region();
            int id_region = controlador_region.idRegion(region);
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.RegionComuna.Where(x => x.IdRegion == id_region).ToList();
                List<string> lista = new List<string>();
                foreach (RegionComuna ReCo in consulta)
                {
                    string retorno = ReCo.Comuna.NombreComuna;
                    lista.Add(retorno);
                }
                return lista;
            }
        }
    }
}
