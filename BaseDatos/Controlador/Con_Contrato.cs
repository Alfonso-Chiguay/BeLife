using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Contrato
    {
        public void generarContrato(Contrato contrato)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {                
                entidades.Contrato.Add(contrato);
                entidades.SaveChanges();
            }
        }

        public Contrato buscarPorNumero(string numero)
        {
            Contrato retorno = new Contrato();
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                if (entidades.Contrato.Any(x => x.Numero.Equals(numero)))
                    return entidades.Contrato.Where(x => x.Numero.Equals(numero)).FirstOrDefault();
                else
                    return retorno;

            }
        }

        

        public List<string> listasDeContratoPorCliente(string RutCliente)
        {
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> retorno = new List<string>();
                var consulta = entidades.Contrato.Where(x => x.RutCliente.Equals(RutCliente)).ToList();
                if (consulta.Count > 0)
                {
                    foreach(Contrato contrato in consulta)
                    {
                        retorno.Add(contrato.CodigoPlan);
                    }                    
                }
                return retorno;
            }
        }

        public void darDeBaja(string RutCliente, string idPlan)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.Contrato.Where(x => x.RutCliente.Equals(RutCliente) && x.CodigoPlan.Equals(idPlan) && x.Vigente == true).FirstOrDefault();
                consulta.Vigente = false;
                entidades.SaveChanges();
            }
        }

        public bool contratoVigente(string RutCliente, string idPlan)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.Contrato.Where(x => x.RutCliente.Equals(RutCliente) && x.CodigoPlan.Equals(idPlan)).ToList();
                foreach(Contrato con in consulta)
                {
                    if (con.Vigente == true)
                        return true;
                }
                return false;
                
            }
        }
    }
}
