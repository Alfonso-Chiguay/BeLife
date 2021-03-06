﻿using BaseDatos.Controlador;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeLife.Vistas;

namespace BeLife
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_clientes_Click(object sender, RoutedEventArgs e)
        {
            Clientes ventana = new Clientes();
            ventana.ShowDialog();
        }

        private void btn_seguros_Click(object sender, RoutedEventArgs e)
        {
            Seguros ventana = new Seguros();
            ventana.ShowDialog();
        }
    }
}
