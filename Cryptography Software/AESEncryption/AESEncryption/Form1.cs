using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace AESEncryption
{
    public partial class Form1 : Form
    {
        private static byte[] IV = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0, 0, 0, 0 }; //initial explanation 
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxEncryptedOutput.Text = Encrypt(textBoxInput.Text, textBoxEncryptPassword.Text, IV);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxDecryptOutput.Text = Decrypt(textBoxEncrypted.Text, textBoxDcryptPassword.Text, IV);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            textBoxEncryptPassword.Enabled = textBoxInput.Text.Length > 0;
            buttonEncrypt.Enabled = textBoxInput.Text.Length > 0;
        }

        private void textBoxEncryptPassword_TextChanged(object sender, EventArgs e)
        {
            textBoxInput.Enabled = textBoxInput.Text.Length > 0;
            buttonEncrypt.Enabled = textBoxInput.Text.Length > 0;
        }

        private void textBoxEncrypted_TextChanged(object sender, EventArgs e)
        {
            textBoxDcryptPassword.Enabled = textBoxInput.Text.Length > 0;
            buttonDecrypt.Enabled = textBoxInput.Text.Length > 0;
        }

        private void textBoxDcryptPassword_TextChanged(object sender, EventArgs e)
        {
            textBoxEncrypted.Enabled = textBoxInput.Text.Length > 0;
            buttonDecrypt.Enabled = textBoxInput.Text.Length > 0;
        }

        private string Encrypt(string plainText, string Password, byte[] IV)
        {
            byte[] Key = Encoding.UTF8.GetBytes(Password);

            // Create a new AesManaged.    
            AesManaged aes = new AesManaged();
            aes.Key = Key;
            aes.IV = IV;

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] InputBytes = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(InputBytes, 0, InputBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] Encrypted = memoryStream.ToArray();
            // Return encrypted data    
            return Convert.ToBase64String(Encrypted);
        }

        private string Decrypt(string plaintext, string Password, byte[] IV)
        {
            byte[] Key = Encoding.UTF8.GetBytes(Password);

            // Create a new AesManaged.    
            AesManaged aes = new AesManaged();
            aes.Key = Key;
            aes.IV = IV;

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

            byte[] InputBytes = Convert.FromBase64String(plaintext);
            cryptoStream.Write(InputBytes, 0, InputBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] Decrypted = memoryStream.ToArray();
            // Return encrypted data    
            return UTF8Encoding.UTF8.GetString(Decrypted, 0, Decrypted.Length);
        }

        private void textBoxEncryptPassword_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length != 16)
            {
                MessageBox.Show("You need to write at least 16 characters.");
            }
        }

        private void textBoxInput_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(textBoxInput.Text.Length > 0)
            {
                byte[] InputBytes = Encoding.UTF8.GetBytes(textBoxInput.Text);
                textBoxDebug.Text = BitConverter.ToString(InputBytes).ToLower().Replace("-"," ");
            }
        }

        private void textBoxEncryptPassword_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBoxEncryptPassword.Text.Length > 0)
            {
                byte[] InputBytes = Encoding.UTF8.GetBytes(textBoxEncryptPassword.Text);
                textBoxDebug.Text = BitConverter.ToString(InputBytes).ToLower().Replace("-", " ");
            }
        }

        private void textBoxEncryptedOutput_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBoxEncryptedOutput.Text.Length > 0)
            {
                byte[] InputBytes = Convert.FromBase64String(textBoxEncryptedOutput.Text);
                textBoxDebug.Text = BitConverter.ToString(InputBytes).ToLower().Replace("-", " ");
            }
        }

        private void textBoxEncrypted_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            byte[] InputBytes = Convert.FromBase64String(textBoxEncrypted.Text);
            textBoxDebug.Text = BitConverter.ToString(InputBytes).ToLower().Replace("-", " ");
        }

        private void textBoxDcryptPassword_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBoxDcryptPassword.Text.Length > 0)
            {
                byte[] InputBytes = Encoding.UTF8.GetBytes(textBoxDcryptPassword.Text);
                textBoxDebug.Text = BitConverter.ToString(InputBytes).ToLower().Replace("-", "");
            }
        }

        private void textBoxEncryptedOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
