using System;
using System.Text;

namespace ChuongTrinhQuanLyXe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var ds = new DsXe();
            ds.Menu();
        }
    }
}
