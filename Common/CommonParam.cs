using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using System.Windows.Forms;
using System.Net;
using System.Data;
using System.Xml;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Model_v1;

namespace Common
{
    public class CommonParam
    {
        public static string GetNowDateTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public static M_kv IP4;

        public static string GetLocalIpAddress
        {
            get
            {
                if (IP4 != null)
                    return IP4.tag.ToString();

                ///兼容处理
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (
                        adapter.OperationalStatus == OperationalStatus.Up &&
                        adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback
                        )
                    {
                        IPInterfaceProperties ip = adapter.GetIPProperties();
                        UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                        foreach (UnicastIPAddressInformation ipadd in ipCollection)
                        {
                            if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ipadd.Address.ToString();
                            }
                        }
                    }
                }

                return null;

                //if (false)
                //{
                //    string strHostName = Dns.GetHostName();  //得到本机的主机名
                //    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP
                //    return ipEntry.AddressList.FirstOrDefault<IPAddress>(x => !x.IsIPv6LinkLocal && x.AddressFamily == AddressFamily.InterNetwork).ToString(); //假设本地主机为单网卡
                //}
            }
        }

        public static List<M_kv> GetAllNetwork()
        {
            List<M_kv> _list = new List<M_kv>();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                if (
                    adapter.OperationalStatus == OperationalStatus.Up &&
                    adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    )
                {
                    IPInterfaceProperties ip = adapter.GetIPProperties();
                    UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                    foreach (UnicastIPAddressInformation ipadd in ipCollection)
                    {
                        if (ipadd.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            var _ip4 = ipadd.Address.ToString();
                            var _has = _list.Where(x => x.key.ToString() == $"{adapter.Id}|{_ip4}");
                            if (_has != null && _has.Count() <= 0)
                            {
                                _list.Add(new M_kv()
                                {
                                    key = $"{adapter.Id}|{_ip4}",
                                    value = $"{adapter.Name},{_ip4}",
                                    tag = _ip4
                                });
                            }
                        }
                    }
                }
            }
            return _list;
        }
    }
}

