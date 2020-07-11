using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_modeloVehiculo
    {
        public List<string> listarModeloPorMarca(string marca)
        {
            Con_vehiculo marca_vehiculo = new Con_vehiculo();
            int id_marca_vehiculo = idTipoModelo(marca);
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.MarcaModeloVehiculo.Where(x => x.IdMarca == id_marca_vehiculo).ToList();
                List<string> lista = new List<string>();
                foreach (MarcaModeloVehiculo MaMoVe in consulta)
                {
                    string retorno_1 = MaMoVe.ModeloVehiculo.Descripcion;
                    lista.Add(retorno_1);
                }
                return lista;
            }
        }
        public int idTipoModelo(string descripcion)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.MarcaVehiculo.Where(x => x.Descripcion.ToUpper().Equals(descripcion.ToUpper())).FirstOrDefault();
                return consulta.IdMarca;
            }
        }
    }
}
