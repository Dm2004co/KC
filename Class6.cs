using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KC
{
    internal class PCC
    {
        static Graphe g = new Graphe();
        #region Dijkstra 
        public (List<int>, List<List<Noeud>>,List<Noeud>,List<List<int>>,List<int>) Djisktra()
        {
            Console.WriteLine("Choissisez une source (noeud de depart) entre 1 et 34 (compris) : ");
            int nb = g.Sommet.Count;
            Noeud n = new Noeud();
            Lien l = new Lien(n);
            List<int> P1 = new List<int>();
           
            List<int> Q = g.Sommet;
            List<Noeud> P3 = new List<Noeud>();
            List<int> Pred = new List<int>();
            List<List<Noeud>> N2 = new List<List<Noeud>>();
            List<List<int>> N1 = new List<List<int>>();
            int i = 0;
            P1.Add(n.Sommet);
            P3.Add(n);
            Pred.Add(n.Sommet);
            while (!g.Sommet.Contains(n.Sommet))
            {
                l.Distance = 0;
            }
            while (Q.Count > 0 )
            {
                List<int> P2 = new List<int>();
                List<Noeud> P4 = new List<Noeud>();
                int s1 = Min();
                Console.WriteLine($"Sommet de distance minimale : {s1}");
                Noeud y = new Noeud(s1);
                Q.Remove(s1);
                P1.Add(s1);
                P2.Add(s1);
                Pred.Add(s1);
                P3.Add(y);
                P4.Add(y);
               // Console.WriteLine("Q privé de s1");
                foreach (int voisinage in g.Succ[y.Sommet])
                {
                    
                    if (g.Succ.TryGetValue(voisinage, out List<int>? value))
                    {
                        if (g.Succ[voisinage].Count > 0 )
                        {

                            Noeud voisin = new Noeud(voisinage);
                            int maj =
                            Maj_Distance(voisin,y);
                            
                            
                        }
                    }
                    
                }


                N1.Add(P2);
                N2.Add(P4);
                Affichage_Dijkstra(P1);
                Console.WriteLine();
                Affichage_Ordre_Dijkstra_Noeud(P3);
                Console.WriteLine();
                P1.Clear();
                P3.Clear();

                int Maj_Distance(Noeud s1, Noeud s2)
                {
                    int maj = 0;
                    if (l.Calcul_Poids(n, s2) > l.Calcul_Poids(n, s1) + l.Calcul_Poids(s1, s2))
                    {
                        maj = l.Calcul_Poids(n, s1) + l.Calcul_Poids(s1, s2);
                        s2.Pred[s2.Sommet] = s1.Sommet;
                        P1.Add(s2.Sommet);
                        P2.Add(s2.Sommet);
                        P3.Add(s2);
                        P4.Add(s2);
                        s2.Couleur = Color.Red;
                        s2.date_Fin = s2.Fin();
                        //Console.WriteLine($"{s2} rajouté à la liste P1");
                    }
                    else
                    {
                        P1.Add(s1.Sommet);
                        P2.Add(s1.Sommet);
                        P3.Add(s1);
                        P4.Add(s1);
                        s1.Couleur = Color.Red;
                        s1.date_Fin = s1.Fin();
                        //Console.WriteLine($"{s1} rajouté à la liste P1");
                    }
                    return maj;
                }
                int Min()
                {
                    int min = int.MaxValue;
                    foreach (int s in Q)
                    {
                        Noeud sdeb = new Noeud(s);
                        int distance = l.Calcul_Poids(n, sdeb);
                        if (distance  < min)
                        {
                            min = distance;
                            n.Sommet = s;
                        }

                    }
                    return n.Sommet;
                }
                
            }

            
            return (P1,N2,P3,N1,Pred);
        }


        public void Affichage_Dijkstra(List<int> Dijkstra)
        {
            string orden = "";
            foreach (int s in Dijkstra)
            {
                orden += Convert.ToString(s) + "|";
            }

            Console.WriteLine();
            Console.WriteLine($"L'ordre de visite : {orden}");
        }


        public void Affichage_Ordre_Dijkstra_Noeud(List<Noeud> Dijkstra)
        {
            Console.WriteLine();
            string[] orden = new string[7];
            foreach (Noeud n in Dijkstra)
            {

                for (int i = 0; i < 7; i++)
                {
                    orden[0] = Convert.ToString($"Sommet = {n.Sommet}");
                    orden[1] = Convert.ToString(n.Couleur);
                    orden[2] = Convert.ToString(n.date_Dec);
                    orden[3] = Convert.ToString(n.date_Fin);
                    orden[4] = Convert.ToString($"Niveau = {n.Niveau}");
                    orden[5] = Convert.ToString($"Degre = {n.Degre}");
                    foreach(int p in n.Pred.Values)
                    {
                        orden[6] = Convert.ToString(p);
                    }
                    


                    Console.WriteLine($"Proprietes de {n.Sommet}  : {orden[i]}");
                }
                Console.WriteLine();
            }

            
        }
        public (List<List<Noeud>>, List<List<int>> , List<Noeud>) Recherche_Chemin(Noeud arrivee)
        {
            (List<int>, List<List<Noeud>>, List<Noeud>, List<List<int>>, List<int>) n = Djisktra();

            List<List<Noeud>> N1 = new List<List<Noeud>>();
            List<Noeud> N4 = new List<Noeud>();
            List<int> N2 = new List<int>();
            List<List<int>> N3 = new List<List<int>>();
            List<int> a = new List<int>();
            int longueur_chemin = 0;
            int index = 0;
            int nb = n.Item4.Count;
            int nb_chemins = 0;

            for (int i = 0; i < nb; i++)
            {
                if (n.Item4[i].Contains(arrivee.Sommet))
                {
                    N3.Add(n.Item4[i]);
                    N1.Add(n.Item2[i]);
                }

            }
            longueur_chemin = Min();
            nb_chemins = a.Count;

            for (int j = 0; j < N3.Count; j++)
            {
                int nc = N3[j].Count;
                if (longueur_chemin == N3.Count)
                {
                    for (int k = 0; k < nc; k++)
                    {
                        if (nb_chemins > 1)
                        {
                            
                            if ((N3[j][0] != arrivee.Sommet))
                            {
                                N3.RemoveAt(j);
                                N1.RemoveAt(j);
                            }
                                
                        }
                        

                    }
                    N2 = N3[0];
                    N4 = N1[0];
                    index = n.Item5.IndexOf(N2[0]);
                }
            }
            Console.WriteLine($"La liste des predecesseurs est :  ");
            n.Item5 = n.Item5.GetRange(0, index);
            Affichage_Dijkstra(n.Item5);
            Console.WriteLine();
                Console.WriteLine($"La longueur du chemin est : {n.Item5.Count + N2.Count} ");
            Affichage_Dijkstra(N2);

                int Min()
                {
                    int min = int.MaxValue;
                
                foreach (var chemin in N3)
                    {
                        
                        int longueur = chemin.Count;
                    int b = longueur;
                        if (longueur < min)
                        {
                            min = longueur;
                            
                        }
                        if(min == b)
                    {
                        a.Add(longueur);
                    }
                        

                    }
                    return min;
                }



                return (N1,N3,N4);

            }
        
        #endregion 
        #region Bellman-Ford
        public SortedList<Lien,int> Bellman_Ford()
        {
            Console.WriteLine("Choissisez un noeud de départ entre 1 et 34 (compris) : ");
            Noeud n = new Noeud();
            Lien l = new Lien(n);
            int V = g.Sommet.Count;
            int Arete = g.AreteList.Count;
            SortedList<Lien, int> ncycle = new SortedList<Lien, int>();
            List<int> d = new List<int>(Arete);
            for (int  i = 0; i < V; i++)
            {
                for(int j = 0; j < g.AreteList.Count;j++ )
                {
                    //foreach(var w in g.Poids)
                    Noeud u = new Noeud(g.AreteList[j].Item1);
                    Noeud v = new Noeud(g.AreteList[j].Item2);
                    Lien y = new Lien(u, v);
                    int min = int.MaxValue;
                    if (!Detection_Cycle_Absorbant(u, y.Poids))
                    {
                        int distance = Maj_Distance(u, v);
                        if (distance < min)
                        {
                            min = distance;
                        }
                        //ncycle[y] = min;



                    }

                }
                }
            
                
            

            int Maj_Distance(Noeud s1, Noeud s2)
            {
                int maj = Math.Min(l.Calcul_Poids(n, s2), l.Calcul_Poids(n, s1) + l.Calcul_Poids(s1, s2));
                if ((maj == l.Calcul_Poids(n, s2)))
                {
                    
                    s2.Pred[s2.Sommet] = s1.Sommet;
                    //P1.Add(s2.Sommet);
                    //P3.Add(s2);
                    s2.Couleur = Color.Red;
                    s2.date_Fin = s2.Fin();
                    //Console.WriteLine($"{s2} rajouté à la liste P1");
                }
                else
                {
                    maj = l.Calcul_Poids(n, s1) + l.Calcul_Poids(s1, s2);
                    //P1.Add(s1.Sommet);
                    //P3.Add(s1);
                    s1.Couleur = Color.Red;
                    s1.date_Fin = s1.Fin();
                    //Console.WriteLine($"{s1} rajouté à la liste P1");
                }
                return maj;
            }
            bool Detection_Cycle_Absorbant(Noeud u , int p = 0 )
            {
                bool detect = false;
                p = g.Existence_Circuit(u, p).Item3;
                if ( p < 0)
                {
                    detect =
                g.Existence_Circuit(u, p).Item1;
                }
                return detect;
            }
            int Comparaison(int a , int b)
            {
                if(a < b )
                {
                    return a;
                }
                if(a == b)
                {
                    return (a | b);
                }
                return b; 
            }
            return ncycle;
        }

        #endregion
        #region FWR (Floyd-Warshall-Roy)
        public void Affichage_FWR(List<int> FWR)
        {
            string orden = "";
            foreach (int s in FWR)
            {
                orden += Convert.ToString(s) + "|";
            }

            Console.WriteLine();
            Console.WriteLine($"L'ordre de visite : {orden}");
        }

        public void Affichage_Ordre_FWR_Noeud(List<Noeud> BFS)
        {
            Console.WriteLine();
            string[] orden = new string[7];
            foreach (Noeud n in BFS)
            {

                for (int i = 0; i < 7; i++)
                {
                    orden[0] = Convert.ToString($"Sommet = {n.Sommet}");
                    orden[1] = Convert.ToString(n.Couleur);
                    orden[2] = Convert.ToString(n.date_Dec);
                    orden[3] = Convert.ToString(n.date_Fin);
                    orden[4] = Convert.ToString($"Niveau = {n.Niveau}");
                    orden[5] = Convert.ToString($"Degre = {n.Degre}");
                    foreach (int p in n.Pred.Values)
                    {
                        orden[6] = Convert.ToString($"Predecesseur de {n.Sommet} est : {p}");
                    }


                    Console.WriteLine($"Proprietes de {n.Sommet}  : {orden[i]}");
                }
                Console.WriteLine();
            }


        }
        public int[,] Floyd_Warshall_Roy()
        {
            
            int n = g.Matrice_ADJ.GetLength(0);
            int nb_sommets = g.Sommet.Count;
            int[,] Floyd = new int[n, n];
            int[,] F = new int[n, n];
            int[,] R = new int[n, n];
            int[,] W = new int[n, n];
            for (int i = 0; i <  n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    for (int k = 0; k < nb_sommets; k++)
                    {
                        R[i, j] = g.Matrice_ADJ[i, j];
                        W[i, j] = g.Matrice_ADJ[i, k];
                        F[i, j] = g.Matrice_ADJ[k, j];
                        Floyd[i, j] = Math.Min(R[i, j], W[i, j] + F[i,j]);
                    }
                }
            }
            return Floyd;
        }

        public void Afficher_FWR(int[,] m)
        {

            for (int i = 0; i < m.GetLength(0); i++)
            {
                int k = 0;
                while (k < m.GetLength(0))
                {
                    Console.Write("===");
                    k++;
                }

                Console.WriteLine();
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    string s = Convert.ToString($"|{m[i, j]}");

                    Console.Write(s);


                }
                Console.WriteLine();

            }
        }
        public SortedList<int, List<int>> Liste_Succ()
        {
            int n = g.Sommet.Count;
            int[,] FWR = Floyd_Warshall_Roy();
            SortedList<int, List<int>> Successeur = new SortedList<int, List<int>>(n);
            for (int a = 0; a < n; a++)
            {
                Successeur[g.Sommet[a]] = new List<int>();

            }
            for (int i = 0; i < FWR.GetLength(0); i++)
            {
                for (int j = 0; j < FWR.GetLength(1); j++)
                {
                    if (FWR[i, j] == 1)
                    {

                        Successeur[g.Sommet[j]].Add(g.Sommet[i]);



                    }
                }

            }

            return Successeur;
        }


        public (bool, List<int>, List<Noeud>)PCC_FWR(Noeud depart, Noeud arrivee)
        {
            Queue<Noeud> BFS = new Queue<Noeud>();
            List<Noeud> Noeud_v = new List<Noeud>();
            List<int> Ordre = new List<int>();
            List<int> Chemin = new List<int>();
            List<Noeud> C = new List<Noeud>();
            SortedList<int, List<int>> L = Liste_Succ();
            bool chemin = false;

            try
            {

                depart.Couleur = Color.Yellow;
                depart.date_Dec = depart.Decouverte();
                depart.Degre = depart.Calcul_Degre(); ;
                //Console.WriteLine($"Debut d'exploration de  : {depart.Sommet}");
                Noeud_v.Add(depart);
                //Console.WriteLine($"Noeud :{depart.Sommet} , visité");
                Ordre.Add(depart.Sommet);
                BFS.Enqueue(depart);
                //Console.WriteLine("Noeud rajouté dans la pile");

                while (BFS.Count > 0)
                {
                    Noeud y = BFS.Dequeue();
                    y.Degre = y.Calcul_Degre();
                    y.Couleur = Color.Red;
                    y.date_Fin = y.Fin();
                    //Console.WriteLine($"Fin d'exploration de : {y.Sommet}");
                    if (L[y.Sommet].Count > 0)
                    {
                        foreach (int voisinage in L[y.Sommet])
                        {
                            if (L.TryGetValue(voisinage, out List<int>? value))
                            {
                                if (L[voisinage].Count > 0)
                                {

                                    Noeud voisin = new Noeud(voisinage);
                                    voisin.Niveau = y.Niveau + 1;
                                    voisin.Degre = voisin.Calcul_Degre();
                                    for (int i = 0; i < Noeud_v.Count; i++)
                                    {

                                        if (!Noeud_v[i].Egale(voisin, Noeud_v[i]) && Occurence(Noeud_v, voisin) < 1)
                                        {
                                            voisin.Couleur = Color.Yellow;
                                            voisin.date_Dec = voisin.Decouverte();
                                            //Console.WriteLine($"Debut d'exploration de  : {voisin.Sommet}");
                                            Noeud_v.Add(voisin);
                                            //Console.WriteLine($"Noeud :{voisin.Sommet} , visité");
                                            Ordre.Add((voisin.Sommet));
                                            BFS.Enqueue(voisin);
                                            //Console.WriteLine($"Noeud : {voisin.Sommet}, rajouté dans la pile");
                                            voisin.Pred[voisin.Sommet] = y.Sommet;

                                        }
                                        if (voisin.Egale(arrivee, voisin))
                                        {
                                            chemin = true;
                                        }
                                    }

                                }

                            }

                        }
                    }
                }


            }

            catch (ArgumentNullException arg)
            {
                Console.WriteLine($" Null Exception  : {arg.Source} , \n{arg.Message}");
            }
            catch (StackOverflowException s)
            {
                Console.WriteLine($" Stack Overflow : {s.Source} , \n{s.Message}");
            }
            catch (IndexOutOfRangeException i)
            {
                Console.WriteLine($" Index Out of Range : {i.Source} , \n{i.Message}");
            }
            catch (Exception ex)
            {
                string s = " (La Liste d'Adjacence)";
                Console.WriteLine($" {ex.Source} , \n{ex.Message + s} ");
            }
            for (int p = 0; p < Ordre.Count; p++)
            {
                if (Ordre[p] == arrivee.Sommet)
                {
                    int index_arrivee = p;

                    Chemin = Ordre.GetRange(0, index_arrivee + 1);
                }
            }
            for (int i = 0; i < Noeud_v.Count; i++)
                if (Noeud_v[i].Egale(arrivee, Noeud_v[i]))
                {
                    int index_arrivee = i;

                    C = Noeud_v.GetRange(0, index_arrivee + 1);
                }
            return (chemin, Chemin, C);


        }

        public int Occurence(List<Noeud> n, Noeud n1)
        {
            int occ = 0;
            for (int i = 0; i < n.Count; i++)
            {
                if (n[i].Sommet == n1.Sommet)
                {
                    occ++;
                }
            }
            return occ;
        }

        #endregion

    }

}
