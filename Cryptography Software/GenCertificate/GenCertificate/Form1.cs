// -----------------------------------------------------------------------------
//
// Copyright © 2021 Everything Embedded 
// All rights reserved
//
// Unauthorised copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.No liablity assumed.
//
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using Org.BouncyCastle.Crypto;
using System.Numerics;
using System.Security;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Asn1.X509;

namespace GenCertificate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.Description = "Select the folder to store the certificates";
            DialogResult result = FolderBrowser.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBoxFolderName.Text = FolderBrowser.SelectedPath;
                buttonGenerate.Enabled = textBoxFolderName.Text.Length > 0;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                AsymmetricCipherKeyPair CertificateKey;

                //let us first generate the root certificate
                X509Certificate2 X509RootCert = Cryptography.CreateCertificate(textBoxSubject.Text, textBoxIssuer.Text, (int)numericUpDownMonths.Value, out CertificateKey);

                //now let us write the certificates files to the folder 
                File.WriteAllBytes(textBoxFolderName.Text + "\\" + "X509Cert.der", X509RootCert.RawData);

                string PublicPEMFile = textBoxFolderName.Text + "\\" + "X509Cert-public.pem";
                string PrivatePEMFile = textBoxFolderName.Text + "\\" + "X509Cert-private.pem";

                //now let us also create the PEM file as well in case we need it
                using (TextWriter textWriter = new StreamWriter(PublicPEMFile, false))
                {
                    PemWriter pemWriter = new PemWriter(textWriter);
                    pemWriter.WriteObject(CertificateKey.Public);
                    pemWriter.Writer.Flush();
                }

                //now let us also create the PEM file as well in case we need it
                using (TextWriter textWriter = new StreamWriter(PrivatePEMFile, false))
                {
                    PemWriter pemWriter = new PemWriter(textWriter);
                    pemWriter.WriteObject(CertificateKey.Private);
                    pemWriter.Writer.Flush();
                }

                MessageBox.Show("The security certificates have been succcessfully generated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while generating certificates. "+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

