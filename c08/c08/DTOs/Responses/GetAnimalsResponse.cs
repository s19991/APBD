using System;

namespace c08.DTOs.Responses
{
    public class GetAnimalsResponse
    {
        public string Name { get; set; }

        public string AnimalType { get; set; }

        public DateTime DateOfAdmission { get; set; }

        public string LastNameOfOwner { get; set; }
    }
}