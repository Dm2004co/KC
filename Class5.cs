using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KC
{
    internal class Coloration
    {
        #region Attributs
        int nombre_chrmatique;
        List<Color> Couleur = [Color.Yellow,Color.Blue,Color.Green,Color.Red];
         static Graphe g = new Graphe();
        #endregion
        public void Welsh_Powell()
        {
            List<Noeud> S = new List<Noeud>(g.Sommet.Count); 
            foreach(int s in g.Sommet)
            {
                for(int i = 0; i < S.Count; i++)
                {
                    S[i] = new Noeud(s);
                    
                }
               
            }
 //g.Tri_Sommet_Degre();
        int couleur = -1;
        while(S.Count > 0)
            {
                couleur++;
                S[couleur].Couleur = Couleur[couleur];
                S.RemoveAt(0);
                //Noeud x = new Noeud();
                foreach(Noeud n  in S)
                {
                    foreach(var key in g.Succ.Keys)
                    {
                        foreach( int sommet in g.Succ[key] )
                        {
                            if (sommet != n.Sommet)
                            {
                                n.Couleur = Couleur[couleur];


                            }
                        }
                    }
                }
            }
        }


    }
}
