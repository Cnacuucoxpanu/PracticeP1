using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;

namespace PracticeP1
{
    public class XmlDataReader
    {
        private XmlDocument xmlDoc;

        public XmlDataReader(XmlDocument document)
        {
            xmlDoc = document;
        }

        public void LoadTrainNumbers(TreeView treeView)
        {
            treeView.CheckBoxes = true;
            treeView.Nodes.Clear();

            TreeNode passengerNode = new TreeNode("Пассажирские");
            TreeNode suburbanNode = new TreeNode("Пригородные");
            TreeNode cargoNode = new TreeNode("Грузовые");
            TreeNode othersNode = new TreeNode("Прочие");

            treeView.Nodes.Add(passengerNode);
            treeView.Nodes.Add(suburbanNode);
            treeView.Nodes.Add(cargoNode);
            treeView.Nodes.Add(othersNode);

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

        public void SetChildNodesCheckedState(TreeNode node, bool isChecked)
        {
            foreach (TreeNode childNode in node.Nodes)
            {
                childNode.Checked = isChecked;
                SetChildNodesCheckedState(childNode, isChecked);
            }
        }

        public void LoadRpNames(ComboBox rpBox)
        {
            rpBox.Items.Clear();

            XmlNodeList rpNodes = xmlDoc.SelectNodes("//RP");
            foreach (XmlNode node in rpNodes)
            {
                rpBox.Items.Add(node.Attributes["name"].Value);
            }
        }

        public (DateTime, DateTime) GetDates()
        {
            XmlNode periodNode = xmlDoc.SelectSingleNode("//Period");
            XmlNode frequencyPeriodNode = xmlDoc.SelectSingleNode("//FrequencyPeriod");

            if (periodNode != null)
            {
                DateTime D1 = DateTime.ParseExact(periodNode.Attributes["D1"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
                DateTime D2 = DateTime.ParseExact(periodNode.Attributes["D2"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
                return (D1, D2);
            }
            else if (frequencyPeriodNode != null)
            {
                DateTime D1 = DateTime.ParseExact(frequencyPeriodNode.Attributes["D1"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
                DateTime D2 = DateTime.ParseExact(frequencyPeriodNode.Attributes["D2"].Value, "dd.MM.yy", CultureInfo.InvariantCulture);
                return (D1, D2);
            }
            else
            {
                return (default, default);
            }
        }

        public List<int> GetSelectedTrainNumbers(TreeView treeView)
        {
            List<int> selectedTrainNumbers = new List<int>();

            foreach (TreeNode categoryNode in treeView.Nodes)
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

            return selectedTrainNumbers;
        }
    }
}
