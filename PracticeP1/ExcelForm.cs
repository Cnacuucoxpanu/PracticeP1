using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Xml;

namespace PracticeP1
{
    public partial class ExcelForm : Form
    {
        private XmlDocument xmlDoc;
        private DateTime D1;
        private DateTime D2;
        private XmlDataReader xmlDataReader;
        private ExcelExporter excelExporter;

        public ExcelForm(XmlDocument document)
        {
            InitializeComponent();
            xmlDoc = document;
            xmlDataReader = new XmlDataReader(xmlDoc);
            excelExporter = new ExcelExporter();

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
        }

        private void LoadDates()
        {
            (D1, D2) = xmlDataReader.GetDates();
            if (D1 == default || D2 == default)
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
            xmlDataReader.LoadTrainNumbers(treeView1);
        }

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                xmlDataReader.SetChildNodesCheckedState(e.Node, e.Node.Checked);
            }
        }

        private void LoadRpNames()
        {
            xmlDataReader.LoadRpNames(RpBox);
        }

        private void ExcelFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedStation = RpBox.SelectedItem?.ToString();
                List<int> selectedTrainNumbers = xmlDataReader.GetSelectedTrainNumbers(treeView1);

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

                excelExporter.ExportToExcel(xmlDoc, selectedTrainNumbers, selectedStation, startDate, endDate, startHour, startMinute, endHour, endMinute);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            var mainForm = new MainForm();
            mainForm.Show();
        }

    }
}
