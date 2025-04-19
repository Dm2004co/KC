using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KC
{
    internal class Coloration
    {
        #region Attributs
        int nombre_chrmatique;
        List<Color> Couleur = [Color.Yellow,Color.Blue,Color.Green,Color.Red,Color.Orange,Color.RebeccaPurple];
         static Graphe g = new Graphe();
        public Coloration()
        {
            nombre_chrmatique = 0;
            Welsh_Powell();
        }
        public int Nombre_Chromatique
        {
            get { return nombre_chrmatique; }
        }
        #endregion
        public List<Noeud> Welsh_Powell()
        {
            List<Noeud> S = g.N();
            g.Tri_Decroissant(S); 

            int index_couleur = 0; 
            List<Noeud> noeud_c = new List<Noeud>(); 

            foreach (Noeud noeud in S)
            {
                if (noeud_c.Contains(noeud)) continue; 

                noeud.Couleur = Couleur[index_couleur]; 
                noeud_c.Add(noeud); 

                for (int j = 0; j < S.Count; j++)
                {
                    Noeud autre = S[j];
                    if (noeud_c.Contains(autre)) continue;

                    bool noeud_c_adjacent = false;
                    foreach (var coloredNode in noeud_c)
                    {
                        if (Est_Adjacent(noeud.Sommet, autre.Sommet))
                        {
                            noeud_c_adjacent = true;
                            break;
                        }
                    }
                    if (!noeud_c_adjacent)
                    {
                        autre.Couleur = Couleur[index_couleur];
                        noeud_c.Add(autre);
                    }
                }
                index_couleur++;
                if (index_couleur >= Couleur.Count) break; 

            }
            nombre_chrmatique = index_couleur;
            return noeud_c;
        }

        private bool Est_Adjacent(int n1, int n2)
        {
           
            if (g.Succ.ContainsKey(n1) && g.Succ[n2].Contains(n1))
            {
                return true;
            }
            if (g.Succ.ContainsKey(n2) && g.Succ[n2].Contains(n1))
            {
                return true;
            }
            return false;
        }
        public void Affichage(List<Noeud> Welsh)
        {
            Console.WriteLine();
            string[] orden = new string[7];
            foreach (Noeud n in Welsh)
            {

                for (int i = 0; i < 7; i++)
                {
                    orden[0] = Convert.ToString($"Sommet = {n.Sommet}");
                    orden[1] = Convert.ToString(n.Couleur);
                    orden[2] = Convert.ToString(n.date_Dec);
                    orden[3] = Convert.ToString(n.date_Fin);
                    orden[4] = Convert.ToString($"Niveau = {n.Niveau}");
                    orden[5] = Convert.ToString($"Degre = {n.Degre}");
                    orden[6] = Convert.ToString(n.Pred);


                    Console.WriteLine($"Proprietes de {n.Sommet}  : {orden[i]}");
                }
                Console.WriteLine();
            }


        }
    }
}
