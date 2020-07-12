using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Contrato
    {
        public void contratoVehiculo(Contrato contrato)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                
                entidades.Contrato.Add(contrato);
                entidades.SaveChanges();
            }
        }
    }
}
