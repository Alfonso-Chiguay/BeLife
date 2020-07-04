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
using System.Drawing;

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para AdministrarCliente.xaml
    /// </summary>
    /// 
    

    public partial class AdministrarCliente : Window
    {
        public AdministrarCliente()
        {
            InitializeComponent();
            


        }

        
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var morado_standar = panelmorado.Background;
        }

        private void txt_rut_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_rut.Text.Equals("Ej: 12345678"))
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
                txt_rut.Text = "Ej: 12345678";
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

        private void txt_nombres_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_nombres.Text.Equals("Ej: Juan Jose"))
            {
                txt_nombres.Text = "";
                txt_nombres.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_nombres_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_nombres.Text.Equals(""))
            {
                txt_nombres.Foreground = new SolidColorBrush(Colors.Gray);
                txt_nombres.Text = "Ej: Juan Jose";
            }
        }

        private void txt_apellidos_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_apellidos.Text.Equals(""))
            {
                txt_apellidos.Foreground = new SolidColorBrush(Colors.Gray);
                txt_apellidos.Text = "Ej: Perez Gonzalez";
            }
        }

        private void txt_apellidos_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_apellidos.Text.Equals("Ej: Perez Gonzalez"))
            {
                txt_apellidos.Text = "";
                txt_apellidos.Foreground = new SolidColorBrush(Colors.Black);
            }
        }



        private void lbl_buscar_MouseEnter(object sender, MouseEventArgs e)
        {
            var moradooscuro = morado_oscuro.Background;
            btn_buscar.Background = new SolidColorBrush(Colors.MediumPurple);
            btn_buscar.Foreground = new SolidColorBrush(Colors.White);
            

        }

        private void lbl_buscar_MouseLeave(object sender, MouseEventArgs e)
        {
            var morado_standar = panelmorado.Background;
            btn_buscar.Background = morado_standar;
            btn_buscar.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void lbl_buscar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var click_color = morado_click.Background;
            btn_buscar.Background = click_color;
        }
    }
}
