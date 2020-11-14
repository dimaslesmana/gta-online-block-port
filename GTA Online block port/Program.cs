using System;


namespace GTA_Online_block_port
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = Firewall.ruleName;
            while (true)
            {
                Console.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                Menu();
            }
        }

        private static void Menu()
        {
            if (!Firewall.isExists())
                return;

            string status = Firewall.isEnabled() ? "Enabled" : "Disabled";

            Console.WriteLine($"Status       : {status}");
            Console.WriteLine($"Blocked port : {Firewall.port} (Outbound Rules)");
            Console.WriteLine($"Protocol     : UDP ({Firewall.protocol})\n");
            Console.Write("Press any key to toggle . . . ");
            Console.ReadKey();
            Firewall.Toggle();
        }
    }
}
