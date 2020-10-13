using System;
using System.Net;

namespace SeleniumProxyAuthentication
{
    public class Proxy
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="proxyProtocol">Proxy Protocol</param>
        /// <param name="proxy">Proxy</param>
        public Proxy(ProxyProtocols proxyProtocol,string proxy)
        {
            this.ProxyProtocol = proxyProtocol;
            var proxyDetails = proxy.Split(':');
            this.Host = proxyDetails[0];
            Int32.TryParse(proxyDetails[1], out int port);
            this.Port = port;
            if (proxyDetails.Length > 2 && proxyDetails.Length == 4)
            {
                this.Credential = new NetworkCredential(proxyDetails[2],proxyDetails[3]);
            }
        }
        /// <summary>
        /// Proxy Host
        /// </summary>
        public string Host { get; private set; }
        /// <summary>
        /// Proxy Port
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// Proxy Credential
        /// </summary>
        public NetworkCredential Credential { get; private set; }
        /// <summary>
        /// Proxy Protocol
        /// </summary>
        public ProxyProtocols ProxyProtocol { get; private set; }
    }
}
