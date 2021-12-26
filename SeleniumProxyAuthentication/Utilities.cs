using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace SeleniumProxyAuthentication
{
    internal class Utilities
    {
        internal static char[] NotAllowedChars = new[] { '\\', '/', '*', ':', '?', '"', '|', '<', '>' };
        internal static Random rnd = new Random();

        internal static Func<string, string> GetValidName = name =>
            NotAllowedChars.Select(x => (name = name.Replace(x, '!'))).Last();

        internal static Func<string> GetCachePath = () =>
        {
            var cachePath = Path.Combine(GenerateCrx.GetAppDataPath(), "ProxyAuthCache");
            try
            {
                if (!Directory.Exists(cachePath))
                    Directory.CreateDirectory(cachePath);
            }
            catch
            {
                // ignored
            }

            return cachePath;
        };

        internal static Func<string, Proxy, string> GetTempFolder = (path, proxy) =>
        {
            var currentFolder = Path.Combine(path, Utilities.GetValidName(proxy.Host + proxy.Port));
            while (Directory.Exists(currentFolder))
            {
                currentFolder += rnd.Next(0, 1000000);
            }
            //Directory of .crx File
            Directory.CreateDirectory(currentFolder);
            return currentFolder;
        };

        internal static Func<string, string> GetCrxDetailsFolder = path =>
        {
            var detailsFolder = Path.Combine(path, "Details");
            try
            {
                if (Directory.Exists(detailsFolder))
                {
                    Directory.Delete(detailsFolder, true);
                }
            }
            catch
            {
                // ignored
            }

            //Directory of manifest and background files
            Directory.CreateDirectory(detailsFolder);
            return detailsFolder;
        };

        internal static Action<CrxManifest, string, string, Proxy> WriteDetailsFiles =
            async (crxManifest, manifestLocation, backgroundLocation, proxy) =>
            {
                await using var streamWriter = new StreamWriter(manifestLocation);
                if (crxManifest == null)
                {
                    await streamWriter.WriteAsync(GenerateCrx.GetManifest(new CrxManifest
                    {
                        version = "1.1.0",
                        manifest_version = 2,
                        name = "Proxy Authentication",
                        permissions = new List<string>
                        {
                            "background",
                            "bookmarks",
                            "clipboardRead",
                            "clipboardWrite",
                            "contentSettings",
                            "contextMenus",
                            "cookies",
                            "debugger",
                            "history",
                            "idle",
                            "management",
                            "notifications",
                            "pageCapture",
                            "topSites",
                            "webNavigation",
                            "proxy", "tabs", "unlimitedStorage", "storage", "<all_urls>", "webRequest",
                            "webRequestBlocking"
                        },
                        background = new BackGround { scripts = new List<string> { "background.js" } },
                        minimum_chrome_version = "50.0.0"
                    }));
                }
                else
                {
                    await streamWriter.WriteAsync(GenerateCrx.GetManifest(new CrxManifest
                    {
                        version = crxManifest.version,
                        manifest_version = crxManifest.manifest_version,
                        name = crxManifest.name,
                        permissions = crxManifest.permissions,
                        background = crxManifest.background,
                        minimum_chrome_version = crxManifest.minimum_chrome_version
                    }));
                }

                await using var streamWriter2 = new StreamWriter(backgroundLocation);
                await streamWriter2.WriteAsync(GenerateCrx.GenerateCrxCode(proxy));
            };

        internal static Func<string, string, string> CreateExtension = (tempFolder, crxDetails) =>
        {
            var crxLocation = Path.Combine(tempFolder, "AuthChromeExtension.crx");
            try
            {
                if (File.Exists(crxLocation))
                    File.Delete(crxLocation);
            }
            catch
            {
                // ignored
            }

            ZipFile.CreateFromDirectory(crxDetails, crxLocation);
            return crxLocation;
        };
    }
}
