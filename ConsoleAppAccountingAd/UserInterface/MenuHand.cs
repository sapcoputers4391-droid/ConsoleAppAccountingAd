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

        public MenuHand(AccountingController controller)
        {
            _controller = controller;
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
                    case "0":
                        Console.WriteLine("Сохранение данных... До свидания!");
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
                Console.WriteLine("2.4 Сменить статус доступности");
                Console.WriteLine("2.5 Удалить площадку");
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
                        RedactStatusAccess();
                        break;
                    case "5":
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
            Console.WriteLine(" тип носителя: ");
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
        }
        private void RedactStatusAccess()
        {
            Console.Clear();
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
                        RedactTitleCampain();
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

        }
        private void DeleteCampain()
        {
            Console.Clear();
            Console.Write("Введите ID: ");
            int id = int.Parse(Console.ReadLine());
            _controller.RemoveAdCampain(id);
        }
        private void RedactTitleCampain()
        {
            Console.Clear();
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
