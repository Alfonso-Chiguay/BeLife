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
    /// Lógica de interacción para Seguros.xaml
    /// </summary>
    public partial class Seguros : Window
    {
        public Seguros()
        {
            InitializeComponent();
        }


        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lbl_vida_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_vida.Background = morado_oscuro.Background;
            btn_vida.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        private void lbl_vida_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_vida.Background = panelmorado.Background;
            btn_vida.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void lbl_vida_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn_vida.Background = morado_click.Background;
        }

        private void lbl_vida_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btn_vida.Background = panelmorado.Background;
            btn_vida.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void btn_vehiculos_Click(object sender, RoutedEventArgs e)
        {            
            Seguros_auto ventana = new Seguros_auto();
            ventana.ShowDialog();
        }

        private void btn_hogar_Click(object sender, RoutedEventArgs e)
        {
            Seguro_hogar ventana = new Seguro_hogar();
            ventana.ShowDialog();
        }
    }
}
