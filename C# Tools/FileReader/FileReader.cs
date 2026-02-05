using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

class GetFile{
    public static string Type(string outputFilename, string fileContents) 
    {
        // is there any better way to do this lmao
        // Reference: https://en.wikipedia.org/wiki/List_of_file_signatures
        int startNum = 0;
        int endNum = startNum+7;
        string fileType = "";
        switch(fileContents[startNum..endNum])
            {
                case "00000000": // Hex: 00
                    startNum += 8;
                    endNum = startNum+7;
                    switch(fileContents[startNum..endNum])
                    {
                        case "00000000": // Hex: 00
                            startNum += 8;
                            endNum = startNum+7;
                            break;
                        case "00000001":
                            break;
                        case "01100001": // Hex: 61 
                            if (fileContents[16..31] == "0111001101101101")
                            {
                                fileType += ".wasm";
                            }
                            break;
                        default:
                            fileType += ".pic"; 
                            // Can also be PIF, SEA, YTR
                            // I have just picked the first from the list.
                            break;
                    }
                    break;
                case "00111100":
                            if(fileContents[16..95] == "0011111101111000011011010110110000100000")
                            {
                                Console.WriteLine("yes");
                                fileType += ".xml";
                            }
                            break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                // case "":
                //     break;
                default:
                    if(fileContents[0..31] == "01000111010010010100011000111000"){
                        fileType += ".gif";
                    }
                    else if (fileContents[(fileContents.Length-32)..fileContents.Length] == 
                                "01101011011011110110110001111001")
                    {
                        fileType += ".dmg";
                    }
                    else{
                        Console.WriteLine(fileContents[0.95]);
                    }
                    break;
        }
        Console.WriteLine(fileType);
        Console.WriteLine(outputFilename);
        outputFilename += fileType;
        return outputFilename;
    }
}

class ManipulateFile
{
    // RIPPED DIRECTLY FROM HERE: https://stackoverflow.com/questions/2426190/how-to-read-file-binary-in-c
    // I HAVE NO SHAME SORRY I DON'T UNDERSTAND HOW THIS WORKS
    public static void ToBinary(string inputFilename)
    {
        string outputFilename = (@".\"+Regex.Replace(inputFilename, @".*\\", "")+".txt");
        if(inputFilename is not null){
            byte[] fileBytes = File.ReadAllBytes(inputFilename);
            StringBuilder sb = new StringBuilder();

            foreach(byte b in fileBytes)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));  
            }

            File.WriteAllText(outputFilename, sb.ToString());
        }

    }
    public static void FromBinary(string inputFilename)
    {
        string outputFilename = (@".\"+Regex.Replace(Regex.Replace(inputFilename, @".*\\", ""), @"\..*",""));
        if(inputFilename is not null){
            string fileContents = File.ReadAllText(inputFilename);
            fileContents = fileContents.Replace(" ","");
            outputFilename = GetFile.Type(outputFilename,fileContents);
            // pippa rippad from https://stackoverflow.com/questions/3436398/convert-a-binary-string-representation-to-a-byte-array
            if(fileContents.Length % 8 == 0){
                int numOfBytes = fileContents.Length / 8;
                byte[] bytes = new byte[numOfBytes];
                for(int i = 0; i < numOfBytes; ++i)
                {
                    bytes[i] = Convert.ToByte(fileContents.Substring(8 * i, 8), 2);
                }
                File.WriteAllBytes(outputFilename, bytes);
            }
            else
            {
                Console.WriteLine("File contents not multiple of 8: " + (fileContents.Length % 8));
            }
        }

    }
}

class FileReader
{
    static void Main(string[] args)
    {
        string fileConvertCheck = Console.ReadLine();
        if (fileConvertCheck is not null)
        {
            string inputFilename = Console.ReadLine();
            if(fileConvertCheck.ToLower().Trim() == "to" && inputFilename is not null)
            {
                ManipulateFile.ToBinary(inputFilename);
            }
            else if(fileConvertCheck.ToLower().Trim() == "from" && inputFilename is not null)
            {
                ManipulateFile.FromBinary(inputFilename);
            }
        }
    }
}