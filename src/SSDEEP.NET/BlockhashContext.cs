namespace SSDEEP.NET
{
    /* A blockhash contains a signature state for a specific (implicit) blocksize.
     * The blocksize is given by SSDEEP_BS(index). The h and halfh members are the
     * FNV hashes, where halfh stops to be reset after digest is SPAMSUM_LENGTH/2
     * long. The halfh hash is needed be able to truncate digest for the second
     * output hash to stay compatible with ssdeep output. */
    sealed class BlockhashContext
    {
        private const int HashPrime = 0x01000193;
        private const int HashInit = 0x28021967;

        public int H;
        public int HalfH;
        public byte[] Digest = new byte[FuzzyConstants.SpamSumLength];
        public byte HalfDigest;
        public int DLen;

        public void Hash(byte c)
        {
            H = Hash(c, H);
            HalfH = Hash(c, HalfH);
        }

        /* A simple non-rolling hash, based on the FNV hash. */
        private static int Hash(byte c, int h) => (h * HashPrime) ^ c;

        public void Reset(bool init = false)
        {
            Digest[init ? DLen : ++DLen] = 0;
            H = HashInit;

            if (DLen >= FuzzyConstants.SpamSumLength / 2)
            {
                return;
            }

            HalfH = HashInit;
            HalfDigest = 0;
        }
    }
}