using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalSigning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PEM files(*.PEM) | *.PEM";
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                textBoxPEM.Text = openFileDialog.FileName;
            }

            textBoxInput.Enabled = textBoxPEM.Text.Length > 0;
            buttonSign.Enabled = textBoxPEM.Text.Length > 0 && textBoxInput.Text.Length > 0;
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxSignature.Text = Cryptography.RSASigntWithPEMPrivateKey(textBoxPEM.Text, textBoxInput.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Signing Error");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            buttonSign.Enabled = textBoxPEM.Text.Length > 0 && textBoxInput.Text.Length > 0;
        }
    }
}
