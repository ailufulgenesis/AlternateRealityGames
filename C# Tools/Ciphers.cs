using System;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

class Dictionaries
{
    public static Dictionary<string, string> binaryToChar = new Dictionary<string, string>{
            {"01000001", "A"},{"01000010", "B"},{"01000011", "C"},{"01000100", "D"},{"01000101", "E"},{"01000110", "F"},{"01000111", "G"},{"01001000", "H"},{"01001001", "I"},{"01001010", "J"},
            {"01001011", "K"},{"01001100", "L"},{"01001101", "M"},{"01001110", "N"},{"01001111", "O"},{"01010000", "P"},{"01010001", "Q"},{"01010010", "R"},{"01010011", "S"},{"01010100", "T"},
            {"01010101", "U"},{"01010110", "V"},{"01010111", "W"},{"01011000", "X"},{"01011001", "Y"},{"01011010", "Z"},
            
            {"01100001", "a"},{"01100010", "b"},{"01100011", "c"},{"01100100", "d"},{"01100101", "e"},{"01100110", "f"},{"01100111", "g"},{"01101000", "h"},{"01101001", "i"},{"01101010", "j"},
            {"01101011", "k"},{"01101100", "l"},{"01101101", "m"},{"01101110", "n"},{"01101111", "o"},{"01110000", "p"},{"01110001", "q"},{"01110010", "r"},{"01110011", "s"},{"01110100", "t"},
            {"01110101", "u"},{"01110110", "v"},{"01110111", "w"},{"01111000", "x"},{"01111001", "y"},{"01111010", "z"},
            
            {"00110001", "1"},{"00100001", "!"},{"00110010", "2"},{"01000000", "@"},{"00110011", "3"},{"00100011", "#"},{"00110100", "4"},{"00100100", "$"},{"00110101", "5"},{"00100101", "%"},
            {"00110110", "6"},{"01011110", "^"},{"00110111", "7"},{"00100110", "&"},{"00111000", "8"},{"00101010", "*"},{"00111001", "9"},{"00101000", "("},{"00110000", "0"},{"00101001", "}"},
            
            {"00100000", " "},{"01100000", "`"},{"01111110", "~"},{"00101101", "-"},{"01011111", "_"},{"00111101", "="},
            {"00101011", "+"},{"01011011", "["},{"01111011", "{"},{"01011101", "]"},{"01111101", "}"},{"01011100", "\\"},
            {"01111100", "|"},{"00111011", ";"},{"00111010", ":"},{"00100111", "\'"},{"00100010", "\""},{"00101100", ","},
            {"00111100", "<"},{"00101110", "."},{"00111110", ">"},{"00101111", "/"},{"00111111", "?"},{"00001010","\n"}
    };
    public static void Binary(char textToBinary){
        if (textToBinary == 'y')
        {
            foreach(KeyValuePair<string, string> kvp in binaryToChar){
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
        }
        else if(textToBinary == 'n'){
            foreach(KeyValuePair<string, string> kvp in binaryToChar){
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Value, kvp.Key);
            }
        }
        else
        {
            Console.WriteLine("uh oh");
        }
    }
}
class Decode
{
    public static string[] supportedDecoders =
    {
        "BINARY"
    };
    private static string[] unsupportedDecoders =
    {
        "MORSE",
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
    
}
class Encode
{
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
}
class Ciphers
{
        static void Main(string[] args)
        {
            string cipherDirectionCheck;
            Console.WriteLine("Encode or Decode");
            cipherDirectionCheck = Console.ReadLine();
            while (cipherDirectionCheck.ToLower() != "decode" && cipherDirectionCheck.ToLower() != "encode")
            {
                cipherDirectionCheck = Console.ReadLine();
            }
            if(cipherDirectionCheck.ToLower() == "decode"){
                Console.WriteLine("Decode what?");
                string decodeType = Console.ReadLine().Trim();
                if(Decode.supportedDecoders.Contains(decodeType.ToUpper())){
                    switch(decodeType.ToUpper()){
                        case "BINARY":
                            Decode.Binary(Console.ReadLine());
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Unsupported method. Please check back later for new updates.");
                }
            }
            else if(cipherDirectionCheck.ToLower() == "encode"){
                Encode.Binary(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("uh oh");
            }
        }
}
