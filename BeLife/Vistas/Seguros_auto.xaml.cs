﻿using System;
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

        }
        public Seguros_auto(Parametro p)
        {
            InitializeComponent();
            Con_vehiculo vehiculo = new Con_vehiculo();
            cb_marca.ItemsSource = vehiculo.listarMarca();
            cliente = p.cliente;
            idPlan = p.idPlan;
            llenarCampos(p);

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
                TXT_IDPLAN.Text = p_cliente.idPlan;
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
                DateTime fecha_1 = dp_fechaVehiculo.SelectedDate.Value;                                
                Con_vehiculo vehiculo = new Con_vehiculo();
                Vehiculo vehiculo_save = new Vehiculo();
                Con_modeloVehiculo Mo_vehiculo = new Con_modeloVehiculo();
                var id_marca = vehiculo.MarcaPorId(cb_marca.SelectedItem.ToString());
                var id_modelo = Mo_vehiculo.buscarIDmodelo(cb_modelo.SelectedItem.ToString());
                vehiculo_save.Patente = txt_patente.Text.ToUpper();
                vehiculo_save.IdMarca = id_marca;
                vehiculo_save.IdModelo = id_modelo;
                vehiculo_save.Anho = fecha_1.Year;
                
                // ------------- TESTEO DE INSERT ------------------
                Contrato contrato = new Contrato();
                Con_Contrato con = new Con_Contrato();
                contrato.Numero = txt_fecha.Text;
                contrato.FechaCreacion = fecha;
                contrato.FechaTermino = fecha.AddYears(2);
                contrato.RutCliente = txt_rut.Text+"-"+txt_dv.Text;
                contrato.CodigoPlan = idPlan;
                contrato.IdTipoContrato = 20;
                contrato.FechaInicioVigencia = fecha;
                contrato.FechaFinVigencia = fecha.AddYears(2);
                contrato.Vigente = true;
                contrato.DeclaracionSalud = false;
                contrato.PrimaAnual = 1111;
                contrato.PrimaMensual = 2222;
                contrato.Observaciones = "NO LO SE";
                contrato.Vehiculo.Add(vehiculo_save);
                
                con.contratoVehiculo(contrato);
                // ------------- TESTEO DE INSERT A CONTRATO ------------------
                MessageBox.Show("DATOS GUARDADOS CORRECTAMENTE", "REGISTRO COMPLETO", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                //https://social.msdn.microsoft.com/Forums/es-ES/d2941e3c-81cc-40d2-9a59-f8716c1ca5ae/validar-patente-vehiculo?forum=vcses

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

        private void dp_fechaVehiculo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Validacion validacion = new Validacion();
            DateTime fecha = dp_fechaVehiculo.SelectedDate.Value;
            if (!validacion.VehiculoFecha(fecha))
            {
                if(fecha.Year > DateTime.Today.Year)
                {
                    lbl_fecha.Content = "AÑO INCORRECTO";
                }
                else if (fecha.Month > DateTime.Today.Month)
                {
                    lbl_fecha.Content = "MES INCORRECTO";
                }
                else
                {
                    lbl_fecha.Content = "HOLAAA?";
                }
            }
            else
            {
                lbl_fecha.Content = "TAMOS BIEN";
                btn_guardado.IsEnabled = true;
            }
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
            if (Regex.IsMatch(txt_patente.Text, "^[a-z-A-Z]{4}[0-9]{2}$"))
            {
                dp_fechaVehiculo.IsEnabled = true;
                //https://social.msdn.microsoft.com/Forums/es-ES/d2941e3c-81cc-40d2-9a59-f8716c1ca5ae/validar-patente-vehiculo?forum=vcses
            }
            else if (Regex.IsMatch(txt_patente.Text, "^[a-z-A-Z]{3}[0-9]{3}$"))
            {
                dp_fechaVehiculo.IsEnabled = true;
            }
            else if (Regex.IsMatch(txt_patente.Text, "^[a-z-A-Z]{2}[0-9]{4}$"))
            {
                dp_fechaVehiculo.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("PATENTE INVALIDA", "ERROR PATENTE", MessageBoxButton.OK, MessageBoxImage.Error);
                dp_fechaVehiculo.DataContext = DateTime.Today;
                dp_fechaVehiculo.IsEnabled = false;
            }
        }

        
    }
}
