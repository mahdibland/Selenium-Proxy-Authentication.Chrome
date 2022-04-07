using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SeleniumProxyAuthentication.Sample
{
    public record Program : IDisposable
    {
        private static readonly ChromeOptions ChromeOptions = new();
        public static void Main()
        {
            // with proxy credential
            ChromeOptions.AddProxyAuthenticationExtension(new Proxy(ProxyProtocols.HTTP, "proxy_server:proxy_port:proxy_username:proxy_password"));

            // without proxy credential
            //chromeOptions.AddProxyAuthenticationExtension(new Proxy(ProxyProtocols.HTTP, "proxy_server:proxy_port"));

            IWebDriver driver = new ChromeDriver(ChromeOptions);
            driver.Navigate().GoToUrl(new Uri("https://myip.ms"));
        }
        public void Dispose()
        {
            ChromeOptions.DeleteExtensionsCache();
        }
    }
}