using System;
using System.Collections.Concurrent;

/*
    Reference list:
        INTERNATIONAL MORSE:    https://morsecode.world/international/morse.html
        CIPHER LIST:            https://www.cryptogram.org/resource-area/cipher-types/
    
*/
class Dictionaries
{
    public static Dictionary<string, char> binaryToChar = new Dictionary<string, char>{
        // uppercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"01000001", 'A'},{"01000010", 'B'},{"01000011", 'C'},{"01000100", 'D'},{"01000101", 'E'},{"01000110", 'F'},{"01000111", 'G'},{"01001000", 'H'},{"01001001", 'I'},{"01001010", 'J'},
            {"01001011", 'K'},{"01001100", 'L'},{"01001101", 'M'},{"01001110", 'N'},{"01001111", 'O'},{"01010000", 'P'},{"01010001", 'Q'},{"01010010", 'R'},{"01010011", 'S'},{"01010100", 'T'},
            {"01010101", 'U'},{"01010110", 'V'},{"01010111", 'W'},{"01011000", 'X'},{"01011001", 'Y'},{"01011010", 'Z'},

        // lowercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"01100001", 'a'},{"01100010", 'b'},{"01100011", 'c'},{"01100100", 'd'},{"01100101", 'e'},{"01100110", 'f'},{"01100111", 'g'},{"01101000", 'h'},{"01101001", 'i'},{"01101010", 'j'},
            {"01101011", 'k'},{"01101100", 'l'},{"01101101", 'm'},{"01101110", 'n'},{"01101111", 'o'},{"01110000", 'p'},{"01110001", 'q'},{"01110010", 'r'},{"01110011", 's'},{"01110100", 't'},
            {"01110101", 'u'},{"01110110", 'v'},{"01110111", 'w'},{"01111000", 'x'},{"01111001", 'y'},{"01111010", 'z'},
        
        // numbers
            {"00110001", '1'},{"00110010", '2'},{"00110011", '3'},{"00110100", '4'},{"00110101", '5'},
            {"00110110", '6'},{"00110111", '7'},{"00111000", '8'},{"00111001", '9'},{"00110000", '0'},
        
        // symbols
            {"00100001", '!'},{"01000000", '@'},{"00100011", '#'},{"00100100", '$'},{"00100101", '%'},{"01011110", '^'},
            {"00100110", '&'},{"00101010", '*'},{"00101000", '('},{"00101001", '}'},{"00100000", ' '},{"01100000", '`'},
            {"01111110", '~'},{"00101101", '-'},{"01011111", '_'},{"00111101", '='},{"00101011", '+'},{"01011011", '['},
            {"01111011", '{'},{"01011101", ']'},{"01111101", '}'},{"01011100", '\\'},{"01111100", '|'},{"00111011", ';'},
            {"00111010", ':'},{"00100111", '\''},{"00100010", '\"'},{"00101100", ','},{"00111100", '<'},{"00101110", '.'},
            {"00111110", '>'},{"00101111", '/'},{"00111111", '?'},{"00001010", '\n'}
    };
    public static Dictionary<string, char> morseToChar = new Dictionary<string, char>    {
        // non-accented characters, probably will not add 
        // accented characters unless demand is met
            {".-", 'A'},{"-...", 'B'},{"-.-.", 'C'},{"-..", 'D'},{".", 'E'},{"..-.", 'F'},{"--.", 'G'},{"....", 'H'},{"..", 'I'},{".---", 'J'},
            {"-.-", 'K'},{".-..", 'L'},{"--", 'M'},{"-.", 'N'},{"---", 'O'},{".--.", 'P'},{"--.-", 'Q'},{".-.", 'R'},{"...", 'S'},{"-", 'T'},
            {"..-", 'U'},{"...-", 'V'},{".--", 'W'},{"-..-", 'X'},{"-.--", 'Y'},{"--..", 'Z'},
        
        // symbols
            {"-.-.--", '!'},{".--.-.", '@'},{".-...", '&'},{"-.--.", '('},{"-.--.-", ')'},{"---...", ':'},{".----.", '\''},
            {".-..-.", '\"'},{"--..--", ','},{".-.-.-", '.'},{"-..-.", '/'},{"..--..", '?'},{".-.-.",'+'},{"/", ' '},{"-....-", '-'}
    };
}
class Decode
{
    public static string[] supportedDecoders =
    {
        "BINARY",
        "MORSE"
    };
    private static string[] unsupportedDecoders =
    {
        // LOW PRIORITY AS OF NOW, MAINLY BECAUSE I NEED TO LEARN THE CIPHERS AND HOW TO ENCODE AND DECODE THEM.
        "AMSCO","AUTOKEY","BACONIAN","BAZERIES","BEAUFORT","BIFID","CADENUS","CHECKERBOARD","COMPLETE COLUMNAR TRANSPOSITION","COMPRESSOCRAT",
        "CONDI","CM BIFID","DIGRAFID","FOURSQUARE","FRACTIONATED MORSE","GRANDPRÉ","GRILLE","GROMARK","GRONSFELD","HEADLINES",
        "HOMOPHONIC","INCOMPLETE COLUMNAR","INTERRUPTED KEY","KEY PHRASE","MONOME-DINOME","MORBIT","MYSZKOWSKI","NICODEMUS","NIHILIST SUBSTITUTION","NIHILIST TRANSPOSITION",
        "NULL","NUMBERED KEY","PERIODIC GROMARK","PHILLIPS","PHILLIPS-RC","PLAYFAIR","POLLUX","PORTA","PORTAX","PROGRESSIVE KEY",
        "QUAGMIRE I","QUAGMIRE II","QUAGMIRE III","QUAGMIRE IV","RAGBABY","RAILFENCE","REDEFENCE","ROUTE TRANSPOSITION","RUNNING KEY","SEQUENCE TRANSPOSITION",
        "SERIATED PLAYFAIR","SLIDEFAIR","SWAGMAN","SYLLABARY","SYLLABARY FOREIGN CHARTS","TRIDIGITAL","TRIFID","TRI-SQUARE","TWIN BIFID","TWIN TRIFID",
        "TWO-SQUARE","VARIANT","VIGENÈRE"  
    };
    public static void Binary(string userInput)
    {
        string input = userInput.Replace(" ","");
        if((input.Length) % 8 != 0){
            Console.WriteLine("\nNote that the input is not a multiple of 8. Proceeding...\n");
            Thread.Sleep(1000);
        }
        for(int i = 0; i < ((double)(input.Length) / 8); i++){
            int startNum = i*8;
            int endNum = startNum+8;
            try{
                Console.Write(Dictionaries.binaryToChar[input[((startNum))..(endNum)]]);
                Thread.Sleep(20);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("\nLeft over binary: " + input[((startNum))..(input.Length)]);
                
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
    }
    public static void Morse(string userInput)
    {
        string[] input = userInput.Split(" ");
        foreach (string morseInputChar in input)
        {
            try{
                Console.Write(Dictionaries.morseToChar[morseInputChar]);
            }
            catch (KeyNotFoundException e)
            {
                Console.Write("<HH>");
            }
        }
    }
}
class Encode
{
    public static string[] supportedEncoders =
    {
        "BINARY","MORSE"
    };
    private static string[] unsupportedEncoders =
    {
        // LOW PRIORITY AS OF NOW, MAINLY BECAUSE I NEED TO LEARN THE CIPHERS AND HOW TO ENCODE AND DECODE THEM.
        "AMSCO","AUTOKEY","BACONIAN","BAZERIES","BEAUFORT","BIFID","CADENUS","CHECKERBOARD","COMPLETE COLUMNAR TRANSPOSITION","COMPRESSOCRAT",
        "CONDI","CM BIFID","DIGRAFID","FOURSQUARE","FRACTIONATED MORSE","GRANDPRÉ","GRILLE","GROMARK","GRONSFELD","HEADLINES",
        "HOMOPHONIC","INCOMPLETE COLUMNAR","INTERRUPTED KEY","KEY PHRASE","MONOME-DINOME","MORBIT","MYSZKOWSKI","NICODEMUS","NIHILIST SUBSTITUTION","NIHILIST TRANSPOSITION",
        "NULL","NUMBERED KEY","PERIODIC GROMARK","PHILLIPS","PHILLIPS-RC","PLAYFAIR","POLLUX","PORTA","PORTAX","PROGRESSIVE KEY",
        "QUAGMIRE I","QUAGMIRE II","QUAGMIRE III","QUAGMIRE IV","RAGBABY","RAILFENCE","REDEFENCE","ROUTE TRANSPOSITION","RUNNING KEY","SEQUENCE TRANSPOSITION",
        "SERIATED PLAYFAIR","SLIDEFAIR","SWAGMAN","SYLLABARY","SYLLABARY FOREIGN CHARTS","TRIDIGITAL","TRIFID","TRI-SQUARE","TWIN BIFID","TWIN TRIFID",
        "TWO-SQUARE","VARIANT","VIGENÈRE"  
    };
    public static void Binary(string userInput)
    {
        string input = userInput;
        foreach(char character in input){
            try{
                Console.Write(Dictionaries.binaryToChar.FirstOrDefault(x => x.Value == character).Key);
                Thread.Sleep(20);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
    }
    public static void Morse(string userInput)
    {
        string input = userInput.ToUpper();
        foreach(char character in input){
            string morseCharacter = Dictionaries.morseToChar.FirstOrDefault(x => x.Value == character).Key;
            if (morseCharacter != null)
            {
                Console.Write(morseCharacter + " ");
                Thread.Sleep(20);
            }
            else
            {
                morseCharacter = Dictionaries.morseToChar.FirstOrDefault(x => x.Value == 'H').Key;
                Console.Write($"/ {morseCharacter} {morseCharacter} / ");
            }
        }
    }
}
class Ciphers
{
        static void Main(string[] args)
        {
            string cipherDirectionCheck;
            Console.WriteLine("Encode or Decode (or shorthand)");
            cipherDirectionCheck = Console.ReadLine();
            while (cipherDirectionCheck.ToLower() != "decode" && cipherDirectionCheck.ToLower() != "encode" && cipherDirectionCheck.ToLower()[0] != 's')
            {
                cipherDirectionCheck = Console.ReadLine();
            }

            //DECODE CHECKS
            if(cipherDirectionCheck.ToLower() == "decode"){
                Console.WriteLine("Decode what?");
                string decodeType = Console.ReadLine().Trim();
                if(Decode.supportedDecoders.Contains(decodeType.ToUpper())){
                    Console.WriteLine($"Type your {decodeType} message:");
                    switch(decodeType.ToUpper()){
                        case "BINARY":
                            Decode.Binary(Console.ReadLine());
                            break;
                        case "MORSE":
                            Decode.Morse(Console.ReadLine());
                            break;
                        default:
                            Console.WriteLine("Unsupported method inside decode switch... somehow...");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Unsupported method. Please check back later for new updates.");
                }
            }

            //ENCODING CHECKS
            else if(cipherDirectionCheck.ToLower() == "encode"){
                Console.WriteLine("Encode what?");
                string encodeType = Console.ReadLine().Trim();
                if(Encode.supportedEncoders.Contains(encodeType.ToUpper())){
                    Console.WriteLine($"Type your {encodeType} message:");
                    switch(encodeType.ToUpper()){
                            case "BINARY":
                                Encode.Binary(Console.ReadLine());
                                break;
                            case "MORSE":
                                Encode.Morse(Console.ReadLine());
                                break;
                            default:
                                Console.WriteLine("Unsupported method inside decode switch... somehow...");
                                break;
                        }
                }
                else
                {
                    Console.WriteLine("Unsupported method. Please check back later for new updates.");
                }
            }

            //SHORTHAND CODE CHECKS
            else if (cipherDirectionCheck.ToLower()[0] == 's')
            {
                switch(cipherDirectionCheck.ToLower()[1])
                {
                    case 'd':
                        switch (cipherDirectionCheck.ToLower()[2])
                        {
                            case 'b':
                                Decode.Binary(Console.ReadLine());
                                break;
                            case 'm':
                                Decode.Morse(Console.ReadLine());
                                break;
                            default:
                                Console.WriteLine("Unsupported shorthand.");
                                break;
                        }
                        break;
                    case 'e':
                        switch (cipherDirectionCheck.ToLower()[2])
                        {
                            case 'b':
                                Encode.Binary(Console.ReadLine());
                                break;
                            case 'm':
                                Encode.Morse(Console.ReadLine());
                                break;
                            default:
                                Console.WriteLine("Unsupported shorthand.");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Unsupported shorthand.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("uh oh");
            }
        }
}
