using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessDetector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            OnRefreshGrid(null, null);
            Timer ticker = new Timer();
            ticker.Interval = 250;
            ticker.Tick += OnRefreshGrid;
            ticker.Start();

        }

        void OnRefreshGrid(object sender, EventArgs e)
        {
            BindingSource source = new BindingSource();
            var table = new DataTable("Process List");

            Process[] processes = Process.GetProcesses();

            table.Columns.Add("Name");
            table.Columns.Add("Id");
            table.Columns.Add("UserName");


            for (int i = 0; i < processes.Length; ++i)
            {
                table.Rows.Add(new object[] { processes[i].ProcessName + ".exe", processes[i].Id, Environment.MachineName });
            }

            table.AcceptChanges();
            source.DataSource = table;

            int scroll = dataGridView1.FirstDisplayedScrollingRowIndex;
            dataGridView1.DataSource = source;



            if (scroll != -1)
                dataGridView1.FirstDisplayedScrollingRowIndex = scroll;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
