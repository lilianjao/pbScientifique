// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
byte[] myfile = File.ReadAllBytes("C://Users//lilia//Documents//PbScientifique//Pour TD1//Pour TD1//pokemon.bmp");
Console.WriteLine("header");
int size_headerinfo = myfile[14];
for (int i = 0; i < myfile.Length; i++)
{
    Console.Write(myfile[i] + " ");
    if (i == 13) { Console.WriteLine("\n\n");
        Console.WriteLine("Header Info");
    }
    if (i == 14 + size_headerinfo) {  Console.WriteLine("\n\n"); }

      
}