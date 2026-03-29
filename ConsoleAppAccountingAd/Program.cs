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

            MenuHand menu = new MenuHand(controller);
            menu.MainLoop();
        }

    }
}
