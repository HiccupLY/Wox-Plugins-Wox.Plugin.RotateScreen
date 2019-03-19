using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeWifi;
using System.Windows.Forms;

namespace Wox.Plugin.SelectWifi
{
    class MyWiFi
    {
//         public delegate void MyExceptin();
//         public MyExceptin me;


        private WlanClient client = new WlanClient();
        public WlanClient.WlanInterface wlanIface;
        public List<Wlan.WlanAvailableNetwork> AvailabelNetworks { get; set;}

        public static String ConvertToMac(String ssid)
        {
            return StringToHex(ssid);
        }
        public static String ConvertToSSID(Wlan.Dot11Ssid ssid)
        {
            return GetStringForSSID(ssid);
        }

        public void SelectWlanIface(int index = 0)
        {
            if (client.Interfaces.Length == 0)
            {
                wlanIface = null;
            }
            else
            {
                wlanIface = client.Interfaces[index];
            }
        }
        /// <summary>
        /// 字符串转Hex
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string StringToHex(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.Default.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString().ToUpper());
        }

        /// <summary>
        /// Converts a 802.11 SSID to a string.
        /// </summary>
        private static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        /// <summary>
        /// 选择没有密码的wifi网络
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wlanIface"></param>
        private void SelectWifiWithNoKey(string name)
        {
            // Connects to a known network with WEP security
            string profileName = name; // this is also the SSID
            string mac = StringToHex(profileName); // 

            //string key = "";
            string myProfileXML = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>manual</connectionMode><MSM><security><authEncryption><authentication>open</authentication><encryption>none</encryption><useOneX>false</useOneX></authEncryption></security></MSM></WLANProfile>", profileName, mac);
            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, myProfileXML, true);
            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
        }

        /// <summary>
        /// 选择当前WIFI
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        public bool SelectCurrentWifi(Wlan.WlanAvailableNetwork network)
        {
            string name = GetStringForSSID(network.dot11Ssid);
            string xml = "";

            if (network.dot11DefaultCipherAlgorithm == Wlan.Dot11CipherAlgorithm.None)
            {
                SelectWifiWithNoKey(name);
                return true;
            }
            else
            {
                foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                {
                    if (profileInfo.profileName == name) // this is typically the network's SSID
                    {
                        xml = wlanIface.GetProfileXml(name);
                        wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, name);
                        return true;
                    }
                }
                //既不是空密码而且也没历史记录时
                MessageBox.Show("没有WiFiProfile信息，请先创建登录信息");
                return false;
            }
        }

        /// <summary>
        /// 列出所有可选择网络并按信号强度由高到低排序。
        /// </summary>
        /// <returns>网络列表</returns>
        public List<Wlan.WlanAvailableNetwork> ListAvailableNetworks()
        {
            // Lists all networks with WEP security
            Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
            AvailabelNetworks = new List<Wlan.WlanAvailableNetwork>();

            foreach (Wlan.WlanAvailableNetwork network in networks)
            {
                if (network.networkConnectable == true && network.wlanSignalQuality >= 30)
                {
                    AvailabelNetworks.Add(network);
                }
            }
            AvailabelNetworks.Sort(
                (x, y) =>
                {
                    int value = y.wlanSignalQuality.CompareTo(x.wlanSignalQuality);
                    return value;
                });
            return AvailabelNetworks;
        }

    }
}
