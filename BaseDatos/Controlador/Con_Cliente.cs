using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Cliente
    {
        public bool existeCliente(string rut, string dv)
        {
            using(BeLifeEntities entidades = new BeLifeEntities()){
                string rut_completo = rut + "-" + dv;
                if (entidades.Cliente.Any(x => x.RutCliente.Equals(rut_completo))) return true;
                else return false;
            }
        }

        public Cliente obtenerCliente(string rut, string dv)
        {
            if (existeCliente(rut, dv))
            {
                using(BeLifeEntities entidades = new BeLifeEntities())
                {
                    string rut_completo = rut + "-" + dv;
                    return entidades.Cliente.Where(x => x.RutCliente.Equals(rut_completo)).FirstOrDefault();
                }
            }
            else
            {
                Cliente cliente_vacio = new Cliente();
                cliente_vacio.Nombres = "No existe";
                return cliente_vacio;
            }
        }

    }
}
