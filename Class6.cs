using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KC
{
    internal class PCC
    {
        static Graphe g = new Graphe();
        int distance = 0;
        public (List<int>,int[,]) Djisktra()
        {
            Console.WriteLine(" Choissisez une source (noeud de depart) entre 1 et 34 (compris) : ");
            int nb = g.Sommet.Count;
            Noeud n = new Noeud();
            Lien l = new Lien(n);
            List<int> P1 = new List<int>();
            int[,] Dijsktra = new int[nb, nb];
            List<(int, int)> P2 = new List<(int, int)>();
            List<int> Q = g.Sommet;
            while (!g.Sommet.Contains(n.Sommet))
            {
                l.Distance = 0;
            }
            while (Q.Count > 0)
            {
                int s1 = Min();
                Console.WriteLine($"Sommet de distance minimale : {s1}");
                Noeud y = new Noeud(s1);
                Q.Remove(s1);
                Console.WriteLine("Q privé de s1");
                foreach (int voisinage in g.Succ[y.Sommet])
                {
                    if (g.Succ.TryGetValue(voisinage, out List<int>? value))
                    {
                        if (g.Succ[voisinage].Count > 0)
                        {

                            Noeud voisin = new Noeud(voisinage);
                            int maj =
                            Maj_Distance(voisin, y);
                            Console.WriteLine(maj);
                            for(int a = 0; a < nb; a++)
                            {
                                for(int b = 0; b < nb; b++)
                                {
                                    Dijsktra[a, b] = s1;
                                }
                            }
                        }
                    }
                }
                int Maj_Distance(Noeud s1, Noeud s2)
                {
                    int maj = 0;
                    if (l.Calcul_Poids(n, s2) > l.Calcul_Poids(n, s1) + l.Calcul_Poids(s1, s2))
                    {
                        maj = l.Calcul_Poids(n, s1) + l.Calcul_Poids(s1, s2);
                        s2.Pred[s2.Sommet] = s1.Sommet;
                        P1.Add(s2.Sommet);
                        s2.Couleur = Color.Red;
                        s2.date_Fin = s2.Fin();
                        Console.WriteLine($"{s2} rajouté à la liste P1");
                    }
                    else
                    {
                        P1.Add(s1.Sommet);
                        s1.Couleur = Color.Red;
                        s1.date_Fin = s1.Fin();
                        Console.WriteLine($"{s1} rajouté à la liste P1");
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
            return (P1,Dijsktra);
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
        public void Bellman_Ford()
        {
            Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
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

        public void Affichage_FWR_Noeud(List<Noeud> FWR)
        {
            string orden = "";
            foreach (Noeud s in FWR)
            {
                orden += Convert.ToString(s) + "|";
            }

            Console.WriteLine();
            Console.WriteLine($"L'ordre de visite : {orden}");
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
                    string s = Convert.ToString($"|{m[i, j]}|");

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


    }
    
}
