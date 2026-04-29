using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleAppAccountingAd.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string NameCompany { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Client() { }
        public Client(int id, string nc, string sn, string name, string pn, string ec) 
        {
            Id = id;
            NameCompany = nc;
            Surname = sn;
            Name = name;
            PhoneNumber = pn;
            Email = ec;
        }
        public void Info()
        {
            Console.WriteLine($"[{Id}] {NameCompany} / Контакт: {Surname} {Name} / Телефон для связи: {PhoneNumber} / Email: {Email}");
        }
    }
}
