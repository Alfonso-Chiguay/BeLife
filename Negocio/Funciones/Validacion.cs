using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Negocio.Funciones
{
    public class Validacion
    {
        public bool MayorEdad(DateTime fechaNacimiento)
        {
            if(fechaNacimiento == null)
            {
                return false;
            }
            
            DateTime fechaActual = DateTime.Today;

            if (fechaNacimiento > fechaActual)
            {
                return false;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;

                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    edad-=1;
                }

                if (edad < 18) return false;
                else return true;
            }
        }

        public bool VehiculoFecha(DateTime fechaVehiculo)
        {
            if(fechaVehiculo == null)
            {
                return false;
            }
            DateTime fechaActual = DateTime.Today;

            if (fechaVehiculo > fechaActual)
            {
                return false;
            }
            else if (fechaVehiculo.Year < 1980 || fechaVehiculo.Year > fechaActual.Year)
                return false;
            else
                return true;
        }

        public bool rutValido(string rut, string dv)
        {
            int rut_numeros;


            if (rut.Length < 7 || rut.Length > 8)
            {
                return false;
            }
            else if (dv.Length != 1)
            {
                return false;
            }
            else if (!Int32.TryParse(rut, out rut_numeros))
            {
                return false;
            }
            else if (dv.ToLower() != "k" && !Int32.TryParse(dv, out rut_numeros))
            {
                return false;
            }
            else return true;
           
        }
        public bool viviendaFecha(DateTime fechaVivienda)
        {
            if (fechaVivienda == null)
            {
                return false;
            }
            DateTime fechaActual = DateTime.Today;

            if (fechaVivienda > fechaActual)
            {
                return false;
            }
            else if (fechaVivienda.Year < 1910 || fechaVivienda.Year > fechaActual.Year)
                return false;
            else
                return true;
        }
        public bool ContratoFecha(DateTime fechaContrato)
        {
            if (fechaContrato == null)
            {
                return false;
            }
            DateTime fechaActual = DateTime.Today;

            if (fechaContrato > fechaActual)
            {
                return true;
            }
            else
                return false;
        }

    }
}
