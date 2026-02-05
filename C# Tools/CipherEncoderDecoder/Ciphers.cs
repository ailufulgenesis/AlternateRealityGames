using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Xml;

/*
    Reference list:
        INTERNATIONAL MORSE:        https://morsecode.world/international/morse.html
        CIPHER LIST:                https://www.cryptogram.org/resource-area/cipher-types/
        ANOTHER CIPHER LISR:        https://rumkin.com/tools/cipher/
        MIT CHEAT SHEET:            https://web.mit.edu/org/b/bloggers/www/snively11/CheatSheet/cheatsheat.pdf
        DCODE CHARACTER ENCODING:   https://www.dcode.fr/tools-list#character_encoding

*/
class Referrals // Might eventually refactor so I don't rely on dictionaries...
{
    public enum supportedCipherShorthands 
    {
        A1Z26 = 0,
        
        ASCII, // Related to asciiCiphers enum
        ASCII85,
        MORSE
    };
    public enum asciiCiphers
    {
        BINARY = 0,
        DECIMAL,
        HEXADECIMAL,
        OCTAL
    }
    public static string[] supportedDecoders =
    {
        "BINARY","MORSE","A1Z26","DECIMAL","HEXADECIMAL","OCTAL","ASCII85",
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
    public static string[] supportedEncoders =
    {
        "BINARY","MORSE","A1Z26","DECIMAL","HEXADECIMAL","OCTAL","ASCII85",
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
    public static Dictionary<string, char> binaryToChar = new Dictionary<string, char> {
        // numbers
            {"00110001", '1'},{"00110010", '2'},{"00110011", '3'},{"00110100", '4'},{"00110101", '5'},
            {"00110110", '6'},{"00110111", '7'},{"00111000", '8'},{"00111001", '9'},{"00110000", '0'},

        // lowercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"01100001", 'a'},{"01100010", 'b'},{"01100011", 'c'},{"01100100", 'd'},{"01100101", 'e'},{"01100110", 'f'},{"01100111", 'g'},{"01101000", 'h'},{"01101001", 'i'},{"01101010", 'j'},
            {"01101011", 'k'},{"01101100", 'l'},{"01101101", 'm'},{"01101110", 'n'},{"01101111", 'o'},{"01110000", 'p'},{"01110001", 'q'},{"01110010", 'r'},{"01110011", 's'},{"01110100", 't'},
            {"01110101", 'u'},{"01110110", 'v'},{"01110111", 'w'},{"01111000", 'x'},{"01111001", 'y'},{"01111010", 'z'},
            
        // uppercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"01000001", 'A'},{"01000010", 'B'},{"01000011", 'C'},{"01000100", 'D'},{"01000101", 'E'},{"01000110", 'F'},{"01000111", 'G'},{"01001000", 'H'},{"01001001", 'I'},{"01001010", 'J'},
            {"01001011", 'K'},{"01001100", 'L'},{"01001101", 'M'},{"01001110", 'N'},{"01001111", 'O'},{"01010000", 'P'},{"01010001", 'Q'},{"01010010", 'R'},{"01010011", 'S'},{"01010100", 'T'},
            {"01010101", 'U'},{"01010110", 'V'},{"01010111", 'W'},{"01011000", 'X'},{"01011001", 'Y'},{"01011010", 'Z'},

        // symbols
            {"00100001", '!'},{"01000000", '@'},{"00100011", '#'},{"00100100", '$'},{"00100101", '%'},{"01011110", '^'},
            {"00100110", '&'},{"00101010", '*'},{"00101000", '('},{"00101001", '}'},{"01100000", '`'},{"01111110", '~'},
            {"00101101", '-'},{"01011111", '_'},{"00111101", '='},{"00101011", '+'},{"01011011", '['},{"01111011", '{'},
            {"01011101", ']'},{"01111101", '}'},{"01011100", '\\'},{"01111100", '|'},{"00111011", ';'},{"00111010", ':'},
            {"00100111", '\''},{"00100010", '\"'},{"00101100", ','},{"00111100", '<'},{"00101110", '.'},{"00111110", '>'},
            {"00101111", '/'},{"00111111", '?'},
            
        // space and new line lmao
            {"00100000", ' '},{"00001010", '\n'}
    };
    public static Dictionary<string, char> morseToChar = new Dictionary<string, char> {
        // numbers
            {"-----", '0'},{".----", '1'},{"..---", '2'},{"...--", '3'},{"....-", '4'},{".....", '5'},{"-....", '6'},{"--...", '7'},{"---..", '8'},{"----.", '9'},
         
        // non-accented characters, probably will not add 
        // accented characters unless demand is met
            {".-", 'A'},{"-...", 'B'},{"-.-.", 'C'},{"-..", 'D'},{".", 'E'},{"..-.", 'F'},{"--.", 'G'},{"....", 'H'},{"..", 'I'},{".---", 'J'},
            {"-.-", 'K'},{".-..", 'L'},{"--", 'M'},{"-.", 'N'},{"---", 'O'},{".--.", 'P'},{"--.-", 'Q'},{".-.", 'R'},{"...", 'S'},{"-", 'T'},
            {"..-", 'U'},{"...-", 'V'},{".--", 'W'},{"-..-", 'X'},{"-.--", 'Y'},{"--..", 'Z'},

        // symbols
            {"-.-.--", '!'},{".--.-.", '@'},{".-...", '&'},{"-.--.", '('},{"-.--.-", ')'},{"---...", ':'},{".----.", '\''},
            {".-..-.", '\"'},{"--..--", ','},{".-.-.-", '.'},{"-..-.", '/'},{"..--..", '?'},{".-.-.",'+'},{"-....-", '-'},
            
        // space lmao
            {"/", ' '}
    };
    public static Dictionary<int, char> a1z26ToChar = new Dictionary<int, char> {
        {0, ' '},{1, 'A'},{2, 'B'},{3, 'C'},{4, 'D'},{5, 'E'},{6, 'F'},{7, 'G'},{8, 'H'},
        {9, 'I'},{10, 'J'},{11, 'K'},{12, 'L'},{13, 'M'},{14, 'N'},{15, 'O'},{16, 'P'},{17, 'Q'},
        {18, 'R'},{19, 'S'},{20, 'T'},{21, 'U'},{22, 'V'},{23, 'W'},{24, 'X'},{25, 'Y'},{26, 'Z'}
    };
    public static Dictionary<int, char> decimalToChar = new Dictionary<int, char>
    {
        // numbers
            {48,'0'},{49,'1'},{50,'2'},{51,'3'},{52,'4'},{53,'5'},{54,'6'},{55,'7'},{56,'8'},{57,'9'},

        // lowercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {97,'a'},{98,'b'},{99,'c'},{100,'d'},{101,'e'},{102,'f'},{103,'g'},{104,'h'},{105,'i'},{106,'j'},
            {107,'k'},{108,'l'},{109,'m'},{110,'n'},{111,'o'},{112,'p'},{113,'q'},{114,'r'},{115,'s'},{116,'t'},
            {117,'u'},{118,'v'},{119,'w'},{120,'x'},{121,'y'},{122,'z'},

        // uppercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {65,'A'},{66,'B'},{67,'C'},{68,'D'},{69,'E'},{70,'F'},{71,'G'},{72,'H'},{73,'I'},{74,'J'},
            {75,'K'},{76,'L'},{77,'M'},{78,'N'},{79,'O'},{80,'P'},{81,'Q'},{82,'R'},{83,'S'},{84,'T'},
            {85,'U'},{86,'V'},{87,'W'},{88,'X'},{89,'Y'},{90,'Z'},

        // symbols
            {96,'`'},{126,'~'},{33,'!'},{64,'@'},{35,'#'},{36,'$'},{37,'%'},{94,'^'},{38,'&'},{42,'*'},
            {40,'('},{41,')'},{45,'-'},{95,'_'},{61,'='},{43,'+'},{91,'['},{123,'{'},{93,']'},{125,'}'},
            {92,'\\'},{124,'|'},{59,';'},{58,':'},{39,'\''},{34,'\"'},{44,','},{60,'<'},{46,'.'},{62,'>'},
            {47,'/'},{63,'?'},
            
        // space and new line lmao
            {32,' '},{10, '\n'}
    };
    
    public static Dictionary<string, char> hexadecimalToChar = new Dictionary<string, char>
    {
        // numbers
            {"30", '0'},{"31", '1'},{"32", '2'},{"33", '3'},{"34", '4'},
            {"35", '5'},{"36", '6'},{"37", '7'},{"38", '8'},{"39", '9'},

        // lowercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"61", 'a'},{"62", 'b'},{"63", 'c'},{"64", 'd'},{"65", 'e'},{"66", 'f'},{"67", 'g'},{"68", 'h'},{"69", 'i'},{"6A", 'j'},
            {"6B", 'k'},{"6C", 'l'},{"6D", 'm'},{"6E", 'n'},{"6F", 'o'},{"70", 'p'},{"71", 'q'},{"72", 'r'},{"73", 's'},{"74", 't'},
            {"75", 'u'},{"76", 'v'},{"77", 'w'},{"78", 'x'},{"79", 'y'},{"7A", 'z'},

        // uppercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"41", 'A'},{"42", 'B'},{"43", 'C'},{"44", 'D'},{"45", 'E'},{"46", 'F'},{"47", 'G'},{"48", 'H'},{"49", 'I'},{"4A", 'J'},
            {"4B", 'K'},{"4C", 'L'},{"4D", 'M'},{"4E", 'N'},{"4F", 'O'},{"50", 'P'},{"51", 'Q'},{"52", 'R'},{"53", 'S'},{"54", 'T'},
            {"55", 'U'},{"56", 'V'},{"57", 'W'},{"58", 'X'},{"59", 'Y'},{"5A", 'Z'},

        // symbols
            {"60", '`'},{"7E", '~'},{"2D", '-'},{"5F", '_'},{"3D", '='},{"2B", '+'},{"5C", '\\'},{"2F", '/'},
            {"5B", '['},{"5D", ']'},{"7B", '{'},{"7D", '}'},{"3B", ';'},{"3A", ':'},{"27", '\''},{"22", '\"'},
            {"2C", ','},{"2E", '.'},{"3F", '?'},{"3C", '<'},{"3E", '>'},{"7C", '|'},{"21", '!'},{"40", '@'},
            {"23", '#'},{"24", '$'},{"25", '%'},{"5E", '^'},{"26", '&'},{"2A", '*'},{"28", '('},{"29", ')'},

        // space and new line lmao
            {"20", ' '},{"0A", '\n'}
    };
    public static Dictionary<string, char> octalToChar = new Dictionary<string, char> 
    {
        // numbers
            {"060", '0'},{"061", '1'},{"062", '2'},{"063", '3'},{"064", '4'},{"065", '5'},{"066", '6'},{"067", '7'},{"070", '8'},{"071", '9'},
        
        // lowercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"141", 'a'},{"142", 'b'},{"143", 'c'},{"144", 'd'},{"145", 'e'},{"146", 'f'},{"147", 'g'},{"150", 'h'},{"151", 'i'},{"152", 'j'},
            {"153", 'k'},{"154", 'l'},{"155", 'm'},{"156", 'n'},{"157", 'o'},{"160", 'p'},{"161", 'q'},{"162", 'r'},{"163", 's'},{"164", 't'},
            {"165", 'u'},{"166", 'v'},{"167", 'w'},{"170", 'x'},{"171", 'y'},{"172", 'z'},

        // uppercase non-accented characters, probably will not add 
        // accented characters unless demand is met
            {"101", 'A'},{"102", 'B'},{"103", 'C'},{"104", 'D'},{"105", 'E'},{"106", 'F'},{"107", 'G'},{"110", 'H'},{"111", 'I'},{"112", 'J'},
            {"113", 'K'},{"114", 'L'},{"115", 'M'},{"116", 'N'},{"117", 'O'},{"120", 'P'},{"121", 'Q'},{"122", 'R'},{"123", 'S'},{"124", 'T'},
            {"125", 'U'},{"126", 'V'},{"127", 'W'},{"130", 'X'},{"131", 'Y'},{"132", 'Z'},
        
        // symbols
            {"140", '`'},{"176", '~'},{"055", '-'},{"137", '_'},{"075", '='},{"053", '+'},{"134", '\\'},{"057", '/'},
            {"133", '['},{"135", ']'},{"173", '{'},{"175", '}'},{"073", ';'},{"072", ':'},{"047", '\''},{"042", '\"'},
            {"054", ','},{"056", '.'},{"077", '?'},{"074", '<'},{"076", '>'},{"041",'!'},{"100",'@'},{"043",'#'},
            {"044", '$'},{"045", '%'},{"136", '^'},{"046", '&'},{"052", '*'},{"050", '('},{"051", ')'},{"174",'|'},

        // space and new line lmao
            {"040", ' '},{"012", '\n'}
    };
}
class Decode
{
    public static string Binary(string userInput)
    {
        string input = userInput.Replace(" ","");
        int decodeCharAmount = 8;
        string output = "";
        if((input.Length) % decodeCharAmount != 0){
            Console.WriteLine($"\nNote that the input is not a multiple of {decodeCharAmount}. Proceeding...\n");
            Thread.Sleep(1000);
        }
        for(int i = 0; i < ((double)(input.Length) / decodeCharAmount); i++){
            int startNum = i*decodeCharAmount;
            int endNum = startNum+decodeCharAmount;
            try{
                if(input[((startNum))..(endNum)] != "00000000")
                {
                    output += Referrals.binaryToChar[input[((startNum))..(endNum)]];
                }
                // Thread.Sleep(20);
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
        return output;
    }
    public static string Morse(string userInput)
    {
        string[] input = userInput.Trim().Split(" ");
        string output = "";
        foreach (string morseInputChar in input)
        {
            try{
                output += Referrals.morseToChar[morseInputChar];
            }
            catch (KeyNotFoundException)
            {
                Console.Write("<HH>");
            }
        }
        return output;
    }
    public static string A1Z26(string userInput)
    {
        string[] input = userInput.Trim().Split(" ");
        string output = "";
        foreach (string a1z26InputChar in input)
        {
            try{
                output += Referrals.a1z26ToChar[int.Parse(a1z26InputChar)];
            }
            catch (KeyNotFoundException e)
            {
                Console.Write($"<{e.Message}>");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"<{e.Message}>");
            }
        }
        return output;
    }
    public static string ASCII85(string userInput)
    {
        string input = userInput;
        int decodeCharAmount = 5;
        string output = "";
        string trueBinaryString = "";
        // Check if multiple of 5, if not add u until multiple of 5
        if((input.Length) % decodeCharAmount != 0){
            string additionalChars = "";
            for(int i = 0; i < input.Length % decodeCharAmount; i++)
            {
                additionalChars += "u";
            }
            input += additionalChars;
        }
        int inputOriginalLength = input.Length;
        // for every 5 chars, encode into decimal
        for(int i = 0; i < ((double)(inputOriginalLength) / decodeCharAmount); i++){
            string binaryString = "";
            string decimalNumbers = "";
            int startNum = 0;
            int endNum = startNum+decodeCharAmount;
            if(input.Length != 0){
                decimalNumbers += Encode.Decimal(input[((startNum))..(endNum)]);
            }

            string[] decimalStringArray = decimalNumbers.Trim().Split(" ");
            int[] decimalIntArray = new int[decimalStringArray.Count()];
            int decimalIterator = 0;
            foreach(string decimalInt in decimalStringArray){
                if(decimalInt is not null){
                    decimalIntArray[decimalIterator] = int.Parse(decimalInt)-33;
                    decimalIterator++;
                }
            }
            // Gets base 10 number from decimal numbers
            double remainder = 0;
            foreach(int decimalNum in decimalIntArray) 
            {
                remainder = remainder * 85 + decimalNum;
            }
            // Gets base2 number from base 10
            while (remainder >= 0){
                double modnum = remainder % 2;
                binaryString += modnum;
                remainder = remainder-modnum;
                remainder = remainder / 2;
                if(remainder == 0){break;}
            }
            input = input[(endNum)..input.Length];
            binaryString += "0";
            char[] reversedBinaryToCharArray = binaryString.ToCharArray();
            Array.Reverse(reversedBinaryToCharArray);
            string unreversedBinaryString = new string(reversedBinaryToCharArray);
            trueBinaryString += unreversedBinaryString;
        }
        output = Decode.Binary(trueBinaryString);
        return output;
    }
    public static string Decimal(string userInput)
    {
        string[] input = userInput.Trim().Split(" ");
        string output = "";
        foreach (string decimalInputChar in input)
        {
            try{
                output += Referrals.decimalToChar[int.Parse(decimalInputChar)];
            }
            catch (KeyNotFoundException e)
            {
                Console.Write($"<decode decimal knfe error: {e.Message}>");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"decode decimal fe error: <{e.Message}>");
            }
        }
        return output;
    }
    public static string Hexadecimal(string userInput)
    {
        string input = userInput.Trim().Replace("\\x","").Replace(" ","").ToUpper();
        int decodeCharAmount = 2;
        string output = "";
        if((input.Length) % decodeCharAmount != 0){
            Console.WriteLine($"\nNote that the input is not a multiple of {decodeCharAmount}. Proceeding...\n");
            Thread.Sleep(1000);
        }
        for(int i = 0; i < ((double)(input.Length) / decodeCharAmount); i++){
            int startNum = i*decodeCharAmount;
            int endNum = startNum+decodeCharAmount;
            try{
                output += Referrals.hexadecimalToChar[input[((startNum))..(endNum)]];
                // Thread.Sleep(20);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("\nLeft over Hexadecimal: " + input[((startNum))..(input.Length)]);
                
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
        return output;
    }
    public static string Octal(string userInput)
    {
        string input = userInput.Trim().Replace("\\x","").Replace(" ","");
        int decodeCharAmount = 3;
        string output = "";
        if((input.Length) % decodeCharAmount != 0){
            Console.WriteLine($"\nNote that the input is not a multiple of {decodeCharAmount}. Proceeding...\n");
            Thread.Sleep(1000);
        }
        for(int i = 0; i < ((double)(input.Length) / decodeCharAmount); i++){
            int startNum = i*decodeCharAmount;
            int endNum = startNum+decodeCharAmount;
            try{
                output += Referrals.octalToChar[input[((startNum))..(endNum)]];
                // Thread.Sleep(20);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("\nLeft over Octal: " + input[((startNum))..(input.Length)]);
                
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }
        return output;
    }
}
class Encode
{
    public static string Binary(string userInput)
    {
        string input = userInput;
        string output = "";
        foreach(char character in input){
            try{
                output += Referrals.binaryToChar.FirstOrDefault(x => x.Value == character).Key;
                // Thread.Sleep(20);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"<{e.Message}>");
                output += "00111100010001010101001001010010010011110101001000111110";
            }
            
        }
        return output;
    }
    public static string Morse(string userInput)
    {
        string input = userInput.ToUpper();
        string output = "";
        foreach(char character in input){
            string morseCharacter = Referrals.morseToChar.FirstOrDefault(x => x.Value == character).Key;
            if (morseCharacter != null)
            {
                output += morseCharacter + " ";
                // Thread.Sleep(20);
            }
            else
            {
                morseCharacter = Referrals.morseToChar.FirstOrDefault(x => x.Value == 'H').Key;
                Console.Write($"/ {morseCharacter} {morseCharacter} / ");
            }
        }
        return output;
    }
    public static string A1Z26(string userInput)
    {
        string input = userInput.ToUpper();
        string output = "";
        foreach(char character in input){
            int a1z26Character = Referrals.a1z26ToChar.FirstOrDefault(x => x.Value == character).Key;
            output += a1z26Character + " ";
            // Thread.Sleep(20);
        }
        return output;
    }
    public static string Decimal(string userInput)
    {
        string input = userInput;
        string output = "";
        foreach(char character in input){
            int decimalCharacter = Referrals.decimalToChar.FirstOrDefault(x => x.Value == character).Key;
            output += decimalCharacter + " ";
            // Thread.Sleep(20);
        }
        return output;
    }
    public static string Hexadecimal(string userInput)
    {
        string input = userInput;
        string output = "";
        foreach(char character in input){
            string hexadecimalCharacter = Referrals.hexadecimalToChar.FirstOrDefault(x => x.Value == character).Key;
            if (hexadecimalCharacter != null)
            {
                output += hexadecimalCharacter + " ";
                // Thread.Sleep(20);
            }
            else
            {
                hexadecimalCharacter = Referrals.morseToChar.FirstOrDefault(x => x.Value == 'H').Key;
                Console.Write($"<error>");
            }
        }
        return output;
    }
    public static string Octal(string userInput)
    {
        string input = userInput;
        string output = "";
        foreach(char character in input){
            string octalCharacter = Referrals.octalToChar.FirstOrDefault(x => x.Value == character).Key;
            if (octalCharacter != null)
            {
            
                output += octalCharacter + " ";
                // Thread.Sleep(20);
            }
            else
            {
                octalCharacter = Referrals.morseToChar.FirstOrDefault(x => x.Value == 'H').Key;
                Console.Write($"<error>");
            }
        }
        return output;
    }
    public static string ASCII85(string userInput)
    {
        string output = "";
        string input = Encode.Binary(userInput);
        string binaryPrep = "";
        double baseTenNumFromSplitBinary = 0D;
        string [] splitBinary;
        int charLength = 32;
        string baseTenNums = "";

        // Split 32 bits, add 0 to reach 32 if not 32 length
        
        for(int iterator = 0; iterator < input.Length % charLength;iterator++)
        {
            input += "0";
        }
        for(int i = 0; i < ((double)(input.Length) / charLength); i++){
            int startNum = i*charLength;
            int endNum = startNum+charLength;
            binaryPrep += input[((startNum))..(endNum)] + " ";
        }
        splitBinary = binaryPrep.Split(" ");

        // Get base 10
        foreach(string binaryNumber in splitBinary){
            baseTenNumFromSplitBinary = 0D;
            int binaryNumberLength = binaryNumber.Length - 1;
            if(binaryNumberLength == -1){break;}
            for(int i = 0; i <= binaryNumberLength; i++)
            {
                baseTenNumFromSplitBinary += (Int32.Parse($"{binaryNumber[i]}") * Math.Pow(2, (binaryNumberLength-i)));
            }
            baseTenNums += baseTenNumFromSplitBinary + " ";
        }
        string[] baseTenSplit = baseTenNums.Split(" ");
        double[] baseTenDoubles = new double[baseTenSplit.Count()];
        for(int i = 0; i < baseTenSplit.Count()-1;i++)
        {
            double.Parse(baseTenSplit[i]);
            baseTenDoubles[i] = double.Parse(baseTenSplit[i]); 
        }

        // Convert to b85
        int initializerBase85Iterator = 0;
        int firstBase85Iterator = 0;
        foreach(double number in baseTenDoubles) 
        {
            var base85ModNumsList = new List<double>(); // this guy is a bastard, forgot to move him into this foreach loop and it was adding shit it didn't need to.
            double divideNumber = number;
            while(divideNumber > 0) {
                double modNumber = divideNumber % 85;
                divideNumber = Math.Floor(divideNumber / 85);
                if(divideNumber == 0){break;}
                initializerBase85Iterator++;
            } 
            firstBase85Iterator = 0;
            divideNumber = number;
            while(divideNumber > 0) {
                double modNumber = divideNumber % 85;
                divideNumber = (divideNumber - modNumber) / 85;
                base85ModNumsList.Add(modNumber);
                if(divideNumber == 0){break;}
                firstBase85Iterator++;
            }
            // base85ModNumsList.Add(divideNumber);
            double[] base85Nums = base85ModNumsList.ToArray();
            Array.Reverse(base85Nums);
            for(int i = 0;i < base85Nums.Count(); i++)
            {
                base85Nums[i] = (base85Nums[i]+33);
            }

            for(int i = 0; i < base85Nums.Count();i++)
            {
                output += Decode.Decimal($"{base85Nums[i]}");
            }
        }
        return output;
    }
}
class Ciphers
{
    static void Main(string[] args)
    {
        string continuePrompt = "N";
        do {
            string mainOutput = "";
            //Console.Clear();
            string? cipherDirectionCheck = null;
            string? userInput = null;
            Console.Write("Encode or Decode (or shorthand) > ");
            do {
                if(cipherDirectionCheck == "buh"){Console.WriteLine("If you meant to type something, type something... otherwise choose encode, decode, or do a shorthand code.");}
                else if(cipherDirectionCheck is not null){Console.WriteLine("Please type encode, decode, or some shorthand code.");}
                cipherDirectionCheck = Console.ReadLine() ?? "buh";
                if(cipherDirectionCheck is not null && cipherDirectionCheck == "help"){
                    Console.WriteLine("Shorthand code is as follows: \n* First character: s to signal shorthand.\n* Second character to represent (e)ncode or (d)ecode.");
                    Console.WriteLine("* Following characters represents the intended code. Current supported codes include:");
                    int iterator = 0;
                    int asciiIterator = 0;
                    foreach(var thing in Enum.GetValues(typeof(Referrals.supportedCipherShorthands)))
                    {
                        Console.WriteLine($"\t> 3rd char as: {thing} = {iterator}");
                        if($"{thing}" == "ASCII"){
                            foreach(var thingie in Enum.GetValues(typeof(Referrals.asciiCiphers)))
                            {
                                Console.WriteLine($"\t> > 4th character as {thingie} = {asciiIterator}");
                                asciiIterator++;
                            }
                        }
                        iterator++;
                    }
                    ;
                };
            } while (cipherDirectionCheck is not null && cipherDirectionCheck.ToLower() != "decode" && cipherDirectionCheck.ToLower() != "encode" && cipherDirectionCheck.ToLower()[0] != 's');

            if(cipherDirectionCheck is not null){
                //DECODE CHECKS
                if(cipherDirectionCheck.ToLower() == "decode"){
                    Console.Write("Decode what? > ");
                    string? decodeType = Console.ReadLine()?.Trim() ?? "crelly stinky";
                    if(Referrals.supportedDecoders.Contains(decodeType.ToUpper())){
                        Console.Write($"Type your {decodeType} message. > ");
                        userInput = Console.ReadLine();
                        if(userInput is not null){
                            switch(decodeType.ToUpper()){
                                case "BINARY":
                                    mainOutput = Decode.Binary(userInput);
                                    break;
                                case "MORSE":
                                    mainOutput = Decode.Morse(userInput);
                                    break;
                                case "A1Z26":
                                    mainOutput = Decode.A1Z26(userInput);
                                    break;
                                case "DECIMAL":
                                    mainOutput = Decode.Decimal(userInput);
                                    break;
                                case "HEXADECIMAL":
                                    mainOutput = Decode.Hexadecimal(userInput);
                                    break;
                                case "OCTAL":
                                    mainOutput = Decode.Octal(userInput);
                                    break;
                                case "ASCII85":
                                    mainOutput = Decode.ASCII85(userInput);
                                    break;
                                default:
                                    mainOutput = "Unsupported method inside decode switch... somehow...";
                                    break;
                            }
                        } else {mainOutput = "Input detected as null.";}
                    }
                    else if (decodeType == "crelly stinky")
                    {
                        mainOutput = "Decode type detected as null... sorry.";
                    }
                    else
                    {
                        mainOutput = "Unsupported method. Please check back later for new updates.";
                    }
                }

                //ENCODING CHECKS
                else if(cipherDirectionCheck.ToLower() == "encode"){
                    Console.Write("Encode what? > ");
                    string? encodeType = Console.ReadLine()?.Trim();
                    if(encodeType is not null && Referrals.supportedEncoders.Contains(encodeType.ToUpper())){
                        Console.Write($"Type your {encodeType} message. > ");
                        userInput = Console.ReadLine();
                        if(userInput is not null){
                            switch(encodeType.ToUpper()){
                                case "BINARY":
                                    mainOutput = Encode.Binary(userInput);
                                    break;
                                case "MORSE":
                                    mainOutput = Encode.Morse(userInput);
                                    break;
                                case "A1Z26":
                                    mainOutput = Encode.A1Z26(userInput);
                                    break;
                                case "DECIMAL":
                                    mainOutput = Encode.Decimal(userInput);
                                    break;
                                case "HEXADECIMAL":
                                    mainOutput = Encode.Hexadecimal(userInput);
                                    break;
                                case "OCTAL":
                                    mainOutput = Encode.Octal(userInput);
                                    break;
                                case "ASCII85":
                                    mainOutput = Encode.ASCII85(userInput);
                                    break;
                                default:
                                    mainOutput = "Unsupported method inside encode switch... somehow...";
                                    break;
                            }
                        } else {mainOutput = "Input detected as null.";}
                    }
                    else
                    {
                        mainOutput = "Unsupported method. Please check back later for new updates.";
                    }
                }

                //SHORTHAND CODE CHECKS
                else if (cipherDirectionCheck.ToLower()[0] == 's')
                {
                    if(cipherDirectionCheck is not null){
                        int thirdCharUserInput = -1;
                        int fourthCharUserInput = -1;
                        try{
                            thirdCharUserInput = Int32.Parse($"{cipherDirectionCheck.ToLower()[2]}");
                            fourthCharUserInput = Int32.Parse($"{cipherDirectionCheck.ToLower()[3]}");
                        }
                        catch (FormatException e)
                        {
                            mainOutput = $"Unsupported shorthand. {e.Message} Please type a numer 0-9 for character 3.";
                            return;
                        }
                        catch(IndexOutOfRangeException)
                        {}

                        Console.Write(" > ");
                        userInput = Console.ReadLine();
                        if(userInput is not null){
                            switch(cipherDirectionCheck.ToLower()[1])
                            {
                                case 'd': //decode
                                    
                                    switch (thirdCharUserInput)
                                    {
                                        case (int)Referrals.supportedCipherShorthands.A1Z26:
                                            mainOutput = Decode.A1Z26(userInput);
                                            break;
                                        case (int)Referrals.supportedCipherShorthands.ASCII:
                                            switch (fourthCharUserInput)
                                            {
                                                case (int)Referrals.asciiCiphers.BINARY:
                                                    mainOutput = Decode.Binary(userInput);
                                                    break;
                                                case (int)Referrals.asciiCiphers.DECIMAL:
                                                    mainOutput = Decode.Decimal(userInput);
                                                    break;
                                                case (int)Referrals.asciiCiphers.HEXADECIMAL:
                                                    mainOutput = Decode.Hexadecimal(userInput);
                                                    break;
                                                case (int)Referrals.asciiCiphers.OCTAL:
                                                    mainOutput = Decode.Octal(userInput);
                                                    break;
                                                default:
                                                    mainOutput = "Unsupported shorthand. For ASCII please type any number 0-3.";
                                                    break;
                                            }
                                            break;
                                        case (int)Referrals.supportedCipherShorthands.ASCII85:
                                            mainOutput = Decode.ASCII85(userInput);
                                            break;
                                        case (int)Referrals.supportedCipherShorthands.MORSE:
                                            mainOutput = Decode.Morse(userInput);
                                            break;
                                        default:
                                            mainOutput = "Unsupported shorthand. " + cipherDirectionCheck.ToLower()[2];
                                            break;
                                    }
                                    break;
                                case 'e': //encode
                                    switch (thirdCharUserInput)
                                    {
                                        case (int)Referrals.supportedCipherShorthands.A1Z26:
                                            mainOutput = Encode.A1Z26(userInput);
                                            break;
                                        case (int)Referrals.supportedCipherShorthands.ASCII:
                                            switch (fourthCharUserInput)
                                            {
                                                case (int)Referrals.asciiCiphers.BINARY:
                                                    mainOutput = Encode.Binary(userInput);
                                                    break;
                                                case (int)Referrals.asciiCiphers.DECIMAL:
                                                    mainOutput = Encode.Decimal(userInput);
                                                    break;
                                                case (int)Referrals.asciiCiphers.HEXADECIMAL:
                                                    mainOutput = Encode.Hexadecimal(userInput);
                                                    break;
                                                case (int)Referrals.asciiCiphers.OCTAL:
                                                    mainOutput = Encode.Octal(userInput);
                                                    break;
                                                default:
                                                    mainOutput = "Unsupported shorthand. For ASCII please type any number 0-3.";
                                                    break;
                                            }
                                            break;
                                        case (int)Referrals.supportedCipherShorthands.ASCII85:
                                            mainOutput = Encode.ASCII85(userInput);
                                            break;
                                        case (int)Referrals.supportedCipherShorthands.MORSE:
                                            mainOutput = Encode.Morse(userInput);
                                            break;
                                        default:
                                            mainOutput = "Unsupported shorthand. " + cipherDirectionCheck.ToLower()[2];
                                            break;
                                    }
                                    break;
                                default:
                                    mainOutput = "Unsupported shorthand.";
                                    break;
                            }
                        } else {mainOutput = "\nInput detected as null.";}
                    } else {mainOutput = "\nInput detected as null.";}
                }
                else
                {
                    mainOutput = "uh oh";
                }
            } else {mainOutput = "cipherDirectionCheck detected as null.";}
            Console.WriteLine(mainOutput);
            Console.Write("Continue encoding / decoding? (Y/N) > ");
            do
            {
                if(continuePrompt.ToUpper() == "Y OR N" || continuePrompt.ToUpper() == ("one or the other, and I mean the character").ToUpper())
                {
                    Console.Write("Haha. Very funny. Type one or the other, and I mean the character. (Y/N) > ");

                }
                else if(continuePrompt.ToUpper()[0] != 'Y' && continuePrompt.ToUpper()[0] != 'N')
                {
                    Console.Write("Please type Y or N. > ");
                }
                continuePrompt = Console.ReadLine() ?? "N";
            } while ((continuePrompt.ToUpper()[0] != 'Y' && continuePrompt.ToUpper()[0] != 'N') || continuePrompt.ToUpper() == "Y OR N");
        } while (continuePrompt.ToUpper() != "N");
    } 
}
