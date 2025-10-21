using System;

namespace RsaAlgorithm
{
    public class hw2()
    {
        // Computes base^expo % mod
        public int powerMod(int baseVal, int exponent, int mod)
        {
            int result = 1;
            baseVal = baseVal % mod;

            while (exponent > 0)
            {
                if (exponent & 1)
                {
                    result = (result * baseVal) % mod;
                }

                baseVal = baseVal % mod;
                exponent = exponent / 2;
            }

            return result;
        }

        // Finds the modular inverse of x modulo phi using extended euclidean algorithm
        public int inverseMod(int e, int phi)
        {

            return -1;
        }

        public void generateKey()
        {

        }

        public int encrypt()
        {
            return powerMod();
        }

        public int decrypt()
        {
            return powerMod();
        }
        
        public static void Main()
        {
            
        }
    }
}