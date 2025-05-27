using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OdataWinClient
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string Url = "https://localhost:7191";
            HttpClient hc = new HttpClient();
            Client c = new Client(Url,hc);
            ICollection<Customer> arr = await c.CustomersApiAllAsync();
            dataGridView1.DataSource=arr.ToArray();
        }
    }
}
