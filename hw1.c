/*  
    Hunter Befort
    09/06/2025
    CSE 4381

    This code takes a user given message and encrypts it using a substitution cipher that is predetermined and constant
*/

#include <stdio.h>
#include <stdlib.h>
#include <time.h>

char plaintext[26] = "abcdefghijklmnopqrstuvwxyz";

void FisherYates(char cipher[], int n)
{
    int temp, x, y;
    srand(time(NULL));
    for(int i = n - 1; i > 0; i--)
    {
        int j = rand() % (i + 1);
        temp = cipher[i];
        cipher[i] = cipher[j];
        cipher[j] = temp;
    }
}

//finds the index of the character in either the alphabet or the cipher
int find_index(char finder, char code[])
{
    for(int i = 0; i < 26; i++)
    {
        if(code[i] == finder)
        {
            return i;
        }
    }
    return -1;
}


//encrypts the message
char* encrypt(char *message, char cipher[])
{
    char find_char;
    int char_index = 0;
    int length = strlen(message);
    char *encrypted_message = (char*) malloc(sizeof(char) * length);

    for(int i = 0; i < length; i++)
    {
        find_char = message[i];
        char_index = find_index(find_char, plaintext);
        //checks for non-alphabetical characters
        if(char_index == -1)
        {
            encrypted_message[i] = message[i];
        }
        else 
        {
            encrypted_message[i] = cipher[char_index];
        }
    }
    
    return encrypted_message;
}


//decrypts the message
char* decrypt(char *message, char cipher[])
{
    char find_char;
    int char_index = 0;
    int length = strlen(message);
    char *decrypted_message = (char*) malloc(sizeof(char) * length);

    for(int i = 0; i < length; i++)
    {
        find_char = message[i];
        char_index = find_index(find_char, cipher);
        if(char_index == -1)
        {
            decrypted_message[i] = message[i];
        }
        else 
        {
            decrypted_message[i] = plaintext[char_index];
        }
    }

    return decrypted_message;
}


int main(int argc, char *argv[])
{
    char message[10000];
    //random substitution alphabet I created by rolling a bunch of dice
    char ciphertext[26] = plaintext; //= {'o', 'g', 'c', 'i', 'k', 'b', 'e', 'f', 'q', 'd', 'a', 's', 'l', 'r', 'w', 'j', 'z', 'n', 'x', 'm', 'v', 'h', 'u', 'p', 't', 'y'};
    FisherYates(ciphertext, 26);
    printf("\nCiphertext = %s\n", ciphertext);

    printf("\nType message you want to encrypt here: ");
    fgets(message, sizeof(message), stdin);

    printf("\nMessage: %s", message);
    char *encryption = encrypt(message, ciphertext);
    printf("\nEncrypted Message: %s", encryption);
    char *decryption = decrypt(encryption, ciphertext);
    printf("\nDecrypted Message: %s\n", decryption);
}