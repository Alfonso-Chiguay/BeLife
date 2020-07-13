using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
