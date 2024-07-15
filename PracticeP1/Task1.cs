using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PracticeP1
{
    public partial class Task1 : Form
    {
        private XmlDocument xmlDoc;
        private Dictionary<string, List<int>> selectedTrainNumbers = new Dictionary<string, List<int>>();
        private DateTime D1;
        private DateTime D2;

        public Task1(XmlDocument document)
        {
            InitializeComponent();
            xmlDoc = document;
            LoadTrainNumbers();
            LoadRpNames();
            LoadDates();

            startHourPicker.Minimum = 0;
            startHourPicker.Maximum = 23;
            startMinutePicker.Minimum = 0;
            startMinutePicker.Maximum = 59;
            endHourPicker.Minimum = 0;
            endHourPicker.Maximum = 23;
            endMinutePicker.Minimum = 0;
            endMinutePicker.Maximum = 59;

            treeView1.AfterCheck += TreeView1_AfterCheck;

            ExcelFileButton.Click += ExcelFileButton_Click;

            this.FormClosing += Task1_FormClosing; 
        }

        private void LoadDates()
        {
            XmlNode periodNode = xmlDoc.SelectSingleNode("//Period");
            XmlNode frequencyPeriodNode = xmlDoc.SelectSingleNode("//FrequencyPeriod");

            if (periodNode != null)
            {
                D1 = DateTime.ParseExact(periodNode.Attributes["D1"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
                D2 = DateTime.ParseExact(periodNode.Attributes["D2"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
            }
            else if (frequencyPeriodNode != null)
            {
                D1 = DateTime.ParseExact(frequencyPeriodNode.Attributes["D1"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
                D2 = DateTime.ParseExact(frequencyPeriodNode.Attributes["D2"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
            }
            else
            {
                MessageBox.Show("Не удалось найти даты D1 и D2 в XML файле.");
                return;
            }

            dateTimePickerStart.MinDate = D1.AddDays(-1);
            dateTimePickerStart.MaxDate = D2.AddDays(1);
            dateTimePickerEnd.MinDate = D1.AddDays(-1);
            dateTimePickerEnd.MaxDate = D2.AddDays(1);
        }

        private void LoadTrainNumbers()
        {
            treeView1.CheckBoxes = true;
            treeView1.Nodes.Clear();

            TreeNode passengerNode = new TreeNode("Пассажирские");
            TreeNode suburbanNode = new TreeNode("Пригородные");
            TreeNode cargoNode = new TreeNode("Грузовые");
            TreeNode othersNode = new TreeNode("Прочие");

            treeView1.Nodes.Add(passengerNode);
            treeView1.Nodes.Add(suburbanNode);
            treeView1.Nodes.Add(cargoNode);
            treeView1.Nodes.Add(othersNode);

            XmlNodeList trainRangeNodes = xmlDoc.SelectNodes("//TrainRange");
            foreach (XmlNode trainRangeNode in trainRangeNodes)
            {
                string trainRangeName = trainRangeNode.Attributes["name"].Value;
                string ranges = trainRangeNode.Attributes["ranges"].Value;

                TreeNode categoryNode = null;

                if (trainRangeName.StartsWith("пассажирские", StringComparison.OrdinalIgnoreCase))
                {
                    categoryNode = passengerNode;
                }
                else if (trainRangeName.StartsWith("пригородные", StringComparison.OrdinalIgnoreCase))
                {
                    categoryNode = suburbanNode;
                }
                else if (trainRangeName.StartsWith("грузовые", StringComparison.OrdinalIgnoreCase))
                {
                    categoryNode = cargoNode;
                }
                else
                {
                    categoryNode = othersNode;
                }

                TreeNode trainRangeNodeUI = new TreeNode(trainRangeName);
                categoryNode.Nodes.Add(trainRangeNodeUI);

                string[] trainRanges = ranges.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string trainRange in trainRanges)
                {
                    string[] rangeParts = trainRange.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (rangeParts.Length == 2)
                    {
                        int startNumber = int.Parse(rangeParts[0]);
                        int endNumber = int.Parse(rangeParts[1]);

                        for (int number = startNumber; number <= endNumber; number++)
                        {
                            TreeNode trainNode = new TreeNode(number.ToString());
                            trainRangeNodeUI.Nodes.Add(trainNode);
                        }
                    }
                }
            }
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                SetChildNodesCheckedState(e.Node, e.Node.Checked);
            }
        }

        private void SetChildNodesCheckedState(TreeNode node, bool isChecked)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = isChecked;
                SetChildNodesCheckedState(childNode, isChecked);
            }
        }

        private void LoadRpNames()
        {
            XmlNodeList rpNodes = xmlDoc.SelectNodes("//RP");
            foreach (XmlNode node in rpNodes)
            {
                RpBox.Items.Add(node.Attributes["name"].Value);
            }
        }

        private void ExcelFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedStation = RpBox.SelectedItem?.ToString();

                List<int> selectedTrainNumbers = new List<int>();

                foreach (TreeNode categoryNode in treeView1.Nodes)
                {
                    foreach (TreeNode trainRangeNode in categoryNode.Nodes)
                    {
                        if (trainRangeNode.Checked)
                        {
                            foreach (TreeNode trainNode in trainRangeNode.Nodes)
                            {
                                selectedTrainNumbers.Add(int.Parse(trainNode.Text));
                            }
                        }
                    }
                }

                if (selectedTrainNumbers.Count == 0 || string.IsNullOrEmpty(selectedStation))
                {
                    MessageBox.Show("Выберите категорию поездов и станцию");
                    return;
                }

                DateTime startDate = dateTimePickerStart.Value;
                DateTime endDate = dateTimePickerEnd.Value;

                int startHour = (int)startHourPicker.Value;
                int startMinute = (int)startMinutePicker.Value;
                int endHour = (int)endHourPicker.Value;
                int endMinute = (int)endMinutePicker.Value;

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

                        DateTime eventDate = dateTimePickerStart.Value.Date;
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
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private bool IsStationMatch(string stationName, string selectedStation)
        {
            string mainStationName = stationName.Split(' ')[0];
            return mainStationName == selectedStation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            var mainForm = new MainForm();
            mainForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Task1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}