using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConStrCC;
using ConnectionCC;

namespace ConStrCC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox3.Text = StringCipherCC.Encrypt(textBox1.Text, textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //textBox3.Text = StringCipherCC.Decrypt(textBox1.Text, textBox2.Text);
            try
            {
                string msg = string.Empty;
                //GetConnection getCon = new GetConnection();
                ////msg = getCon.testConnection("Server=CGKDCPWDB01,54218;Database=caresnet;User Id=user_appsup1;Password = abcd.1234");
                DataTable dt = new DataTable();
                //dt = getCon.execSqlReturnDataTable("select * from ServiceRequestStatus(nolock)", "Server=CGKDCPWDB01,54218;Database=caresnet;User Id=user_appsup1;Password = abcd.1234");

                using (Database db = new Database("Server=CGKDCPWDB01,54218;Database=caresnet;User Id=user_appsup1;Password = abcd.1234"))
                {
                    //db.ExecuteText("select * from ServiceRequestStatus(nolock)");
                    dt = db.ExecuteTextRetrieveDataTable("select * from ServiceRequestStatus(nolock)");
                }

                    MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed");
                //throw;
            }
        }
    }
}
