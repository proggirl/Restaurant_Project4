using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Project4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Server server = new Server();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int chQ = Convert.ToInt32(txtCh.Text);
                int eQ = Convert.ToInt32(txtEgg.Text);
                string d = comboBox1.SelectedItem.ToString();
                string customer = textBox1.Text;
                if (string.IsNullOrEmpty(customer))
                {
                    MessageBox.Show("Please enter customer name");
                    return;
                }


                server.Receive(customer, chQ, eQ, d);
                string s = "Customer " + customer + " ordered ";
                if (chQ > 0)
                    s += "Chicken " + chQ;
                if (eQ > 0)
                    s += ", Egg " + eQ;
                s += " " + d;
                listBox1.Items.Add(s);

            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                var (data, quality) = server.Send();
                
                foreach(var item in data)
                {
                    listBox1.Items.Add(item);
                }
                label5.Text = quality;
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                var (strs, quality) = server.ServeAll(server.requests);
                foreach (var s in strs)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        listBox1.Items.Add(s);
                    }

                }
                label5.Text = quality;
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
    }
}
