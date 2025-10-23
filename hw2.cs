using System;
using System.Data.SqlTypes;
using System.Numerics;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RsaAlgorithm
{
    public class hw2()
    {
        // These two methods are simply here to create a random prime number for the keypair generator. 
        // I found examples of the Rabin Miller Test online and used them here because I felt like doing way too much.
        // These two methods are the only code found from an outside source.
        public static BigInteger GenerateRandomPrime(int bits)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[(bits + 7) / 8];
                BigInteger candidate;

                do
                {
                    rng.GetBytes(bytes);
                    bytes[^1] |= 0x80;
                    candidate = new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
                    candidate |= 1;
                } while(!RabinMillerTest(candidate));
                return candidate;
            }
        }
        public static bool RabinMillerTest(BigInteger candidate)
        {
            if(candidate < 2) return false;
            if(candidate == 2 || candidate == 3) return true;
            if(candidate % 2 == 0) return false;

            BigInteger even = candidate - 1;
            int timesDivis = 0;

            while(even % 2 == 0)
            {
                even = even / 2;
                timesDivis++;
            }

            using(var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[candidate.GetByteCount];

                for(int i = 0; i < 5; i++)
                {
                    BigInteger isPrime;
                    do
                    {
                        rng.GetBytes(bytes);
                        isPrime = new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
                    } while(isPrime < 2 || isPrime >= candidate - 2);

                    BigInteger checker = BigInteger.ModPow(isPrime, even, candidate);
                    if(checker == 1 || checker == candidate - 1)
                    {
                        continue;
                    }

                    bool continueOuter = false;
                    for(int j = 0; j < timesDivis - 1; j++)
                    {
                        checker = BigInteger.ModPow(checker, 2, candidate);
                        if(checker == candidate - 1)
                        {
                            continueOuter = true;
                            break;
                        }
                    }

                    if(continueOuter)
                    {
                        continue;
                    }
                    
                    return false;
                }
            }

            return true;
        }






        // Computes base^expo % mod
        public static BigInteger powerMod(BigInteger baseVal, BigInteger exponent, BigInteger mod)
        {
            BigInteger result = 1;
            baseVal = baseVal % mod;

            while (exponent > 0)
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
            BigInteger randPrime1 = GenerateRandomPrime(16);
            BigInteger randPrime2 = GenerateRandomPrime(16);

            while (randPrime1 == randPrime2)
            {
                randPrime2 = GenerateRandomPrime(16);
            }

            mod = randPrime1 * randPrime2;

            BigInteger phi = (randPrime1 - 1) * (randPrime2 - 1);

            pub = 65537;

            inverseMod(pub, phi);

            Console.WriteLine("Generated primes:\nFirst prime: {randomPrime1}\nSecond prime: {randomPrime2}\n");
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

            Console.WriteLine("Public Key (Public, Modulus): ({pub}, {mod})");
            Console.WriteLine("Private Key (Private, Modulus): ({priv}, {mod})\n");


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