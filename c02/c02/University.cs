using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace c02
{
    [Serializable]
    public class University
    {
        private static string tmp = "p0lska 123";

        [XmlAttribute("createdAt")]
        public string CreatedAt { get; set; }      

        [XmlAttribute("author")]
        public string Author { get; set; }
        
        public List<Student> Students { get; set; }
    }
}