using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace KC
{
    class ACPM
    {
        static Graphe g = new Graphe();
        int root;
        const int poids = 1;
        List<(int, int)> A;
        List<int> S;
        bool spanning = false;
        List<Noeud> n;
        public ACPM()
        {
            //this.n = g.N();
            this.root = Selection_Root();
            Suppression();
            //Edmonds();

        }
        int Selection_Root()
        {

            int temp = int.MinValue;
            int racine = 0;
            int t = 0;
           
            for (int i = 0; i < n.Count; i++)
            {

                if (n[i].Degre > temp)
                {
                    temp = n[i].Degre;
                    t = n[i].Sommet;
                }
                racine = t;
            }
            return racine;
        }
        void Suppression()
        {
            for (int j = 0; j < g.AreteList.Count; j++)
            {
                if (g.AreteList[j].Item2 == this.root)
                {
                    g.AreteList.RemoveAt(j);
                }
            }
        }
        void Selection()
        {
            int p = int.MaxValue;
            foreach (var s in g.Sommet)
            {
                foreach (var a in g.AreteList)
                {
                    Noeud l1 = new Noeud(a.Item1);
                    Noeud l2 = new Noeud(a.Item2);
                    if (a.Item2 == s)
                    {
                        Lien l = new Lien(l1, l2);
                        if(l.Poids < p)
                        {
                            p = l.Poids;
                            
                        }

                    }
                    g.AreteList.Remove((a.Item1, a.Item2));
                }

            }
        }
        bool Identification_Contraction_de_Cycle()
        {
            #region Identification de Cycle
            bool c = false;
            (int,int)[] A0 = new (int, int)[g.AreteList.Count];
            g.AreteList.CopyTo(A0);
            int[] S0 = new  int[g.Sommet.Count];
            g.Sommet.CopyTo(S0);
            S = g.Sommet;
            A = g.AreteList;
            int vc = 0;
            foreach (var s in S)
            {
                foreach (var a in A)
                {
                    if (a.Item1 == s)
                    {
                        Noeud n1 = new Noeud(s);
                        if(g.Existence_Circuit(n1).Item1 == false)continue ;
                        else
                        {
                            c = true;
                            
                            #region Contraction sortant
                            for (int i = 0; i < g.Existence_Circuit(n1).Item2.Count; i++)
                            {
                                int sommet = g.Existence_Circuit(n1).Item2[i];
                                S.Remove(sommet);
                            }
                           (int,int) e = (s, vc);
                            A.Add(e);
                            #endregion
                        }
                    }
                    if (a.Item2 == s)
                    {
                        Noeud n2 = new Noeud(s);
                        if (g.Existence_Circuit(n2).Item1 == false) continue;
                        else
                        {
                            c = true;
                            #region Contraction entrant
                            for (int i = 0; i < g.Existence_Circuit(n2).Item2.Count;i++)
                            {
                                int sommet = g.Existence_Circuit(n2).Item2[i];
                                S.Remove(sommet);
                            }
                            (int, int) e = (vc,s);
                            A.Add(e);
                            #endregion
                        }
                    }
                    
                }
            }
            #endregion
           


            return c;

        }
       void Edmonds()
        {
            int e = 0;
            
            foreach(var element in n)
            {
                while (e != n.D2)
                {
                    Selection();
                    Identification_Contraction_de_Cycle();
                    e++;
                }
            }
            
        }
    }
}
