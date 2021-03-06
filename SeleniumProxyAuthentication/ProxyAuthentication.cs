﻿using System.IO;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Chrome;

namespace SeleniumProxyAuthentication
{
    /// <summary>
    /// Auth Proxy For Chrome
    /// </summary>
    public static class ProxyAuthentication
    {
        /// <summary>
        /// Add Proxy With Extension To Chrome Option (Chrome Only Support Http Auth Proxies (!Didn't Test It With Socks4 Auth Proxies))
        /// </summary>
        /// <param name="chromeOptions"></param>
        /// <param name="proxy">Your Proxy With This Format host:port or host:port:user:pass as string</param>
        /// <param name="crxManifest">Edit Chrome Extension Manifest File (Leave it Empty If You Don't Want To Change It)</param>
        public static void AddProxyAuthenticationExtension(this ChromeOptions chromeOptions, Proxy proxy, [Optional] CrxManifest crxManifest)
        {
            var cachePath = Utilities.GetCachePath();

            var tempFolder = Utilities.GetTempFolder(cachePath, proxy);

            var crxDetailsFolder = Utilities.GetCrxDetailsFolder(tempFolder);

            var manifestLocation = Path.Combine(crxDetailsFolder, "manifest.json");
            var backgroundLocation = Path.Combine(crxDetailsFolder, "background.js");

            Utilities.WriteDetailsFiles(crxManifest, manifestLocation, backgroundLocation, proxy);

            chromeOptions.AddExtension(Utilities.CreateExtension(tempFolder, crxDetailsFolder));
        }
        /// <summary>
        /// Delete All Files That Made By Extensions
        /// </summary>
        /// <param name="chromeOptions"></param>
        public static void DeleteExtensionsCache(this ChromeOptions chromeOptions)
        {
            var cacheFolder = Path.Combine(GenerateCrx.GetAppDataPath(), "ProxyAuthCache");
            if(Directory.Exists(cacheFolder))
                Directory.Delete(cacheFolder);
        }
    }
}
