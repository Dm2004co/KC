using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.AccessControl;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace KC
{
    internal class Noeud
    {

        int sommet;
        SortedList<int, int> pred= new SortedList<int, int>();
        int degre;
        static Graphe g = new Graphe();
        Color couleur = new Color();
        int niveau;
        DateTime date_dec;
        DateTime date_fin;
         
        public Noeud()
        {
            string imput = Console.ReadLine(); 
            this.sommet = Insertion_Sommet(imput);
            this.couleur = Color.White;
            this.pred = new SortedList<int,int>(34);
            //this.degre = Calcul_Degre();
            this.niveau = Calcul_Niveau();
            this.date_fin = Fin();
            this.date_dec = Decouverte();
            
        }
        public Noeud(int sommet)
        {
            
            this.sommet = sommet;
            this.couleur = Color.White;
            this.pred = new SortedList<int, int>(34);
            //this.degre = Calcul_Degre();
            this.niveau = Calcul_Niveau();
            this.date_fin = Fin();
            this.date_dec = Decouverte();
        }
 
        public int Niveau
        {
            get { return this.niveau; }
        }
        /// <summary>
        /// Propriete d'un sommet 
        /// </summary>
        public int Sommet
        {
            get { return this.sommet; }
            set { this.sommet = value; }
        }

        /// <summary>
        /// Propriete de la Liste des predecesseurs
        /// </summary>
        public SortedList<int,int> Pred
        {
            get { return this.pred; }
        }
        /// <summary>
        /// Propriete d'un degre d'un Noeud
        /// </summary>
        public int Degre
        {
            get { return this.degre; }
        }
        /// <summary>
        /// Propriete de la couleur d'un Noeud
        /// </summary>
        public Color Couleur
        {
            get { return this.couleur; }
            set { this.couleur = value;}
        }
        /// <summary>
        /// Propriete de la date de decouverte
        /// </summary>
        public DateTime date_Dec
        {
            get { return this.date_dec; }
            
        }
        /// <summary>
        /// Propriete de la date de fin 
        /// </summary>
        public DateTime date_Fin
        {
            get { return this.date_fin; }
            
        }
        /// <summary>
        /// Propriete static du graphe
        /// </summary>
        public static Graphe G { get { return g; } }
        /// <summary>
        /// Selection d'un sommet avec des contraintes 
        /// </summary>
        /// <returns></returns>
        public  static int Insertion_Sommet( string n )
        {
            int a = Convert.ToInt32(n);
            
            try
            {
                while (G.Sommet.Contains(a) == false || a < 1 || a > 34 )
            { 
                Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                a = Convert.ToInt32(Console.ReadLine());
            }

            }
            catch (FormatException f )
            {
                Console.WriteLine($" Format Exception  : {f.Source} , \n{f.Message}");
            }
            catch (ArgumentNullException arg)
            {
                Console.WriteLine($" Null Exception  : {arg.Source} , \n{arg.Message}");
            }
            
            return a;
        }
        

        
        /// <summary>
        /// Methode pour calculer le degre d'un Noeud en fct de la matrice d'adjacence
        /// </summary>
        /// <returns></returns>
        /*public int Calcul_Degre()
        {
             
           for(int i = 0;i < G.Matrice_ADJ.GetLength(0);i++)
            {
                if (G.Matrice_ADJ[i,this.Sommet-1] == 1)
                    {
                        this.degre++;
                    }
                
                for (int j  = 0; j < G.Matrice_ADJ.GetLength(1); j++)
                {
                    if (G.Matrice_ADJ[this.Sommet-1,j] == 1)
                {
                    this.degre++;
                }
                }
            }
           return this.degre;
        }*/
        public int Calcul_Niveau()
        {
               
            
            return this.niveau;
            
        }
        /// <summary>
        /// Initialisation de la date de decouverte
        /// </summary>
        /// <returns></returns>
        public DateTime Decouverte()
        {
            
            if( this.couleur == Color.Yellow)
            {
               this.date_dec = DateTime.Now;
            }
            return this.date_dec;
        }
        /// <summary>
        /// Initialisation de la date de fin
        /// </summary>
        /// <returns></returns>
        public DateTime Fin()
        {
            if (this.couleur == Color.Red)
            {
                this.date_fin = DateTime.Now;
            }
            return this.date_fin;
        }
        /// <summary>
        /// Methode de verification de l'egalite des noeuds
        /// </summary>
        /// <param name="n"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public bool Egale(Noeud n , Noeud a )
        {
            if(n.Sommet == a.Sommet)
            {
                return true;
            }
            return false;
        }
       
        
        
    }
}
