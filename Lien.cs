using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC
{
    internal class Lien
    {

        static Graphe g = new Graphe();
        (Noeud, Noeud) arete;
        bool orientation;
        bool ponderation;
        int poids;
        int distance;
        /// <summary>
        /// Constructeur naturel de la classe Lien
        /// </summary>
        public Lien(Noeud n , Noeud p)
        {
            this.arete = (n,p);
            this.orientation = false;
            this.ponderation = false;
            this.poids = Calcul_Poids(n,p);
            this.distance = 0;
        }
        public Lien(Noeud n )
        {
            this.orientation = false;
            this.ponderation = false;
            this.poids = 0;
            this.distance = this.poids;
        }
        /// <summary>
        /// Propirete d'une arete
        /// </summary>
        public (Noeud, Noeud) Arete
        {
            get { return this.arete; }
            set { this.arete = value; }
        }
        /// <summary>
        /// Propirete de la liste d'aretes
        /// </summary>

        /// <summary>
        /// Propirete de l'orientation d'un graphe
        /// </summary>
        public bool Orientation
        {
            get { return this.orientation; }
        }
        /// <summary>
        /// Propirete de la ponderation d'un graphe
        /// </summary>
        public bool Ponderation
        {
            get { return this.ponderation; }
        }
        /// <summary>
        /// Distance d'une arete(arc)
        /// </summary>
        public int Distance
        {
            get { return this.distance; }
            set { this.distance = value; }

        }
        /// <summary>
        /// Poids d'une arete(arc)
        /// </summary>
        public int Poids
        {
            get { return this.poids; }
            set { this.poids = value; }
        }
       
        public int Calcul_Distance(Noeud a , Noeud b)
        {
            //Existence de Chemin entre a et b 
            //Calcul de distance avec le nombre de sommets renvoyes entre a et b
            return this.distance;
        }
        public int Calcul_Poids(Noeud a , Noeud b)
        {
            int poids = 0;
            if(g.Chemin(a, b).Item1)
            {
                List<int> Chemin = g.Chemin(a, b).Item2;
                poids = Chemin.Count;
            }
            
            return poids; 
        }
        

    }
}

