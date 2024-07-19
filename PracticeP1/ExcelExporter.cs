using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Xml;

namespace PracticeP1
{
    public class ExcelExporter
    {
        public void ExportToExcel(XmlDocument xmlDoc, List<int> selectedTrainNumbers, string selectedStation, DateTime startDate, DateTime endDate, int startHour, int startMinute, int endHour, int endMinute)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Train Schedule");

            worksheet.Cell(1, 1).Value = "Train Number";
            worksheet.Cell(1, 2).Value = "Station Name";
            worksheet.Cell(1, 3).Value = "Arrival Time";
            worksheet.Cell(1, 4).Value = "Departure Time";
            worksheet.Cell(1, 5).Value = "Date";

            int currentRow = 2;

            XmlNodeList threadNodes = xmlDoc.SelectNodes("//Thread");
            foreach (XmlNode threadNode in threadNodes)
            {
                string trainNumber = threadNode.Attributes["trainNum"].Value;

                if (!selectedTrainNumbers.Contains(int.Parse(trainNumber)))
                    continue;

                XmlNodeList eventNodes = threadNode.SelectNodes("Event");
                foreach (XmlNode eventNode in eventNodes)
                {
                    string stationName = eventNode.Attributes["nameRP"].Value;
                    if (!IsStationMatch(stationName, selectedStation))
                        continue;

                    int arr = int.Parse(eventNode.Attributes["arr"].Value) / 2;
                    int dep = int.Parse(eventNode.Attributes["dep"].Value) / 2;

                    DateTime eventDate = startDate.Date;
                    DateTime arrivalTime = eventDate.AddMinutes(arr);
                    DateTime departureTime = eventDate.AddMinutes(dep);

                    if (arrivalTime.Hour >= startHour && arrivalTime.Hour < endHour)
                    {
                        if (arrivalTime.Hour == endHour && arrivalTime.Minute > endMinute)
                            continue;

                        worksheet.Cell(currentRow, 1).Value = trainNumber;
                        worksheet.Cell(currentRow, 2).Value = stationName;
                        worksheet.Cell(currentRow, 3).Value = arrivalTime.ToString("HH:mm");
                        worksheet.Cell(currentRow, 4).Value = departureTime.ToString("HH:mm");
                        worksheet.Cell(currentRow, 5).Value = eventDate.ToString("dd.MM.yy");

                        currentRow++;
                    }
                }
            }

            if (currentRow == 2)
            {
                MessageBox.Show("По выбранным категориям поездов не найдено");
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Excel файл создан");
                }
            }
        }

        private bool IsStationMatch(string stationName, string selectedStation)
        {
            string mainStationName = stationName.Split(' ')[0];
            return mainStationName == selectedStation;
        }
    }
}
