using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BaseDatos;
using BaseDatos.Controlador;
using Negocio;
using Negocio.Funciones;
using System.Text.RegularExpressions;

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para Seguro_hogar.xaml
    /// </summary>
    public partial class Seguro_hogar : Window
    {
        public Seguro_hogar()
        {
            InitializeComponent();
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            Con_region comuna = new Con_region();
            cb_region.ItemsSource = comuna.listarRegion();
        }

        DateTime fecha = DateTime.Now;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_codigoSeguro.IsEnabled = false;
            if (txt_valorInmueble.Text.Equals("0") || txt_valorInmueble.Text.Equals("00") || txt_valorInmueble.Text.Equals("000")
                || txt_valorInmueble.Text.Equals("0000"))
            {
                MessageBox.Show("VALOR INMUEBLE DEBE SER DISTINTO DE 0");
            }
            else if (Regex.IsMatch(txt_valorInmueble.Text, "^[1-9]{1}$") || Regex.IsMatch(txt_valorInmueble.Text, "^[0-9]{2}$") ||
                Regex.IsMatch(txt_valorInmueble.Text, "^[0-9]{3}$") || Regex.IsMatch(txt_valorInmueble.Text, "^[0-9]{4}$"))
            {
                MessageBox.Show("VALOR INMUEBLE ES NUMERICO, ESTA NITIDO#");
            }
            else
                MessageBox.Show("VALOR INMUEBLE INVALIDO");

        }

        private void cb_region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_comuna.IsEnabled = true;
            Con_Comuna comuna = new Con_Comuna();
            cb_comuna.ItemsSource = comuna.listaComunaPorRegion(cb_region.SelectedItem.ToString());
        }

        private void dp_fechaConstruccion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Validacion validacion = new Validacion();
            DateTime fecha = dp_fechaConstruccion.SelectedDate.Value;
            if(!validacion.viviendaFecha(fecha))
            {
                if (fecha.Year > DateTime.Today.Year)
                {
                    lbl_fecha.Content = "AÑO INCORRECTO";
                }
                else if (fecha.Month > DateTime.Today.Month)
                {
                    lbl_fecha.Content = "MES INCORRECTO";
                }
                else
                {
                    lbl_fecha.Content = "MUY VIEJA";
                }
            }
            else
            {
                lbl_fecha.Content = "TAMOS BIEN DIJO EL KOKU";
            }
        }

        private void txt_direccion_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_direccion.Text.Equals("Calle, Departamento,Torre,etc."))
            {
                txt_direccion.Text = "";
                txt_direccion.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_direccion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_direccion.Text.Equals(""))
            {
                txt_direccion.Foreground = new SolidColorBrush(Colors.Gray);
                txt_direccion.Text = "Calle, Departamento,Torre,etc.";
            }
        }

        private void txt_valorInmueble_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_valorInmueble.Text.Equals("0"))
            {
                txt_valorInmueble.Text = "";
                txt_valorInmueble.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_valorInmueble_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_valorInmueble.Text.Equals(""))
            {
                txt_valorInmueble.Foreground = new SolidColorBrush(Colors.Gray);
                txt_valorInmueble.Text = "0";
            }
        }

        private void txt_codigoPostal_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_codigoPostal.Text.Equals("9999999"))
            {
                txt_codigoPostal.Text = "";
                txt_codigoPostal.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_codigoPostal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_codigoPostal.Text.Equals("0000000"))
            {
                lbl_postal.Content = "CODIGO POSTAL INVALIDO";
                MessageBox.Show("CODIGO POSTAL INVALIDO", "ERROR CODIGO POSTAL", MessageBoxButton.OK, MessageBoxImage.Error);
            }          
            else if (Regex.IsMatch(txt_codigoPostal.Text, "^[0-9]{7}$"))
            {
                lbl_postal.Content = "CODIGO POSTAL OK";
            }
            else
            {
                lbl_postal.Content = "CODIGO POSTAL INVALIDO";
                MessageBox.Show("CODIGO POSTAL INVALIDO", "ERROR CODIGO POSTAL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
