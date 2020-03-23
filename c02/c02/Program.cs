using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Microsoft.VisualBasic;

namespace c02
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultInput = @"data.csv";
            string defaultOutput = @"result.xml";
            string defaultDataType = "xml";
            
            if (args.Length == 3)
            {
                defaultInput = args[0];
                defaultOutput = args[1];
                defaultDataType = args[2];
            }
            List<Student> studentsList = LoadCSVData(defaultInput);
            University university = new University
            {
                CreatedAt = $"{DateTime.Now}",
                Author = "Fabian Schmidt",
                Students = studentsList
            };
            if (defaultDataType.Equals("xml"))
            {
                WriteXML(university, defaultOutput);    
            }
            else if (defaultDataType.Equals("json"))
            {
                WriteJSON(studentsList, defaultOutput);    
            }
            
        }
        
        private static List<Student> LoadCSVData(string path)
        {
            var studentsList = new List<Student>();
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    bool canBeParsed = true;
                    string[] data = line.Split(',');
                    foreach (var element in data)
                    {
                        if (element.Equals(""))
                        {
                            canBeParsed = false;
                        }
                    }
                    if (data.Length != 9)
                    {
                        Log($"Failed parsinge line \"{line}\"");
                    }
                    else{
                        
                        var student = new Student
                        {
                            IndexNumber = $"s{data[4]}",
                            FirstName = data[0],
                            Lastname = data[1],
                            BirthDate = DateTime.Parse(data[5]).Date,
                            Email = data[6],
                            MothersName = data[7],
                            FathersName = data[8],
                            Studies = new Studies
                            {
                                Faculty = data[2],
                                Mode = data[3]
                            }
                        };
                        if (canBeParsed)
                        {
                            studentsList.Add(student);
                        }
                        else
                        {
                           Log($"{student}"); 
                        }
                        
                    }
                    
                }
            }

            return studentsList;
        }

        private static void WriteXML(University university, string outputPath)
        {
            FileStream writer = new FileStream(outputPath, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(University), new XmlRootAttribute("uczelnia"));
            serializer.Serialize(writer, university);
        }

        private static void WriteJSON(List<Student> studentsList, string outputPath)
        {
            // todo na razie odsawilem to na pozniej
            /*
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            string json = JsonConvert.SerializeXmlNode(doc);
            
            var jsonString = JsonSerializer.Serialize(studentsList);
            File.WriteAllText(outputPath, jsonString);*/
        }

        private static void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter("log.txt"))  
            {  
                writer.WriteLine(message);
                Console.WriteLine($"LOG [{DateTime.Now}] {message}");
            }  
        }
        
    }
}