using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;


namespace GTA_Online_block_port
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            Console.Title = "GTAO block port";
            while (showMenu)
            {
                Console.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                showMenu = Firewall.Menu();
            }
        }
    }
}
