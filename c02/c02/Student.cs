using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace c02
{
    [Serializable]
    public class Student
    {
        
        [XmlAttribute("indexNumber")]
        public string IndexNumber { get; set; }
        
        public string FirstName { get; set; }
        
        public string Lastname { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string Email { get; set; }
        
        public string MothersName { get; set; }
        
        public string FathersName { get; set; }
        
        public Studies Studies { get; set; }

        public override string ToString()
        {
            return $"{IndexNumber} fname:{FirstName} lname:{Lastname} bdate:{BirthDate} email:{Email}";
        }
    }
}