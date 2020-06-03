using NetCoreWebApp.Models;
using System.Collections.Generic;

namespace NetCoreWebApp.Services
{
    public class EctsService
    {
        public EctsService()
        {
        }

        public int CalculateEctsSum(List<Grade> grades)
        {
            int sum = 0;
            foreach (var g in grades)
            {
                if (g.SubjectType == "Group 1")
                {
                    sum += 5;
                }
                else if (g.Subject == "Group 2")
                {
                    sum += 3;
                }
                else if (g.SubjectType == "Group 3")
                {
                    sum += 1;
                }
            }

            return sum;
        }
    }
}