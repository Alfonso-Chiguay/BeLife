using BaseDatos;
using BaseDatos.Controlador;
using Negocio.Funciones;
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
using static BeLife.Vistas.AdministrarCliente;
using System.Text.RegularExpressions;

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para Seguro_vida.xaml
    /// </summary>
    public partial class Seguro_vida : Window
    {
        Cliente cliente;
        string idPlan;
        public Seguro_vida()
        {
            InitializeComponent();
            cb_idPlan.Items.Add("VID01");
            cb_idPlan.Items.Add("VID02");
            cb_idPlan.Items.Add("VID03");
            cb_estadoCivil.Items.Add("SOLTERO");
            cb_estadoCivil.Items.Add("CASADO");
            cb_estadoCivil.Items.Add("DIVORCIADO");
            cb_estadoCivil.Items.Add("VIUDO");
            cb_sexo.Items.Add("HOMBRE");
            cb_sexo.Items.Add("MUJER");
            dp_inicioContrato.DisplayDateStart = fecha;
            dp_inicioContrato.DisplayDateEnd = fecha.AddMonths(1);
            dp_terminoContrato.DisplayDateStart = fecha.AddYears(1);
        }

        public Seguro_vida(Parametro p)
        {
            InitializeComponent();
            cliente = p.cliente;
            idPlan = p.idPlan;
            llenarCampos(p);
            dp_inicioContrato.DisplayDateStart = fecha;
            dp_inicioContrato.DisplayDateEnd = fecha.AddMonths(1);
            dp_terminoContrato.DisplayDateStart = fecha.AddYears(1);
        }

        public Seguro_vida(Cliente c)
        {
            InitializeComponent();
            cliente = c;
            Parametro p = new Parametro();
            p.cliente = c;
            p.idPlan = "";
            llenarCampos(p);
            cb_estadoCivil.Items.Add("SOLTERO");
            cb_estadoCivil.Items.Add("CASADO");
            cb_estadoCivil.Items.Add("DIVORCIADO");
            cb_estadoCivil.Items.Add("VIUDO");
            cb_sexo.Items.Add("HOMBRE");
            cb_sexo.Items.Add("MUJER");
            dp_inicioContrato.DisplayDateStart = fecha;
            dp_inicioContrato.DisplayDateEnd = fecha.AddMonths(1);
            dp_terminoContrato.DisplayDateStart = fecha.AddYears(1);
        }

        DateTime fecha = DateTime.Now;

        public void llenarCampos(Parametro p_cliente)
        {
            txt_contrato.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_rut.Text = p_cliente.cliente.RutCliente.Split('-')[0];
            txt_dv.Text = p_cliente.cliente.RutCliente.Split('-')[1];
            txt_nombre.Text = p_cliente.cliente.Nombres;
            txt_apellidos.Text = p_cliente.cliente.Apellidos;
            dp_fechaNacimiento.SelectedDate = p_cliente.cliente.FechaNacimiento;
            Con_Sexo consexo = new Con_Sexo();
            Con_EstadoCivil concivil = new Con_EstadoCivil();

            cb_sexo.Items.Add(consexo.sexoPorId(p_cliente.cliente.IdSexo));
            cb_sexo.SelectedIndex = 0;
            cb_estadoCivil.Items.Add(concivil.ecivilPorId(p_cliente.cliente.IdEstadoCivil));
            cb_estadoCivil.SelectedIndex = 0;

            txt_rut.IsEnabled = false;
            txt_dv.IsEnabled = false;
            dp_inicioContrato.IsEnabled = true;
            btn_buscarRut.IsEnabled = false;
            btn_listaCliente.IsEnabled = false;
            if (!p_cliente.idPlan.Equals(""))
            {
                cb_idPlan.Items.Add(p_cliente.idPlan);
                cb_idPlan.SelectedIndex = 0;
                cb_idPlan.IsEnabled = false;
            }
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

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_listaCliente_Click(object sender, RoutedEventArgs e)
        {
            ListarClientes ventana = new ListarClientes("Seguro_vida");
            this.Close();
            ventana.ShowDialog();
        }

        private void btn_buscarRut_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(txt_rut.Text, "^[0-9]{8}$") || Regex.IsMatch(txt_rut.Text, "^[0-9]{7}$"))
            {
                Validacion validacion = new Validacion();
                Con_Cliente con_cliente = new Con_Cliente();
                if (validacion.rutValido(txt_rut.Text, txt_dv.Text))
                {
                    if (con_cliente.existeCliente(txt_rut.Text, txt_dv.Text))
                    {
                        cliente = con_cliente.obtenerCliente(txt_rut.Text, txt_dv.Text);
                        Parametro p = new Parametro();
                        p.cliente = cliente;
                        p.idPlan = "";
                        llenarCampos(p);
                        dp_inicioContrato.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Cliente no existe", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                        txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
                        txt_dv.Foreground = new SolidColorBrush(Colors.Gray);
                        txt_rut.Text = "Ej: 12345678";
                        txt_dv.Text = "N";
                    }
                }
                else
                    MessageBox.Show("RUT invalido", "Campo : RUT", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txt_rut.Text.Equals("00000000"))
                MessageBox.Show("RUT invalido", "Campo : RUT", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("RUT invalido", "Campo : RUT", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void dp_inicioContrato_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dp_terminoContrato.IsEnabled = true;
        }

        private void dp_terminoContrato_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Validacion validacion = new Validacion();
            DateTime fecha_1 = dp_fechaNacimiento.SelectedDate.Value;
            if (!validacion.ContratoFecha(fecha_1))
            {
                lbl_vigencia.Content = "Si";
            }
            else
            {
                lbl_vigencia.Content = "No";
            }
            rbtn_no.IsEnabled = true;
            rbtn_si.IsEnabled = true;
        }

        private void rbtn_si_Checked(object sender, RoutedEventArgs e)
        {
            txt_observacion.IsEnabled = true;
            btn_calcular_prima.IsEnabled = true;
        }

        private void rbtn_no_Checked(object sender, RoutedEventArgs e)
        {
            txt_observacion.IsEnabled = true;
            btn_calcular_prima.IsEnabled = true;
        }

        private void calcularPrima()
        {
            Validacion validacion = new Validacion();
            DateTime fecha_1 = dp_fechaNacimiento.SelectedDate.Value;
            double calculo = 0;
            if (validacion.MayorEdad(fecha_1))
            {
                int edad = validacion.edadContratante(fecha_1);
                if (edad >= 18 && edad <= 25)
                {
                    calculo += 3.6;
                }
                else if (edad >= 26 && edad <= 45)
                {
                    calculo += 2.4;
                }
                else
                    calculo += 6.0;

                if (cb_idPlan.SelectedIndex != -1)
                {
                    Con_Plan conplan = new Con_Plan();
                    Plan plan = conplan.obtenerPlanPorId(cb_idPlan.SelectedItem.ToString());
                    calculo += plan.PrimaBase;
                    if (cb_sexo.SelectedItem.Equals("HOMBRE"))
                        calculo += 2.4;
                    else
                        calculo += 1.2;

                    if (cb_estadoCivil.SelectedItem.Equals("SOLTERO"))
                        calculo += 4.8;
                    else if (cb_estadoCivil.SelectedItem.Equals("CASADO"))
                        calculo += 2.4;
                    else
                        calculo += 3.6;

                    txt_primaAnual.Text = calculo.ToString();
                    txt_primaMensual.Text = Math.Round((calculo / 12), 2).ToString();
                }
            }
        }

        private void btn_calcular_prima_Click(object sender, RoutedEventArgs e)
        {
            if (cb_idPlan.SelectedIndex != -1)
            {
                var pregunta = MessageBox.Show("¿Esta seguro que quiere calcular? una vez realizado, no podra hacer mas modificaciones",
                "Confirmar accion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (pregunta == MessageBoxResult.Yes)
                {
                    calcularPrima();
                    dp_inicioContrato.IsEnabled = false;
                    dp_terminoContrato.IsEnabled = false;
                    rbtn_no.IsEnabled = false;
                    rbtn_si.IsEnabled = false;
                    txt_observacion.IsEnabled = false;
                    btn_buscarRut.IsEnabled = false;
                    btn_calcular_prima.IsEnabled = false;
                    btn_listaCliente.IsEnabled = false;
                    btn_buscarContrato.IsEnabled = false;

                    btn_guardar.IsEnabled = true;
                }
            }
            else
                MessageBox.Show("ERROR EN EL ID DEL PLAN", "ERROR ID PLAN", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_listaCliente_Click_1(object sender, RoutedEventArgs e)
        {
            ListarClientes cliente = new ListarClientes("Seguro_vida");
            this.Close();
            cliente.ShowDialog();
        }

        private void btn_guardar_Click(object sender, RoutedEventArgs e)
        {
            bool salud;
            bool vigencia;
            if (lbl_vigencia.Content.Equals("Si"))
                vigencia = true;
            else
                vigencia = false;

            if (rbtn_si.IsChecked == true)
                salud = true;
            else
                salud = false;
            Con_Sexo consexo = new Con_Sexo();
            Con_EstadoCivil concivil = new Con_EstadoCivil();
            var id_sexo = consexo.obtenerId(cb_sexo.SelectedItem.ToString());
            var id_civil = concivil.obtenerId(cb_estadoCivil.SelectedItem.ToString());

            Contrato contrato = new Contrato();
            contrato.Numero = txt_contrato.Text;
            contrato.FechaCreacion = fecha;
            contrato.FechaTermino = dp_terminoContrato.SelectedDate.Value;
            contrato.RutCliente = txt_rut.Text + "-" + txt_dv.Text;
            contrato.CodigoPlan = cb_idPlan.SelectedItem.ToString();
            contrato.IdTipoContrato = 10;
            contrato.FechaInicioVigencia = dp_inicioContrato.SelectedDate.Value;
            contrato.FechaFinVigencia = dp_terminoContrato.SelectedDate.Value;
            contrato.Vigente = vigencia;
            contrato.DeclaracionSalud = salud;
            contrato.PrimaAnual = Double.Parse(txt_primaAnual.Text); ;
            contrato.PrimaMensual = Double.Parse(txt_primaMensual.Text); ;
            contrato.Observaciones = txt_observacion.Text;
            Con_Contrato con_contrato = new Con_Contrato();
            con_contrato.generarContrato(contrato);
            MessageBox.Show("DATOS GUARDADOS CORRECTAMENTE", "REGISTRO COMPLETO", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
