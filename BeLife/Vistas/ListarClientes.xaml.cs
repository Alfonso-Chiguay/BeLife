using BaseDatos;
using BaseDatos.Controlador;
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
    /// Lógica de interacción para ListarClientes.xaml
    /// </summary>
    public partial class ListarClientes : Window
    {
        public ListarClientes()
        {
            InitializeComponent();
            cb_filtro.Items.Add("ESTADO CIVIL");
            cb_filtro.Items.Add("RUT");
            cb_filtro.Items.Add("SEXO");
            cb_filtro.Items.Add("TODOS");
            txt_filtro.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cb_filtro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string seleccion = cb_filtro.SelectedItem.ToString();
            if (seleccion.Equals("RUT"))
            {
                txt_filtro.Text = "Ej: 11111111-1";
                cb_filtro_seleccion.Visibility = Visibility.Hidden;
                txt_filtro.Visibility = Visibility.Visible;
                btn_filtrar.Content = "Filtrar RUT";
            }
                 
            else if (seleccion.Equals("SEXO"))
            {
                cb_filtro_seleccion.Visibility = Visibility.Visible;
                txt_filtro.Visibility = Visibility.Hidden;
                Con_Sexo controlador = new Con_Sexo();
                cb_filtro_seleccion.ItemsSource = controlador.listarSexos();
                btn_filtrar.Content = "Filtrar sexo";
            }
                
            else if(seleccion.Equals("ESTADO CIVIL"))
            {
                cb_filtro_seleccion.Visibility = Visibility.Visible;
                txt_filtro.Visibility = Visibility.Hidden;
                Con_EstadoCivil controlador = new Con_EstadoCivil();
                cb_filtro_seleccion.ItemsSource = controlador.listarEstadoCivil();
                btn_filtrar.Content = "Filtrar e. civil";
            }
            else if (seleccion.Equals("TODOS"))
            {
                txt_filtro.Visibility = Visibility.Hidden;
                cb_filtro_seleccion.Visibility = Visibility.Hidden;
                btn_filtrar.Content = "Filtrar todo";
            }
                
        }

        private void txt_filtro_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_filtro.Text = "";
            txt_filtro.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void btn_filtrar_Click(object sender, RoutedEventArgs e)
        {
            list_cliente.Items.Clear();
            var seleccion = cb_filtro.SelectedItem.ToString();
            if (seleccion.Equals("SEXO"))
            {
                Con_Cliente filtro = new Con_Cliente();
                List<Cliente> lista = filtro.filtarPorSexo(cb_filtro_seleccion.SelectedItem.ToString());
                foreach(Cliente c in lista)
                {
                    list_cliente.Items.Add(c.ToString());
                }
                //list_cliente.ItemsSource = lista;
            }
        }
    }
}
