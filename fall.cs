//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    internal class fall
    {
        public ArrayList lista = new ArrayList();
       

     
        
        internal static void test(double a, ArrayList lista)
        {
            int b;
            Convert.ToDouble(lista); // convert จาก string เป็น double
            b = lista.BinarySearch(a);
            if (b>0)
            {
                Console.WriteLine("asd");
            }
            
        }
    }
}