using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NativeWifi;

using Wox.Plugin;



namespace Wox.Plugin.SelectWifi
{
    public class Main : IPlugin
    {
        MyWiFi wifi = new MyWiFi();

        public void Init(PluginInitContext context)
        {
            wifi.SelectWlanIface();
        }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();
            var keyword = "";
            List<Wlan.WlanAvailableNetwork> WifiList = new List<Wlan.WlanAvailableNetwork>();
            //MessageBox.Show(query.Search);

            WifiList = wifi.ListAvailableNetworks();

            if (query.Search != "")
            {
                keyword = query.SecondSearch.ToLower();
                //MessageBox.Show(keyword);
                var tmp = new List<Wlan.WlanAvailableNetwork>();
                foreach (var network in WifiList)
                {
                    if (MyWiFi.ConvertToSSID(network.dot11Ssid).ToLower().Contains(keyword))
                        tmp.Add(network);
                }
                WifiList = tmp;
            }
            
            foreach(var network in WifiList)
            {
                Result networkResult = new Result()
                {
                    Title = MyWiFi.ConvertToSSID(network.dot11Ssid) ,
                    IcoPath = IconSelect(network.wlanSignalQuality),
                    SubTitle = String.Format("Found network with Mac {0} with SingalVolume {1}."
                        , MyWiFi.ConvertToMac(MyWiFi.ConvertToSSID(network.dot11Ssid)), network.wlanSignalQuality),
                    Action = (c) =>
                        {
                            return wifi.SelectCurrentWifi(network);
                        }
                };
                if(wifi.wlanIface.InterfaceState == Wlan.WlanInterfaceState.Connected)
                    if(wifi.wlanIface.CurrentConnection.profileName == networkResult.Title)
                        networkResult.Title = networkResult.Title + "(CurrentWifi)" ;

                results.Add(networkResult);
            }
            return results;
        }


        private String IconSelect(uint volume)
        {
            var result = "Images\\";
            if (volume < 50)
                return result + "small.png";
            else if (volume < 75)
                return result + "medium.png";
            else
                return result + "big.png";
        }

    }

}
