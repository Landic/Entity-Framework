using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace GoodsDapper
{
    class MainClass
    {
        static string? connectionString;

        static void Main()
        {
            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");

            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Отображение");
                    Console.WriteLine("2. CRUD");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            SelectDB();
                            break;
                        case 2:
                            CRUD();
                            break;
                        case 0:
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void SelectDB()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Отображение всех покупателей");
                    Console.WriteLine("2. Отображение email всех покупателей");
                    Console.WriteLine("3. Отображение списка разделов");
                    Console.WriteLine("4. Отображение списка акционных товаров");
                    Console.WriteLine("5. Отображение всех городов");
                    Console.WriteLine("6. Отображение всех стран");
                    Console.WriteLine("7. Отображение всех покупателей из конкретного города");
                    Console.WriteLine("8. Отображение всех покупателей из конкретной страны");
                    Console.WriteLine("9.  Отображение всех акций для конкретной страны");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            ShowAllCustomers();
                            break;
                        case 2:
                            ShowAllEmails();
                            break;
                        case 3:
                            ShowAllInterestAreas();
                            break;
                        case 4:
                            ShowAllPromotions();
                            break;
                        case 5:
                            ShowAllCities();
                            break;
                        case 6:
                            ShowAllCountries();
                            break;
                        case 7:
                            CustomersFromCurrentCity();
                            break;
                        case 8:
                            CustomersFromCurrentCountry();
                            break;
                        case 9:
                            PromotionsFromCurrentCountry();
                            break;
                        case 0:
                            return;
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void CRUD()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Вставка информации о новых покупателях");
                    Console.WriteLine("2. Вставка новых стран");
                    Console.WriteLine("3. Вставка новых городов");
                    Console.WriteLine("4. Вставка информации о новых разделах");
                    Console.WriteLine("5. Вставка информации о новых акционных товарах");
                    Console.WriteLine("6. Обновление информации о покупателях");
                    Console.WriteLine("7. Обновление информации о странах");
                    Console.WriteLine("8. Обновление информации о городах");
                    Console.WriteLine("9. Обновление информации о разделах");
                    Console.WriteLine("10. Обновление информации об акционных товарах");
                    Console.WriteLine("11. Удаление информации о покупателях");
                    Console.WriteLine("12. Удаление информации о странах");
                    Console.WriteLine("13. Удаление информации о городах");
                    Console.WriteLine("14. Удаление информации о разделах");
                    Console.WriteLine("15. Удаление информации об акционных товарах");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            AddCustomer();
                            break;
                        case 2:
                            AddCountry();
                            break;
                        case 3:
                            AddCity();
                            break;
                        case 4:
                            AddInterestArea();
                            break;
                        case 5:
                            AddPromotionGood();
                            break;
                        case 6:
                            EditCustomer();
                            break;
                        case 7:
                            EditCountry();
                            break;
                        case 8:
                            EditCity();
                            break;
                        case 9:
                            EditInterest();
                            break;
                        case 10:
                            EditPromotionGood();
                            break;
                        case 11:
                            RemoveCustomer();
                            break;
                        case 12:
                            RemoveCountry();
                            break;
                        case 13:
                            RemoveCity();
                            break;
                        case 14:
                            RemoveInterest();
                            break;
                        case 15:
                            RemovePromotionGood();
                            break;
                        case 0:
                            return;
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void ShowAllCustomers(bool expect = true)
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"
                            SELECT 
                                c.Id,
                                c.FullName,
                                c.DateOfBirth,
                                c.Gender,
                                c.Email,
                                co.Title AS Country,
                                ci.Title AS City
                            FROM Customers c
                            INNER JOIN Countries co ON c.CountryId = co.Id
                            INNER JOIN Сities ci ON c.CityId = ci.Id";

                var customers = db.Query<CustomerVM>(query);

                foreach (var cust in customers)
                {
                    Console.WriteLine($"Id: {cust.Id}");
                    Console.WriteLine($"Full Name: {cust.FullName}");
                    Console.WriteLine($"Date of Birth: {cust.DateOfBirth}");
                    Console.WriteLine($"Gender: {cust.Gender}");
                    Console.WriteLine($"Email: {cust.Email}");
                    Console.WriteLine($"Country: {cust.Country}");
                    Console.WriteLine($"City: {cust.City}");
                    Console.WriteLine("----------------------------------");
                }
            }
            if (expect)
                Console.ReadKey();
        }
        static void ShowAllEmails(bool expect = true)
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"
                            SELECT 
                                c.Id,
                                c.Email
                            FROM Customers c";

                var customers = db.Query<Customer>(query);

                foreach (var cust in customers)
                {
                    Console.WriteLine($"Id: {cust.Id}");
                    Console.WriteLine($"Email: {cust.Email}");
                    Console.WriteLine("----------------------------------");
                }

            }
            if (expect)
                Console.ReadKey();
        }
        static void ShowAllPromo(bool expect = true)
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @$"
                        SELECT 
                            p.Id,
                            p.PromotionName,
                            p.StartDate,
                            p.EndDate,
                            co.Title AS Country  
                        FROM Promotions as p
                        JOIN Countries as co ON co.Id = p.CountryId";

                var customers = db.Query<Promotion>(query);
                foreach (var cust in customers)
                {
                    Console.WriteLine($"Id: {cust.Id}");
                    Console.WriteLine($"Promotion Name: {cust.PromotionName}");
                    Console.WriteLine($"Start Date: {cust.StartDate}");
                    Console.WriteLine($"End Date: {cust.EndDate}");
                    Console.WriteLine("----------------------------------");
                }
            }
            if (expect)
                Console.ReadKey();
        }
        static void ShowAllInterestAreas(bool expect = true)
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"
                            SELECT 
                                i.Id,
                                i.AreaName
                            FROM InterestAreas as i";

                var customers = db.Query<InterestArea>(query);

                foreach (var cust in customers)
                {
                    Console.WriteLine($"Id: {cust.Id}");
                    Console.WriteLine($"Area Name: {cust.AreaName}");
                    Console.WriteLine("----------------------------------");
                }
            }
            if (expect)
                Console.ReadKey();
        }
        static void ShowAllPromotions(bool expect = true)
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"
                            SELECT 
                                p.Id,
                                p.GoodName
                            FROM PromotionGoods as p";

                var customers = db.Query<PromotionGood>(query);

                foreach (var cust in customers)
                {
                    Console.WriteLine($"Id: {cust.Id}");
                    Console.WriteLine($"Good Name: {cust.GoodName}");
                    Console.WriteLine("----------------------------------");
                }
            }
            if (expect)
                Console.ReadKey();
        }
        static void ShowAllCountries(bool expect = true)
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"
                            SELECT 
                                c.Id,
                                c.Title
                            FROM Countries as c";

                var customers = db.Query<Country>(query);

                foreach (var cust in customers)
                {
                    Console.WriteLine($"Id: {cust.Id}");
                    Console.WriteLine($"Country: {cust.Title}");
                    Console.WriteLine("----------------------------------");
                }
            }
            if (expect)
                Console.ReadKey();
        }
        static void ShowAllCities(bool expect = true)
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"
                            SELECT 
                                c.Id,
                                c.Title
                            FROM Сities as c";

                var customers = db.Query<City>(query);

                foreach (var cust in customers)
                {
                    Console.WriteLine($"Id: {cust.Id}");
                    Console.WriteLine($"City : {cust.Title}");
                    Console.WriteLine("----------------------------------");
                }
            }
            if (expect)
                Console.ReadKey();
        }
        static void CustomersFromCurrentCity()
        {
            try
            {
                Console.Clear();
                ShowAllCities(false);
                Console.WriteLine("Введите Id конкрретного города");
                int result = int.Parse(Console.ReadLine()!);
                Console.Clear();
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = @$"
                            SELECT 
                                c.Id,
                                c.FullName,
                                c.DateOfBirth,
                                c.Gender,
                                c.Email,
                                co.Title AS Country,
                                ci.Title AS City
                            FROM Customers c
                            INNER JOIN Countries co ON c.CountryId = co.Id
                            INNER JOIN Сities ci ON c.CityId = ci.Id
                            Where ci.Id = {result}";

                    var customers = db.Query<CustomerVM>(query);
                    if (customers.Any())
                    {
                        foreach (var cust in customers)
                        {
                            Console.WriteLine($"Id: {cust.Id}");
                            Console.WriteLine($"Full Name: {cust.FullName}");
                            Console.WriteLine($"Date of Birth: {cust.DateOfBirth}");
                            Console.WriteLine($"Gender: {cust.Gender}");
                            Console.WriteLine($"Email: {cust.Email}");
                            Console.WriteLine($"Country: {cust.Country}");
                            Console.WriteLine($"City: {cust.City}");
                            Console.WriteLine("----------------------------------");
                        }
                    }
                    else Console.Write("Покупателей не найдено");
                }
            }
            catch { Console.WriteLine("Неверный ввод"); }
            Console.ReadKey();
        }
        static void CustomersFromCurrentCountry()
        {
            try
            {
                Console.Clear();
                ShowAllCountries(false);
                Console.WriteLine("Введите Id конкретной страны");
                int result = int.Parse(Console.ReadLine()!);
                Console.Clear();
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = @$"
                            SELECT 
                                c.Id,
                                c.FullName,
                                c.DateOfBirth,
                                c.Gender,
                                c.Email,
                                co.Title AS Country,
                                ci.Title AS City
                            FROM Customers c
                            INNER JOIN Countries co ON c.CountryId = co.Id
                            INNER JOIN Сities ci ON c.CityId = ci.Id
                            Where co.Id = {result}";

                    var customers = db.Query<CustomerVM>(query);
                    if (customers.Any())
                    {
                        foreach (var cust in customers)
                        {
                            Console.WriteLine($"Id: {cust.Id}");
                            Console.WriteLine($"Full Name: {cust.FullName}");
                            Console.WriteLine($"Date of Birth: {cust.DateOfBirth}");
                            Console.WriteLine($"Gender: {cust.Gender}");
                            Console.WriteLine($"Email: {cust.Email}");
                            Console.WriteLine($"Country: {cust.Country}");
                            Console.WriteLine($"City: {cust.City}");
                            Console.WriteLine("----------------------------------");
                        }
                    }
                    else Console.Write("Покупателей не найдено");
                }
            }
            catch { Console.WriteLine("Неверный ввод"); }
            Console.ReadKey();
        }
        static void PromotionsFromCurrentCountry()
        {
            try
            {
                Console.Clear();
                ShowAllCountries(false);
                Console.WriteLine("Введите Id конкретной страны");
                int result = int.Parse(Console.ReadLine()!);
                Console.Clear();
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var query = @$"
                            SELECT 
                                p.PromotionName,
                                p.StartDate,
                                p.EndDate,
                                co.Title AS Country  
                            FROM Promotions as p
                            JOIN Countries as co ON co.Id = p.CountryId
                            WHERE co.Id = {result}";

                    var customers = db.Query<PromotionVM>(query);
                    if (customers.Any())
                    {
                        foreach (var cust in customers)
                        {
                            Console.WriteLine($"Promotion Name: {cust.PromotionName}");
                            Console.WriteLine($"Start Date: {cust.StartDate}");
                            Console.WriteLine($"End Date: {cust.EndDateDate}");
                            Console.WriteLine($"Country Name: {cust.Country}");
                            Console.WriteLine("----------------------------------");
                        }
                    }
                    else Console.Write("Акций не найдено");

                }

            }
            catch { Console.WriteLine("Неверный ввод"); }
            Console.ReadKey();
        }
        //CRUD
        static void AddCustomer()
        {
            try
            {
                Console.Clear();

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string fullname;
                    do
                    {
                        Console.WriteLine("Введите имя покупателя: ");
                        fullname = Console.ReadLine()!;
                    }
                    while (fullname.Trim().IsNullOrEmpty());

                    Console.WriteLine("Введите дату рождения покупателя: ");
                    DateTime date = DateTime.Parse(Console.ReadLine()!);
                    Console.WriteLine("Введите email покупателя: ");
                    string email = Console.ReadLine()!;
                    Console.WriteLine("Введите пол покупателя: \n1 - M\n2 - Ж");
                    int res = int.Parse(Console.ReadLine()!);
                    string gender = res == 1 ? "Male" : "Female";
                    email = email.Trim();
                    ShowAllCountries(false);
                    Console.Write("Введите Id страны: ");
                    int countryId = int.Parse(Console.ReadLine()!);
                    ShowAllCities();
                    Console.Write("Введите Id города: ");
                    int cityId = int.Parse(Console.ReadLine()!);
                    var customer = new Customer
                    {
                        FullName = fullname,
                        DateOfBirth = date,
                        Gender = gender,
                        Email = email,
                        CountryId = countryId,
                        CityId = cityId
                    };

                    var sqlQuery = "INSERT INTO Customers (FullName, DateOfBirth, Gender, Email, CountryId,CityId) " +
                        "VALUES(@FullName, @DateOfBirth, @Gender, @Email, @CountryId,@CityId )";
                    int number = db.Execute(sqlQuery, customer);
                    if (number != 0)
                        Console.WriteLine("Успешно добавлен!");
                }
            }
            catch { Console.WriteLine("Неверный ввод"); }
            Console.ReadKey();
        }
        static void AddCountry()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string country;
                do
                {
                    Console.WriteLine("Введите название новой страны: ");
                    country = Console.ReadLine()!;
                }
                while (country.Trim().IsNullOrEmpty());
                var newcountry = new Country { Title = country };
                var sqlQuery = "INSERT INTO Countries (Title) VALUES(@Title)";
                int number = db.Execute(sqlQuery, newcountry);
                if (number != 0)
                    Console.WriteLine($"Страна успешно добавлена!");
            }
            Console.ReadKey();
        }
        static void AddCity()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string country;
                do
                {
                    Console.WriteLine("Введите название нового города: ");
                    country = Console.ReadLine()!;
                }
                while (country.Trim().IsNullOrEmpty());
                var newcountry = new City { Title = country };
                var sqlQuery = "INSERT INTO Сities (Title) VALUES (@Title)";
                int number = db.Execute(sqlQuery, newcountry);
                if (number != 0)
                    Console.WriteLine($"Город успешно добавлен!");
            }
            Console.ReadKey();
        }
        static void AddInterestArea()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string country;
                do
                {
                    Console.WriteLine("Введите название нового раздела: ");
                    country = Console.ReadLine()!;
                }
                while (country.Trim().IsNullOrEmpty());
                var newcountry = new InterestArea { AreaName = country };
                var sqlQuery = "INSERT INTO InterestAreas (AreaName) VALUES(@AreaName)";
                int number = db.Execute(sqlQuery, newcountry);
                if (number != 0)
                    Console.WriteLine($"Раздел успешно добавлена!");
            }
            Console.ReadKey();
        }
        static void AddPromotionGood()
        {
            try
            {
                Console.Clear();
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string fullname;
                    do
                    {
                        Console.WriteLine("Введите название товара: ");
                        fullname = Console.ReadLine()!;
                    }
                    while (fullname.Trim().IsNullOrEmpty());
                    ShowAllPromo(false);
                    Console.Write("Введите Id акции: ");
                    int promId = int.Parse(Console.ReadLine()!);
                    var customer = new PromotionGood
                    {
                        GoodName = fullname,
                        PromotionId = promId
                    };

                    var sqlQuery = "INSERT INTO PromotionGoods (GoodName,PromotionId) " +
                        "VALUES(@GoodName,@PromotionId )";
                    int number = db.Execute(sqlQuery, customer);
                    if (number != 0)
                        Console.WriteLine("Товар успешно добавлен!");
                }
            }
            catch { Console.WriteLine("Неверный ввод"); }
            Console.ReadKey();
        }
        static void EditCountry()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllCountries(false);
                Console.WriteLine("Введите порядковый номер страны: ");
                int number = int.Parse(Console.ReadLine()!);
                string country;
                do
                {
                    Console.WriteLine("Введите новое название страны: ");
                    country = Console.ReadLine()!;
                }
                while (country.Trim().IsNullOrEmpty());
                var sqlQuery = "UPDATE Countries SET Title = @Title WHERE Id = @Id";
                number = db.Execute(sqlQuery, new { Id = number, Title = country });
                if (number != 0)
                    Console.WriteLine("Страна успешно изменена!");
            }
            Console.ReadKey();
        }
        static void EditCity()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllCities(false);
                Console.WriteLine("Введите порядковый номер города: ");
                int number = int.Parse(Console.ReadLine()!);
                string country;
                do
                {
                    Console.WriteLine("Введите новое название города: ");
                    country = Console.ReadLine()!;
                }
                while (country.Trim().IsNullOrEmpty());
                var newcountry = new City { Title = country, Id = number };
                var sqlQuery = "UPDATE Сities SET Title = @Title WHERE Id = @Id";
                number = db.Execute(sqlQuery, newcountry);
                if (number != 0)
                    Console.WriteLine("Город успешно изменен!");
            }
            Console.ReadKey();
        }
        static void EditInterest()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllInterestAreas(false);
                Console.WriteLine("Введите порядковый номер раздела: ");
                int number = int.Parse(Console.ReadLine()!);
                string interest;
                do
                {
                    Console.WriteLine("Введите новое название раздела: ");
                    interest = Console.ReadLine()!;
                }
                while (interest.Trim().IsNullOrEmpty());
                var sqlQuery = "UPDATE InterestAreas SET AreaName = @AreaName WHERE Id = @Id";
                number = db.Execute(sqlQuery, new { Id = number, AreaName = interest });
                if (number != 0)
                    Console.WriteLine("Раздел успешно изменен!");
            }
            Console.ReadKey();
        }
        static void EditCustomer()
        {
            try
            {
                Console.Clear();
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    ShowAllCustomers(false);
                    Console.WriteLine("Введите порядковый номер клиента: ");
                    int number = int.Parse(Console.ReadLine()!);
                    string fullname;
                    do
                    {
                        Console.WriteLine("Введите имя студента: ");
                        fullname = Console.ReadLine()!;
                    }
                    while (fullname.Trim().IsNullOrEmpty());
                    Console.WriteLine("Введите дату рождения покупателя: ");
                    DateTime date = DateTime.Parse(Console.ReadLine()!);
                    Console.WriteLine("Введите email покупателя: ");
                    string email = Console.ReadLine()!;
                    Console.WriteLine("Введите пол покупателя: \n1 - M\n2 - Ж");
                    int res = int.Parse(Console.ReadLine()!);
                    string gender = res == 1 ? "Male" : "Female";
                    email = email.Trim();
                    ShowAllCountries(false);
                    Console.Write("Введите Id страны: ");
                    int countryId = int.Parse(Console.ReadLine()!);
                    ShowAllCities(false);
                    Console.Write("Введите Id города: ");
                    int cityId = int.Parse(Console.ReadLine()!);
                    var customer = new Customer
                    {
                        Id = number,
                        FullName = fullname,
                        DateOfBirth = date,
                        Gender = gender,
                        Email = email,
                        CountryId = countryId,
                        CityId = cityId
                    };
                    var sqlQuery = "UPDATE Customers SET FullName = @FullName, DateOfBirth = @DateOfBirth, Gender = @Gender, " +
                        "Email = @Email, CountryId = @CountryId, CityId =@CityId WHERE Id = @Id";
                    number = db.Execute(sqlQuery, customer);
                    if (number != 0)
                        Console.WriteLine("Клиент успешно изменен!");
                }
            }
            catch { Console.WriteLine("Неправильный ввод"); }
            Console.ReadKey();
        }
        static void EditPromotionGood()
        {
            try
            {
                Console.Clear();
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    ShowAllPromotions(false);
                    Console.Write("Введите Id товара: ");
                    int id = int.Parse(Console.ReadLine()!);
                    string fullname;
                    do
                    {
                        Console.WriteLine("Введите название товара: ");
                        fullname = Console.ReadLine()!;
                    }
                    while (fullname.Trim().IsNullOrEmpty());
                    ShowAllPromo(false);
                    Console.Write("Введите Id акции: ");
                    int promId = int.Parse(Console.ReadLine()!);
                    var customer = new PromotionGood
                    {
                        Id = id,
                        GoodName = fullname,
                        PromotionId = promId
                    };

                    var sqlQuery = "UPDATE PromotionGoods SET GoodName = @GoodName, PromotionId =@PromotionId WHERE Id = @Id";
                    int number = db.Execute(sqlQuery, customer);
                    if (number != 0)
                        Console.WriteLine("Товар успешно изменен!");
                }
            }
            catch { Console.WriteLine("Неправильный ввод"); }
            Console.ReadKey();
        }
        static void RemoveCountry()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllCountries(false);
                Console.WriteLine("Введите порядковый номер страны: ");
                int number = int.Parse(Console.ReadLine()!);
                var sqlQuery = "DELETE FROM Countries WHERE Id = @id";
                number = db.Execute(sqlQuery, new { Id = number });
                if (number != 0)
                    Console.WriteLine("Страна успешно удалена!");
            }
            Console.ReadKey();
        }
        static void RemoveCity()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllCities(false);
                Console.WriteLine("Введите порядковый номер города: ");
                int number = int.Parse(Console.ReadLine()!);
                var sqlQuery = "DELETE FROM Сities WHERE Id = @id";
                number = db.Execute(sqlQuery, new { Id = number });
                if (number != 0)
                    Console.WriteLine("Город успешно удален!");
            }
            Console.ReadKey();
        }
        static void RemoveInterest()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllInterestAreas(false);
                Console.WriteLine("Введите порядковый номер раздела: ");
                int number = int.Parse(Console.ReadLine()!);
                var sqlQuery = "DELETE FROM InterestAreas WHERE Id = @id";
                number = db.Execute(sqlQuery, new { Id = number });
                if (number != 0)
                    Console.WriteLine("Раздел успешно удален!");
            }
            Console.ReadKey();
        }
        static void RemoveCustomer()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllCustomers(false);
                Console.WriteLine("Введите порядковый номер клиента: ");
                int number = int.Parse(Console.ReadLine()!);
                var sqlQuery = "DELETE FROM Customers WHERE Id = @id";
                number = db.Execute(sqlQuery, new { Id = number });
                if (number != 0)
                    Console.WriteLine("Клиент успешно удален!");
            }
            Console.ReadKey();
        }
        static void RemovePromotionGood()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                ShowAllPromotions(false);
                Console.WriteLine("Введите порядковый номер товара: ");
                int number = int.Parse(Console.ReadLine()!);
                var sqlQuery = "DELETE FROM PromotionGoods WHERE Id = @id";
                number = db.Execute(sqlQuery, new { Id = number });
                if (number != 0)
                    Console.WriteLine("Товар успешно удален!");
            }
            Console.ReadKey();
        }

    }
}
