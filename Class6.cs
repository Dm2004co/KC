using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC
{
    internal class PCC
    {
        static Graphe g = new Graphe();
        int distance = 0;
        public void Djisktra()
        {
            Noeud n = new Noeud();
            Lien l = new Lien(n);
            while (!g.Sommet.Contains(n.Sommet) && n.Couleur!= Color.Red)
            {
                l.Distance = 0;
            }
        }
        public void Bellman_Ford()
        {

        }

        public void Floyd_Warshall()
        {

        }


    }
}
