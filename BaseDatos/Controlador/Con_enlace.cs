using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_enlace
    {
        public List<string> listarModeloPorMarca(string marca)
        {
            Con_modeloVehiculo vehiculo = new Con_modeloVehiculo();
            Con_vehiculo marca_vehiculo = new Con_vehiculo();
            int id_marca_vehiculo = vehiculo.idTipoModelo(marca);
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.MarcaModeloVehiculo.Where(x => x.IdMarca == id_marca_vehiculo).ToList();                
                List<string> lista = new List<string>();
                foreach (MarcaModeloVehiculo MaMoVe in consulta)
                {             
                    string retorno = MaMoVe.IdModelo.ToString();
                    lista.Add(retorno);
                }
                return lista;
            }
        }
    }
}
