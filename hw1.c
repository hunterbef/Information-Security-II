#include <stdio.h>
#include <string.h>
#include <ctype.h>

const char alphabet[] = "abcdefghijklmnopqrstuvwxyz";


int find_index(char finder, char *code)
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
    char find_char = "";
    int char_index = 0;
    int length = strlen(message);
    char *encrypted_message = (char*)malloc(sizeof(char) * length);

    for(int i = 0; i < 26; i++)
    {
        find_char = message[i];
        char_index = find_index(find_char, alphabet);
        printf("Character: %c\tIndex: %d", find_char, char_index);
    }
}

//decrypts the message
char* decrypt(char *message, char cipher[])
{
    int length = strlen(message);
    char *decrypted_message = (char*)malloc(sizeof(char) * length);
}

int main(int argc, char *argv[])
{
    char *message = "";
    //random substitution alphabet I created by rolling a bunch of dice
    char cipher[26] = {'o', 'g', 'c', 'i', 'k', 'b', 'e', 'f', 'q', 'd', 'a', 's', 'l', 'r', 'w', 'j', 'z', 'n', 'x', 'm', 'v', 'h', 'u', 'p', 't', 'y'};

    printf("\nType message you want to encrypt here: ");
    scanf("%s", &message);

    char *encryption = encrypt(message, cipher);
    //printf("\nEncrypted Message: %s", encryption);
    //char *decryption = decrypt(encryption, cipher);
    //printf("\nDecrypted Message: %s", decryption);
}