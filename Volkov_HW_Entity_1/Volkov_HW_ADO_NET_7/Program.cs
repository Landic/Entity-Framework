namespace Volkov_HW_ADO_NET_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CountryDBContext())
            {
                if (context.Database.EnsureCreated())
                {
                    var countries = new List<Country>
                    {
                        new Country(){Name = "Ukraine", NameCapital = "Kyiv", Population=40000000, Area = 603628, Continent="Europa"},
                        new Country(){Name = "Germany", NameCapital = "Berlin", Population=83200000, Area = 357592, Continent="Europa"},
                        new Country(){Name = "France", NameCapital = "Paris", Population=67750000, Area = 643801, Continent="Europa"},
                        new Country(){Name = "Spain", NameCapital = "Barcelona", Population=47420000, Area = 506030, Continent="Europa"},
                        new Country(){Name = "Poland", NameCapital = "Warsaw", Population=37750000, Area = 312696, Continent="Europa"},
                        new Country(){Name = "China", NameCapital = "Beijing", Population=1409670000, Area = 9596961, Continent="Asia"},
                        new Country(){Name = "Japan", NameCapital = "Tokyo", Population=125416877, Area = 377975, Continent="Asia"},
                        new Country(){Name = "USA", NameCapital = "Washington", Population=334914895, Area = 9833520, Continent="North America"},
                        new Country(){Name = "Canada", NameCapital = "Ottawa", Population=40528396, Area = 3855100, Continent="North America"},
                        new Country(){Name = "Brazil", NameCapital = "Brasilia", Population=203062512, Area = 8515767, Continent="South America"}
                    };
                    Console.WriteLine("База данных создана и заполнена");
                    context.Countries.AddRange(countries);
                    context.SaveChanges();
                }
                Console.Clear();
                short input = 0;
                while (true)
                {
                    Console.WriteLine("1. Вся информация о странах\n" +
                                      "2. Все страны\n" +
                                      "3. Столицы\n" +
                                      "4. Европейские страны\n" +
                                      "5. Страны с площадью больше конкретного числа\n" +
                                      "6. Все страны у которых в название присутствуют буквы А, Е\n" +
                                      "7. Все страны у которых название начинается с буквы А\n" +
                                      "8. Все страны у которых площадь в указаном диапозоне\n" +
                                      "9. Все страны у которых жителей больше конкретно указаного\n" +
                                      "0. Выход");
                    Console.Write("Ввод -> ");
                    input = short.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            ShowAllCountries();
                            continue;
                        case 2:
                            Console.Clear();
                            ShowCountry();
                            continue;
                        case 3:
                            Console.Clear();
                            ShowCapital();
                            continue;
                        case 4:
                            Console.Clear();
                            ShowEuropaCountry();
                            continue;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("Введите число");
                            Console.Write("Ввод -> ");
                            int area = int.Parse(Console.ReadLine());
                            Console.Clear();
                            ShowCountryArea(area);
                            continue;
                        case 6:
                            Console.Clear();
                            ShowCountryWhereWordAOrE();
                            continue;
                        case 7:
                            Console.Clear();
                            ShowCountryWhereStartWordA();
                            continue;
                        case 8:
                            Console.Clear();
                            Console.WriteLine("Введите начало");
                            Console.Write("Ввод -> ");
                            int areaStart = int.Parse(Console.ReadLine());
                            Console.Clear();
                            Console.WriteLine("Введите конец");
                            Console.Write("Ввод -> ");
                            int areaEnd = int.Parse(Console.ReadLine());
                            Console.Clear();
                            ShowCountryAreaRange(areaStart, areaEnd);
                            continue;
                        case 9:
                            Console.Clear();
                            Console.WriteLine("Введите количество жителей");
                            Console.Write("Ввод -> ");
                            int population = int.Parse(Console.ReadLine());
                            Console.Clear();
                            ShowCountryWherePopulationBiggerThan(population);
                            continue;
                        case 0:
                            Console.Clear();
                            break;
                    }
                    break;
                }
            }
        }

        static public void ShowAllCountries()
        {
            using(var context = new CountryDBContext())
            {
                var countries = from i in context.Countries select i;
                foreach (var i in countries)
                {
                    Console.WriteLine($"Страна: {i.Name} -- столица: {i.NameCapital} -- население: {i.Population} -- площадь: {i.Area} -- континент: {i.Continent}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowCapital()
        {
            using(var context = new CountryDBContext())
            {
                var capital = context.Countries.Select(i => i.NameCapital);
                foreach(var i in capital)
                {
                    Console.WriteLine($"Столица: {i}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowEuropaCountry()
        {
            using(var context = new CountryDBContext())
            {
                var countryEuropa = context.Countries.Where(i => i.Continent == "Europa").Select(i => i.Name);
                foreach(var i in countryEuropa)
                {
                    Console.WriteLine($"Страна: {i}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowCountryArea(float area)
        {
            using(var context = new CountryDBContext())
            {
                var areaCountry = context.Countries.Where(i => i.Area > area).Select(i => i.Name);
                foreach(var i in areaCountry)
                {
                    Console.WriteLine($"Страна: {i}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowCountryWhereWordAOrE() 
        {
            using(var context = new CountryDBContext())
            {
                var country = context.Countries.Where(i => i.Name.Contains("a") || i.Name.Contains("e")).Select(i => i.Name);
                foreach(var i in country)
                {
                    Console.WriteLine($"Страна: {i}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowCountryWhereStartWordA()
        {
            using(var context = new CountryDBContext())
            {
                var country = context.Countries.Where(i => i.Name.StartsWith("A")).Select(i => i.Name);
                foreach(var i in country)
                {
                    Console.WriteLine($"Страна: {i}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowCountryAreaRange(float start, float end)
        {
            using(var context = new CountryDBContext())
            {
                var countryArea = context.Countries.Where(i => i.Area > start && i.Area < end).Select(i => i.Name);
                foreach(var i in countryArea)
                {
                    Console.WriteLine($"Страна: {i}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowCountryWherePopulationBiggerThan(long population)
        {
            using(var context = new CountryDBContext())
            {
                var countryBiggerPopulation = context.Countries.Where(i => i.Population > population).Select(i => i.Name);
                foreach(var i in countryBiggerPopulation)
                {
                    Console.WriteLine($"Страна: {i}");
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        static public void ShowCountry()
        {
            using(var context = new CountryDBContext())
            {
                var country = from i in context.Countries select i.Name;
                foreach (var i in country)
                    Console.WriteLine($"Страна: {i}");
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
