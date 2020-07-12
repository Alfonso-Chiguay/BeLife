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
        }

        public void llenarCampos(Parametro p_cliente)
        {
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_rut.Text = p_cliente.cliente.RutCliente.Split('-')[0];
            txt_dv.Text = p_cliente.cliente.RutCliente.Split('-')[1];
            txt_nombres.Text = p_cliente.cliente.Nombres;
            txt_apellidos.Text = p_cliente.cliente.Apellidos;
            dp_fechaNacimiento.SelectedDate = p_cliente.cliente.FechaNacimiento;
            Con_Sexo con_sexo = new Con_Sexo();
            Con_EstadoCivil con_civil = new Con_EstadoCivil();

            cb_sexo.Items.Add(con_sexo.sexoPorId(p_cliente.cliente.IdSexo));
            cb_sexo.SelectedIndex = 0;
            cb_estadoCivil.Items.Add(con_civil.ecivilPorId(p_cliente.cliente.IdEstadoCivil));
            cb_estadoCivil.SelectedIndex = 0;

            txt_rut.IsEnabled = false;
            txt_dv.IsEnabled = false;

            if (!p_cliente.idPlan.Equals(""))
            {
                txt_idPlan.Text = p_cliente.idPlan;
            }
        }
        DateTime fecha = DateTime.Now;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txt_codigoSeguro.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_codigoSeguro.IsEnabled = false;
            if (lbl_postal.Content.Equals("CODIGO POSTAL OK"))
            {
                if (lbl_fecha.Content.Equals("TAMOS BIEN DIJO EL KOKU"))
                {
                    if (!txt_direccion.Text.Equals("Calle, Departamento,Torre,etc.") && txt_direccion.Text.Length > 5)
                    {
                        if (cb_region.Text.Equals("") || cb_comuna.Text.Equals(""))
                        {
                            MessageBox.Show("Ingrese direccion");
                        }
                        else
                        {
                            if (txt_valorInmueble.Text.Equals("0") || txt_valorInmueble.Text.Equals("00") || txt_valorInmueble.Text.Equals("000")
                                || txt_valorInmueble.Text.Equals("0000"))
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
                                    Cont_region.asegurarVivienda(vivienda);
                                    MessageBox.Show("DATOS GUARDADOS CORRECTAMENTE", "REGISTRO COMPLETO", MessageBoxButton.OK, MessageBoxImage.Information);
                                    this.Close();
                                }
                            }
                        }
                    }
                    else
                        MessageBox.Show("Ingrese direccion");
                }
                else
                    MessageBox.Show("Fecha Incorrecta");
            }
            else
                MessageBox.Show("Codigo Postal Invalido");
            //----------------CODIGO PODRIA SER INNECESARIO -------------------------

            /*if (!txt_direccion.Text.Equals("Calle, Departamento,Torre,etc.") && txt_direccion.Text.Length > 5)
               if (txt_valorInmueble.Text.Equals("0") || txt_valorInmueble.Text.Equals("00") || txt_valorInmueble.Text.Equals("000")
                   || txt_valorInmueble.Text.Equals("0000"))
               {
                   MessageBox.Show("VALOR INMUEBLE DEBE SER DISTINTO DE 0");
               }
               else if (Regex.IsMatch(txt_valorInmueble.Text, "^[1-9]{1}$") || Regex.IsMatch(txt_valorInmueble.Text, "^[0-9]{2}$") ||
                   Regex.IsMatch(txt_valorInmueble.Text, "^[0-9]{3}$") || Regex.IsMatch(txt_valorInmueble.Text, "^[0-9]{4}$"))
                    {
                       if (Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{1}$") || Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{2}$") ||
                       Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{3}$") || Regex.IsMatch(txt_valor_contenido.Text, "^[0-9]{4}$"))
                       {
                           if (lbl_fecha.Content.Equals("TAMOS BIEN DIJO EL KOKU"))
                           {
                               if (lbl_postal.Content.Equals("CODIGO POSTAL OK"))
                               {
                                   if (cb_region.Text.Equals("") || cb_comuna.Text.Equals(""))
                                   {
                                       MessageBox.Show("Ingrese region / comuna");
                                   }
                                   else
                                   {
                                       /*DateTime fecha_1 = dp_fechaConstruccion.SelectedDate.Value;
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
                                       Cont_region.asegurarVivienda(vivienda);
                                       MessageBox.Show("DATOS GUARDADOS CORRECTAMENTE", "REGISTRO COMPLETO", MessageBoxButton.OK, MessageBoxImage.Information);
                                       this.Close();
                                   }
                               }
                               else
                               {
                                   MessageBox.Show("Codigo Postal Invalido");
                               }
                           }
                           else
                               MessageBox.Show("Fecha Incorrecta");
                       }
                       else
                           MessageBox.Show("VALOR CONTENIDO INVALIDO");
                    }

               else
                   MessageBox.Show("VALOR INMUEBLE INVALIDO");
           else
               MessageBox.Show("Ingrese direccion");*/
            //----------------CODIGO PODRIA SER INNECESARIO -------------------------
        }

        private void cb_region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_comuna.IsEnabled = true;
            Con_Comuna comuna = new Con_Comuna();
            cb_comuna.ItemsSource = comuna.listaComunaPorRegion(cb_region.SelectedItem.ToString());
        }

        private void dp_fechaConstruccion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Validacion validacion = new Validacion();
            DateTime fecha = dp_fechaConstruccion.SelectedDate.Value;
            lbl_fecha.Foreground = new SolidColorBrush(Colors.White);
            if(!validacion.viviendaFecha(fecha))
            {
                if (fecha.Year > DateTime.Today.Year)
                {
                    lbl_fecha.Content = "AÑO INCORRECTO";
                }
                else if (fecha.Month > DateTime.Today.Month)
                {
                    lbl_fecha.Content = "MES INCORRECTO";
                }
                else
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
    }
}
