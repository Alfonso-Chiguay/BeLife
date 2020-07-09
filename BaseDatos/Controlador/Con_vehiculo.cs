﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatos.Controlador
{
    public class Con_vehiculo
    {
        public List<string> listarMarca()
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                List<string> lista = new List<string>();
                var consulta = entidades.MarcaVehiculo.ToList();
                foreach (MarcaVehiculo MarVe in consulta)
                {
                    lista.Add(MarVe.Descripcion.ToUpper());
                }
                return lista;
            }
        }

        public int MarcaPorId(int id)
        {
            using (BeLifeEntities entidades = new BeLifeEntities())
            {
                var consulta = entidades.MarcaVehiculo.Where(x => x.IdMarca == id).FirstOrDefault();
                return consulta.IdMarca;
            }
        }
    }
}