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
using BaseDatos;
using BaseDatos.Controlador;
using System.Windows.Shapes;
using Negocio.Funciones;
using System.Text.RegularExpressions;

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para Seguros_auto.xaml
    /// </summary>
    public partial class Seguros_auto : Window
    {
        public Seguros_auto()
        {
            InitializeComponent();
        }

        DateTime fecha = DateTime.Now;

        
        private void btn_buscar_en_lista_Click(object sender, RoutedEventArgs e)
        {
            Lista_contratos ventana = new Lista_contratos();
            ventana.lbl_tipoContrato.Content = "VEHICULO";
            ventana.ShowDialog();
        }

        private void btn_guardado_Click(object sender, RoutedEventArgs e)
        {
            txt_fecha.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_fecha.IsEnabled = false;
            Con_vehiculo vehiculo = new Con_vehiculo();
            cb_marca.ItemsSource = vehiculo.listarMarca();
            if (Regex.IsMatch(txt_patente.Text, "^[a-z-A-Z]{4}[0-9]{2}$"))
            {
                MessageBox.Show("TIPO DE PATENTE AAAA-99");
                //https://social.msdn.microsoft.com/Forums/es-ES/d2941e3c-81cc-40d2-9a59-f8716c1ca5ae/validar-patente-vehiculo?forum=vcses
            }
            else if (Regex.IsMatch(txt_patente.Text, "^[a-z-A-Z]{3}[0-9]{3}$"))
            {
                MessageBox.Show("TIPO DE PATENTE AAA-999");
            }
            else if (Regex.IsMatch(txt_patente.Text, "^[a-z-A-Z]{2}[0-9]{4}$"))
            {
                MessageBox.Show("TIPO DE PATENTE AA-9999");
            }
            else
                MessageBox.Show("NI IDEA ");

        }

        private void cb_marca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_modelo.IsEnabled = true;
            Con_enlace vehiculo = new Con_enlace();
            cb_modelo.ItemsSource = vehiculo.listarModeloPorMarca(cb_marca.SelectedItem.ToString());
        }

        private void txt_fecha_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_fecha.Text.Equals("yyyyMMddhhmmss"))
            {
                txt_fecha.Text = "";
                txt_fecha.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_fecha_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_fecha.Text.Equals(""))
            {
                txt_fecha.Foreground = new SolidColorBrush(Colors.Gray);
                txt_fecha.Text = "yyyyMMddhhmmss";
            }
        }

        private void txt_rut_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txt_rut.Text.Equals("Ej :12345678"))
            {
                txt_rut.Text = "";
                txt_rut.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_rut_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_rut.Text.Equals(""))
            {
                txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
                txt_rut.Text = "Ej :12345678";
            }
        }

        private void txt_dv_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_dv.Text.Equals("N"))
            {
                txt_dv.Text = "";
                txt_dv.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_dv_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_dv.Text.Equals(""))
            {
                txt_dv.Foreground = new SolidColorBrush(Colors.Gray);
                txt_dv.Text = "N";
            }
        }

        private void btn_buscarRut_Click(object sender, RoutedEventArgs e)
        {
            Validacion validacion = new Validacion();
            Con_Cliente controlador_cliente = new Con_Cliente();
            //if (validacion.rutValido(txt_rut.Text, txt_dv.Text))
            {
                Con_vehiculo vehiculo = new Con_vehiculo();
                cb_marca.ItemsSource = vehiculo.listarMarca();
                //if (controlador_cliente.existeCliente(txt_rut.Text, txt_dv.Text))
                //{
                    Cliente cliente = controlador_cliente.obtenerCliente(txt_rut.Text, txt_dv.Text);
                    txt_nombre.Text = cliente.Nombres;
                    txt_apellidos.Text = cliente.Apellidos;
                    cb_marca.IsEnabled = true;
                //}
                //else
                //{
                    //MessageBox.Show("Cliente no existe", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
                    txt_dv.Foreground = new SolidColorBrush(Colors.Gray);
                    txt_rut.Text = "Ej: 12345678";
                    txt_dv.Text = "N";
                //}
            }
           // else
             //   MessageBox.Show("RUT invalido", "Campo : RUT", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_buscarCliente_Click(object sender, RoutedEventArgs e)
        {
            ListarClientes ventana = new ListarClientes(true);
            ventana.ShowDialog();
        }

        private void dp_fechaVehiculo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Validacion validacion = new Validacion();
            DateTime fecha = dp_fechaVehiculo.SelectedDate.Value;
            if (!validacion.VehiculoFecha(fecha))
            {
                if(fecha.Year > DateTime.Today.Year)
                {
                    lbl_fecha.Content = "AÑO INCORRECTO";
                }
                else if (fecha.Month > DateTime.Today.Month)
                {
                    lbl_fecha.Content = "MES INCORRECTO";
                }
                else
                {
                    lbl_fecha.Content = "HOLAAA?";
                }
            }
            else
            {
                lbl_fecha.Content = "TAMOS BIEN";
            }
        }
    }
}
