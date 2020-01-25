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
        private static INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
        private static INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

        public static bool Menu()
        {
            string fwStatus;
            string not_fwStatus;
            string rule_name = "GTAO block port";
            int protocol = 17;
            string port = "6672";
            
            var rule = firewallPolicy.Rules.OfType<INetFwRule>().Where(n => n.Name == rule_name).FirstOrDefault();

            if (rule == null)
            {
                Console.WriteLine("Rule does not exist! \nPress any key to add firewall rule");
                Console.ReadKey();
                Add(rule_name, protocol, port);
                return true;
            }
            else
            {
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
            }
            Console.WriteLine(rule_name + " = " + fwStatus);
            Console.WriteLine("Blocked port = "+ port + " (UDP) (Outbound Rules)\n");
            Console.WriteLine("Press any key to " + not_fwStatus + " . . .");
            Console.ReadKey();
            rule.Enabled = !rule.Enabled;
            rule = null;
            return true;
        }

        private static void Add(string name, int protocol, string port)
        {
            firewallRule.Enabled = false;
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            firewallRule.Name = name;
            firewallRule.Description = "Allowing users to always connect to empty sessions";
            firewallRule.InterfaceTypes = "All";
            firewallRule.Protocol = protocol;
            firewallRule.LocalPorts = port;

            firewallPolicy.Rules.Add(firewallRule);
        }
    }
}
