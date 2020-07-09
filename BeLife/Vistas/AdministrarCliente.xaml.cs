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
using BaseDatos.Controlador;
using Negocio.Funciones;
using BaseDatos;
using System.Data;

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
            
            btn_buscar.Background = morado_oscuro.Background;
            btn_buscar.Foreground = new SolidColorBrush(Colors.White);
            

        }

        private void lbl_buscar_MouseLeave(object sender, MouseEventArgs e)
        {
            
            btn_buscar.Background = panelmorado.Background;
            btn_buscar.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void lbl_buscar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn_buscar.Background = morado_click.Background;
            Validacion validacion = new Validacion();
            Con_Cliente controlador_cliente = new Con_Cliente();
            Con_Sexo controlador_sexo = new Con_Sexo();
            Con_EstadoCivil controlador_eCivil = new Con_EstadoCivil();
            if (validacion.rutValido(txt_rut.Text, txt_dv.Text))
            {
                Con_EstadoCivil c_estadoCivil = new Con_EstadoCivil();
                Con_Sexo c_sexo = new Con_Sexo();
                cb_sexo.ItemsSource = c_sexo.listarSexos();
                cb_estado_civil.ItemsSource = c_estadoCivil.listarEstadoCivil();
                if (controlador_cliente.existeCliente(txt_rut.Text, txt_dv.Text))
                {
                    Cliente cliente = controlador_cliente.obtenerCliente(txt_rut.Text, txt_dv.Text);
                    txt_nombres.Text = cliente.Nombres;
                    txt_apellidos.Text = cliente.Apellidos;
                    dp_fecha_nacimiento.SelectedDate = cliente.FechaNacimiento;
                    cb_sexo.SelectedItem = controlador_sexo.sexoPorId(cliente.IdSexo);
                    cb_estado_civil.SelectedItem = controlador_eCivil.ecivilPorId(cliente.IdEstadoCivil);
                    btn_buscar.IsEnabled = false;
                    lbl_buscar.IsEnabled = false;
                    btn_buscar_lista.IsEnabled = false;
                    txt_rut.IsEnabled = false;
                    txt_dv.IsEnabled = false;
                    cb_tipo_contrato.IsEnabled = true;                    
                    Con_TipoContrato controlador_contrato = new Con_TipoContrato();
                    cb_tipo_contrato.ItemsSource = controlador_contrato.listarTiposContrato();
                }
                else
                {
                    var pregunta_agregar = MessageBox.Show("Cliente no existe, ¿desea agregarlo?", "¿Ingresar nuevo cliente?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(pregunta_agregar == MessageBoxResult.Yes)
                    {
                        txt_nombres.IsEnabled = true;
                        txt_apellidos.IsEnabled = true;
                        dp_fecha_nacimiento.IsEnabled = true;
                        cb_sexo.IsEnabled = true;
                        cb_estado_civil.IsEnabled = true;
                        lbl_buscar.IsEnabled = false;
                        btn_buscar.IsEnabled = false;
                        btn_buscar_lista.IsEnabled = false;
                        btn_verificar.IsEnabled = true;
                        txt_rut.IsEnabled = false;
                        txt_dv.IsEnabled = false;
                    }

                    else
                    {
                        txt_rut.Text = "Ej: 12345678";
                        txt_dv.Text = "N";
                        txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
                        txt_dv.Foreground = new SolidColorBrush(Colors.Gray);
                    }

                }               
                
            }
            else
                MessageBox.Show("RUT invalido", "Campo: RUT", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_verificar_Click(object sender, RoutedEventArgs e)
        {
            Validacion validacion = new Validacion();
            bool todo_bien = true;
            if(txt_nombres.Text.Length == 0 || txt_nombres.Text.Equals("Ej: Juan Jose")){
                MessageBox.Show("Ingrese los nombres","Campo: Nombres",MessageBoxButton.OK,MessageBoxImage.Error);
                todo_bien = false;
            }
            if(txt_apellidos.Text.Length == 0 || txt_apellidos.Text.Equals("Ej: Perez Gonzalez"))
            {
                MessageBox.Show("Ingrese los apellidos", "Campo: Apellidos", MessageBoxButton.OK, MessageBoxImage.Error);
                todo_bien = false;
            }

            if (lbl_edad.Content.Equals("Ingrese una edad"))
            {
                MessageBox.Show("Debe seleccionar una fecha de nacimiento", "Campo: Fecha de nacimiento", MessageBoxButton.OK, MessageBoxImage.Error);
                lbl_edad.Foreground = new SolidColorBrush(Colors.Red);
                todo_bien = false;
            }
            
            if (lbl_edad.Content.Equals("El cliente debe tener 18 años o mas") ||
                lbl_edad.Content.Equals("Año incorrecto") ||
                lbl_edad.Content.Equals("Mes incorrecto") )
            {
                MessageBox.Show("Verifique la edad del cliente", "Campo: Fecha de nacimiento", MessageBoxButton.OK, MessageBoxImage.Error);
                todo_bien = false;
            }
            if (cb_sexo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un sexo", "Campo: Sexo", MessageBoxButton.OK, MessageBoxImage.Error);
                todo_bien = false;
            }
            if (cb_estado_civil.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado civil", "Campo: Estado Civil", MessageBoxButton.OK, MessageBoxImage.Error);
                todo_bien = false;
            }
            if (todo_bien)
            {
                MessageBox.Show("La información es correcta", "Informacion verificada", MessageBoxButton.OK, MessageBoxImage.Information);
                cb_tipo_contrato.IsEnabled = true;
                Con_TipoContrato controlador_contrato = new Con_TipoContrato();
                cb_tipo_contrato.ItemsSource = controlador_contrato.listarTiposContrato();
            } 
                

        }

        private void dp_fecha_nacimiento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Validacion validacion = new Validacion();
            DateTime fecha_elegida = dp_fecha_nacimiento.SelectedDate.Value;
            if (!validacion.MayorEdad(fecha_elegida)) {
                if(fecha_elegida.Year> DateTime.Today.Year)
                {
                    lbl_edad.Content = "Año incorrecto";
                    lbl_edad.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if(fecha_elegida.Month > DateTime.Today.Month)
                {
                    lbl_edad.Content = "Mes incorrecto";
                    lbl_edad.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    lbl_edad.Content = "El cliente debe tener 18 años o mas";
                    lbl_edad.Foreground = new SolidColorBrush(Colors.Red);
                }
               
            }
            else
            {
                lbl_edad.Foreground = new SolidColorBrush(Colors.Transparent);
                lbl_edad.Content = "Todo bien";
            }
        }

        private void cb_tipo_contrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_tipo_plan.IsEnabled = true;
            Con_Plan controlador = new Con_Plan();
            cb_tipo_plan.ItemsSource = controlador.listarPlanPorContrato(cb_tipo_contrato.SelectedItem.ToString());
        }

        private void btn_buscar_lista_Click(object sender, RoutedEventArgs e)
        {
            ListarClientes ventana = new ListarClientes(true);
            ventana.ShowDialog();
        }

        public void llenar_formulario(Cliente cliente)
        {
            txt_nombres.Text = cliente.Nombres;
        }
    }
}
