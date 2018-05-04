using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class NN
    {

        public void yes()
        {
            var lines = File.ReadLines(@"C:\Users\Goon\Desktop\sppj2\Falldetection-test\bin\AnyCPU\Debug\data.csv");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }



            // Console.WriteLine("asd");
        }
    }
}
