using System;
using System.Text.Json;

namespace SeleniumProxyAuthentication
{
    internal static class GenerateCrx
    {
        /// <summary>
        /// Convert Protocol to Scheme
        /// </summary>
        internal static readonly Func<ProxyProtocols, string> GetScheme = protocol =>
            (protocol == ProxyProtocols.HTTP) ? "http" :
            (protocol == ProxyProtocols.SOCKS4) ? "socks4" :
            (protocol == ProxyProtocols.SOCKS4A) ? "socks4a" :
            (protocol == ProxyProtocols.SOCKS5) ? "socks5" : String.Empty;

        /// <summary>
        /// Get Proxy Rule For Protocols (Always use singleProxy Not proxyforHttp)
        /// </summary>
        internal static readonly Func<string> GetProxyRule = () => "singleProxy";

        /// <summary>
        /// Get APPData Path from Machine
        /// </summary>
        internal static Func<string> GetAppDataPath =
            () => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// Generate Manifest Json
        /// </summary>
        internal static Func<CrxManifest, string> GetManifest = crxManifest => JsonSerializer.Serialize(crxManifest);

        /// <summary>
        /// Generate Js Code For Crx Background
        /// </summary>
        internal static Func<Proxy,string> GenerateCrxCode = proxy => 
            $@"
            var config = {{
                mode: 'fixed_servers',
                rules: {{
                    {GetProxyRule()}: {{
                        scheme: '" + GetScheme(proxy.ProxyProtocol) + @"',
                        host: '" + proxy.Host +@"',
                        port: "+ proxy.Port +@"
                    },
                    bypassList:['foobar.com']
                }
            };
            chrome.proxy.settings.set({value: config, scope: 'regular'}, function() { });

            function callbackFn(details)
            {
                return { authCredentials: { username: '" + proxy.Credential.UserName + @"', password: '" + proxy.Credential.Password + @"' } };
            }

            chrome.webRequest.onAuthRequired.addListener(
                callbackFn,
                {urls: ['<all_urls>']},
                ['blocking']
            );
            ";
    }
}
