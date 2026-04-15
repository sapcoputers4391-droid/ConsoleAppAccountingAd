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

                var titleRange = worksheet.Range("A1:F1");
                titleRange.Merge();
                titleRange.Value = "Клиенты";
                titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.FontSize = 14;

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
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
                var heaferRange = worksheet.Range("H1:M1");
                heaferRange.Merge();
                heaferRange.Value = "Платформы";
                heaferRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                heaferRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                heaferRange.Style.Font.Bold = true;
                heaferRange.Style.Font.FontSize = 14;

                worksheet.Cell(2, 15).Value = "Id Заказа";
                worksheet.Cell(2, 16).Value = "Id Клиента";
                worksheet.Cell(2, 17).Value = "Id Платформы";
                worksheet.Cell(2, 18).Value = "Начало заказа";
                worksheet.Cell(2, 19).Value = "Конец заказа";
                worksheet.Cell(2, 20).Value = "Продолжительность";
                worksheet.Cell(2, 21).Value = "Статус";
                worksheet.Cell(2, 22).Value = "Пожелания клиента";

                for(int i = 0; i < campains.Count; i++)
                {
                    worksheet.Cell(i + 3, 15).Value = campains[i].Id;
                    worksheet.Cell(i + 3, 16).Value = campains[i].ClientId;
                    worksheet.Cell(i + 3, 17).Value = campains[i].PlatformId;
                    worksheet.Cell(i + 3, 18).Value = campains[i].StartDate;
                    worksheet.Cell(i + 3, 18).Style.DateFormat.Format = "dd.mm.yyyy";
                    worksheet.Cell(i + 3, 19).Value = campains[i].EndDate;
                    worksheet.Cell(i + 3, 19).Style.DateFormat.Format = "dd.mm.yyyy";
                    worksheet.Cell(i + 3, 20).Value = campains[i].Days();
                    worksheet.Cell(i + 3, 21).Value = campains[i].Status;
                    worksheet.Cell(i + 3, 22).Value = campains[i].Notes;
                }
                var headRange = worksheet.Range("O1:V1");
                headRange.Merge();
                headRange.Value = "Заказы";
                headRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                headRange.Style.Font.Bold = true;
                headRange.Style.Font.FontSize = 14;
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }
        public void ImportData(AccountingController controller, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл базы данных еще не создан.");
                return;
            }

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.Rows();

                int loadedClients = 0;

                foreach (var row in rows)
                {
                    if (row.RowNumber() < 3) continue;
                    var idCell = row.Cell(1);
                    if (!idCell.IsEmpty() && idCell.TryGetValue(out int id))
                    {
                        if (!controller.Clients.Any(c => c.Id == id))
                        {
                            controller.AddClient(new Client(
                                id,
                                row.Cell(2).GetString(),
                                row.Cell(3).GetString(),
                                row.Cell(4).GetString(),
                                row.Cell(5).GetString(),
                                row.Cell(6).GetString()
                            ));
                            loadedClients++;
                        }
                    }
                    var pIdCell = row.Cell(8);
                    if (!pIdCell.IsEmpty() && pIdCell.TryGetValue(out int pId))
                    {
                        if (!controller.Platforms.Any(p => p.Id == pId))
                        {
                            string availStr = row.Cell(13).GetValue<string>();
                            bool isAvail = availStr.Equals("True", StringComparison.OrdinalIgnoreCase) ||
                                           availStr.Equals("Да", StringComparison.OrdinalIgnoreCase);

                            controller.AddAdPlatform(new AdPlatform(
                                pId,
                                row.Cell(9).GetString(),
                                row.Cell(10).GetString(),
                                row.Cell(11).GetValue<decimal>(),
                                row.Cell(12).GetValue<int>(),
                                isAvail
                            ));
                        }
                    }
                }
                Console.WriteLine($"Успешно загружено клиентов: {loadedClients}");
            }
        }
    }
}
