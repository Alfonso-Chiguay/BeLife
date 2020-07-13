using BaseDatos;
using BaseDatos.Controlador;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
            btn_obtener.Visibility = Visibility.Hidden;
        }
        string ventana;
        public ListarClientes(string nombre_ventana)
        {
            InitializeComponent();
            cb_filtro.Items.Add("ESTADO CIVIL");
            cb_filtro.Items.Add("RUT");
            cb_filtro.Items.Add("SEXO");
            cb_filtro.Items.Add("TODOS");
            txt_filtro.Foreground = new SolidColorBrush(Colors.Gray);
            btn_obtener.Visibility = Visibility.Visible;
            ventana = nombre_ventana;
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

        Con_Cliente filtro = new Con_Cliente();
        private void btn_filtrar_Click(object sender, RoutedEventArgs e)
        {

            if (cb_filtro.SelectedIndex != -1)
            {

                var seleccion = cb_filtro.SelectedItem.ToString();
                var segunda_opcion = cb_filtro_seleccion.SelectedIndex;
                if (seleccion.Equals("SEXO") && segunda_opcion != -1)
                {

                    List<Cliente> lista = filtro.filtarPorSexo(cb_filtro_seleccion.SelectedItem.ToString());
                    List<Carga> carga = new List<Carga>();
                    foreach (Cliente c in lista)
                    {
                        Carga car = new Carga();
                        car.Rut = c.RutCliente;
                        car.Nombre = c.Nombres;
                        car.F_Nacimiento = c.FechaNacimiento.ToShortDateString();
                        carga.Add(car);

                    }

                    dg_listaClientes.ItemsSource = carga;
                }
                else if (seleccion.Equals("ESTADO CIVIL") && segunda_opcion != -1)
                {
                    List<Cliente> lista = filtro.filtarPorECivil(cb_filtro_seleccion.SelectedItem.ToString());
                    List<Carga> carga = new List<Carga>();
                    foreach (Cliente c in lista)
                    {
                        Carga car = new Carga();
                        car.Rut = c.RutCliente;
                        car.Nombre = c.Nombres;
                        car.F_Nacimiento = c.FechaNacimiento.ToShortDateString();
                        carga.Add(car);

                    }
                    dg_listaClientes.ItemsSource = carga;
                }

                else if (seleccion.Equals("RUT") && !txt_filtro.Text.Equals("Ej: 11111111-1"))
                {
                    try {
                        string rut = txt_filtro.Text.Split('-')[0];
                        string dv = txt_filtro.Text.Split('-')[1];
                        Cliente cliente = filtro.obtenerCliente(rut, dv);
                        List<Carga> carga = new List<Carga>();
                        Carga car = new Carga();
                        if (cliente.Nombres.Equals("No existe"))
                        {

                            car.Rut = "NO";
                            car.Nombre = "HAY";
                            car.F_Nacimiento = "DATOS";
                            carga.Add(car);
                        }
                        else
                        {

                            car.Rut = cliente.RutCliente;
                            car.Nombre = cliente.Nombres;
                            car.F_Nacimiento = cliente.FechaNacimiento.ToShortDateString();
                            carga.Add(car);

                        }
                        dg_listaClientes.ItemsSource = carga;
                    }
                    catch (Exception)
                    {
                        List<Carga> carga = new List<Carga>();
                        Carga car = new Carga();
                        car.Rut = "NO";
                        car.Nombre = "HAY";
                        car.F_Nacimiento = "DATOS";
                        carga.Add(car);
                        dg_listaClientes.ItemsSource = carga;
                    }
                    
                }
                else if (seleccion.Equals("TODOS"))
                {
                    List<Cliente> lista = filtro.filtarTodo();
                    List<Carga> carga = new List<Carga>();
                    foreach (Cliente c in lista)
                    {
                        Carga car = new Carga();
                        car.Rut = c.RutCliente;
                        car.Nombre = c.Nombres;
                        car.F_Nacimiento = c.FechaNacimiento.ToShortDateString();
                        carga.Add(car);

                    }
                    dg_listaClientes.ItemsSource = carga;
                }
                
            }
        }

        private void btn_obtener_Click(object sender, RoutedEventArgs e)
        {
            
            Cliente cliente = new Cliente();
            var fila = dg_listaClientes.SelectedItem as Carga;
            
            if(fila != null)
            {
                Con_Cliente con = new Con_Cliente();
                cliente = con.obtenerCliente(fila.Rut.Split('-')[0], fila.Rut.Split('-')[1]);
                if (ventana.Equals("AdministrarCliente"))
                {
                    AdministrarCliente vent = new AdministrarCliente(cliente);
                    this.Close();
                    vent.ShowDialog();
                }
                else if (ventana.Equals("Seguro_hogar"))
                {
                    Seguro_hogar vent = new Seguro_hogar(cliente);
                    this.Close();
                    vent.ShowDialog();
                }
                else if (ventana.Equals("Seguros_auto"))
                {
                    Seguros_auto vent = new Seguros_auto(cliente);
                    this.Close();
                    vent.ShowDialog();
                }
                else if (ventana.Equals("Seguro_vida"))
                {
                    Seguro_vida vent = new Seguro_vida(cliente);
                    this.Close();
                    vent.ShowDialog();
                }
            }
        }

        private class Carga
        {
            public string Rut { get; set; }
            public string Nombre { get; set; }
            public string F_Nacimiento { get; set; }
        }
    }
}