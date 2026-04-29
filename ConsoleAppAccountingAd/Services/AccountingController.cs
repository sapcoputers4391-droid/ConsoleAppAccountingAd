using ConsoleAppAccountingAd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppAccountingAd.Services
{
    internal class AccountingController
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<AdPlatform> Platforms { get; set; } = new List<AdPlatform>();
        public List<AdCampain> Campains { get; set;} = new List<AdCampain>();
        public void AddClient(Client client)
        {
            if(Clients.Exists(x => x.Id == client.Id ))
            {
                Console.WriteLine("Ошибка. Уже существует клиент с таким Id.");
                return;
            }
            Clients.Add(client);
            Console.WriteLine("Клиент успешно добавлен!");
        }
        public void RemoveClient(int id)
        {
            var client = Clients.Find(x => x.Id == id);
            if (client != null)
            {
                Clients.Remove(client);
                Console.WriteLine($"Клиент {id} успешно удален.");
            }
            else
            {
                Console.WriteLine("Клиент не найден.");
            }
        }
        public Client GetClientById(int id)
        { 
            return Clients.FirstOrDefault(c => c.Id == id);
        }
        public void TopClent()
        {
            foreach (var client in Clients)
            {
                decimal Top = 0;
                foreach (var campains in Campains)
                {
                    if (campains.ClientId == client.Id)
                    {
                        var platform = GetPlatformById(campains.PlatformId);
                        if (platform != null)
                        {
                            Top += campains.ResultSum(platform);
                        }
                    }
                }
                if (Top > 0)
                {
                    Console.WriteLine($"Клиент: {client.Id} {client.Name} {client.Surname} / Сумма его заказов: {Top} бел.руб.");
                }
            }
        }
        public int GetNextClientId()
        {
            if (Clients.Count == 0) return 1;
            int maxId = 0;
            foreach (var client in Clients)
            {
                if (client.Id > maxId)
                {
                    maxId = client.Id;
                }
            }
            return maxId + 1;
        }
        public void AddAdPlatform(AdPlatform platform)
        {
            if(Platforms.Exists(y => y.Id == platform.Id))
            {
                Console.WriteLine("Ошибка. Рекламная площадка с таким Id уже существует.");
                return ;
            }
            Platforms.Add(platform);
            Console.WriteLine("Рекламная площадка успешно добавлена!");
        }
        public void RemoveAdPlaform(int id)
        {
            var platform = Platforms.Find(y => y.Id == id);
            if(platform != null)
            {  
                Platforms.Remove(platform);
                Console.WriteLine($"Рекламная площадка {id} успешно удалена");
            }
            else
            {
                Console.WriteLine("Рекламная площадка не найдена");
            }
        }
        public int GetNextPlatformId()
        {
            if (Platforms.Count == 0) return 1;

            int maxId = 0;
            foreach (var platform in Platforms)
            {
                if (platform.Id > maxId)
                {
                    maxId = platform.Id;
                }
            }
            return maxId + 1;
        }
        public AdPlatform GetPlatformById(int id)
        {
            return Platforms.FirstOrDefault(p => p.Id == id);
        }
        public void AddAdCampain(AdCampain adCampain)
        {
            if(Campains.Exists(z => z.Id == adCampain.Id))
            {
                Console.WriteLine("Ошибка. Заказ с тамим Id уже существует.");
                return;
            }
            Campains.Add(adCampain);
            Console.WriteLine("Заказ успешно добавлен!");
        }
        public int GetNextAdCampainId()
        {
            if (Campains.Count == 0) return 1;

            int maxId = 0;
            foreach (var campain in Campains)
            {
                if (campain.Id > maxId)
                {
                    maxId = campain.Id;
                }
            }
            return maxId + 1;
        }
        public void RemoveAdCampain(int id)
        {
            var adCampain = Campains.Find(z => z.Id == id);
            if(adCampain != null)
            {
                Campains.Remove(adCampain);
                Console.WriteLine($"Заказ {id} успешно удален.");
            }
            else
            {
                Console.WriteLine("Заказ не найден");
            }
        }
        public AdCampain GetCampainById(int id)
        {
            return Campains.FirstOrDefault(s => s.Id == id);
        }
        public decimal GetMonthlyRevenue(int month, int year)
        {
            decimal totalRevenue = 0;

            foreach (var campaign in Campains)
            {
                if (campaign.StartDate.Month == month && campaign.StartDate.Year == year)
                {
                    var platform = GetPlatformById(campaign.PlatformId);
                    if (platform != null)
                    {
                        totalRevenue += campaign.ResultSum(platform);
                    }
                }
            }
            return totalRevenue;
        }
        public void ShowLongestCampains()
        {
            if (Campains.Count == 0)
            {
                Console.WriteLine("Заказов пока нет.");
                return;
            }
            var sortedCampains = new List<AdCampain>(Campains);

            sortedCampains.Sort((x, y) => y.Days().CompareTo(x.Days()));

            Console.WriteLine("Самые долгие заказы");

            int count = Math.Min(3, sortedCampains.Count);
            for (int i = 0; i < count; i++)
            {
                var c = sortedCampains[i];
                Console.WriteLine($"{i + 1}. Заказ №{c.Id} | Длительность: {c.Days()} дней");
                Console.WriteLine($"   Даты: {c.StartDate:dd.MM.yyyy} - {c.EndDate:dd.MM.yyyy}");
            }
        }
    }
}
