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
using static BeLife.Vistas.AdministrarCliente;
using System.Reflection;

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para Seguros_auto.xaml
    /// </summary>
    public partial class Seguros_auto : Window
    {
        Cliente cliente;
        string idPlan;
        public Seguros_auto()
        {
            InitializeComponent();
            Con_vehiculo vehiculo = new Con_vehiculo();
            cb_marca.ItemsSource = vehiculo.listarMarca();
            txt_fecha.Text = fecha.ToString("yyyyMMddhhmmss");
            cb_idPlan.Items.Add("VEH01");
            cb_idPlan.Items.Add("VEH02");
            cb_idPlan.Items.Add("VEH03");
            dp_fechaInicio.DisplayDateStart = fecha;
            dp_fechaInicio.DisplayDateEnd = fecha.AddMonths(1);
            dp_fechaTermino.DisplayDateStart = fecha.AddYears(1);
        }
        public Seguros_auto(Parametro p)
        {
            InitializeComponent();
            Con_vehiculo vehiculo = new Con_vehiculo();
            cb_marca.ItemsSource = vehiculo.listarMarca();
            cliente = p.cliente;
            idPlan = p.idPlan;
            llenarCampos(p);
            dp_fechaInicio.DisplayDateStart = fecha;
            dp_fechaInicio.DisplayDateEnd = fecha.AddMonths(1);
            dp_fechaTermino.DisplayDateStart = fecha.AddYears(1);
        }

        

        public Seguros_auto(Cliente c)
        {
            InitializeComponent();
            Con_vehiculo vehiculo = new Con_vehiculo();
            cb_marca.ItemsSource = vehiculo.listarMarca();
            cliente = c;
            Parametro p = new Parametro();
            p.cliente = c;
            p.idPlan = "";
            llenarCampos(p);
            cb_idPlan.Items.Add("VEH01");
            cb_idPlan.Items.Add("VEH02");
            cb_idPlan.Items.Add("VEH03");
            dp_fechaInicio.DisplayDateStart = fecha;
            dp_fechaInicio.DisplayDateEnd = fecha.AddMonths(1);
            dp_fechaTermino.DisplayDateStart = fecha.AddYears(1);
        }

        DateTime fecha = DateTime.Now;

        public void llenarCampos(Parametro p_cliente)
        {
            txt_fecha.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_rut.Text = p_cliente.cliente.RutCliente.Split('-')[0];
            txt_dv.Text = p_cliente.cliente.RutCliente.Split('-')[1];
            txt_nombre.Text = p_cliente.cliente.Nombres;
            txt_apellidos.Text = p_cliente.cliente.Apellidos;
            dp_FechaNacimiento.SelectedDate = p_cliente.cliente.FechaNacimiento;
            Con_Sexo consexo = new Con_Sexo();
            Con_EstadoCivil concivil = new Con_EstadoCivil();

            cb_sexo.Items.Add(consexo.sexoPorId(p_cliente.cliente.IdSexo));
            cb_sexo.SelectedIndex = 0;
            cb_estadoCivil.Items.Add(concivil.ecivilPorId(p_cliente.cliente.IdEstadoCivil));
            cb_estadoCivil.SelectedIndex = 0;

            txt_rut.IsEnabled = false;
            txt_dv.IsEnabled = false;

            cb_marca.IsEnabled = true;
            txt_patente.IsEnabled = true;
            if (!p_cliente.idPlan.Equals(""))
            {
                cb_idPlan.Text = p_cliente.idPlan;
            }
        }


        private void btn_buscar_en_lista_Click(object sender, RoutedEventArgs e)
        {
            Lista_contratos ventana = new Lista_contratos();
            ventana.lbl_tipoContrato.Content = "VEHICULO";
            ventana.ShowDialog();
        }

        private void btn_guardado_Click(object sender, RoutedEventArgs e)
        {
            if (cb_modelo.Text.Equals("") || cb_modelo.Text.Equals("") || txt_patente.Equals(""))
            {
                MessageBox.Show("Faltan Datos", "ERROR REGISTRO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool vigencia;
                if (txt_vigencia.Text.Equals("Si"))
                    vigencia = true;
                else
                    vigencia = false;

                if (Regex.IsMatch(txt_anho.Text, "^[0-9]{4}$"))
                {
                    int anho = Int32.Parse(txt_anho.Text);
                    if (anho <= 1980 || anho <= 2020)
                    {
                            if (txt_primaAnual.Text.Equals("0") || txt_primaMensual.Text.Equals("0"))
                            {
                                MessageBox.Show("ERROR EN LAS PRIMAS");
                            }
                            else
                            {
                                Con_vehiculo vehiculo = new Con_vehiculo();
                                Vehiculo vehiculo_save = new Vehiculo();
                                Con_modeloVehiculo Mo_vehiculo = new Con_modeloVehiculo();
                                var id_marca = vehiculo.MarcaPorId(cb_marca.SelectedItem.ToString());
                                var id_modelo = Mo_vehiculo.buscarIDmodelo(cb_modelo.SelectedItem.ToString());
                                vehiculo_save.Patente = txt_patente.Text.ToUpper();
                                vehiculo_save.IdMarca = id_marca;
                                vehiculo_save.IdModelo = id_modelo;
                                vehiculo_save.Anho = anho;

                                Contrato contrato = new Contrato();
                                Con_Contrato con = new Con_Contrato();
                                contrato.Numero = txt_fecha.Text;
                                contrato.FechaCreacion = fecha;
                                contrato.FechaTermino = dp_fechaTermino.SelectedDate.Value;
                                contrato.RutCliente = txt_rut.Text + "-" + txt_dv.Text;
                                contrato.CodigoPlan = cb_idPlan.SelectedItem.ToString();
                                contrato.IdTipoContrato = 20;
                                contrato.FechaInicioVigencia = dp_fechaInicio.SelectedDate.Value;
                                contrato.FechaFinVigencia = dp_fechaTermino.SelectedDate.Value;
                                contrato.Vigente = vigencia;
                                contrato.DeclaracionSalud = false;
                                contrato.PrimaAnual = Double.Parse(txt_primaAnual.Text); 
                                contrato.PrimaMensual = Double.Parse(txt_primaMensual.Text); 
                                contrato.Observaciones = txt_observaciones.Text;
                                contrato.Vehiculo.Add(vehiculo_save);

                                con.generarContrato(contrato);
                                // ------------- TESTEO DE INSERT A CONTRATO ------------------
                                MessageBox.Show("DATOS GUARDADOS CORRECTAMENTE", "REGISTRO COMPLETO", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                       
                    }
                    else
                        MessageBox.Show("AÑO INCORRECTO");
                }
                else
                    MessageBox.Show("PATENTE INVALIDA");
            }    

        }

        private void cb_marca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_modelo.IsEnabled = true;
            Con_modeloVehiculo vehiculo = new Con_modeloVehiculo();
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
            if (validacion.rutValido(txt_rut.Text, txt_dv.Text))
            {
                if (controlador_cliente.existeCliente(txt_rut.Text, txt_dv.Text))
                {
                    cliente = controlador_cliente.obtenerCliente(txt_rut.Text, txt_dv.Text);
                    Parametro p = new Parametro();
                    p.cliente = cliente;
                    p.idPlan = "";
                    llenarCampos(p);
                        
                    
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

        private void btn_buscarCliente_Click(object sender, RoutedEventArgs e)
        {
            ListarClientes ventana = new ListarClientes("Seguros_auto");
            this.Close();
            ventana.ShowDialog();
        }

        private void txt_patente_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_patente.Text.Equals(""))
            {
                txt_patente.Text = "";
                txt_patente.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void cb_modelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_patente.Foreground = new SolidColorBrush(Colors.Black);
            txt_patente.Text = "";
            txt_patente.IsEnabled = true;
            btn_validarPatente.IsEnabled = true;
        }

        private void btn_validarPatente_Click(object sender, RoutedEventArgs e)
        {
            Con_vehiculo vehiculo = new Con_vehiculo();
            if (vehiculo.patenteValida(txt_patente.Text))
            {
                if (vehiculo.existePatente(txt_patente.Text))
                {
                    txt_anho.IsEnabled = false;
                    MessageBox.Show("Patente ya existe", "Error patente", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    txt_anho.IsEnabled = true;                    
                }
            }
            else
                MessageBox.Show("Patente invalida", "Error patente", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void txt_anho_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_anho.Text.Equals(""))
            {
                txt_anho.Text = "1980";
            }
        }


        private void dp_fechaTermino_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Validacion validacion = new Validacion();
            DateTime fecha_1 = dp_fechaTermino.SelectedDate.Value;
            if (!validacion.ContratoFecha(fecha_1))
            {
                txt_vigencia.Text = "No";
            }
            else
            {
                txt_vigencia.Text = "Si";
            }
        }

        private void dp_fechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dp_fechaTermino.IsEnabled = true;
        }





        private void btn_calcularPrimas_Click(object sender, RoutedEventArgs e)
        {
            if (cb_idPlan.SelectedIndex != -1)
            {
                if (Regex.IsMatch(txt_anho.Text, "^[0-9]{4}$"))
                {
                    int anho = Int32.Parse(txt_anho.Text);
                    if (anho >= 1980 && anho <= 2020)
                    {
                        var pregunta = MessageBox.Show("¿Esta seguro que quiere calcular? una vez realizado, no podra hacer mas modificaciones",
                        "Confirmar accion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (pregunta == MessageBoxResult.Yes)
                        {
                            calcularPrima();
                            btn_guardado.IsEnabled = true;

                            txt_patente.IsEnabled = false;
                            txt_anho.IsEnabled = false;
                            dp_fechaInicio.IsEnabled = false;
                            dp_fechaTermino.IsEnabled = false;
                            txt_observaciones.IsEnabled = false;
                            cb_marca.IsEnabled = false;
                            cb_idPlan.IsEnabled = false;
                            cb_modelo.IsEnabled = false;

                            btn_buscarCliente.IsEnabled = false;
                            btn_buscar_en_lista.IsEnabled = false;
                            btn_buscarRut.IsEnabled = false;
                            btn_validarPatente.IsEnabled = false;
                            btn_calcularPrimas.IsEnabled = false;
                        }
                    }
                    else
                        MessageBox.Show("ERROR EN EL AÑO DEL VEHICULO", "ERROR AÑO VEHICULO", MessageBoxButton.OK, MessageBoxImage.Error);
                }                
            }
            else
            {
                MessageBox.Show("ERROR EN EL ID DEL PLAN","ERROR ID PLAN",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }

        private void calcularPrima()
        {
            Validacion validacion = new Validacion();
            DateTime fecha_1 = dp_FechaNacimiento.SelectedDate.Value;
            double calculo = 0;
            if (Regex.IsMatch(txt_anho.Text, "^[0-9]{4}$"))
            {
                int anho = Int32.Parse(txt_anho.Text);
                if (anho >= 1980 && anho <=2020)
                {

                    if (anho == 2020)
                    {
                        calculo += 1.2;
                    }
                    else if (anho < 2020 && anho >= 2015)
                    {
                        calculo += 0.8;
                    }
                    else
                    {
                        calculo += 0.4;
                    }


                        if (validacion.MayorEdad(fecha_1))
                        {
                            int edad = validacion.edadContratante(fecha_1);
                            if (edad >= 18 && edad <= 25)
                            {
                                calculo += 1.2;
                            }
                            else if (edad >= 26 && edad <= 45)
                            {
                                calculo += 2.4;
                            }
                            else
                                calculo += 3.2;
                            if (cb_idPlan.SelectedIndex != -1)
                            {
                                Con_Plan conplan = new Con_Plan();
                                Plan plan = conplan.obtenerPlanPorId(cb_idPlan.SelectedItem.ToString());
                                calculo += plan.PrimaBase;
                                if (cb_sexo.SelectedItem.Equals("HOMBRE"))
                                {
                                    calculo += 0.8;
                                }
                                else
                                    calculo += 0.4;

                                txt_primaAnual.Text = calculo.ToString();
                                txt_primaMensual.Text = Math.Round((calculo/12),2).ToString();
                            }
                        }
                        else
                            MessageBox.Show("DEBE SER MAYOR DE EDAD");
                }
                else
                    MessageBox.Show("Año invalido");
            }
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
