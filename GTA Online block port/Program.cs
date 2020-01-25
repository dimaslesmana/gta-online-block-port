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

            Console.Title = Firewall.rule_name;
            
            while (showMenu)
            {
                Console.Clear();
                showMenu = Firewall.Menu();
            }
        }
    }
}
