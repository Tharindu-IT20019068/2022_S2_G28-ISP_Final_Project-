using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
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
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Macs;

namespace GenCertificate
{
    public partial class HMAC : Form
    {
        public HMAC()
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
        class Cryptography
        {
            public static string ComputerHMAC(string Text, string Key, IDigest Algorithm)
            {
                HMac hmac = new HMac(Algorithm);
                hmac.Init(new KeyParameter(Encoding.UTF8.GetBytes(Key)));
                byte[] Output = new byte[hmac.GetMacSize()];
                byte[] bytes = Encoding.UTF8.GetBytes(Text);

                hmac.BlockUpdate(bytes, 0, bytes.Length);
                hmac.DoFinal(Output, 0);

                //convert the byte array to a hex string so we can easily compare our output with those from websites
                string Result = BitConverter.ToString(Output).Replace("-", "").ToLower();
                return Result;
            }
        }





    }
}
