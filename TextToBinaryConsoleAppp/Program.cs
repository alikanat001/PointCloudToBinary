using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TextToBinaryConsoleAppp
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new Form1());
            
            

            //var ffs = new FileStream("demo.bin", FileMode.Open, FileAccess.Read);
            //using (var bs = new BufferedStream(ffs))
            //using (var r = new BinaryReader(bs))
            //{
            //    while (r.BaseStream.Position != r.BaseStream.Length)
            //    {

            //        int numPoints = r.ReadInt32();
            //        var ColorCoef = r.ReadSingle();
            //        //Console.WriteLine(ColorCoef);
            //        test = new Vector3[numPoints];
            //        for (int i = 0; i < numPoints; i++)
            //        {
            //            var x = r.ReadSingle();
            //            var y = r.ReadSingle();
            //            var z = r.ReadSingle();
            //            test[i] = new Vector3(x, y, z);
            //            //test[i].Display();
            //        }

            //    }
            //}
            
        
        


        }
    }
}
