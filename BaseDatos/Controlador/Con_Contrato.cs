using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Contrato
    {
        public void contratoVehiculo(Contrato contrato, string idContrato, string patente)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                // CREO QUE ESTO AGREGA VALORES LA TABLA "FANTASMA" DE CONTRATO VEHICULO //
                Contrato c = new Contrato(); //--CREO QUE ESTA TABLA ES INNECESARIA-->{Numero = idContrato};
                c.Vehiculo.Add(new Vehiculo() { Patente = patente });
                entidades.Contrato.Add(contrato);
                entidades.Contrato.Add(c);
                entidades.SaveChanges();
            }
        }
    }
}
