using System;
using Org.BouncyCastle.Crypto.Digests;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto;

namespace GenHMAC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            IDictionary<IDigest, string> comboSource = new Dictionary<IDigest, string>();
            comboSource.Add(new MD2Digest(), "MD2 Digest");
            comboSource.Add(new MD4Digest(), "MD4 Digest");
            comboSource.Add(new MD5Digest(), "MD5 Digest");
            comboSource.Add(new Gost3411Digest(), "GOST3411 Digest");
            comboSource.Add(new RipeMD128Digest(), "RIPEMD128 Digest");
            comboSource.Add(new RipeMD160Digest(), "RIPEMD160 Digest");
            comboSource.Add(new RipeMD256Digest(), "RIPEMD256 Digest");
            comboSource.Add(new RipeMD320Digest(), "RIPEMD320 Digest");
            comboSource.Add(new Sha1Digest(), "SHA1 Digest");
            comboSource.Add(new Sha224Digest(), "SHA224 Digest");
            comboSource.Add(new Sha256Digest(), "SHA256 Digest");
            comboSource.Add(new Sha384Digest(), "SHA384 Digest");
            comboSource.Add(new Sha512Digest(), "SHA512 Digest");
            comboSource.Add(new TigerDigest(), "Tiger Digest");
            comboSource.Add(new WhirlpoolDigest(), "Whirlpool Digest");

            comboBoxAlgorithm.DataSource = new BindingSource(comboSource, null);
            comboBoxAlgorithm.DisplayMember = "Value";
            comboBoxAlgorithm.ValueMember = "Key";
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            textBoxKey.Enabled = textBoxInput.Text.Length > 0;
        }
        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {
            buttonGenHMAC.Enabled = textBoxKey.Text.Length > 0;
            comboBoxAlgorithm.Enabled = textBoxKey.Text.Length > 0;
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGenHMAC_Click(object sender, EventArgs e)
        {
            try
            {
                IDigest AlgorithmToUse = (IDigest)comboBoxAlgorithm.SelectedValue;
                textBoxHMAC.Text = Cryptography.ComputerHMAC(textBoxInput.Text, textBoxKey.Text, AlgorithmToUse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }


    }
}
