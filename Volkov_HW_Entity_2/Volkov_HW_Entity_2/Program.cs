using DBContext;
using Country_Table;
using Microsoft.EntityFrameworkCore;

namespace Volkov_HW_Entity_2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            short input = 0;
            while (true)
            {
                Console.WriteLine("1. Показать всю информацию о странах\n" +
                                  "2. Добавить страну\n" +
                                  "3. Изменить информацию\n" +
                                  "4. Удаление страны\n" +
                                  "0. Выход");
                Console.Write("Ввод -> ");
                input = short.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        ShowAllInformation();
                        continue;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Введите название страны");
                        Console.Write("Ввод -> ");
                        string nameCountry = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Введите столицу");
                        Console.Write("Ввод -> ");
                        string capital = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Введите площадь");
                        Console.Write("Ввод -> ");
                        float area =float.Parse(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Введите население");
                        Console.Write("Ввод -> ");
                        long population = long.Parse(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Выберете континент:\n1. Asia\n2. Europa\n3. North America\n4. South America\n5. Africa");
                        Console.Write("Ввод -> ");
                        input = short.Parse(Console.ReadLine());
                        string continent = string.Empty;
                        switch (input)
                        {
                            case 1:
                                continent = "Asia";
                                break;
                            case 2:
                                continent = "Europa";
                                break;
                            case 3:
                                continent = "North America";
                                break;
                            case 4:
                                continent = "South America";
                                break;
                            case 5:
                                continent = "Africa";
                                break;
                            default:
                                Console.WriteLine("Введено неверно!");
                                continue;
                        }
                        Console.Clear();
                        AddCountry(continent, nameCountry, capital,area,population);
                        continue;
                    case 3:
                        Console.Clear();
                        ShowInformation();
                        Console.Write("Ввод -> ");
                        input = short.Parse(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Введите название страны");
                        Console.Write("Ввод -> ");
                        string newCountry = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Введите столицу");
                        Console.Write("Ввод -> ");
                        string newCapital = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Введите площадь");
                        Console.Write("Ввод -> ");
                        float newArea = float.Parse(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Введите население");
                        Console.Write("Ввод -> ");
                        long newPopulation = long.Parse(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Выберете континент:\n1. Asia\n2. Europa\n3. North America\n4. South America\n5. Africa");
                        Console.Write("Ввод -> ");
                        short input2 = short.Parse(Console.ReadLine());
                        string newContinent = string.Empty;
                        switch (input2)
                        {
                            case 1:
                                newContinent = "Asia";
                                break;
                            case 2:
                                newContinent = "Europa";
                                break;
                            case 3:
                                newContinent = "North America";
                                break;
                            case 4:
                                newContinent = "South America";
                                break;
                            case 5:
                                newContinent = "Africa";
                                break;
                            default:
                                Console.WriteLine("Введено неверно!");
                                continue;
                        }
                        Console.Clear();
                        UpdateCountry(input, newCountry, newCapital, newPopulation, newArea, newContinent);
                        continue;
                    case 4:
                        Console.Clear();
                        ShowInformation();
                        Console.Write("Ввод -> ");
                        input = short.Parse(Console.ReadLine());
                        Console.Clear();
                        DeleteCountry(input);
                        continue;
                    case 0:
                        Console.Clear();
                        break;
                }
                break;
            }
        }

        public static void ShowAllInformation()
        {
            using(var context = new CountryDBContext())
            {
                var info = context.country.ToList();
                foreach(var i in info)
                {
                    Console.WriteLine($"Страна: {i.Name} -- Столица: {i.NameCapital} -- Площадь: {i.Area} -- Население: {i.Population} -- Континент: {i.Continent.Name}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        public static void ShowInformation()
        {
            using(var context = new CountryDBContext())
            {
                var info = context.country.ToList();
                foreach (var i in info)
                {
                    Console.WriteLine($"{i.ID} -> Страна: {i.Name} -- Столица: {i.NameCapital} -- Площадь: {i.Area} -- Население: {i.Population} -- Континент: {i.Continent.Name}");
                }
            }
        }

        public static void AddCountry(string cont, string name, string nameCapital, float area, long population)
        {
            using(var context = new CountryDBContext())
            {
                try
                {
                    var continent = context.continent.FirstOrDefault(i => i.Name == cont);
                    if (continent == null)
                    {
                        throw new Exception("Континент не найден!");
                    }
                    var country = new Country { Name = name, NameCapital = nameCapital, Area = area, Population = population, Continent = continent };
                    context.country.Add(country);
                    context.SaveChanges();
                    Console.WriteLine("Страна добавлена!");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    return;
                }
            }
        }

        public static void UpdateCountry(int ID, string name, string capital, long population, float area, string cont)
        {
            using(var context = new CountryDBContext())
            {
                try
                {
                    var country = context.country.Include(i => i.Continent).FirstOrDefault(i => i.ID == ID);
                    if (country == null)
                    {
                        throw new Exception("Страна не найдена!");
                    }
                    var continent = context.continent.FirstOrDefault(i => i.Name == cont);
                    if (continent == null)
                    {
                        throw new Exception("Континент не найден не найдена!");
                    }
                    country.Name = name;
                    country.NameCapital = capital;
                    country.Population = population;
                    country.Area = area;
                    country.Continent = continent;
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
            }
            Console.WriteLine("Информация обновлена!");
            Console.ReadKey();
            Console.Clear();
        }

        public static void DeleteCountry(int ID)
        {
            using( var context = new CountryDBContext())
            {
                try
                {
                    var country = context.country.FirstOrDefault(c => c.ID == ID);
                    if (country == null)
                    {
                        throw new InvalidOperationException("Страна не найдена!");
                    }

                    context.country.Remove(country);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
            }
            Console.WriteLine("Страна удалена!");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
