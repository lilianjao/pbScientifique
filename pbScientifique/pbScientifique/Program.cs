// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

afficherImageOctet("C://Users//lilia//Documents//PbScientifique//Pour TD1//Pour TD1//pokemon.bmp");

static void afficherImageOctet(string arbo)
{
    byte[] myfile = File.ReadAllBytes(arbo);
    Console.WriteLine("header");
    int size_headerinfo = myfile[14]; //hello word

    for(int i =0; i < 14; i++)
    {
        Console.Write(myfile[i]+" ");
    }
    Console.WriteLine("\nheader info");
    for (int i =14;i< 14 + size_headerinfo;i++)
    {
        Console.Write(myfile[i] + " ");
    }Console.WriteLine("\n\n\npixel photo :");
    for (int i = 14 + size_headerinfo;i<myfile.Length;i++)
    {
            Console.Write(myfile[i] + " ");
      
    }
    
}


