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
    }
}
