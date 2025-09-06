#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

char alphabet[26] = "abcdefghijklmnopqrstuvwxyz";

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
        char_index = find_index(find_char, alphabet);
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
            decrypted_message[i] = alphabet[char_index];
        }
    }

    return decrypted_message;
}

int main(int argc, char *argv[])
{
    char message[10000];
    //random substitution alphabet I created by rolling a bunch of dice
    char cipher[26] = {'o', 'g', 'c', 'i', 'k', 'b', 'e', 'f', 'q', 'd', 'a', 's', 'l', 'r', 'w', 'j', 'z', 'n', 'x', 'm', 'v', 'h', 'u', 'p', 't', 'y'};

    printf("\nType message you want to encrypt here: ");
    fgets(message, sizeof(message), stdin);

    printf("\nMessage: %s", message);
    char *encryption = encrypt(message, cipher);
    printf("\nEncrypted Message: %s", encryption);
    char *decryption = decrypt(encryption, cipher);
    printf("\nDecrypted Message: %s\n", decryption);
}