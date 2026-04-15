using ConsoleAppAccountingAd.Services;
using ConsoleAppAccountingAd.UserInterface;
using System;

namespace ConsoleAppAccountingAd
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountingController controller = new AccountingController();
            ExcelManager excel = new ExcelManager();

            string fileName = @"..\..\..\AdAgency_Report.xlsx";
            try
            {
                excel.ImportData(controller, fileName);
                Console.WriteLine("Данные успешно загружены из базы Excel.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("База данных пуста или файл занят другим процессом.");
            }

            MenuHand menu = new MenuHand(controller);
            menu.MainLoop();
        }

    }
}
