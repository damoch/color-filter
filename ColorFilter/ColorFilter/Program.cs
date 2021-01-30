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
            _filter = new Filter(true);
            _filter.LoadImage("baboon.png");
            _filter.SetColorToFilter(255, 80, 140);
            _filter.SetPercentageMargin(100, 12, 100);
            _filter.FilterColors();
            _filter.ApplyFilter();
            _filter.SaveResult();
            Console.ReadKey();
        }
    }
}
