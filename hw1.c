/*  
    Hunter Befort
    09/06/2025
    CSE 4381

    This code takes a user given message and encrypts it using a substitution with a randomized ciphertext, displays the encrypted message
        decrypts the encrypted message, and then displays the original message from the decrypted ciphertext.
    The main downside to this basic cryptosystem is it converts uppercase letters to lowercase letters
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <time.h>

char plaintext[26] = "abcdefghijklmnopqrstuvwxyz";
char upperPlaintext[26] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

//Fisher Yates algorithm that will randomize the alphabet to create a unique (somewhat) ciphertext for each compilation of the program
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
        //checks if the character is uppercase first, then checks for non-alphabetical characters
        if(char_index == -1)
        {
            char_index = find_index(find_char, upperPlaintext);
            if(char_index == -1)
            {
                encrypted_message[i] = message[i];
            }
            else 
            {
                encrypted_message[i] = cipher[char_index];
            }
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
    //ciphertext is the plaintext randomized through a FisherYates algorithm, so the ciphertext is randomized each time the cryptosystem is run
    char ciphertext[26] = "abcdefghijklmnopqrstuvwxyz";
    FisherYates(ciphertext, 26);

    char message[10000];
    printf("\nType message you want to encrypt here: ");
    fgets(message, sizeof(message), stdin);

    printf("\nMessage: %s", message);
    char *encryption = encrypt(message, ciphertext);
    printf("Encrypted Message: %s", encryption);
    char *decryption = decrypt(encryption, ciphertext);
    printf("Decrypted Message: %s\n", decryption);
}