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
using BaseDatos;
using BaseDatos.Controlador;
using Negocio;
using Negocio.Funciones;
using System.Text.RegularExpressions;
using static BeLife.Vistas.AdministrarCliente;

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para Seguro_hogar.xaml
    /// </summary>
    public partial class Seguro_hogar : Window
    {
        Cliente cliente;
        string idPlan;
        

        public Seguro_hogar()
        {
            InitializeComponent();
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            Con_region comuna = new Con_region();
            cb_region.ItemsSource = comuna.listarRegion();
            cb_idplan.Items.Add("HOG01");
            cb_idplan.Items.Add("HOG02");
            cb_idplan.Items.Add("HOG03");
            txt_codigoPostal.IsEnabled = false;
            dp_fechaConstruccion.IsEnabled = false;
            cb_region.IsEnabled = false;
            txt_valorInmueble.IsEnabled = false;
            txt_valor_contenido.IsEnabled = false;
            txt_comentarios.IsEnabled = false;
            btn_guardar.IsEnabled = false;
            dp_inicio_contrato.IsEnabled = false;
            txt_direccion.IsEnabled = false;



        }
        public Seguro_hogar(Parametro p)
        {
            InitializeComponent();
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            Con_region comuna = new Con_region();
            cb_region.ItemsSource = comuna.listarRegion();
            cliente = p.cliente;
            idPlan = p.idPlan;
            llenarCampos(p);
        }

        public Seguro_hogar(Cliente c)
        {
            InitializeComponent();
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            Con_region comuna = new Con_region();
            cb_region.ItemsSource = comuna.listarRegion();
            cliente = c;
            Parametro p = new Parametro();
            p.cliente = c;
            p.idPlan = "";
            llenarCampos(p);
            cb_idplan.Items.Add("HOG01");
            cb_idplan.Items.Add("HOG02");
            cb_idplan.Items.Add("HOG03");
        }

        public void llenarCampos(Parametro p_cliente)
        {
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_rut.Text = p_cliente.cliente.RutCliente.Split('-')[0];
            txt_dv.Text = p_cliente.cliente.RutCliente.Split('-')[1];
            txt_nombres.Text = p_cliente.cliente.Nombres;
            txt_apellidos.Text = p_cliente.cliente.Apellidos;
            
            Con_Sexo con_sexo = new Con_Sexo();
            Con_EstadoCivil con_civil = new Con_EstadoCivil();

            cb_sexo.Items.Add(con_sexo.sexoPorId(p_cliente.cliente.IdSexo));
            cb_sexo.SelectedIndex = 0;
            cb_estadoCivil.Items.Add(con_civil.ecivilPorId(p_cliente.cliente.IdEstadoCivil));
            cb_estadoCivil.SelectedIndex = 0;

            txt_rut.IsEnabled = false;
            txt_dv.IsEnabled = false;
            btn_guardar.IsEnabled = false;

            if (!p_cliente.idPlan.Equals(""))
            {
                cb_idplan.Items.Add(p_cliente.idPlan);
                cb_idplan.SelectedIndex = 0;
                cb_idplan.IsEnabled = false;
            }
        }
        DateTime fecha = DateTime.Now;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddHHmmss");
            txt_codigoSeguro.IsEnabled = false;
            if (cb_idplan.SelectedIndex != -1)
            {
                if (lbl_postal.Content.Equals("CODIGO POSTAL OK"))
                {
                    if (lbl_fecha.Content.Equals("TAMOS BIEN DIJO EL KOKU"))
                    {
                        if (!txt_direccion.Text.Equals("Calle, Departamento,Torre,etc.") && txt_direccion.Text.Length > 5)
                        {
                            if (cb_region.SelectedIndex == -1 || cb_comuna.SelectedIndex == -1)
                            {
                                MessageBox.Show("Seleccione Region y comuna");
                            }
                            else
                            {
                                int inmueble;
                                if (Int32.TryParse(txt_valorInmueble.Text, out inmueble) && inmueble == 0)
                                {
                                    MessageBox.Show("VALOR INMUEBLE DEBE SER DISTINTO DE 0");
                                }
                                else
                                {
                                    if (Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{1}$") || Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{2}$") ||
                                        Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{3}$") || Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{4}$"))
                                    {
                                        DateTime fecha_1 = dp_fechaConstruccion.SelectedDate.Value;
                                        Con_Comuna Cont_comuna = new Con_Comuna();
                                        Con_region Cont_region = new Con_region();
                                        Vivienda vivienda = new Vivienda();
                                        var id_comuna = Cont_comuna.buscarIDcomuna(cb_comuna.Text);
                                        var id_region = Cont_region.idRegion(cb_region.Text);
                                        vivienda.CodigoPostal = Int32.Parse(txt_codigoPostal.Text);
                                        vivienda.Anho = fecha_1.Year;
                                        vivienda.Direccion = txt_direccion.Text;
                                        vivienda.ValorInmueble = Int32.Parse(txt_valorInmueble.Text);
                                        vivienda.ValorContenido = Int32.Parse(txt_valor_contenido.Text);
                                        vivienda.IdRegion = id_region;
                                        vivienda.IdComuna = id_comuna;

                                        idPlan = cb_idplan.SelectedItem.ToString();

                                        Contrato contrato = new Contrato();
                                        contrato.Numero = txt_codigoSeguro.Text;
                                        contrato.FechaCreacion = fecha;
                                        contrato.FechaTermino = dp_inicio_contrato.SelectedDate.Value.AddYears(1);
                                        contrato.RutCliente = cliente.RutCliente;
                                        contrato.CodigoPlan = idPlan;
                                        contrato.IdTipoContrato = 30;
                                        contrato.FechaInicioVigencia = dp_inicio_contrato.SelectedDate.Value;
                                        contrato.FechaFinVigencia = dp_inicio_contrato.SelectedDate.Value.AddYears(1);
                                        contrato.Vigente = true;
                                        contrato.DeclaracionSalud = true;
                                        contrato.PrimaAnual = Double.Parse(lbl_prima_anual.Content.ToString());
                                        contrato.PrimaMensual = Double.Parse(lbl_prima_mensual.Content.ToString());
                                        contrato.Observaciones = txt_comentarios.Text;
                                        contrato.Vivienda.Add(vivienda);

                                        Con_Contrato con_contrato = new Con_Contrato();
                                        con_contrato.generarContrato(contrato);




                                        MessageBox.Show("DATOS GUARDADOS CORRECTAMENTE", "REGISTRO COMPLETO", MessageBoxButton.OK, MessageBoxImage.Information);
                                        this.Close();
                                    }
                                }
                            }
                        }
                        else
                            MessageBox.Show("Ingrese una direccion valida");
                    }
                    else
                        MessageBox.Show("Fecha Incorrecta");
                }
                else
                    MessageBox.Show("Codigo Postal Invalido");
            }
            else
                MessageBox.Show("Debe seleccionar un tipo de contrato");

           
        }

        private void cb_region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_comuna.IsEnabled = true;
            Con_Comuna comuna = new Con_Comuna();
            cb_comuna.ItemsSource = comuna.listaComunaPorRegion(cb_region.SelectedItem.ToString());
        }

        private void dp_fechaConstruccion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lbl_fecha.Visibility = Visibility.Hidden;
            Validacion validacion = new Validacion();
            DateTime fecha = dp_fechaConstruccion.SelectedDate.Value;
            lbl_fecha.Foreground = new SolidColorBrush(Colors.White);
            if(!validacion.viviendaFecha(fecha))
            {
                if (fecha.Year > DateTime.Today.Year)
                {
                    lbl_fecha.Content = "AÑO INCORRECTO";
                }
                else if (fecha.Month > DateTime.Today.Month && fecha.Year == DateTime.Today.Year)
                {
                    lbl_fecha.Content = "MES INCORRECTO";
                }
                else if (fecha.Year < 1910)
                {
                    lbl_fecha.Content = "MUY VIEJA";
                }
            }
            else
            {
                lbl_fecha.Content = "TAMOS BIEN DIJO EL KOKU";
            }
        }

        private void txt_direccion_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_direccion.Text.Equals("Calle, Departamento,Torre,etc."))
            {
                txt_direccion.Text = "";
                txt_direccion.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_direccion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_direccion.Text.Equals(""))
            {
                txt_direccion.Foreground = new SolidColorBrush(Colors.Gray);
                txt_direccion.Text = "Calle, Departamento,Torre,etc.";
            }
        }

        private void txt_valorInmueble_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_valorInmueble.Text.Equals("0"))
            {
                txt_valorInmueble.Text = "";
                txt_valorInmueble.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_valorInmueble_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_valorInmueble.Text.Equals(""))
            {
                txt_valorInmueble.Foreground = new SolidColorBrush(Colors.Gray);
                txt_valorInmueble.Text = "0";
            }
            
        }

        private void txt_codigoPostal_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_codigoPostal.Text.Equals("9999999"))
            {
                txt_codigoPostal.Text = "";
                txt_codigoPostal.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        
        private void txt_codigoPostal_LostFocus(object sender, RoutedEventArgs e)
        {
            lbl_postal.Visibility = Visibility.Hidden;
            if (txt_codigoPostal.Text.Equals("0000000"))
            {
                lbl_postal.Content = "CODIGO POSTAL INVALIDO";
                MessageBox.Show("CODIGO POSTAL INVALIDO", "ERROR CODIGO POSTAL", MessageBoxButton.OK, MessageBoxImage.Error);
            }          
            else if (Regex.IsMatch(txt_codigoPostal.Text, "^[0-9]{7}$"))
            {
                var postal = Int32.Parse(txt_codigoPostal.Text);
                if (postal >= 1000000)
                    lbl_postal.Content = "CODIGO POSTAL OK";
                else
                {
                    lbl_postal.Foreground = new SolidColorBrush(Colors.Red);
                    lbl_postal.Content = "CODIGO POSTAL INVALIDO";
                }
                
            }
            else
            {
                lbl_postal.Content = "CODIGO POSTAL INVALIDO";
                MessageBox.Show("CODIGO POSTAL INVALIDO", "ERROR CODIGO POSTAL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txt_valor_contenido_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txt_valor_contenido.Text.Equals("0"))
            {
                txt_valor_contenido.Text = "";
                txt_valor_contenido.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txt_valor_contenido_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_valor_contenido.Text.Equals(""))
            {
                txt_valor_contenido.Foreground = new SolidColorBrush(Colors.Gray);
                txt_valor_contenido.Text = "0";
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

        private void txt_rut_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_rut.Text.Equals(""))
            {
                txt_rut.Text = "Ej: 12345678";
                txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void txt_dv_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txt_dv.Text.Equals(""))
            {
                txt_dv.Text = "N";
                txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
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

        private void btn_buscarRut_Click(object sender, RoutedEventArgs e)
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
                    txt_codigoSeguro.IsEnabled = false;
                    txt_codigoPostal.IsEnabled = true;
                    dp_fechaConstruccion.IsEnabled = true;
                    cb_region.IsEnabled = true;
                    txt_valorInmueble.IsEnabled = true;
                    txt_valor_contenido.IsEnabled = true;
                    txt_comentarios.IsEnabled = true;
                    btn_guardar.IsEnabled = true;
                    dp_inicio_contrato.IsEnabled = true;
                    txt_direccion.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Cliente no existe", "Error cliente", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_rut.Foreground = new SolidColorBrush(Colors.Gray);
                    txt_dv.Foreground = new SolidColorBrush(Colors.Gray);
                    txt_rut.Text = "Ej: 12345678";
                    txt_dv.Text = "N";
                }
            }
            else
                MessageBox.Show("RUT invalido", "Campo : RUT", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_buscar_listado_Click(object sender, RoutedEventArgs e)
        {
            ListarClientes listado = new ListarClientes("Seguro_hogar");
            this.Close();
            listado.ShowDialog();
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void calcularPrima()
        {
            if (dp_fechaConstruccion.SelectedDate.HasValue)
            {
                double calculo = 0;
                int anio_construccion = dp_fechaConstruccion.SelectedDate.Value.Year;
                anio_construccion = fecha.Year - anio_construccion;
                if (anio_construccion <= 5)
                    calculo += 1;
                else if (anio_construccion <= 10)
                    calculo += 0.8;
                else if (anio_construccion <= 30)
                    calculo += 0.4;
                else
                    calculo += 0.2;


                if (cb_region.SelectedIndex != -1)
                {
                    string region = cb_region.SelectedItem.ToString();
                    if (region.Equals("Metropolitana de Santiago"))
                        calculo += 3.2;

                    else
                        calculo += 2.8;

                    if (cb_idplan.SelectedIndex != -1)
                    {
                        Con_Plan conplan = new Con_Plan();
                        Plan plan = conplan.obtenerPlanPorId(cb_idplan.SelectedItem.ToString());
                        calculo += plan.PrimaBase;
                        double inmueble;
                        double contenido;
                        if (Double.TryParse(txt_valorInmueble.Text, out inmueble))
                            calculo += inmueble;

                        if (Double.TryParse(txt_valor_contenido.Text, out contenido))
                            calculo += contenido;

                        lbl_prima_anual.Content = calculo.ToString();
                        lbl_prima_mensual.Content = Math.Round((calculo / 12),2).ToString();
                    }
                }

                
            }
            

            
            
            
            
        }

        private void btn_calcular_primas_Click(object sender, RoutedEventArgs e)
        {
            var pregunta = MessageBox.Show("¿Esta seguro que quiere calcular? una vez realizado, no podra hacer mas modificaciones",
                "Confirmar accion", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if(pregunta == MessageBoxResult.Yes)
            {
                calcularPrima();
                txt_codigoPostal.IsEnabled = false;
                dp_fechaConstruccion.IsEnabled = false;
                cb_region.IsEnabled = false;
                cb_comuna.IsEnabled = false;
                txt_valorInmueble.IsEnabled = false;
                txt_valor_contenido.IsEnabled = false;
                txt_comentarios.IsEnabled = false;
                btn_guardar.IsEnabled = true;
                dp_inicio_contrato.IsEnabled = false;
                txt_direccion.IsEnabled = false;

            }
            
        }
    }
}
