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

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para Seguro_vida.xaml
    /// </summary>
    public partial class Seguro_vida : Window
    {
        public Seguro_vida()
        {
            InitializeComponent();
        }


        private void txt_contrato_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_contrato.Text.Equals("yyyyMMddhhmmss"))
            {
                txt_contrato.Text = "";
                txt_contrato.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_contrato_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_contrato.Text.Equals(""))
            {
                txt_contrato.Text = "yyyyMMddhhmmss";
                txt_contrato.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void txt_rut_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_rut.Text.Equals(""))
            {
                txt_rut.Text = "Ej: 12345678";
                txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void txt_rut_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_rut.Text.Equals("Ej: 12345678"))
            {
                txt_rut.Text = "";
                txt_rut.Foreground = new SolidColorBrush(Colors.Black);
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
                txt_dv.Text = "N";
                txt_dv.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
