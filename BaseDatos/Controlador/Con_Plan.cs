using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_Plan
    {
        public List<Plan> listarPlanPorContrato(string descripcion_contrato)
        {
            Con_TipoContrato contrato = new Con_TipoContrato();
            int id_contrato = contrato.idTipoContrato(descripcion_contrato);
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.Plan.Where(x => x.IdTipoContrato == id_contrato).ToList();
                List<Plan> lista = new List<Plan>();
                foreach(Plan plan in consulta)
                {
                    Plan plan_temp = new Plan();
                    plan_temp.IdPlan = plan.IdPlan;
                    plan_temp.Nombre = plan.Nombre;
                    plan_temp.PrimaBase = plan.PrimaBase;
                    lista.Add(plan_temp);
                }
                return lista;
            }
        }
    }
}
