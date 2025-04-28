using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static LINQ_PART2.Program;

namespace LINQ_PART2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Профессия C#-разработчик Язык C# Модуль 15.Основы LINQ.Часть 2 
            Практика
            */
            var classes = new[]
            {
               new Classroom { Students = {"Evgeniy", "Sergey", "Andrew"}, },
               new Classroom { Students = {"Anna", "Viktor", "Vladimir"}, },
               new Classroom { Students = {"Bulat", "Alex", "Galina"}, }
             };
            var allStudents = GetAllStudents(classes);

            Console.WriteLine(string.Join(" ", allStudents));

            //Evgeniy Sergey Andrew Anna Viktor Vladimir Bulat Alex Galina
        }

        static string[] GetAllStudents(Classroom[] classes)
        {
            return (
                 from classroom in classes
                 from student in classroom.Students
                 select student
            ).ToArray();

            //return classes
            //    .SelectMany(classroom => classroom.Students)
            //    .ToArray();

            //return classes
            //    .SelectMany(c => c.Students)
            //    .ToArray();
        }

        public class Classroom
        {
            public List<string> Students = new List<string>();
        }
    }    
}

/*
https://learn.microsoft.com/ru-ru/dotnet/csharp/linq/standard-query-operators/grouping-data  Группирование данных (C#)
 
Практика
Есть список учеников школы с разбивкой по классам:

using System;
using System.Collections.Generic;
using System.Linq;
 
namespace Task1
{
   class Program
   {
       static void Main(string[] args)
       {
           var classes = new []
           {
               new Classroom { Students = {"Evgeniy", "Sergey", "Andrew"}, },
               new Classroom { Students = {"Anna", "Viktor", "Vladimir"}, },
               new Classroom { Students = {"Bulat", "Alex", "Galina"}, }
           };
           var allStudents = GetAllStudents(classes);
         
           Console.WriteLine(string.Join(" ", allStudents));
       }
 
       static string [] GetAllStudents( Classroom [] classes )
       {
           // ???
       }
      
       public class Classroom
       {
           public List<string> Students = new List<string>();
       }
   }
}
Напишите метод, который соберет всех учеников всех классов в один список, используя LINQ.
*/
