using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace France___Fix_Events_0._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xmlPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            xmlPathTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void fixEventsButton_Click(object sender, EventArgs e)
        {
            string directoryPath = xmlPathTextBox.Text;
            string[] xmlFilePaths = Directory.GetFiles(directoryPath, "*_LcmsResult.xml", SearchOption.AllDirectories);

            try
            {
                RoadSectionPathInfo[] roadSectionPathsInfo = new RoadSectionPathInfo[xmlFilePaths.Length];
                for (int i = 0; i < xmlFilePaths.Length; i++)
                {
                    roadSectionPathsInfo[i] = new RoadSectionPathInfo(xmlFilePaths[i]);
                }

                XmlFileReader xmlFileReader = new XmlFileReader();
                string[] eventType = new string[xmlFilePaths.Length];
                for (int i = 0; i < xmlFilePaths.Length; i++)
                {
                    eventType[i] = xmlFileReader.GetElementContent(xmlFilePaths[i], "EventInformation", "Type");
                }

                for (int i = 0; i < xmlFilePaths.Length; i++)
                {
                    Console.Write(roadSectionPathsInfo[i].RoadSectionName + "\t");
                    Console.Write(roadSectionPathsInfo[i].RoadNumber + "\t");
                    Console.Write(roadSectionPathsInfo[i].RoadSide + "\t");
                    Console.Write(roadSectionPathsInfo[i].RoadLaneNumber + "\t");
                    Console.Write(roadSectionPathsInfo[i].RoadMeter + " \t");
                    Console.WriteLine(eventType[i]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
