using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NativeWifi;
using ScreenRotation;
using Wox.Plugin;

namespace ConsoleTest
{
    class Program
    {
        static WlanClient client = new WlanClient();

        /// <summary>
        /// 字符串转Hex
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToHex(string str)
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
        static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }

        private static void SelectWifiWithNoKey(string name, ref WlanClient.WlanInterface wlanIface)
        {
            // Connects to a known network with WEP security
            string profileName = name; // this is also the SSID
            string mac = StringToHex(profileName); // 

            //string key = "";
            string myProfileXML = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>manual</connectionMode><MSM><security><authEncryption><authentication>open</authentication><encryption>none</encryption><useOneX>false</useOneX></authEncryption></security></MSM></WLANProfile>", profileName, mac);
            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, myProfileXML, true);
            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
        }

        static void SelectCurrentWifi(Wlan.WlanAvailableNetwork network, WlanClient.WlanInterface wlanIface)
        {
            string name = GetStringForSSID(network.dot11Ssid);
            string xml = "";
            foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
            {
                if(profileInfo.profileName == name) // this is typically the network's SSID
                    xml = wlanIface.GetProfileXml(name);
                else
                {
                    if(network.dot11DefaultCipherAlgorithm == Wlan.Dot11CipherAlgorithm.None)
                    {
                        SelectWifiWithNoKey(name, ref wlanIface);
                    }
                    else
                    {
                        Console.WriteLine("没有WiFiProfile信息，请先创建登录信息");
                    }

                    //Console.ReadKey();
                            
                }
            }
 


        }

        static void Main(string[] args)
        {
            //            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            //            {
            //                // Lists all networks with WEP security
            //                Wlan.WlanAvailableNetwork[] networks = wlanIface.GetAvailableNetworkList(0);
            //                List<Wlan.WlanAvailableNetwork> availabelNetworks = new List<Wlan.WlanAvailableNetwork>();
            //
            //                foreach (Wlan.WlanAvailableNetwork network in networks)
            //                {
            //                    if(network.networkConnectable == true && network.wlanSignalQuality >= 30) 
            //                    {
            //                        Console.WriteLine("Found network with SSID {0} with SingalVolume {1}.", GetStringForSSID(network.dot11Ssid),network.wlanSignalQuality);
            //                        availabelNetworks.Add(network);
            //                    }
            //                }
            //                availabelNetworks.Sort(
            //                    (x, y) =>
            //                    {
            //                        int value = y.wlanSignalQuality.CompareTo(x.wlanSignalQuality);
            //                        return value;
            //                });
            //                Console.WriteLine("=======================");
            //                       
            //                foreach (var network in availabelNetworks)
            //                {
            //                    Console.WriteLine("Found network with SSID {0} with SingalVolume {1}.", GetStringForSSID(network.dot11Ssid), network.wlanSignalQuality);
            //                    
            //                    if(GetStringForSSID(network.dot11Ssid) == "BUAA-WiFi")
            //                    {
            //                        //SelectCurrentWifi(network,wlanIface);
            //                    }
            //                }
            //
            //
            //
            //                Console.WriteLine(wlanIface.CurrentConnection.profileName);
            //            }

            var d1 = MyDisplay.GetDisplay(1);
            Console.WriteLine(d1);
            var d2 = MyDisplay.GetDisplay(2);
            Console.WriteLine(d2);

            ScreenRotation.Main tmp = new Main();
            tmp.Init(null);
            var items = tmp.Query(new Query()
            {
                ActionKeyword = "RS",
                Terms = new string[]{"RS","2","u"}
            });
            
            string strSHow = string.Empty;
            foreach (var v in Screen.AllScreens)
            {
                strSHow += v.ToString() + "\r\n";
            }
            
            MyDisplay.Rotate(2, MyDisplay.Orientations.DEGREES_CW_180);
            Console.WriteLine(strSHow);
            Console.ReadLine();
            
            MyDisplay.Rotate(2, MyDisplay.Orientations.DEGREES_CW_270);

        }
        
    }
}
