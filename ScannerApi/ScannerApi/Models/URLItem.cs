using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScannerApi.Models
{
    public class URLItem
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string Result { get; set; }
        public string Sha1 { get; set; }
    }
}
