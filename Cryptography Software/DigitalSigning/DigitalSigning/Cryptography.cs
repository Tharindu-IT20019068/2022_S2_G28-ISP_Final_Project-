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

namespace DigitalSigning
{
    class Cryptography
    {
        public static string RSASigntWithPEMPrivateKey(string PrivateKeyPEMFileName, string Text)
        {
            byte[] BytesToSign = Encoding.UTF8.GetBytes(Text);
            AsymmetricCipherKeyPair KeyPair = null;
            TextReader reader = File.OpenText(PrivateKeyPEMFileName);
            KeyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

            byte[] Signature = RSASigntWithPrivateKey(KeyPair, BytesToSign);
            string Result = Convert.ToBase64String(Signature);

            return Result;
        }

        public static byte[] RSASigntWithPrivateKey(AsymmetricCipherKeyPair KeyPair, byte[] BytesToSign)
        {
            // compute the SHA 256 hash from the bytes to sign received
            Sha256Digest sha256Digest = new Sha256Digest();
            byte[] TheHash = new byte[sha256Digest.GetDigestSize()];
            sha256Digest.BlockUpdate(BytesToSign, 0, BytesToSign.Length);
            sha256Digest.DoFinal(TheHash, 0);

            PssSigner Signer = new PssSigner(new RsaEngine(), new Sha256Digest(), sha256Digest.GetDigestSize());
            Signer.Init(true, KeyPair.Private);
            Signer.BlockUpdate(TheHash, 0, TheHash.Length);
            byte[] Signature = Signer.GenerateSignature();

            return Signature;
        }
    }
}
