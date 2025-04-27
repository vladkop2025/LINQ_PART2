using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_PART2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Профессия C#-разработчик Язык C# Модуль 15.Основы LINQ.Часть 2 
            2/8   15.1. Операции с множествами
            Кроме типовых задач выборки и сортировки часто возникают более комплексные.

            При работе с данными возникает необходимость создавать и компоновать из существующих коллекций новые множества объектов по определенным критериям. 

            Объединение множеств, поиск пересекающихся элементов, сумма и разность коллекций — все эти операции под силу LINQ, и в этом юните мы рассмотрим основные из них.

            *************************
            Разность Except
            *************************
            
            Бывает необходимо исключить из одной коллекции то, что содержится в другой.
            Для этого служит метод Except():

            // Список напитков в продаже
            string[] drinks = { "Вода", "Кока-кола", "Лимонад", "Вино" };
            // Алкогольные напитки
            string[] alcohol = { "Вино", "Пиво", "Сидр" };

            // Убираем алкоголь из ассортимента
            var drinksForKids = drinks.Except(alcohol);

            foreach (string drink in drinksForKids)
                Console.WriteLine(drink);

            //Вода
            //Кока - кола
            //Лимонад

                Важно! Метод Except() возвращает только уникальные элементы.
            Это значит, что если бы массив drinks у нас содержал повторения, например так: 

            string[] drinks = { "Вода", "Вода", "Кока-кола", "Лимонад" , "Вино"};
            Результат вывода всё равно был бы такой, как на скриншоте выше.


            *************************
            Пересечение Intersect 
            *************************

            Для нахождения общих элементов коллекций используйте Intersect():

            string[] cars = { "Волга", "Москвич", "Нива", "Газель" };
            string[] buses = { "Газель", "Икарус", "ЛиАЗ" };

            // поищем машины, которые можно считать микроавтобусами
            var microBuses = cars.Intersect(buses);

            foreach (string mb in microBuses)
                Console.WriteLine(mb);

            В итоговую выборку попал один элемент: 

            Газель

            *************************
            Объединение Union
            *************************

            Соединить две коллекции в одну возможно с помощью метода Union().

            В результате получится коллекция с элементами из первой и второй, но без повторений. 

            Рассмотрим на примере: 

            string[] cars = { "Волга", "Москвич", "Москвич", "Нива", "Газель" };
            string[] buses = { "Газель", "Икарус", "ЛиАЗ" };

            var vehicles = cars.Union(buses);

            foreach (string v in vehicles)
                Console.WriteLine(v);

            Волга
            Москвич
            Нива
            Газель
            Икарус
            ЛиАЗ

            *************************
            Объединение Concat
            *************************

            Если нам нужно просто прибавить элементы одной коллекции к другой, не заботясь о дублировании, есть метод Concat():

            // объединяет с дубликатами

            string[] cars = { "Волга", "Москвич", "Москвич", "Нива", "Газель" };
            string[] buses = { "Газель", "Икарус", "ЛиАЗ" };

            var vehicles = cars.Concat(buses);

            foreach (string v in vehicles)
                Console.WriteLine(v);

            Волга
            Москвич
            Москвич
            Нива
            Газель
            Газель
            Икарус
            ЛиАЗ

            *************************
            Объединение Distinct
            *************************

            Также мы можем проверить коллекцию на наличие дубликатов и удалить их с помощью метода Distinct():

            string[] cars = { "Волга", "Москвич", "Москвич", "Нива", "Газель" };

            // удалим дубликаты
            var uniqueCars = cars.Distinct();

            foreach (string v in uniqueCars)
                Console.WriteLine(v);

            Волга
            Москвич
            Нива
            Газель

            Кстати, последовательно вызвав cars.Concat(buses).Distinct();, мы получим результат, идентичный cars.Union(buses).

            */

            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}

/*
Задание 15.1.1
Какой принцип работы у метода Except()?

устраняет дубликаты в той коллекции, для которой вызывается, и те, которые содержатся во второй X
только удаляет те, которые содержатся во второй
добавляет в коллекцию содержание другой
нет верного ответа

Ответ:дублированные элементы останутся, если они уже содержатся в первой коллекции.


Задание 15.1.2
Как найти общие элементы в коллекции a и b?

1. a.UnionWith(b)
2. b.UnionWith(a)   X
3. a.Intersect(b)   X
4. b.Intersect(a)
5. Нет верного ответа

Ответ:
3. Верно, так мы найдем элементы, которые есть в обеих коллекциях.
4. Верно, так мы тоже найдем элементы из обеих коллекций.


Задание 15.1.3
Что выведет данный код?

int[] uneven = { 1, 3, 5 };
int[] even = { 2, 4, 6 , 2 };
 
var nums = uneven.Union(even);
 
foreach (var num in nums)
   Console.WriteLine(num);

1 3 5 2 4 6 2
1 2 3 4 5 6 2
1 3 5 2 4 6             X
нет верного варианта

Ответ:метод объединит обе коллекции и отбросит последнюю двойку как дубликат.


Задание 15.1.4
Напишите метод для поиска общих букв в двух словах.

class Program
{
   static void Main(string[] args)
   {
       Console.WriteLine( CountCommon("one", "two"));
   }
 
   static int CountCommon( string word1, string word2 )
   {
       var amount = word1.Intersect(word2)//   ищем пересечение
.Count(); // считаем количество
       return amount;
   }
}


Задание 15.1.5
Напишите недостающий код так, чтобы на выходе мы получили список всех IT-компаний без повторений.

var softwareManufacturers = new List<string>()
{
   "Microsoft", "Apple", "Oracle"
};
 
var hardwareManufacturers = new List<string>()
{
   "Apple", "Samsung", "Intel"
};
 
var itCompanies = // ?

Ответ:

var itCompanies = hardwareManufacturers.Union(softwareManufacturers); 
// или
var itCompanies = softwareManufacturers.Union(hardwareManufacturers);


Задание 15.1.6
Напишите программу, которая будет принимать на вход строку текста с консоли (конструкция Console.Readline()) и выводить в ответ список 
содержащихся в строке уникальных символов без пробелов и следующих знаков препинания: , . ; :  ? !.

Подсказка:
Строка в то же время является массивом элементов типа char.

static void Main(string[] args)
{
   Console.WriteLine("Введите текст:");
  
   // читаем ввод
   var text = Console.ReadLine();
  
   // сохраняем список знаков препинания и символ пробела в коллекцию
   var punctuation = new List< char>() { ' ', ',', '.', ';', ':', '!', '?' };
  
   // валидация ввода
   if (string.IsNullOrEmpty(text))
   {
       Console.WriteLine("Вы ввели пустой текст");
       return;
   }
  
   Console.WriteLine();
   Console.WriteLine("Текст без знаков препинания: ");
 
   // так как строка - это массив char, мы можем вызвать метод  except  и удалить знаки препинания
   var noPunctuation = text.Except(punctuation).ToArray();
  
   // вывод
   Console.WriteLine(noPunctuation);
}

*/
