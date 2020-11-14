using System;
using System.Linq;
using NetFwTypeLib;

namespace GTA_Online_block_port
{
    class Firewall
    {
        public static string ruleName = "GTA Online block port";
        public static string port = "6672";
        public static int protocol = 17;
        private static INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
        private static INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

        public static bool isExists()
        {
            if (firewallPolicy.Rules.OfType<INetFwRule>().Where(n => n.Name == ruleName).FirstOrDefault() == null)
            {
                Console.Write("Firewall Rule does not exist!\nPress any key to add a new one . . . ");
                Console.ReadKey();
                addRule();

                return false;
            }
            return true;
        }

        public static bool isEnabled()
        {
            return firewallPolicy.Rules.OfType<INetFwRule>().Where(n => n.Name == ruleName).FirstOrDefault().Enabled;
        }

        public static void Toggle()
        {
            var rule = firewallPolicy.Rules.OfType<INetFwRule>().Where(n => n.Name == ruleName).FirstOrDefault();
            rule.Enabled = !rule.Enabled;
        }

        private static void addRule()
        {
            firewallRule.Enabled = false;
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
            firewallRule.Name = ruleName;
            firewallRule.Description = "Allowing users to always connect to empty sessions";
            firewallRule.InterfaceTypes = "All";
            firewallRule.Protocol = protocol;
            firewallRule.LocalPorts = port;

            firewallPolicy.Rules.Add(firewallRule);
        }
    }
}