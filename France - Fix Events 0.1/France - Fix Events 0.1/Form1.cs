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
using Npgsql;

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
            try
            {
                string directoryPath = xmlPathTextBox.Text;
                string[] xmlFilePaths = Directory.GetFiles(directoryPath, "*_LcmsResult.xml", SearchOption.AllDirectories);

                // Getting road info from xml file path
                RoadPathInfo[] roadPathsInfo = new RoadPathInfo[xmlFilePaths.Length];
                for (int i = 0; i < xmlFilePaths.Length; i++)
                {
                    roadPathsInfo[i] = new RoadPathInfo(xmlFilePaths[i]);
                }

                // Getting info about event type from xml file
                XmlFileReader xmlFileReader = new XmlFileReader();
                string[] eventsType = new string[xmlFilePaths.Length];
                for (int i = 0; i < xmlFilePaths.Length; i++)
                {
                    eventsType[i] = xmlFileReader.GetElementContent(xmlFilePaths[i], "EventInformation", "Type");
                }

                // Updating geometries from database
                SQLManager sqlManager = new SQLManager();
                string connectionString = sqlManager.GetConnectionString(
                    hostTextBox.Text,
                    int.Parse(portTextBox.Text),
                    loginTextBox.Text,
                    passwordTextBox.Text,
                    databaseComboBox.SelectedItem.ToString());
                sqlManager.UpdateGeometries(eventsType, roadPathsInfo, connectionString);

                Console.WriteLine("Geometries Updated");
                MessageBox.Show("Geometries Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            try
            {
                SQLManager sqlManager = new SQLManager();
                string connectionString = sqlManager.GetConnectionString(
                    hostTextBox.Text,
                    int.Parse(portTextBox.Text),
                    loginTextBox.Text,
                    passwordTextBox.Text,
                    "postgres");
                databaseComboBox.Items.Clear();
                databaseComboBox.Items.AddRange(sqlManager.GetDatabaseNames(connectionString));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
