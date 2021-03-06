﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace Waher.Security.EllipticCurves
{
    /// <summary>
    /// Edwards25519 Elliptic Curve, as defined in RFC7748 & RFC8032:
    /// https://tools.ietf.org/html/rfc7748
    /// https://tools.ietf.org/html/rfc8032
    /// </summary>
    public class Edwards25519 : EdwardsTwistedCurve
    {
        private static readonly BigInteger p0 = BigInteger.Pow(2, 255) - 19;
        private static readonly BigInteger d0 = BigInteger.Parse("37095705934669439343138083508754565189542113879843219016388785533085940283555");
        private static readonly BigInteger n0 = BigInteger.Pow(2, 252) + BigInteger.Parse("14def9dea2f79cd65812631a5cf5d3ed", NumberStyles.HexNumber);
        private static readonly BigInteger BasePointX = BigInteger.Parse("15112221349535400772501151409588531511454012693041857206046113283949847762202");
        private static readonly BigInteger BasePointY = BigInteger.Parse("46316835694926478169428394003475163141307993866256225615783033603165251855960");

        /// <summary>
        /// Edwards25519 Elliptic Curve, as defined in RFC7748 & RFC8032:
        /// https://tools.ietf.org/html/rfc7748
        /// https://tools.ietf.org/html/rfc8032
        /// </summary>
        public Edwards25519()
            : base(p0, new PointOnCurve(BasePointX, BasePointY), d0, n0, Cofactor: 8)
        {
        }

        /// <summary>
        /// Edwards25519 Elliptic Curve, as defined in RFC7748 & RFC8032:
        /// https://tools.ietf.org/html/rfc7748
        /// https://tools.ietf.org/html/rfc8032
        /// </summary>
        /// <param name="Secret">Secret.</param>
        public Edwards25519(byte[] Secret)
            : base(p0, new PointOnCurve(BasePointX, BasePointY), d0, n0, Cofactor: 8, Secret)
        {
        }

        /// <summary>
        /// Name of curve.
        /// </summary>
        public override string CurveName => "Edwards25519";

        /// <summary>
        /// Number of bits used to encode the y-coordinate.
        /// </summary>
        public override int CoordinateBits => 254;

        /// <summary>
        /// Calculates a private key from a secret.
        /// </summary>
        /// <param name="Secret">Binary secret.</param>
        /// <returns>Private key</returns>
        public override byte[] CalculatePrivateKey(byte[] Secret)
        {
            byte[] Bin = Hashes.ComputeSHA512Hash(Secret);

            Bin[0] &= 0xf8;
            Bin[31] &= 0x3f;
            Bin[31] |= 0x40;

            return Bin;
        }

        /// <summary>
        /// Creates a signature of <paramref name="Data"/> using the EdDSA algorithm.
        /// </summary>
        /// <param name="Data">Payload to sign.</param>
        /// <param name="HashFunction">Hash function to use.</param>
        /// <returns>Signature.</returns>
        public override byte[] Sign(byte[] Data)
        {
            return EdDSA.Sign(Data, this.PrivateKey, Hashes.ComputeSHA512Hash, this.orderBits, this);
        }

        /// <summary>
        /// Verifies a signature of <paramref name="Data"/> made by the EdDSA algorithm.
        /// </summary>
        /// <param name="Data">Payload to sign.</param>
        /// <param name="PublicKey">Public Key of the entity that generated the signature.</param>
        /// <param name="Signature">Signature</param>
        /// <returns>If the signature is valid.</returns>
        public override bool Verify(byte[] Data, byte[] PublicKey, byte[] Signature)
        {
            return EdDSA.Verify(Data, PublicKey, Hashes.ComputeSHA512Hash, this.orderBits,
                this, Signature);
        }

    }
}
