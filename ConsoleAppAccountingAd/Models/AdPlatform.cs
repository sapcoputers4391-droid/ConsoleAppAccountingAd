using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace ConsoleAppAccountingAd.Models
{
    public class AdPlatform
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public decimal BasePriceDay { get; set; }
        public int ApproximateReach {  get; set; }
        public bool IsAvailable { get; set; }
        public AdPlatform() { }
        public AdPlatform(int id, string title, string category, decimal basePriceDay, int approximateReach, bool isAvailable)
        {
            Id = id;
            Title = title;
            Category = category;
            BasePriceDay = basePriceDay;
            ApproximateReach = approximateReach;
            IsAvailable = isAvailable;
        }
        public void Info()
        {
            Console.WriteLine($"[{Id}] {Title} / Категория: {Category} / Стоимость размещения рекламы в сутки: {BasePriceDay} / Приблизительный охват людей: {ApproximateReach} / Статус доступности: {IsAvailable}");
        }
    }
}
