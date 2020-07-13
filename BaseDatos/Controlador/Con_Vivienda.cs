using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Vivienda
    {
        public Vivienda obtenerPorCodPostal(int codigoPostal)
        {
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                return entidades.Vivienda.Where(x => x.CodigoPostal == codigoPostal).FirstOrDefault();
            }
        }

        public bool postalValido(string codigoPostal)
        {
            if (Regex.IsMatch(codigoPostal, "^[0-9]{7}$"))
                return true;
            else
                return false;
        }
        public bool existeCodigoPostal(int codigoPostal)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                if (entidades.Vivienda.Any(x => x.CodigoPostal.Equals(codigoPostal)))
                    return true;
                else
                    return false;
            }
        }
    }
}
