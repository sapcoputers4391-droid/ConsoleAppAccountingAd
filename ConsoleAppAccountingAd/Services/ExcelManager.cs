using ClosedXML.Excel;
using ConsoleAppAccountingAd.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ConsoleAppAccountingAd.Services
{
    internal class ExcelManager
    {
        public static void OpenInExcel(string fileName)
        {
            if (File.Exists(fileName))
            { 
                Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("Ошибка: Сначала нужно сохранить данные в файл!");
            }
        }
        public void ExportOther(List<Client> clients, List<AdPlatform> platforms, List<AdCampain> campains, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Учет рекламы");
                worksheet.Cell(2, 1).Value = "Id";
                worksheet.Cell(2, 2).Value = "Название компании клиента";
                worksheet.Cell(2, 3).Value = "Фамилия клиента";
                worksheet.Cell(2, 4).Value = "Имя клиентиа";
                worksheet.Cell(2, 5).Value = "номер мобильного телефона";
                worksheet.Cell(2, 6).Value = "Email клиента";

                var headerRange = worksheet.Range("A1:E1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                for (int i = 0; i < clients.Count; i++)
                {
                    worksheet.Cell(i + 3, 1).Value = clients[i].Id;
                    worksheet.Cell(i + 3, 2).Value = clients[i].NameCompany;
                    worksheet.Cell(i + 3, 3).Value = clients[i].Surname;
                    worksheet.Cell(i + 3, 4).Value = clients[i].Name;
                    worksheet.Cell(i + 3, 5).Value = clients[i].PhoneNumber;
                    worksheet.Cell(i + 3, 6).Value = clients[i].Email;
                }
                worksheet.Cell(2, 8).Value = "Id";
                worksheet.Cell(2, 9).Value = "Название платформы";
                worksheet.Cell(2, 10).Value = "Тип носителя";
                worksheet.Cell(2, 11).Value = "Стоимость размещения рекламы в день(бел. руб.)";
                worksheet.Cell(2, 12).Value = "Приблизительный охват людей";
                worksheet.Cell(2, 13).Value = "Доступность";

                for (int i = 0; i < platforms.Count; i++)
                {
                    worksheet.Cell(i + 3, 8).Value = platforms[i].Id;
                    worksheet.Cell(i + 3, 9).Value = platforms[i].Title;
                    worksheet.Cell(i + 3, 10).Value = platforms[i].Category;
                    worksheet.Cell(i + 3, 11).Value = platforms[i].BasePriceDay;
                    worksheet.Cell(i + 3, 12).Value = platforms[i].ApproximateReach;
                    worksheet.Cell(i + 3, 13).Value = platforms[i].IsAvailable;
                }
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }
    }
}
