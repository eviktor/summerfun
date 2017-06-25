using DamienG.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imageconverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string text1 = richTextBox1.Text;

            label2.Text = GenerateHash(text1);
            label4.Text = GenerateMD5(text1);

        }
        public string GenerateHash(string text)
        {
            string hashString = text;

            using (var sha512 = SHA1.Create())
            {
                SHA1Managed crypt = new SHA1Managed();
                var hash = new StringBuilder();

                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
                Console.WriteLine(hash.ToString());
                return hash.ToString();
            }
        }
        public string GenerateMD5(string text)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(text);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach(byte theByte in hash)
            {
                sb.Append(theByte.ToString("x2"));
            }
            return sb.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt";
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dialog.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }
    }
}
