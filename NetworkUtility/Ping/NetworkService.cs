using NetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Ping
{
    public class NetworkService
    {
        readonly IDNS _dnsService;
        public NetworkService(IDNS dNS) 
        {
            _dnsService = dNS;
        }
        public string SendPing()
        {
            var dnsSuccess = _dnsService.SendDNS();
            if (dnsSuccess)
            {
                return "Ping";
            }
            return "Ping failed";
        }

        public int PingTimeout(int a, int b) 
        {
            return a + b;
        }

        public PingOptions GetPingOptions() 
        { 
            return new PingOptions()
            {
                DontFragment = true,
                Ttl = 1,
            };
        }
        public DateTime LastPingDate() 
        { 
            return DateTime.Now;
        }
        public IEnumerable<PingOptions> GetPingOptionsList() 
        { 
            IEnumerable<PingOptions> pingOptions = new List<PingOptions>()
            {
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1,
                },    
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1,
                },     
                new PingOptions()
                {
                    DontFragment = true,
                    Ttl = 1,
                },
            };
            return pingOptions;
        }
    }
}
