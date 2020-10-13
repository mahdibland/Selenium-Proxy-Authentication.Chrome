# Selenium Proxy Authentication For Chrome

> Easily add your auth proxies to your chrome Driver with one line of code With Extension

## Installation

> You Can Simply Add this Library to your project with Nuget Package:
[![Nuget](NugetPackage)](https://nuget.com/NugetLink)

## How to Use it

- For adding proxy

```C#
ChromeOptions chromeOption = new ChromeOptions();
chromeOptions.AddProxyAuthenticationExtension(new SeleniumProxyAuthentication.Proxy(
                    ProxyProtocols.HTTP,
                    "Your Porxy"
                    ));
```
- Remove Entire Cache That Created By Extensions

```C#
new ChromeOptions().DeleteExtensionsCache();
```

##  Guides

### Proxy Format

- Host:Port:Username:Password
- Host:Port

## Contact

> Email: mahdi.blandsoft98.ir@gmail.com<br/>
> Telegram: https://t.me/HERO_OTOES<br />
> Discord: <a href="mahdibland#4828">mahdibland#4828<a/><br/>
    
## Licence

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](https://github.com/mahdibland/Base64-Encoder-Decoder)
