# Selenium Proxy Authentication For Chrome

> Easily add your auth proxies to your chrome Driver with one line of code With Extension

## Installation

> You Can Simply Add this Library to your project with Nuget Package: <a href="https://www.nuget.org/packages/SeleniumProxyAuthentication.Chrome/">
    <img src="https://www.nuget.org/Content/gallery/img/logo-header.svg" width="80" height="25"/>
    </a>
```markdown
Install-Package SeleniumProxyAuthentication.Chrome -Version 2.0.0
```

## How to Use it

- First you need to download the chrome driver from here (make sure that the version of your chrome browser should be match with the version of chrome driver that your download!)

```
https://chromedriver.chromium.org/downloads
```

- Then put the chromedriver.exe in your project directory

- Create a global chrome option

```C#
private static readonly ChromeOptions ChromeOptions = new();
```
 
- Attach your proxy to the chrome option using the extension method that comes with nuget package

```C#
ChromeOptions.AddProxyAuthenticationExtension(new SeleniumProxyAuthentication.Proxy(
                    ProxyProtocols.HTTP,
                    "host:port:username:password"
                    ));
```

- Cretae chrome driver and use the chrome option

```C#
IWebDriver driver = new ChromeDriver(ChromeOptions);
driver.Navigate().GoToUrl(new Uri("https://github.com"));
```

- Remove Entire Cache That Created By Extensions (In the Dispose Function)

```C#
chromeOptions.DeleteExtensionsCache();
```

* also see the sample project to see how it's work <a href="https://github.com/mahdibland/Selenium-Proxy-Authentication.Chrome/blob/main/SeleniumProxyAuthentication.Sample/Program.cs">Link to sample project</a>

##  Guides

#### Proxy Format

* Host:Port:Username:Password
* Host:Port

## Contact

> Email: mahdi.blandsoft98.ir@gmail.com<br/>
> Telegram: https://t.me/ONll_CH4N<br />
    
## Licence

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](https://github.com/mahdibland/Selenium-Proxy-Authentication.Chrome)
