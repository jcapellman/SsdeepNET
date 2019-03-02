namespace SSDEEP.NET
{
    internal sealed class Roll
    {
        private readonly byte[] _window = new byte[FuzzyConstants.RollingWindow];
        private int _h1;
        private int _h2;
        private int _h3;
        private int _n;

        public int Sum() => _h1 + _h2 + _h3;

        /*
         * a rolling hash, based on the Adler checksum. By using a rolling hash
         * we can perform auto resynchronisation after inserts/deletes
    
         * internally, h1 is the sum of the bytes in the window and h2
         * is the sum of the bytes times the index
    
         * h3 is a shift/xor based rolling hash, and is mostly needed to ensure that
         * we can cope with large blocksize values
         */
        internal void Hash(byte c)
        {
            _h2 -= _h1;
            _h2 += FuzzyConstants.RollingWindow * c;

            _h1 += c;
            _h1 -= _window[_n % FuzzyConstants.RollingWindow];

            _window[_n % FuzzyConstants.RollingWindow] = c;
            _n++;

            /* The original spamsum AND'ed this value with 0xFFFFFFFF which
             * in theory should have no effect. This AND has been removed
             * for performance (jk) */
            _h3 <<= 5;
            _h3 ^= c;
        }
    }
}