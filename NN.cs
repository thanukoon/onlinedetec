using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class NN
    {
        public ArrayList nn =new ArrayList();

        public ArrayList yes()
        {

            //  var lines = File.ReadLines(@"C:\Users\Goon\Desktop\sppj2\Falldetection-test\bin\AnyCPU\Debug\data.csv");


            TextReader tr;
            tr = File.OpenText(@"C:\Users\Goon\Desktop\sppj2\Falldetection-test\bin\AnyCPU\Debug\NN.csv");

           string Actor;
            Actor = tr.ReadLine();

               while (Actor != null)
              {
                  nn.Add(Actor);
                  Actor = tr.ReadLine();
              
              }
           


            return (nn);


        }

        
    }
}
