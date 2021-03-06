﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_EstadoCivil
    {
        public List<string> listarEstadoCivil()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> lista = new List<string>();
                var consulta = entidades.EstadoCivil.ToList();
                foreach (EstadoCivil estCiv in consulta)
                {
                    lista.Add(estCiv.Descripcion.ToUpper());
                }
                return lista;
            }
        }

        public string ecivilPorId(int id)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.EstadoCivil.Where(x => x.IdEstadoCivil == id).FirstOrDefault();
                return consulta.Descripcion.ToUpper();
            }
        }

        public int obtenerId(string descripcion)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.EstadoCivil.Where(x => x.Descripcion.ToUpper().Equals(descripcion.ToUpper())).FirstOrDefault();
                return consulta.IdEstadoCivil;
            }

        }
    }
}
