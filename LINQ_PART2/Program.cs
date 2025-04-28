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
            5/8   15.4. Соединение коллекций

            Соединение в LINQ позволяет объединить разные наборы объектов в коллекциях по общим свойствам, по аналогии с тем, как мы объединяем разные таблицы 
            по общему ключу в SQL (будет изучаться в последующих модулях), используя операторы Join.

            *************************
            Join()
            *************************

            Как правило, эту операцию можно применять к двум наборам коллекций, имеющим общий критерий.

            Создадим следующие модели данных: 

            В данном случае  свойство Manufacturer у Car и Name у класса Manufacturer будут общим ключом, по которому можно соединить эти две сущности.

            Создадим набор данных:

            // Список моделей
            var cars = new List<Car>()
            {
                new Car() { Model  = "SX4", Manufacturer = "Suzuki"},
                new Car() { Model  = "Grand Vitara", Manufacturer = "Suzuki"},
                new Car() { Model  = "Jimny", Manufacturer = "Suzuki"},
                new Car() { Model  = "Land Cruiser Prado", Manufacturer = "Toyota"},
                new Car() { Model  = "Camry", Manufacturer = "Toyota"},
                new Car() { Model  = "Polo", Manufacturer = "Volkswagen"},
                new Car() { Model  = "Passat", Manufacturer = "Volkswagen"},
            };

            // Список автопроизводителей
            var manufacturers = new List<Manufacturer>()
            {
                 new Manufacturer() { Country = "Japan", Name = "Suzuki" },
                 new Manufacturer() { Country = "Japan", Name = "Toyota" },
                 new Manufacturer() { Country = "Germany", Name = "Volkswagen" },
            };

            Соединим и сопоставим коллекции: 

            var result = from car in cars // выберем машины
                         join m in manufacturers on car.Manufacturer equals m.Name // соединим по общему ключу (имя производителя) с производителями
                         select new //   спроецируем выборку в новую анонимную сущность
                         {
                             Name = car.Model,
                             Manufacturer = m.Name,
                             Country = m.Country
                         };

            // выведем результаты
            foreach (var item in result)
                Console.WriteLine($"{item.Name} - {item.Manufacturer} ({item.Country})");

            В результате получим модели автомобилей с информацией о марке и стране-изготовителе:

            SX4 - Suzuki(Japan)
            Grand Vitara -Suzuki(Japan)
            Jimny - Suzuki(Japan)
            Land Cruiser Prado - Toyota(Japan)
            Camry - Toyota(Japan)
            Polo - Volkswagen(Germany)
            Passat - Volkswagen(Germany)

            Метод расширения Join(), как обычно, предоставляет ещё один вариант сделать то же самое:

            var result2 = cars.Join(manufacturers, // передаем в качестве параметра вторую коллекцию
                car => car.Manufacturer, // указываем общее свойство для первой коллекции
                m => m.Name, // указываем общее свойство для второй коллекции
                 (car, m) =>
                    new // проекция в новый тип
                    {
                        Name = car.Model,
                        Manufacturer = m.Name,
                        Country = m.Country
                 });

            *************************
            GroupJoin()
            *************************

            Позволяет одновременно с тем, что мы делали выше (соединение последовательностей), произвести группировку.

            // Выборка + группировка
            var result2 = manufacturers.GroupJoin(
               cars, // первый набор данных
               m => m.Name, // общее свойство второго набора
               car => car.Manufacturer, // общее свойство первого набора
               (m, crs) => new  // результат выборки
               {
                   Name = m.Name,
                   Country = m.Country,
                   Cars = crs.Select(c => c.Model)
               });

            // Вывод:
            foreach (var team in result2)
            {
                Console.WriteLine(team.Name + ":");

                foreach (string car in team.Cars)
                    Console.WriteLine(car);

                Console.WriteLine();
            }

            Результат: 

            Suzuki:
                SX4
                Grand Vitara
                Jimny

            Toyota:
                Land Cruiser Prado
                Camry

            Volkswagen:
                Polo
                Passat

            *************************
            Zip()
            *************************

            Данный метод позволяет попарно соединять последовательности. 

            При необходимости можно на ходу выполнить дополнительные операции:

            //  объявляем две коллекции
            var letters = new string[] { "A", "B", "C", "D", "E" };
            var numbers = new int[] { 1, 2, 3 };

            // проводим "упаковку" элементов, сопоставляя попарно
            var q = letters.Zip(numbers, (l, n) => l + n.ToString());

            // вывод
            foreach (var s in q)
                Console.WriteLine(s);

            Вывод: 

            A1
            B2
            C3

            */

        }
        // Модель автомобиля
        public class Car
        {
            public string Model { get; set; }
            public string Manufacturer { get; set; }
        }

        // Завод - изготовитель
        public class Manufacturer
        {
            public string Name { get; set; }
            public string Country { get; set; }
        }
    }    
}

/*
Задание 15.4.2
Теперь сгруппируйте сотрудников по отделам, выведя на экран последовательно сначала название отдела, а затем список его сотрудников:

static void Main(string[] args)
{
   var departments = new List<Department>()
   {
       new Department() {Id = 1, Name = "Программирование"},
       new Department() {Id = 2, Name = "Продажи"}
   };
      
   var employees = new List<Employee>()
   {
       new Employee() { DepartmentId = 1, Name = "Инна", Id = 1},
       new Employee() { DepartmentId = 1, Name = "Андрей", Id = 2},
       new Employee() { DepartmentId = 2, Name = "Виктор ", Id = 3},
       new Employee() { DepartmentId = 3, Name = "Альберт ", Id = 4},
   };
  
   // Ваш код здесь
}

var depsWithEmployees =  departments.GroupJoin(
       employees, // первый набор данных
       d => d.Id, // общее свойство второго набора
       e  => e.DepartmentId, // общее свойство первого набора
       (d, emps ) => new  // результат выборки
       {
           Name = d.Name,
           Employees = emps.Select(e=>e.Name)
       });
  
   // Пробегаемся по кажлому отделу
   foreach (var dep in depsWithEmployees)
   {
       Console.WriteLine(dep.Name + ":");
      
       // Выводим сотрудников
       foreach (string emp in dep.Employees)
           Console.WriteLine(emp);
 
       Console.WriteLine();
   }


Задание 15.4.3

Что должны иметь две коллекции для того, чтобы из них можно было произвести совместную выборку?
коллекции должны быть одного типа данных
они должны иметь простой тип данных
должны быть динамическими
их типы данных должны иметь общее свойство  X

Ответ: это является необходимым условием подобно тому, как общий ключ должен быть у таблиц SQL, между которыми выполняется join.


Задание 15.4.4

Позволяет ли метод Zip соединять два разнотипных набора данных?
да, на выходе дает ArrayList
да, но придется выполнить преобразование типов с помощью лямбда-выражения   X
нет

Ответ: Zip() на выходе дает список однотипных элементов.


Задание 15.4.5
Дан код:

static void Main()
{
   var customers = new Customer[]
   {
       new Customer{ID = 5, Name = "Андрей"},
       new Customer{ID = 6, Name = "Сергей"},
       new Customer{ID = 7, Name = "Юлия"},
       new Customer{ID = 8, Name = "Анна"}
   };
 
   var orders = new Order[]
   {
       new Order{ID = 6, Product = "Игру"},
       new Order{ID = 7, Product = "Компьютер"},
       new Order{ID = 3, Product = "Рубашку"} ,
       new Order{ID = 5, Product = "Книгу"}
   };
 
   var query = from c in customers
       join o in orders on c.ID equals o.ID
       select new { c.Name, o.Product };
   foreach (var group in query)
       Console.WriteLine($"{group.Name} покупает {group.Product}");
}

Какой товар остался без покупателя?

Введите слово  Рубашку

Какой идентификатор должен быть у этого товара, чтобы он тоже попал в выборку?

Введите цифру 8

Измените код так, чтобы все товары получили покупателя.
*/
