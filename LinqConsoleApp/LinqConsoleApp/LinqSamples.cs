﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }
        // private DataGridView _gridView = new DataGridView();// GUI ble
        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n1");
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          emp.Empno,
                          emp.Ename,
                          emp.Job,
                          emp.Salary,
                          emp.HireDate,
                          emp.Deptno,
                          emp.Mgr
                      };

            //2. Lambda and Extension methods
            res.ToList().ForEach(x => Console.WriteLine(x));
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n2");
            var res = from emp in Emps
                where emp.Job == "Backend programmer" && emp.Salary > 1000 
                orderby emp.Ename descending 
                select new
                {
                    emp.Empno,
                    emp.Ename,
                    emp.Job,
                    emp.Salary,
                    emp.HireDate,
                    emp.Deptno,
                    emp.Mgr
                };
            
            res.ToList().ForEach(x => Console.WriteLine(x));
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n3");
            var res = Emps.Max(emp => emp.Salary);
            Console.WriteLine(res);
        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n4");
            var max = Emps.Max(emp => emp.Salary);
            var res = from emp in Emps
                where emp.Salary == max 
                select new
                {
                    emp.Empno,
                    emp.Ename,
                    emp.Job,
                    emp.Salary,
                    emp.HireDate,
                    emp.Deptno,
                    emp.Mgr
                };
            res.ToList().ForEach(x => Console.WriteLine(x));
        }
        
        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n5");
            var res = from emp in Emps
                select new
                {
                    Nazwisko = emp.Ename,
                    Praca = emp.Job
                };
            res.ToList().ForEach(x => Console.WriteLine(x));
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n6");
            var res = Emps
                .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
                {
                    emp,
                    dept
                });
            res.ToList().ForEach(x => Console.WriteLine(x));
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n7");
            var res = from emp in Emps
                group emp by emp.Job into jobGroup
                select new
                {
                    Praca = jobGroup.Key,
                    Licznosc = (from emp in Emps
                            where emp.Job == jobGroup.Key select emp
                        ).Count()
                };
            res.ToList().ForEach(x => Console.WriteLine(x));
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n8");
            var job = "Backend programmer";
            bool hasJob = Emps.Any(emp => emp.Job == job);
            Console.WriteLine($"Emp contains {job}: {hasJob}");
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n9");
            var res = from emp in Emps
                where emp.Job == "Frontend programmer"
                orderby emp.HireDate descending
                select new
                {
                    emp.Empno,
                    emp.Ename,
                    emp.Job,
                    emp.Salary,
                    emp.HireDate,
                    emp.Deptno,
                    emp.Mgr
                };
            Console.WriteLine(res.First());
        }

        /// <summary>
        /// SELECT Enaclassme, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10Button_Click()
        {
            // todo
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n11");
            var result = Emps
                .Select(emp => emp.Salary)
                .Aggregate((res, next) => res > next ? res : next);
            Console.WriteLine(result);
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {
            Console.WriteLine($"\n{new String('=', 10)}\n12");
            var res = from emp in Emps
                from dept in Depts
                select new
                {    
                    emp.Empno,
                    emp.Salary,
                    dept.Deptno,
                    dept.Dname,
                    dept.Loc
                };
            res.ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
