using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace GTA_Online_block_port
{
    class Firewall
    {
        public static string rule_name = "GTAO block port";
        private static int protocol = 17; // 17 = UDP
        private static string ports = "6672"; // Port
        private static string fwStatus;
        private static string not_fwStatus;
        private static INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
        private static INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

        public static bool Menu()
        {
            var rule = firewallPolicy.Rules.OfType<INetFwRule>().Where(n => n.Name == rule_name).FirstOrDefault();

            if (rule == null)
            {
                Console.WriteLine("Rule does not exist! \nPress any key to add firewall rule");
                Console.ReadKey();
                Console.Clear();
                Add();
                return true;
            }

            if (rule.Enabled)
            {
                fwStatus = "Enabled";
                not_fwStatus = "disable";
            }
            else
            {
                fwStatus = "Disabled";
                not_fwStatus = "enable";
            }
            Console.WriteLine(rule_name + " = " + fwStatus);
            Console.WriteLine("Blocked port = " + ports + " (UDP) (Outbound Rules)\n");
            Console.WriteLine("Press any key to " + not_fwStatus + " . . .");
            Console.ReadKey();
            rule.Enabled = !rule.Enabled;
            return true;
        }

        private static void Add()
        {
            firewallRule.Enabled = false;
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT; // Outbound Rules
            firewallRule.Name = rule_name;
            firewallRule.Description = "Allowing users to always connect to empty sessions";
            firewallRule.InterfaceTypes = "All";
            firewallRule.Protocol = protocol;
            firewallRule.LocalPorts = ports;

            firewallPolicy.Rules.Add(firewallRule);
        }
    }
}
