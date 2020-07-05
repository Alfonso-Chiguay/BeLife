using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Plan
    {
        public List<string> listarPlanPorContrato(string descripcion_contrato)
        {
            Con_TipoContrato contrato = new Con_TipoContrato();
            int id_contrato = contrato.idTipoContrato(descripcion_contrato);
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.Plan.Where(x => x.IdTipoContrato == id_contrato).ToList();
                List<string> lista = new List<string>();
                foreach(Plan plan in consulta)
                {
                    string retorno = plan.IdPlan.ToString() + " - " + plan.Nombre.ToString().ToUpper();
                    lista.Add(retorno);
                    
                }
                return lista;
            }
        }
    }
}
