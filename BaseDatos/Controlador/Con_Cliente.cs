using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Cliente
    {

        public void crearCliente(Cliente cliente)
        {
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                entidades.Cliente.Add(cliente);
                entidades.SaveChanges();
            }
        }

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

        
        public List<Cliente> filtarPorSexo(string sexo)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                Con_Sexo c_sexo = new Con_Sexo();
                int idsexo = c_sexo.obtenerId(sexo);
                return entidades.Cliente.Where(x => x.IdSexo == idsexo).ToList();
            }
            
        }

        public List<Cliente> filtarPorECivil(string e_civil)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                Con_EstadoCivil c_ecivil = new Con_EstadoCivil();
                int idcivil = c_ecivil.obtenerId(e_civil);
                return entidades.Cliente.Where(x => x.IdEstadoCivil == idcivil).ToList();
            }   
        }

        public List<Cliente> filtarTodo()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                
                return entidades.Cliente.ToList();
            }
        }

    }
}
