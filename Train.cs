using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    
   class Train
    {
       public static List<double[]> data = new List<double[]>();
        public static  List<double[]> data2 = new List<double[]>();
        private NN cal = new NN();

        public void getlist(  List<double[]> count5 )
        {
            data2.AddRange(count5);
            //getdata2();
        }

        public void getdata2(double[] deta)
        {
          /*  for (int i = 0; i<55;i++)
            {
                data2[0][i] = deta[i]; 
            } */

           


            Console.WriteLine(data.Count);
           

        }
        
    }
    
}
