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
            List<int> P1 = new List<int>();
            List<(int, int)> P2 = new List<(int, int)>();
            List<int> Q = g.Sommet;
            while (!g.Sommet.Contains(n.Sommet) && n.Couleur != Color.Red)
            {
                l.Distance = 0;
            }
            while (Q.Count > 0)
            {
                int s1 = Min();
                Noeud y = new Noeud(s1);
                Q.Remove(s1);
                foreach (int voisinage in g.Succ[y.Sommet])
                {
                    if (g.Succ.TryGetValue(voisinage, out List<int>? value))
                    {
                        if (g.Succ[voisinage].Count > 0)
                        {

                            Noeud voisin = new Noeud(voisinage);
                            Maj_Distance(voisin, y);
                        }
                    }
                }
                int Maj_Distance(Noeud s1, Noeud s2)
                {
                    int maj = 0;
                    if (l.Calcul_Distance(n, s2) > l.Calcul_Distance(n, s1) + l.Calcul_Poids(s1, s2))
                    {
                        maj = l.Calcul_Distance(n, s1) + l.Calcul_Poids(s1, s2);
                        s2.Pred[s2.Sommet] = s1.Sommet;
                        P1.Add(s2.Sommet);
                    }
                    else
                    {
                        P1.Add(s1.Sommet);
                    }
                    return maj;
                }
                int Min()
                {
                    int min = 0;
                    foreach (int s in Q)
                    {
                        Noeud sdeb = new Noeud(s);
                        int distance = 0;
                        if ((distance = l.Calcul_Distance(n, sdeb)) < min)
                        {
                            min = distance;
                            n.Sommet = s;
                        }

                    }
                    return n.Sommet;
                }
            }
        }
        public void Bellman_Ford()
        {
            Noeud n = new Noeud();
            Lien l = new Lien(n);
            int V = g.Sommet.Count;

            for(int  i = 0; i < V; i++)
            {
                foreach(var ar in g.AreteList)
                {
                    foreach(var w  in g.Poids)
                    {
                        Noeud u = new Noeud(ar.Item1);
                        Noeud v = new Noeud(ar.Item2);
                        Maj_Distance(u,v);
                        // Verification de l'existence d'un cycle/circuit absorbant
                    }
                    
                }
            }

            int Maj_Distance(Noeud s1, Noeud s2)
            {
                int maj = 0;
                if (l.Calcul_Distance(n, s2) < l.Calcul_Distance(n, s1) + l.Calcul_Poids(s1, s2))
                {
                    maj = l.Calcul_Distance(n, s1) + l.Calcul_Poids(s1, s2);
                    s2.Pred[s2.Sommet] = s1.Sommet;
                    //P1.Add(s2.Sommet);
                }
                else
                {
                    //P1.Add(s1.Sommet);
                }
                return maj;
            }
        }

        public void Floyd_Warshall()
        {

        }


    }
}
