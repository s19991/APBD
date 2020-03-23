using System;
using System.Xml.Serialization;

namespace c02
{
    [Serializable]
    public class Studies
    {
        public string Faculty { get; set; }
        
        public string Mode { get; set; }
    }
}