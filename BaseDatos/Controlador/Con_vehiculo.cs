using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_vehiculo
    {
        public List<string> listarMarca()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> lista = new List<string>();
                var consulta = entidades.MarcaVehiculo.ToList();
                foreach (MarcaVehiculo MarVe in consulta)
                {
                    lista.Add(MarVe.Descripcion.ToUpper());
                }
                return lista;
            }
        }

        public void asegurarVehiculo(Vehiculo vehiculo)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                entidades.Vehiculo.Add(vehiculo);
                entidades.SaveChanges();
            }
        }

        public int MarcaPorId(string marca)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.MarcaVehiculo.Where(x => x.Descripcion == marca).FirstOrDefault();
                return consulta.IdMarca;
            }
        }

    }
}
