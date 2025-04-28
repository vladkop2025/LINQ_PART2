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
            4/8   15.3. Группировка
            ,
            Задачи группировки тоже встречаются очень часто. Рассмотрим наш пример с автомобилями, допустим, нам нужно сгруппировать их по странам-производителям.

            *************************
            GroupBy()
            *************************
            
            var cars = new List<Car>()
            {
                new Car("Suzuki", "JP"),
                new Car("Toyota", "JP"),
                new Car("Opel", "DE"),
                new Car("Kamaz", "RUS"),
                new Car("Lada", "RUS"),
                new Car("Honda", "JP"),
            };

            //Сгруппируем по стране-производителю(используя ключевое слово groupby):
            var carsByCountry = from car in cars
                                group car by car.CountryCode;

            // Пробежимся по группам
            foreach (var grouping in carsByCountry)
            {
                Console.WriteLine(grouping.Key + ":");

                // внутри каждой группы пробежимся по элементам
                foreach (var car in grouping)
                    Console.WriteLine(car.Manufacturer);

                Console.WriteLine();
            }

             //Вывод:

            //JP:
            //    Suzuki
            //    Toyota
            //Honda

            //    DE:
            //    Opel

            //RUS:
            //    Kamaz
            //    Lada

            Результатом оператора group является выборка, которая состоит из групп. 

            Каждая группа представляет объект IGrouping<string, Car>, где string — тип ключа, а параметр Car — тип сгруппированных объектов.
            Ключ каждой группы доступен через свойство Key.

            Через метод расширения тот же результат достигается в одну строчку: 

            // Группировка по стране - производителю ( через метод - расширение)
            var carsByCountry = cars.GroupBy(car => car.CountryCode);

            При группировке мы можем использовать уже известную вам проекцию для создания объекта нового типа: 

            var carsByCountry = cars
                .GroupBy(car => car.CountryCode) // группируем по стране-производителю
                .Select(group => new
                { //  создаем новую сущность анонимного типа
                  Name = group.Key,
                  Amount = group.Count()
                });

            И даже осуществлять вложенные запросы, используя ключевое слово into.

            var carsByCountry2 = from car in cars
                                 group car by car.CountryCode into grouping // выборка в локальную переменную для вложенного запроса
                                 select new
                                 {
                                     Name = grouping.Key,
                                     Count = grouping.Count(),
                                     Cars = from p in grouping select p //  выполним подзапрос, чтобы заполнить список машин внутри нашего нового типа
                                 };
            // Выведем результат
            foreach (var group in carsByCountry2)
            {
            // Название группы и количество элементов
                 Console.WriteLine($"{group.Name} : {group.Count} авто");
  
                 foreach(Car car in group.Cars)
                 Console.WriteLine(car.Manufacturer);
  
                Console.WriteLine();
            }

            Вывод:
            JP : 3 авто
            Suzuki
            Toyota
            Honda

            DE : 1 авто
            Opel

            RUS : 2 авто
            Kamaz
            Lada

            Аналогичный запрос с помощью метода расширения, как всегда, выглядит менее громоздким: 

            var carsByCountry2 = cars
                .GroupBy(car => car.CountryCode)
                 .Select(g => new
                 {
                    Name = g.Key,
                    Count = g.Count(),
                    Cars = g.Select(c =>c)
                });

            */
        }
        public class Car
        {
            public string Manufacturer { get; set; }
            public string CountryCode { get; set; }

            public Car(string manufacturer, string countryCode)
            {
                Manufacturer = manufacturer;
                CountryCode = countryCode;
            }
        }
    }    
}

/*
Задание 15.3.1

Для чего служит ключевое слово into, и где его можно использовать?

можно использовать в любом месте программы, служит для промежуточной выборки объектов в переменную
можно использовать в методах расширения и выражениях LINQ, служит для промежуточной выборки значений запроса.
для промежуточной выборки и подзапросов, используется только в LINQ-выражениях                                  X
нигде в C# нельзя использовать, а вообще это что-то из SQL

Задание 15.3.2
Что мы получаем на выходе после отработки метода GroupBy()?

коллекцию такого же типа, но со сгруппированными вместе элементами
коллекцию групп — новых объектов, внутри которых, в свою очередь, лежат наши элементы
метод GroupBy() ничего не возвращает, он только изменяет текущую коллекцию                  X
нет верного ответа

Ответ: мы получим объект типа IGrouping <TKey, TValue>, где ключом будет элемент, по которому группируем.


Задание 15.3.3
Дан список контактов:

var phoneBook = new List<Contact>();
 
// добавляем контакты
phoneBook.Add(new Contact("Игорь", 79990000001, "igor@example.com"));
phoneBook.Add(new Contact("Сергей", 79990000010, "serge@example.com"));
phoneBook.Add(new Contact("Анатолий", 79990000011, "anatoly@example.com"));
phoneBook.Add(new Contact("Валерий", 79990000012, "valera@example.com"));
phoneBook.Add(new Contact("Сергей", 799900000013, "serg@gmail.com"));
phoneBook.Add(new Contact("Иннокентий", 799900000013, "innokentii@example.com"));
Некоторые из них имеют реальный email-адрес, а некоторые — фейковый (те, которые в домене example).

Нам нужно разбить их на две группы: фейковые и реальные, и вывести результат в консоль.

Решите эту задачу с помощью группировки.

//  в качестве критерия группировки передаем домен адреса электронной почты
var grouped = phoneBook.GroupBy(c => c.Email.Split("@").Last());
 
// обрабатываем получившиеся группы
foreach (var group in grouped)
{
   // если ключ содержит example, значит, это фейк
   if (group.Key.Contains("example"))
   {
       Console.WriteLine("Фейковые адреса: ");
 
       foreach (var contact in group)
           Console.WriteLine(contact.Name + " " + contact.Email);
      
   }
   else
   {
       Console.WriteLine("Реальные адреса: ");
       foreach (var contact in group)
           Console.WriteLine(contact.Name + " " + contact.Email);
   }
 
   Console.WriteLine();
}
*/
