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

namespace BeLife.Vistas
{
    /// <summary>
    /// Lógica de interacción para Seguros_auto.xaml
    /// </summary>
    public partial class Seguros_auto : Window
    {
        public Seguros_auto()
        {
            InitializeComponent();
        }

        DateTime fecha = DateTime.Now;

        
        private void btn_buscar_en_lista_Click(object sender, RoutedEventArgs e)
        {
            Lista_contratos ventana = new Lista_contratos();
            ventana.lbl_tipoContrato.Content = "VEHICULO";
            ventana.ShowDialog();
        }

        private void btn_guardado_Click(object sender, RoutedEventArgs e)
        {
            txt_fecha.Text = fecha.ToString("yyyyMMddhhmmss");
            txt_fecha.IsEnabled = false;
            Con_vehiculo vehiculo = new Con_vehiculo();
            cb_marca.ItemsSource = vehiculo.listarMarca();
            MessageBox.Show("HOLA");
        }

        private void btn_buscarContrato_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BUSCAR SEGURO");
        }

        private void cb_marca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_modelo.IsEnabled = true;
        }
    }
}
