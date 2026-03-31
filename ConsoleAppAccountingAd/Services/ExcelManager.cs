using ClosedXML.Excel;
using ConsoleAppAccountingAd.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppAccountingAd.Services
{
    internal class ExcelManager
    {
        public void ExportOrders(List<Client> clients, List<AdPlatform> platforms, List<AdCampain> campains, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Учет рекламы");
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Название компании клиента";
                worksheet.Cell(1, 3).Value = "Фамилия клиента";
                worksheet.Cell(1, 4).Value = "Имя клиентиа";
                worksheet.Cell(1, 5).Value = "номер мобильного телефона";
                worksheet.Cell(1, 6).Value = "Email клиента";

                var headerRange = worksheet.Range("A1:E1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                for (int i = 0; i < clients.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = clients[i].Id;
                    worksheet.Cell(i + 2, 2).Value = clients[i].NameCompany;
                    worksheet.Cell(i + 2, 3).Value = clients[i].Surname;
                    worksheet.Cell(i + 2, 4).Value = clients[i].Name;
                    worksheet.Cell(i + 2, 5).Value = clients[i].PhoneNumber;
                    worksheet.Cell(i + 2, 6).Value = clients[i].PhoneNumber;
                }

                worksheet.Columns().AdjustToContents(); 
                workbook.SaveAs(filePath);
            }
        }
    }
}
