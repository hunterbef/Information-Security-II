import random
from math import pow 
 
prime = random.randint(2, 10)

def powerMod(base, exp, mod): 
    result = 1
    y = base

    while exp > 0:
        if exp % 2 != 0:
            result = (result * y) % mod 
        y = (y * y) % mod 
        exp = int(exp / 2)
    
    return result % mod

def gcd(a, b):
    if(a < b):
        return gcd(b, a)
    elif(a % b == 0):
        return b
    else:
        return gcd(b, a % b)
    
def hash(plaintext, q):
    val = 0
    for char in plaintext:
        val = (val * 257 * ord(char)) % q
    return val
    
    
def generateKey(prime, generator, privKey):
    key = random.randint(10**20, prime)
    while(gcd(prime, key) != 1):
        key = random.randint(10**20, prime)
    return key

def sign(plaintext, prime, exp, root):
    ciphertext = []

    privKey = generateKey(prime)
    sender = powerMod(exp, privKey, prime)
    pubKey = powerMod(root, privKey, prime)

    for i in range(0, len(plaintext)):
        ciphertext.append(plaintext[i])

    for i in range(0, len(ciphertext)):
        ciphertext[i] = sender * ord(ciphertext[i])

    return ciphertext, pubKey


def verify(ciphertext, prime, privKey, root):
    plaintext = []

    exp = powerMod(prime, privKey, root)
    for i in range(0, len(ciphertext)):
        plaintext.append(chr(int(ciphertext[i] / exp)))

    return plaintext

if __name__ == '__main__':
    prime = random.randint(10**20, 10**50)
    generator = random.randint(2, prime)
    privKey = 127

    keys = generateKey(prime, generator, privKey)
    exp = powerMod(generator, privKey, prime)

    message = input("Enter a message to encrypt: ")
    print("Original message: ", message)

    ciphertext, pubKey = sign(message, prime, exp, root)
    print("Encrypted message: ", ciphertext)

    decryption = verify(ciphertext, pubKey, privKey, prime)
    plaintext = ''.join(decryption)
    print("Decrypted message: ", plaintext)