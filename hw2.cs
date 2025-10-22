using System;
using System.Numerics;
using System.Text;

namespace RsaAlgorithm
{
    public class hw2()
    {
        // Computes base^expo % mod
        public static BigInteger powerMod(BigInteger baseVal, BigInteger exponent, BigInteger mod)
        {
            BigInteger result = 1;
            baseVal = baseVal % mod;

            while(exponent > 0)
            {
                if ((exponent & 1) != 0)
                {
                    result = (result * baseVal) % mod;
                }

                baseVal = (baseVal * baseVal) % mod;
                exponent = exponent / 2;
            }

            return result;
        }

        // Finds the modular inverse of x modulo phi using extended euclidean algorithm
        public static BigInteger inverseMod(int pub, int phi)
        {
            BigInteger tempPhi = phi, x = 1, y = 0;
            if(phi == 1)
            {
                return 0;
            }

            while(pub > 1)
            {
                BigInteger quotient = (pub / phi);
                BigInteger temp = phi;
                phi = pub % phi;
                pub = temp;
                temp = y;
                y = x - quotient * y;
                x = temp;
            }
            
            if(x < 0)
            {
                x = x + tempPhi;
            }

            return x;
        }

        // Generates a key pair with two random prime numbers
        public static void generateKeyPair(out BigInteger pub, out BigInteger priv, out BigInteger mod)
        {
            BigInteger randPrime1 = 53;
            BigInteger randPrime2 = 61;

            while(randPrime1 == randPrime2)
            {
                randPrime2 = 0;
            }

            mod = randPrime1 * randPrime2;

            BigInteger phi = (randPrime1 - 1) * (randPrime2 - 1);

            pub = 65537;

            inverseMod(pub, phi);
        }

        public static string encrypt(BigInteger plainVal, BigInteger pubExp, BigInteger mod)
        {
            return powerMod(plainVal, pubExp, mod);
        }

        public static string decrypt(BigInteger cipherVal, BigInteger privExp, BigInteger mod)
        {
            return powerMod(cipherVal, privExp, mod);
        }
        
        public static void Main()
        {
            BigInteger mod, pub, priv;

            generateKeyPair(out pub, out priv, out mod);

            Console.WriteLine($"Public Key (Public, Modulus): ({pub}, {mod})");
            Console.WriteLine($"Private Key (Private, Modulus): ({priv}, {mod})\n");


            //reads user input to 
            Console.Write("Enter a message to encrypt: ");
            string message = Console.ReadLine() ?? "";
            Console.WriteLine("Original Message: {message}");

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            BigInteger[] encryptedBlocks = new BigInteger[messageBytes.Length];

            for(int i = 0; i < messageBytes.Length; i++)
            {
                encryptedBlocks[i] = encrypt(messageBytes[i], pub, mod);
            }

            Console.Write("Encrypted Message: ");
            foreach(BigInteger block in encryptedBlocks)
            {
                Console.Write(block + " ");
            }


            byte[] decryptedBytes = new byte[encryptedBlocks.Length];
            for(int i = 0; i < encryptedBlocks.Length; i++)
            {
                decryptedBytes = (byte)decrypt(encryptedBlocks[i], priv, mod);
            }
            string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine($"\nDecrypted Message: {decryptedMessage}");
        }
    }
}