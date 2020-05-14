using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProcessDetector
{
    public partial class DetectorForm : Form
    {
        string uuu;

        public DetectorForm(string username)
        {
            InitializeComponent();
            uuu = username;
            textBox1.Text = uuu;
            OnRefreshGrid(null, null);
            //в реальном времени отслеживание процессов
            //Timer ticker = new Timer();
            //ticker.Interval = 250;
            //ticker.Tick += OnRefreshGrid;
            //ticker.Start();
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        void OnRefreshGrid(object sender, EventArgs e)
        {
            BindingSource source = new BindingSource();
            var table = new DataTable("Process List");

            Process[] processes = Process.GetProcesses();

            table.Columns.Add("Название процесса");
            table.Columns.Add("Идентификатор");
            table.Columns.Add("MachineName");
            table.Columns.Add("Пользователь");



            for (int i = 0; i < processes.Length; ++i)
            {

                if ((processes[i].ProcessName != "svchost") && (processes[i].ProcessName != "conhost") && (processes[i].ProcessName != "lsass") && (processes[i].ProcessName != "explorer") && (processes[i].ProcessName != "sihost") && (processes[i].ProcessName != "system"))
                {
                    
                    table.Rows.Add(new object[] { processes[i].ProcessName + ".exe", processes[i].Id, Environment.MachineName, uuu });

                   string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\VSProjects\\SoftwareInventory\\SoftwareInventory\\SoftwareInventory\\App_Data\\si_db.mdf;Integrated Security=True;Connection timeout=60";
                    SqlConnection Connection;
                    Connection = new SqlConnection(connectionString);

                    try
                    {
                        Connection.Open();
                        string query = "INSERT INTO UsersApp (processName, machineName, processId, userName) VALUES (@processName, @machineName, @processId, @userName)";

                        SqlCommand command = new SqlCommand(query, Connection);
                        command.Parameters.Add("@processName", processes[i].ProcessName);
                        command.Parameters.Add("@machineName", Environment.MachineName);
                        command.Parameters.Add("@processId", processes[i].Id);
                        command.Parameters.Add("@userName", uuu);

                        command.ExecuteNonQuery();
                        Connection.Close();

                    }
                    catch (SqlException ex)
                    {
                        label1.Text = "connect fail";
                        MessageBox.Show(ex.Message);
                    }

                }
            }

            table.AcceptChanges();
            source.DataSource = table;

            int scroll = dataGridView1.FirstDisplayedScrollingRowIndex;
            dataGridView1.DataSource = source;



            if (scroll != -1)
                dataGridView1.FirstDisplayedScrollingRowIndex = scroll;

        }

        private void DetectorForm_Load(object sender, EventArgs e)
        {

        }

 
        
    }
}
