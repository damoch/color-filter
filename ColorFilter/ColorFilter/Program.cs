using ColorFilterLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorFilter
{
    class Program
    {
        private static Filter _filter;
        static void Main(string[] args)
        {
            _filter = new Filter();
            _filter.LoadImage("PC130895.JPG");
            _filter.SetColorToFilter(173, 29, 29);
            _filter.SetErrorMarginForColor(40, 20, 20);
            _filter.FilterColors();
            _filter.ApplyFilter();
            _filter.SaveResult("result.jpg");
            Console.ReadKey();
        }
    }
}
