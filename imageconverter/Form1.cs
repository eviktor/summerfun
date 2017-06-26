using DamienG.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

            GetFile();
            //GenerateFileName(file);
            //EncryptFile(@"C:\Users\evikt\Desktop\test.txt", @"C:\Users\evikt\Desktop\enc.txt", secretKey);
            //DecryptFile(@"C:\Users\evikt\Desktop\enc.txt", @"C:\Users\evikt\Desktop\dec.txt", secretKey);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string file = GetFile();
            GenerateFileName(file);
        }


        public string GetFile()
        {
            string file = null;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
            }
            else
            {
                MessageBox.Show("Error");
            }
            label5.Text = file;
            return file;
        }


        static void GenerateFileName(string file)
        {
            string fileNamn = file;
            string path = @"C:\Users\evikt\Desktop";
            string fileName = Path.GetRandomFileName() + "Encrypted.txt";

            string secretKey = GenerateKey();
            GCHandle gch = GCHandle.Alloc(secretKey, GCHandleType.Pinned);
            path = Path.Combine(path, fileName);

            EncryptFile(file, path, secretKey);
        }

        static string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        public static void EncryptFile(string inputFile, string outputFile, string key)
        {

            FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.ASCII.GetBytes(key);
            DES.IV = Encoding.ASCII.GetBytes(key);

            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        static void DecryptFile(string inputFile, string outputFile, string key)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(key);

            FileStream fsread = new FileStream(inputFile, FileMode.Open, FileAccess.Read);

            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);

            StreamWriter fsDecrypted = new StreamWriter(outputFile);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }


    }
}
