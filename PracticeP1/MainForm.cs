using System;
using System.Windows.Forms;
using System.Xml;

namespace PracticeP1
{
    public partial class MainForm : Form
    {
        private string loadedFilePath;
        private XmlDocument xmlDoc;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadXMLButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                openFileDialog.Title = "Select an XML File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        loadedFilePath = openFileDialog.FileName;
                        xmlDoc = new XmlDocument();
                        xmlDoc.Load(loadedFilePath);
                        MessageBox.Show("Файл загружен");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки файла: {ex.Message}");
                    }
                }
            }
        }

        private void DeleteXMLButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(loadedFilePath))
            {
                loadedFilePath = null;
                xmlDoc = null;
                MessageBox.Show("Файл удален");
            }
            else
            {
                MessageBox.Show("Файл не был добавлен");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (xmlDoc != null)
            {
                this.Hide();
                var excelForm = new ExcelForm(xmlDoc);
                excelForm.Show();
            }
            else
            {
                MessageBox.Show("Загрузите XML файл");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}