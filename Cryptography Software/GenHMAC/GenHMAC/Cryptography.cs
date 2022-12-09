// -----------------------------------------------------------------------------
//
// Copyright © 2021 Everything Embedded 
// All rights reserved
//
// Unauthorised copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential. No liablity assumed.
//
//-----------------------------------------------------------------------------

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
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

namespace GenHMAC
{
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
            string Result = BitConverter.ToString(Output).Replace("-","").ToLower();
            return Result;
        }
    }
}
