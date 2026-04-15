using ConsoleAppAccountingAd.Models;
using ConsoleAppAccountingAd.Services;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace ConsoleAppAccountingAd.UserInterface
{
    internal class MenuHand
    {
        private readonly AccountingController _controller;
        private readonly ExcelManager _excelManager;

        public MenuHand(AccountingController controller)
        {
            _controller = controller;
            _excelManager = new ExcelManager();
        }
        public void MainLoop()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================");
                Console.WriteLine("===СИСТЕМА УЧЕТА РЕКЛАМЫ В АГЕНСТВЕ===");
                Console.WriteLine("========================================================");
                Console.WriteLine("===Главное меню===");
                Console.WriteLine("1. Управление клиентами");
                Console.WriteLine("2. Управление рекламными площадками");
                Console.WriteLine("3. Учет рекламных компаний(заказов)");
                Console.WriteLine("4. Аналитика");
                Console.WriteLine("5. Открытие ЭТ MS Excel");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("0. Сохранить и выйти");
                Console.WriteLine("==========================================================");
                Console.WriteLine("Выберите действие: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ClientSubMenu();
                        break;
                    case "2":
                        AdPlatformSubMenu();
                        break;
                    case "3":
                        AdCampainSubMenu();
                        break;
                    case "4":
                        AnalitikSubMenu();
                        break;
                    case "5":
                        string fileName = @"..\..\..\AdAgency_Report.xlsx";
                        Console.WriteLine("Генерация отчета и открытие Excel...");
                        ExcelManager excel = new ExcelManager();
                        excel.ExportOther(_controller.Clients, _controller.Platforms, _controller.Campains, fileName);
                        ExcelManager.OpenInExcel(fileName);
                        break;
                    case "0":
                        Console.WriteLine("Сохранение данных... До свидания!");
                        string filePath = @"..\..\..\AdAgency_Report.xlsx";
                        _excelManager.ExportOther(
                            _controller.Clients,
                            _controller.Platforms,
                            _controller.Campains,
                            filePath
                        );

                        Console.WriteLine("Готово! Файл сохранен в папке проекта.");
                        return;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }

            }
        }
        private void ClientSubMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================");
                Console.WriteLine("===СИСТЕМА УЧЕТА РЕКЛАМЫ В АГЕНСТВЕ===");
                Console.WriteLine("========================================================");
                Console.WriteLine("1.1. Просмотреть список всех клиентов");
                Console.WriteLine("1.2. Добавить нового клиента");
                Console.WriteLine("1.3. Редактировать данные клиента (по ID)");
                Console.WriteLine("1.4. Удалить клиента из базы");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("0. Вернуться в главное меню");
                Console.WriteLine("========================================================");
                Console.WriteLine("Выберите действие: ");
                string choice = Console.ReadLine();
                if (choice == "0") break;
                switch(choice)
                {
                    case "1":
                        AllClients();
                        break;
                    case "2":
                        AddClient();
                        break;
                    case "3":
                        RedactInfoClient();
                        break;
                    case "4":
                        DeleteClient();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void AllClients()
        {
            Console.Clear();
            foreach (var client in _controller.Clients)
            {
                client.Info();
            }
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }
        private void AddClient()
        {
            Console.Clear();
            Console.Write("Введите ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название компании: ");
            string company = Console.ReadLine();

            Console.WriteLine("Введите фамилию: ");
            string surname = Console.ReadLine();

            Console.WriteLine("Введите имя: ");
            string name = Console.ReadLine();

            Console.WriteLine("Введите номер телефона: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Введите Email: ");
            string email = Console.ReadLine();
            Client newClient = new Client(id, company, surname, name, phone, email);
            _controller.AddClient(newClient);

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
        private void RedactInfoClient()
        { 
            Console.Clear();
            Console.Write("Введите ID клиента, которого хотите изменить: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var client = _controller.GetClientById(id);

                if (client != null)
                {
                    Console.WriteLine($"\nРедактирование клиента: {client.NameCompany}");
                    Console.WriteLine("1. Изменить название компании");
                    Console.WriteLine("2. Изменить Фамилию");
                    Console.WriteLine("3. Изменить Имя");
                    Console.WriteLine("4. Изменить Телефон");
                    Console.WriteLine("5. Изменить Email");
                    Console.WriteLine("0. Отмена");
                    Console.Write("\nВыберите поле: ");

                    string fieldChoice = Console.ReadLine();
                    switch (fieldChoice)
                    {
                        case "1":
                            Console.Write("Новое название: ");
                            client.NameCompany = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("Новая фамилия: ");
                            client.Surname = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("Новое имя: ");
                            client.Name = Console.ReadLine();
                            break;
                        case "4":
                            Console.Write("Новый телефон: ");
                            client.PhoneNumber = Console.ReadLine();
                            break;
                        case "5":
                            Console.Write("Новый Email: ");
                            client.Email = Console.ReadLine();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    Console.WriteLine("\nДанные успешно обновлены!");
                }
                else
                {
                    Console.WriteLine("Клиент с таким ID не найден.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите числовой ID.");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        private void DeleteClient()
        {
            Console.Clear();
            Console.Write("Введите ID: ");
            int id = int.Parse(Console.ReadLine());
            _controller.RemoveClient(id);
        }
        private void AdPlatformSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================");
                Console.WriteLine("===СИСТЕМА УЧЕТА РЕКЛАМЫ В АГЕНСТВЕ===");
                Console.WriteLine("========================================================");
                Console.WriteLine("2.1 Показать доступные площадки");
                Console.WriteLine("2.2 Добавить новую площадку");
                Console.WriteLine("2.3 Изменить стоимость размещения");
                Console.WriteLine("2.4 Удалить площадку");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("0. Вернуться в главное меню");
                Console.WriteLine("========================================================");
                Console.WriteLine("Выберите действие: ");
                string choice = Console.ReadLine();
                if (choice == "0") break;
                switch (choice)
                {
                    case "1":
                        AllPlatform();
                        break;
                    case "2":
                        AddNewPlatform();
                        break;
                    case "3":
                        RedactInfoPlatform();
                        break;
                    case "4":
                        DeletePlatform();
                        break ;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void AllPlatform()
        {
            Console.Clear();
            foreach (var adplatform in _controller.Platforms)
            {
                adplatform.Info();
            }
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }
        private void AddNewPlatform()
        {
            Console.Clear();
            Console.WriteLine("Введите Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите название платформы:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите тип носителя: ");
            string category = Console.ReadLine();
            Console.WriteLine("Введите стоимость размещения за один день: ");
            decimal bpd = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Введите приблизительный охват людей: ");
            int reach = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите статус доступности: ");
            string statusimput = Console.ReadLine();
            bool available = statusimput == "1";
            AdPlatform newPlatform = new AdPlatform(id, title, category, bpd, reach, available);
            _controller.AddAdPlatform(newPlatform);

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
        private void RedactInfoPlatform()
        {
            Console.Clear();
            Console.Write("Введите ID платформы, которою хотите изменить: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var platform  = _controller.GetPlatformById(id);

                if (platform != null)
                {
                    Console.WriteLine($"\nРедактирование платформы: {platform.Title}");
                    Console.WriteLine("1. Изменить название палатформы");
                    Console.WriteLine("2. Изменить тип носителя");
                    Console.WriteLine("3. Изменить стоимость размещение за один день");
                    Console.WriteLine("4. Изменить приблизительный охват людей");
                    Console.WriteLine("5. Изменить состояние доступа");
                    Console.WriteLine("0. Отмена");
                    Console.Write("\nВыберите поле: ");

                    string fieldChoice = Console.ReadLine();
                    switch (fieldChoice)
                    {
                        case "1":
                            Console.Write("Новое название: ");
                            platform.Title = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("Новый тип носителя: ");
                            platform.Category = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("Новая цена: ");
                            platform.BasePriceDay = decimal.Parse(Console.ReadLine());
                            break;
                        case "4":
                            Console.Write("Новый охват: ");
                            platform.ApproximateReach = int.Parse(Console.ReadLine());
                            break;
                        case "5":
                            Console.Write("Доступна? (1 - Да, 0 - Нет): ");
                            string status = Console.ReadLine();
                            platform.IsAvailable = (status == "1");
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    Console.WriteLine("\nДанные успешно обновлены!");
                }
                else
                {
                    Console.WriteLine("Платформа с таким ID не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите числовой ID.");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        private void DeletePlatform()
        {
            Console.Clear();
            Console.Write("Введите ID: ");
            int id = int.Parse(Console.ReadLine());
            _controller.RemoveAdPlaform(id);
        }
        private void AdCampainSubMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================");
                Console.WriteLine("===СИСТЕМА УЧЕТА РЕКЛАМЫ В АГЕНСТВЕ===");
                Console.WriteLine("========================================================");
                Console.WriteLine("3.1 Оформить новый заказ");
                Console.WriteLine("3.2 Журнал всех заказов");
                Console.WriteLine("3.3 Удалить заказ");
                Console.WriteLine("3.4 Изменить примечания к заказу");
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("0. Вернуться в главное меню");
                Console.WriteLine("========================================================");
                Console.WriteLine("Выберите действие: ");
                string choice = Console.ReadLine();
                if (choice == "0") break;
                switch (choice)
                {
                    case "1":
                        AddNewCampain();
                        break;
                    case "2":
                        ShowAllCampain();
                        break;
                    case "3":
                        DeleteCampain();
                        break;
                    case "4":
                        RedactInfoCampain();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void ShowAllCampain()
        {
            Console.Clear();
            foreach (var adcampain in _controller.Campains)
            {
                adcampain.Info();
            }
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }
        private void AddNewCampain()
        {
            Console.Clear();
            Console.WriteLine("Введите Id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();
            int clientId = int.Parse(Console.ReadLine());
            Console.WriteLine();
            int platformId = int.Parse(Console.ReadLine());
            Console.WriteLine() ;
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine();
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine();
            string status = Console.ReadLine();
            Console.WriteLine();
            string notes = Console.ReadLine();
            AdCampain NewCampain = new AdCampain(id, clientId, platformId, startDate, endDate, status, notes);
            _controller.AddAdCampain(NewCampain);
        }
        private void DeleteCampain()
        {
            Console.Clear();
            Console.Write("Введите ID: ");
            int id = int.Parse(Console.ReadLine());
            _controller.RemoveAdCampain(id);
        }
        private void RedactInfoCampain()
        {
            Console.Clear();
            Console.Write("Введите ID заказы, которою хотите изменить: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var campain = _controller.GetCampainById(id);

                if (campain != null)
                {
                    Console.WriteLine($"\nРедактирование заказа: {campain.Id}");
                    Console.WriteLine("1. Изменить дату начала заказа");
                    Console.WriteLine("2. Изменить дату окончания заказа");
                    Console.WriteLine("3. Изменить статус");
                    Console.WriteLine("4. Изменить пожелания клиента");
                    Console.WriteLine("0. Отмена");
                    Console.Write("\nВыберите поле: ");

                    string fieldChoice = Console.ReadLine();
                    switch (fieldChoice)
                    {
                        case "1":
                            Console.Write("Новоя дата начала: ");
                            campain.StartDate = DateTime.Parse(Console.ReadLine());
                            break;
                        case "2":
                            Console.Write("Новая конечная дата: ");
                            campain.EndDate = DateTime.Parse(Console.ReadLine());
                            break;
                        case "3":
                            Console.Write("Новый статус: ");
                            campain.Status = Console.ReadLine();
                            break;
                        case "4":
                            Console.Write("Новые пожелания клиента: ");
                            campain.Notes = Console.ReadLine();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    Console.WriteLine("\nДанные успешно обновлены!");
                }
                else
                {
                    Console.WriteLine("Платформа с таким ID не найдена.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите числовой ID.");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        private void AnalitikSubMenu()
        {
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("=======================================================");
                Console.WriteLine("===СИСТЕМА УЧЕТА РЕКЛАМЫ В АГЕНСТВЕ===");
                Console.WriteLine("========================================================");
                Console.WriteLine("4.1 Подсчет дохода за определенный месяц");
                Console.WriteLine("4.2 Топ клиенты");
                Console.WriteLine("4.3 Самые прибыльные рекламные площадки");
                string choice = Console.ReadLine();
                if (choice == "0") break;
                switch (choice)
                {
                    case "1":
                        CalculatIncomMonth();
                        break;
                    case "2":
                        TopCliensts();
                        break;
                    case "3":
                        MostProfitAdPlatform();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }

            }
        }
        private void CalculatIncomMonth()
        {
            Console.Clear();
        }
        private void TopCliensts()
        {
            Console.Clear();
        }
        private void MostProfitAdPlatform()
        {
            Console.Clear();
        }
    }
}
