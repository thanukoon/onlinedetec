﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    /// <summary>
    /// Interaction logic for main.xaml
    /// </summary>
    public partial class main : Window
    {
        ArrayList test = new ArrayList();
        public main()
        {
            InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            please.Text = "Please Wait";

            progress.IsIndeterminate = true;
            await LoadData();


            MainWindow c2 = new MainWindow();
            c2.datatest(test);
            c2.Show();
            this.Close();

        }

        private async Task LoadData()
        {
            await Task.Run(() =>
            {

                NN t1 = new NN();
               test.AddRange(t1.yes());

                Thread.Sleep(2000);
            });
         /* foreach (var item in test)
            {
                Console.WriteLine( item);
            } */

        }
    }
}
