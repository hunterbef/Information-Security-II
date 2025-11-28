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
    
def extendedgcd(a, b):
    if b == 0:
        return a, 1, 0
    root, x, y = extendedgcd(b, a % b)
    privKey = y
    pubKey = x - (a // b) * y
    return root, privKey, pubKey
    
def modInv(a, mod):
    root, privKey, pubKey = extendedgcd(a, mod)
    return privKey % mod
    
def generateKey(prime):
    key = random.randint(2, prime - 2)
    while(gcd(prime, key) != 1):
        key = random.randint(2, prime - 2)
    return key

def sign(message, prime, root, privKey):
    hash = sum([ord(char) for char in message]) % (prime - 1)

    key = generateKey(prime)

    mod = powerMod(root, key, prime)

    inv = modInv(key, prime - 1)
    sig = (inv * (hash - privKey * mod)) % (prime - 1)

    return (mod, sig)


def verify(message, signature, prime, root, pubKey):
    mod, sig = signature

    hash = sum([ord(char) for char in message]) % (prime - 1)

    left = powerMod(root, hash, prime)
    right = (powerMod(pubKey, mod, prime) * powerMod(mod, sig, prime)) % prime

    return left == right

if __name__ == '__main__':
    prime = random.randint(10**10, 10**12)
    root = random.randint(2, prime - 1)

    privKey = random.randint(2, prime - 2)
    pubKey = powerMod(root, privKey, prime)

    message = input("Enter a message to sign: ")

    sig = sign(message, prime, root, privKey)
    print("Signature: ", sig)

    valid = verify(message, sig, prime, root, pubKey)
    print("Signature valid? ", valid)