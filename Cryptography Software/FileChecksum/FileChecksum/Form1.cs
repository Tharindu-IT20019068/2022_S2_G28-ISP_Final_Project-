using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto;
using System.IO;

namespace FileChecksum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

            comboBoxDigest.DataSource = new BindingSource(comboSource, null);
            comboBoxDigest.DisplayMember = "Value";
            comboBoxDigest.ValueMember = "Key";
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files(*.*) | *.*";
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                textBoxFile.Text = openFileDialog.FileName;
            }
            button1.Enabled = textBoxFile.Text.Length > 0;
            comboBoxDigest.Enabled = textBoxFile.Text.Length > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxChecksum.Text = ComputeChecksum(textBoxFile.Text, (IDigest)comboBoxDigest.SelectedValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private string ComputeChecksum(string FileName, IDigest Algorithm)
        {
            byte[] BytesToSign = File.ReadAllBytes(FileName);
            byte[] TheHash = new byte[Algorithm.GetDigestSize()];
            Algorithm.BlockUpdate(BytesToSign, 0, BytesToSign.Length);
            Algorithm.DoFinal(TheHash, 0);
            string Result = BitConverter.ToString(TheHash ?? new byte[] { }).Replace("-", "");

            return Result;
        }
    }
}
