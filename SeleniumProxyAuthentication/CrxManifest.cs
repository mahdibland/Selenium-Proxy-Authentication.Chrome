using System.Collections.Generic;

namespace SeleniumProxyAuthentication
{
    public class CrxManifest
    {
        public string version { get; set; }
        public int manifest_version { get; set; }
        public string name { get; set; }
        public List<string> permissions { get; set; }
        public BackGround background { get; set; }
        public string minimum_chrome_version { get; set; }
    }
    public partial class BackGround
    {
        public List<string> scripts { get; set; }
    }
}
