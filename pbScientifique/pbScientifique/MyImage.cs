using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing;

namespace testBitmap0
{
    internal class MyImage
    {
        private string typeImage;
        private int tailleFichier;
        private int offSet;
        private int largeur;
        private int longueur;
        private int nbBitesCouleur;
        private Color[,] matrice;

        public MyImage(string typeImage, int tailleFichier, int offSet, int largeur, int longueur, int nbBitesCouleur, Color[,] matrice)
        {
            this.typeImage = typeImage;
            this.tailleFichier = tailleFichier;
            this.offSet = offSet;
            this.largeur = largeur;
            this.longueur = longueur;
            this.nbBitesCouleur = nbBitesCouleur;
            this.matrice = matrice;
        }

        public string TypeImage
        {
            get { return this.typeImage; }
        }
        public int TailleFichier { get { return this.tailleFichier; } }
        public int OffSet { get { return this.offSet; } }
        public int Largeur { get { return this.largeur; } }
        public int Longueur { get { return this.longueur; } }
        public int NbBitesCouleur { get { return this.nbBitesCouleur; } }
        public Color[,] Matrice { get { return this.matrice; } }

        public MyImage TransfomationInstanceClasse(string myFile)
        {
            string tampon = "";
            for (int i = myFile.Length; i > myFile.Length - 4; i++)
            {
                tampon = tampon + myFile[i];
            }
            if (tampon != ".bmp") { return null; }
            byte[] tail = File.ReadAllBytes(myFile);
            Console.WriteLine("\n HEADER \n");
            for (int i = 0; i < 14; i++) { int tailleHeader Console.Write(myFile[i] + " "); }
            Console.WriteLine("n HEADER INFO \n");
            for (int i = 14; i < 54; i++) { Console.Write(myFile[i] + " "); }
            Console.WriteLine("\n IMAGE \n ");
            for (int i = 54; i < )
                this.tailleFichier = tail.Length;
            this.largeur = this.matrice.GetLength(1);
            this.longueur = this.matrice.GetLength(0);

        }
    }
}
