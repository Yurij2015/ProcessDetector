using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessDetector
{
    public partial class MainForm : Form
    {

        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\VSProjects\\SoftwareInventory\\SoftwareInventory\\SoftwareInventory\\App_Data\\si_db.mdf;Integrated Security=True;";
        string key = "key2020";


        public MainForm()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == key)
            {
                DetectorForm detectorForm = new DetectorForm();
                detectorForm.Show();
            }
            else
            {
                MessageBox.Show("Не правильный ключ доступа для пользователя " + textBox1.Text);
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            MessageBox.Show("Соединение установлено!");
            label3.Text = "Соединение установлено!";
            cnn.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
