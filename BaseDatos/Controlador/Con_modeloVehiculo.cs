using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    class Con_modeloVehiculo
    {
        public List<string> listarModeloPorVehiculo()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.ModeloVehiculo.ToList();
                List<string> lista = new List<string>();
                foreach (ModeloVehiculo MoVe in consulta)
                {
                    lista.Add(MoVe.Descripcion.ToUpper());
                }
                return lista;
            }
        }
        public int idTipoModelo(string descripcion)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.ModeloVehiculo.Where(x => x.Descripcion.ToUpper().Equals(descripcion.ToUpper())).FirstOrDefault();
                return consulta.IdModelo;
            }
        }
    }
}
