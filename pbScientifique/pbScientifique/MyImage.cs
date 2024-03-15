using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing;
using System.IO;

namespace pbScientifique
{
    public class MyImage
    {
        private string typeImage;
        private int tailleFichier;
        private int offSet;
        private int largeur;
        private int hauteur;
        private int nbBitsCouleur;
        private byte[,,] matrice;

        public MyImage(string typeImage = "", int tailleFichier = 0, int offSet = 0, int largeur = 0, int longueur = 0, int nbBitsCouleur = 0, byte[,,] matrice = null)
        {
            this.typeImage = typeImage;
            this.tailleFichier = tailleFichier;
            this.offSet = offSet;
            this.largeur = largeur;
            this.hauteur = longueur;
            this.nbBitsCouleur = nbBitsCouleur;
            this.matrice = matrice;
        }

        public string TypeImage
        {
            get { return this.typeImage; }
        }
        public int TailleFichier { get { return this.tailleFichier; } }
        public int OffSet { get { return this.offSet; } }
        public int Largeur { get { return this.largeur; } }
        public int Hauteur { get { return this.hauteur; } }
        public int NbBitsCouleur { get { return this.nbBitsCouleur; } }
        public byte[,,] Matrice { get { return this.matrice; } }

        public MyImage TransformationInstanceClasse(string myFile)
        {
            byte[] tail = File.ReadAllBytes(myFile);
            Console.WriteLine("\n HEADER \n");

            byte[] findTailleHeader = new byte[4];    //Ce tableau sert à contenir les 4 octets pour trouver la taille du header.
            byte[] tailFichier = new byte[4];   //Ce tableau sert à contenir les 4 octets pour trouver la taille du fichier en octets.
            byte[] findLargeur = new byte[4];   //
            byte[] findHauteur = new byte[4];
            byte[] findNbOctetsParCouleur = new byte[2];

            if (tail[0] != 66 && tail[1] != 77)
            {
                Console.WriteLine("Ce n'est pas un fichier Bitmap");
                //while (tail[0] != && tail[1] != 77) { return TransformationInstanceClasse(myFile); }
            }

            int j = 0;
            for (int i = 0; i < 14; i++)
            {
                if (i > 1 && i <= 5) { tailFichier[j] = tail[i]; j += 1; }
                Console.Write(tail[i] + " ");
            }
            this.tailleFichier = Convertir_Endian_To_Int(tailFichier);
            Console.WriteLine("\n HEADER INFO \n");
            j = 0;
            int tailleHeader = 0;

            for (int i = 14; i < 54 + tailleHeader; i++)
            {

                if (i <= 17)
                {
                    findTailleHeader[j] = tail[i];
                    j += 1;
                }
                tailleHeader = Convertir_Endian_To_Int(findTailleHeader);

                j = 0;
                if (i > 18 && i <= 23)
                {
                    findLargeur[j] = tail[i];
                    j += 1;
                }
                if (i > 23 && i <= 27)
                {
                    findHauteur[j] = tail[i];
                    j += 1;
                }
                j = 0;
                if (i > 29 && i <= 31)
                {
                    findNbOctetsParCouleur[j] = tail[i];
                }

                Console.Write(tail[i] + " ");
            }

            this.largeur = Convertir_Endian_To_Int(findLargeur);
            this.hauteur = Convertir_Endian_To_Int(findHauteur);
            this.offSet = Convertir_Endian_To_Int(findTailleHeader) + 14;
            int nbOctetsCouleur = (findNbOctetsParCouleur[1] << 8) | findNbOctetsParCouleur[0];
            this.nbBitsCouleur = nbOctetsCouleur;

            j = 0;
            Console.WriteLine("\n IMAGE \n ");
            for (int i = 54; i < tail.Length; i++)
            {
                for (int k = i; k < i + 60; k++)
                {
                    Console.Write(tail[i] + ' ');
                }
                Console.WriteLine();
            }
            int octetsParPixel = this.nbBitsCouleur / 8;
            int m = (4 - (this.largeur * octetsParPixel) % 4) % 4;
            this.matrice = new byte[this.hauteur, this.largeur, 3];
            int index = this.offSet;
            for (int i = 0; i < this.hauteur; i++)
            {
                for (int n = 0; n < this.largeur; n++)
                {
                    this.matrice[i, n, 2] = tail[index++];
                    this.matrice[i, n, 1] = tail[index++];
                    this.matrice[i, n, 0] = tail[index++];
                }
                index += m;
            }

            Console.WriteLine("\nTaille du fichier en octet : " + this.tailleFichier);
            Console.WriteLine("\nType d'image : " + this.typeImage);
            Console.WriteLine("\nValeur de l'offset : " + this.offSet);
            Console.WriteLine("\nLargeur de l'image : " + this.largeur);
            Console.WriteLine("\nHauteur de l'image : " + this.hauteur);
            Console.WriteLine("\nNombres de Bits par couleurs " + this.nbBitsCouleur);
            MyImage image = new MyImage(this.typeImage, this.tailleFichier, this.offSet, this.largeur, this.hauteur, this.nbBitsCouleur, this.matrice);
            return image;

        }

        public int Convertir_Endian_To_Int(byte[] tab)
        {
            if (tab == null || tab.Length > 4)
                throw new ArgumentException("Tableau non valide");
            int result = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                //Chaque octet est décalé de 8 bits multiplié par sa position dans le tableau
                result |= tab[i] << (8 * i);
            }
            return result;
        }
        public byte[] Convertir_Int_To_Endian(int val)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(val & 0xFF);
            bytes[1] = (byte)((val >> 8) & 0xFF);
            bytes[2] = (byte)((val >> 16) & 0xFF);
            bytes[3] = (byte)((val >> 24) & 0xFF);
            return bytes;
        }

        static string numToBin(int num)
        { // algo de conversion de décimal en binaire 
            string binaire = "";
            while (num != 0)
            {

                if (num % 2 == 0)
                {
                    binaire = "0" + binaire;
                }
                else
                {
                    binaire = "1" + binaire;
                }
                num = num / 2;

            }
            if (binaire.Length != 8)
            {
                for (int i = 0; i < 8 - binaire.Length + 1; i++)
                {
                    binaire = "0" + binaire;
                }
            }
            return binaire;
        }

        public void From_Image_To_File(string file)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    matrice[i, j] = numToBin(matrice[i, j]);
                }
            }
        }


        /*public void CreationNouveauDossier()
        {
            int[] tab =
            File.WriteAllBytes("C:/Users/basil/OneDrive - De Vinci/Documents/BASILE/ETUDE/COUR ESILV/A2/S4/PSI", tab);
        }
        */
    }
}

