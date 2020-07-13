using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        

        public int MarcaPorId(string marca)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.MarcaVehiculo.Where(x => x.Descripcion == marca).FirstOrDefault();
                return consulta.IdMarca;
            }
        }

        public bool existePatente(string patente)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                if (entidades.Vehiculo.Any(x => x.Patente.Equals(patente)))
                    return true;
                else
                    return false;
            }
        }

        public bool patenteValida(string patente)
        {
            if (Regex.IsMatch(patente, "^[a-z-A-Z]{4}[0-9]{2}$"))
            {
                return true;
            }
            else if (Regex.IsMatch(patente, "^[a-z-A-Z]{3}[0-9]{3}$"))
            {
                return true;
            }
            else if (Regex.IsMatch(patente, "^[a-z-A-Z]{2}[0-9]{4}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
