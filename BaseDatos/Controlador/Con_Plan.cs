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

        public class planTipoContrato
        {
            public int idContrato { get; set; }
            public string descripcionContrato { get; set; }
            public string idPlan { get; set; }
            public string nombrePlan { get; set; }
        }

        public List<planTipoContrato> listarInfoJoin()
        {
            using(BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = (from x in entidades.TipoContrato
                                join y in entidades.Plan on x.IdTipoContrato equals y.IdTipoContrato
                                select new planTipoContrato
                                {   idContrato = x.IdTipoContrato,
                                    descripcionContrato = x.Descripcion,
                                    idPlan = y.IdPlan,
                                   nombrePlan = y.Nombre
                                }).ToList();
                return consulta;
            }

        }
    }
}
