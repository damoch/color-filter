using ColorFilterLib;
using System;
using System.Diagnostics;
using System.Reflection;

namespace ColorFilter
{
    class Program
    {
        private static Filter _filter;
        static void Main(string[] args)
        {
            Console.WriteLine($"ColorFilter v{Version}");
            if(args.Length != 7)
            {
                Console.WriteLine("Missing parameters.\nUsage: ColorFilter.exe <filename> <ColorRed> <ColorGreen> <ColorBlue> <ToleranceRed> <ToleranceGreen> <ToleranceBlue>");
                Console.ReadKey();
                return;
            }
            _filter = new Filter();
            try
            {
                _filter.LoadImage(args[0]);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to load {args[0]} - {ex.Message}");
                Console.ReadKey();
                return;
            }

            try
            {

                var r = int.Parse(args[1]);
                var g = int.Parse(args[2]);
                var b = int.Parse(args[3]);

                _filter.SetColorToFilter(r, g, b);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Incorrect color values: {args[1]} {args[2]} {args[3]} - {ex.Message} Only values between 0-255 are accepted as parameters");
                Console.ReadKey();
                return;
            }

            try
            {
                var rTolerance = int.Parse(args[4]);
                var gTolerance = int.Parse(args[5]);
                var bTolerance = int.Parse(args[6]);

                _filter.SetTolerance(rTolerance, gTolerance, bTolerance);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Incorrect tolerance values: {args[1]} {args[2]} {args[3]} - {ex.Message} Only values between 0-100 are accepted as parameters");
                Console.ReadKey();
                return;
            }


            _filter.FilterColors();
            _filter.ApplyFilter();
            _filter.SaveResult();
            _filter.ReleaseResources();
            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();
        }

        public static string Version
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                return String.Format("{0}.{1}", fvi.ProductMajorPart, fvi.ProductMinorPart);
            }
        }
    }
}
