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
    /// Lógica de interacción para Lista_contratos.xaml
    /// </summary>
    public partial class Lista_contratos : Window
    {
        public Lista_contratos()
        {
            InitializeComponent();
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
