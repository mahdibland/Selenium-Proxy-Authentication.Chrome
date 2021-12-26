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
            (protocol == ProxyProtocols.HTTPS) ? "https" :
            (protocol == ProxyProtocols.SOCKS4) ? "socks4" :
            (protocol == ProxyProtocols.SOCKS5) ? "socks5" : String.Empty;

        /// <summary>
        /// Get Proxy Rule For Protocols (Always use singleProxy Not proxyForHttp for any http, https, ftp server)
        /// </summary>
        //internal static readonly Func<Proxy, string> GetProxyRule = (proxy) => proxy.ProxyProtocol == ProxyProtocols.HTTP || proxy.ProxyProtocol == ProxyProtocols.HTTPS ? "singleProxy" : "fallbackProxy";
        internal static readonly Func<Proxy, string> GetProxyRule = (proxy) => "singleProxy";

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
        /// https://developer.chrome.com/docs/extensions/reference/proxy/
        /// </summary>
        internal static Func<Proxy,string> GenerateCrxCode = proxy => 
$@"
var config2 = {{
                mode: ""fixed_servers"",
                rules: {{
                    " + GetProxyRule(proxy) + @": {
                        scheme: '" + GetScheme(proxy.ProxyProtocol) + @"',
                        host: '" + GetScheme(proxy.ProxyProtocol) + "://" + proxy.Host + @"',
                        port: " + proxy.Port + @"
                    }
                }
            }


chrome.proxy.settings.set({value: config2, scope: 'regular'}, function(config) {    console.log(JSON.stringify(config));  });

function callbackFn(details) {return {authCredentials: {username: '" + proxy.Credential.UserName + @"',password: '" + proxy.Credential.Password + @"'}}};

chrome.webRequest.onAuthRequired.addListener(
    callbackFn,
    { urls:['<all_urls>']},
    ['asyncBlocking']
);
";
    }
}



//let config = {{
//    mode: 'fixed_servers',
//    rules:
//    {
//    {
//    { GetProxyRule(proxy)}: {
//    {
//    schema: '" + GetScheme(proxy.ProxyProtocol) + @"',
//    host: '" + proxy.Host +@"',
//    port: "+ proxy.Port + $@"
//    }
//    }
//    }
//    }
//}};
//function(details, callbackFn) {
//    console.log("onAuthRequired!", details, callbackFn);
//    callbackFn({
//        authCredentials: 
//    });
//},

//function callbackFn(details)
//{
//{
//    return {
//    {
//        authCredentials: { { username: '" + proxy.Credential.UserName + @"', password: '" + proxy.Credential.Password + @"' } };
//    }