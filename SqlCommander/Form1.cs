using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace SqlCommander
{
    public partial class Form1 : Form
    {
        static MySqlConnection conn;
        static List<Type> types = new List<Type>();
        public Form1()
        {
            InitializeComponent();

            InitComboBox();
            conn = MySqlConnector.Open();
        }
        void InitComboBox()
        {
            comboBox1.Items.Clear();

            types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IRegulation)))).ToList();
            foreach (Type t in types)
            {
                comboBox1.Items.Add(t.Name);
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.Text = "没有解析组件";
        }

        IRegulation GetSelectRegulation()
        {
            Type t = types[comboBox1.SelectedIndex];
            return Activator.CreateInstance(t) as IRegulation;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("请先打开文件");
                return;
            }
            StreamReader reader = new StreamReader(textBox1.Text);
            int currentLine = 1;
            string line = reader.ReadLine();
            if (comboBox1.SelectedIndex < 0)
                return;
            if (line != null)
            {
                string createTableCommand = GetSelectRegulation().GetCreateTableCommand(line);
                try
                {
                    MySqlCommand mycmd = new MySqlCommand(createTableCommand, conn);

                    mycmd.ExecuteNonQuery();

                    while (line != null)
                    {
                        string insertTableCommand = GetSelectRegulation().GetInsertTableCommand(line);
                        mycmd = new MySqlCommand(insertTableCommand, conn);
                        mycmd.ExecuteNonQuery();
                        line = reader.ReadLine();
                        currentLine++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(currentLine + "Occur Error:" + ex.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tableName = GetSelectRegulation().TableName;
            MySqlCommand mycmd = new MySqlCommand("Drop table " + tableName, conn);

            mycmd.ExecuteNonQuery();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }
    }
}
