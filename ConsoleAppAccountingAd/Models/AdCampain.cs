using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppAccountingAd.Models
{
    public class AdCampain
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int PlatformId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public AdCampain() { }
        public AdCampain(int id, int clientId, int platformId, DateTime startDate, DateTime endDate, string notes)
        {
            Id = id;
            ClientId = clientId;
            PlatformId = platformId;
            if (endDate < startDate)
            {
                StartDate = endDate;
                EndDate = startDate;
            }
            else
            {
                StartDate = startDate;
                EndDate = endDate;
            }
            Notes = notes;
        }
        public int Days()
        {
            int day;
            return day = (EndDate - StartDate).Days;
        }
        public decimal ResultSum(AdPlatform platform)
        {
            int days = Days();
            decimal price = platform.BasePriceDay; 
            if ( days == 0)
            {
                return price;
            }
            return price * days;
        }
        public bool IsActive()
        {
            DateTime today = DateTime.Now;
            return today >= StartDate && today <= EndDate;
        }
        public string Status
        {
            get
            {
                DateTime today = DateTime.Now;
                if (today >= StartDate && today <= EndDate)
                {
                    return "Активен";
                }
                if (today < StartDate)
                {
                    return "Ожидание";
                }
                return "Завершён";
            }
            set { }
        }
        public void Info()
        {
            Console.WriteLine($"[{Id}] / Индификационный номер клиента: {ClientId} / Индификационный номер площадки: {PlatformId} / Продолжительность рекламной компании: {StartDate}-{EndDate} {Days()} / Текущее состояние: {Status} / Примечания клиента: {Notes}");
        }
    }
}
