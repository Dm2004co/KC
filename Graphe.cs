//using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace KC
{
    internal class Graphe
    {
        List<(int, int)> areteList = new List<(int, int)> ();
        List<int> sommet = new List<int>();
        int[,] matrice_ADJ = new int[34, 34];
        int[,] matrice_incidence;
        int degre_max;
        bool simple = true;
        bool multigraphe = true;
        bool complet = true;
        bool regulier = false;
        bool biparti = false;
        bool planaire = false;
        SortedList<int, List<int>> succ = new SortedList<int, List<int>> ();
        SortedList<int, List<int>> pred = new SortedList<int, List<int>>();
        SortedList<int, List<int>> poids = new SortedList<int, List<int>>();
        List<int>niveau = new List<int>();
        List<Noeud>noeuds = new List<Noeud>();
        List<DateTime>date_decouverte = new List<DateTime>();
        List<DateTime> date_fin = new List<DateTime>();
        List<int>degre = new List<int>();
        bool orientation = false;
        bool ponderation = false;
        int niv = 0;
        /// <summary>
        /// Constructeur naturel d'un Graphe
        /// </summary>
        public Graphe()
        {
            this.orientation = Orientation();
            this.ponderation = Ponderation();
            this.areteList = new List<(int, int)> { (1, 2),(2,1), (1, 3),(3,1), (1, 4),(4,1), (4, 2),(2,4), (4, 3),(3,4), (5, 1),(1,5), (6, 1),(1,6), (7, 1),(1,7), (7, 5),(5,7), (7, 6),(6,7), (8, 1),(1,8), (8, 3),(3,8), (8, 4),(4,8), (8, 2),(2,8), (9, 1),(1,9) ,(9, 3),(3,9), (10, 3),(3,10), (11, 1),(1,11), (11, 5),(5,11) ,(11, 6),(6,11), (12, 1),(1,12), (13, 1),(1,13), (13, 4),(4,13), (14, 1),(1,14), (14, 2),(2,14), (14, 3),(3,14), (14, 4),(4,14), (17, 6),(6,17), (17, 7),(7,17), (18, 1),(1,18), (18, 2),(2,18), (20, 1),(1,20), (20, 2),(2,20), (22, 5),(5,22), (22, 1),(1,22), (26, 24),(24,26), (26, 25),(25,26), (28, 3),(3,28), (28, 24),(24,28), (28, 25),(25,28), (29, 3),(3,29), (30, 24),(24,30), (30, 27),(27,30), (31, 2),(2,31), (31, 9),(9,31), (32, 1),(1,32), (32, 25),(25,31), (32, 26),(26,32), (32, 29),(29,32), (33, 9),(9,33), (33, 16), (33, 15),(15,33), (33, 19),(19,33), (33, 21),(21,33), (33, 23),(23,33), (33, 24),(24,33), (33, 30),(30,33), (33, 31),(31,33), (33, 32),(32,33), (34, 9),(9,34), (34, 16),(16,34), (34, 15),(15,34), (34, 19),(19,34), (34, 21),(21,34), (34, 23),(23,34), (34, 24),(24,34), (34, 30),(30,34), (34, 31),(31,34), (34, 32),(32,34), (34, 33),(33,34), (34, 10),(10,34), (34, 20),(20,34), (34, 27),(27,34), (34, 28),(28,34), (34, 29),(29,34) };
            this.sommet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 };
            this.matrice_ADJ = Matrice();
            this.matrice_incidence = Mat_Incidence();
            this.succ = Liste_Succ();
            this.pred = Liste_Pred();
            this.degre_max = Deg_Max();
            this.niv = 0;
            this.noeuds = N();
            

        }
        /// <summary>
        ///  Propriete de la Ponderation des aretes(arcs)
        /// </summary>
        public bool Ponderation_Arc
        {
            get { return this.ponderation; }
        }
        /// <summary>
        /// Propriete de l'Orientation des aretes(arcs)
        /// </summary>
        public bool Orientation_Arc
        {
            get { return this.orientation; }
        }
        /// <summary>
        /// Degre du graphe
        /// </summary>
        public int Degre_Max
        {
            get { return this.degre_max;}
        }
        /// <summary>
        /// Matrice d'incidence
        /// </summary>
        public int[,]Matrice_Incidence
        {
            get { return this.matrice_incidence;}
        }
        /// <summary>
        /// Liste des aretes(arc) du graphe
        /// </summary>
        public List<(int, int)> AreteList
        {
            get { return this.areteList; }
            set { this.areteList = value; }

        }
        /// <summary>
        /// Representation planaire
        /// </summary>
        public bool Planaire
        {
            get {  return this.planaire; }
        }
        /// <summary>
        /// Liste des sommets du graphe 
        /// </summary>
        public List <int> Sommet
            { 
            get { return sommet; }
            set {  this.sommet = value; }
            }
        /// <summary>
        /// Presence uniquement d'arete simple
        /// </summary>
        public bool Simple
        {
            get { return simple; }
        }
        /// <summary>
        /// Presence d'aretes multiples ou de boucles
        /// </summary>
        public bool Multigraphe
        {
            get { return multigraphe; }
        }
        /// <summary>
        /// Degre du graphe
        /// </summary>
        
        /// <summary>
        /// Presence d'un graphe NP connexe et  K-regulier  
        /// </summary>
        public bool Complet
        {
            get { return complet; }
        }
        /// <summary>
        /// Egalite des degres des Noeuds d'un Graphe
        /// </summary>
        public bool Regulier
        { 
            get { return regulier; } 
        }
        /// <summary>
        /// Propriete de la liste des Succcesseurs
        /// </summary>
        public SortedList <int, List<int>> Succ
        {
            get { return this.succ; }
            set { this.succ = value; }
        }
        /// <summary>
        /// Propriete de la Liste des Predecesseurs
        /// </summary>
        public SortedList<int, List<int>> Pred
        {
            get { return this.pred; }
            set { this.pred = value;  }
        }
        /// <summary>
        /// Presence de deux sous-ensembles liés entre-eux
        /// </summary>
        public bool Biparti
        {
            get {return biparti;}
        }
        /// <summary>
        /// Listes des niveaux des sommets
        /// </summary>
        public List<int> Niveau
        {
            get { return this.niveau; }
        }
        /// <summary>
        /// Listes des Noeuds du graphe
        /// </summary>
        public List<Noeud> Noeuds
        {
            get { return this.noeuds; }
        }
        /// <summary>
        /// Listes des dates de decouverte
        /// </summary>
        public List<DateTime> Date_Dec
        {
            get { return this.date_decouverte; }
        }
        /// <summary>
        /// Listes des dates de fin
        /// </summary>
        public List<DateTime> Date_Fin
        {
            get { return this.date_fin;}
        }

        /// <summary>
        /// Propriete de la matrice d'adjacence
        /// </summary>
        public int[,] Matrice_ADJ
        {
            get { return this.matrice_ADJ; }
            set { this.matrice_ADJ = value; }
        }
        /// <summary>
        /// Listes des degres de chaque sommet
        /// </summary>
        public List<int> Degre
        {
            get { return this.degre; }
        }
        /// <summary>
        /// Affichage des niveaux des sommets(Noeuds)
        /// </summary>
        /// <param name="Niv"></param>
        public void Affichage_Niveau(List<int> Niv)
        {
            string n = "";
            foreach (int s in Niv)
            {
                 n+= Convert.ToString(s) + "|";
            }

            Console.WriteLine();
            Console.WriteLine($"Ordre des Niveaux : {n}");
        }
       
        /// <summary>
        /// Orientation du graphe
        /// </summary>
        /// <returns></returns>
        public bool Orientation()
        {
            bool oriente = true;
            int n = this.Matrice_ADJ.GetLength(0);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                    if (this.matrice_ADJ[i,j] == this.matrice_ADJ[j,i])
                    {
                        oriente = false;
                    }
            }
            return oriente;
        }
        /// <summary>
        /// Ponderation du graphe
        /// </summary>
        /// <returns></returns>
        public bool Ponderation()
        {
            bool pondere = false;
            int n = this.Matrice_ADJ.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (this.matrice_ADJ[i,j] > 1)
                    {
                        pondere = true;
                    }
            }
            return pondere;
        }
        /// <summary>
        /// Creation de la matrice d'adjacence
        /// </summary>
        /// <returns></returns>
        public int[,] Matrice()
        { 
            int n = Sommet.Count;
            int[,] Matrice_Adjacence = new int[n, n];
           
                    for(int i  = 0; i < n; i++)
            {
                for(int j = 0; j < n;  j++)
                {
                    Matrice_Adjacence[i, j] = Exist_Adjacence(Sommet[i], Sommet[j]);
                }
            }
                  
            return Matrice_Adjacence;
        }
         /// <summary>
        /// Couplage des aretes à un duo(sommet,sommet)
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public int Exist_Adjacence (int s1 , int s2)
        {
           
               foreach (var arete in AreteList)
            {
                if( (s1 == arete.Item1 &&  s2 == arete.Item2) )
                {
                    return 1;
                }
               
            } return 0;
              
                
         }
           
        /// <summary>
        /// Affichage de la matrice d'adjacence
        /// </summary>
        public void Afficher_Adjacence (int[,]m)
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
        /// <summary>
        /// Creation de la matrice d'incidence
        /// </summary>
        /// <returns></returns>
        public int[,] Mat_Incidence()
        {
            int nb_Arcs = AreteList.Count;
            int nb_Sommets = Sommet.Count;
            int[,] m = new int[nb_Arcs, nb_Sommets];
            for(int i = 0; i <  nb_Arcs; i++)
            {
                for(int j = 0; j < nb_Sommets; j++)
                {
                    m[i,j] = Exist_Incidence(Sommet[j]);
                }
            }
            return m;
        }

        /// <summary>
        /// Couplage des aretes à un un sommet
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int Exist_Incidence(int s)
        {
            foreach (var arete in AreteList)
            {
                if (arete.Item1 == s)
                {
                    //e = true;
                    return 1;
                }
                    if (arete.Item2 == s)
                    {
                        return  - 1;
                    }
             
            }
            return 0;
          
        }

       /// <summary>
       /// Affichage de la matrice d'incidence
       /// </summary>
       /// <param name="m"></param>
        public void Afficher_Incidence(int[,] m)
        {

            for (int i = 0; i < m.GetLength(0); i++)
            {
                /*int k = 0;
                while (k < m.GetLength(0) + m.GetLength(1))
                {
                    Console.Write("---");
                    k++;
                }*/

                //Console.WriteLine();
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    string s = Convert.ToString($"{m[i, j]}|");

                    Console.Write(s);


                }
                Console.WriteLine();

            }
        }
        
        /// <summary>
        /// Permutation entre deux object de meme type
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void Permutation(object a ,object b)
        {
            object temp = new object();
            temp.Equals (a);
            a.Equals (b);
            b.Equals(temp);
        }

        /// <summary>
        /// Initialisation de la liste des Successeurs
        /// </summary>
        /// <returns></returns>
        public SortedList <int,List<int>> Liste_Succ()
        {
            int n = Sommet.Count;
            SortedList<int,List<int>> Successeur = new SortedList<int, List<int>>(n);
            for(int a = 0; a < n; a++)
            {
                  Successeur[Sommet[a]] = new List<int>();
                
            }
            for(int i = 0;i < this.matrice_ADJ.GetLength(0); i++)
            {
               for(int j = 0;  j < this.matrice_ADJ.GetLength(1); j++)
                {
                    if (this.matrice_ADJ[i, j] == 1)
                    {

                        Successeur[Sommet[j]].Add(Sommet[i]);
                        
                        

                    }
                }
                
            }

            return Successeur;
        }

        /// <summary>
        /// Initialisation de la liste des Predecesseurs
        /// </summary>
        /// <returns></returns>
        public SortedList<int, List<int>> Liste_Pred()
        {
            int n = Sommet.Count;
            SortedList<int, List<int>> Predecesseur = new SortedList<int, List<int>>(n);
            for (int a = 0; a < n; a++)
            {
                    Predecesseur[Sommet[a]] = new List<int>();
            }
            for (int i = 0; i < this.matrice_ADJ.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrice_ADJ.GetLength(1); j++)
                {
                    if (this.matrice_ADJ[i, j] == 1)
                    {
                        Predecesseur[Sommet[j]].Add(Sommet[i]);
                    }
                }

            }

            return Predecesseur;
        }


        /// <summary>
        /// Parcours en profondeur ou Deep First Search
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        public List<int> DFS (Noeud depart)
        {
            Stack<Noeud> DFS = new Stack<Noeud>();
            List<Noeud> Noeud_v = new List<Noeud>();
            List<int> Ordre = new List<int>();

            try
            {
              
                depart.Couleur = Color.Yellow;
                depart.date_Dec = depart.Decouverte();
                depart.Degre = depart.Calcul_Degre(); ;
                Console.WriteLine($"Debut d'exploration de  : {depart.Sommet}");
                Noeud_v.Add(depart);
                Console.WriteLine($"Noeud :{depart.Sommet} , visité");
                Ordre.Add(depart.Sommet);
                DFS.Push(depart);
                Console.WriteLine("Noeud rajouté dans la pile");

                while (DFS.Count > 0)
                {
                    Noeud y = DFS.Pop();
                    y.Degre = y.Calcul_Degre();
                    y.Couleur = Color.Red;
                    y.date_Fin = y.Fin();
                    Console.WriteLine($"Fin d'exploration de : {y.Sommet}");
                    if (Succ[y.Sommet].Count > 0)
                    {
                        foreach (int voisinage in Succ[y.Sommet])
                        {
                            if (Succ.TryGetValue(voisinage, out List<int>? value))
                            {
                                if (Succ[voisinage].Count > 0)
                                {

                                    Noeud voisin = new Noeud(voisinage);
                                    voisin.Niveau = y.Niveau + 1 ;
                                    voisin.Degre = voisin.Calcul_Degre();
                                    for (int i = 0; i < Noeud_v.Count; i++)
                                      {

                                        if (!Noeud_v[i].Egale(voisin, Noeud_v[i]) && Occurence(Noeud_v,voisin) < 1)
                                        {
                                            voisin.Couleur = Color.Yellow;
                                            voisin.date_Dec = voisin.Decouverte();
                                            Console.WriteLine($"Debut d'exploration de  : {voisin.Sommet}");
                                            Noeud_v.Add(voisin);
                                            Console.WriteLine($"Noeud :{voisin.Sommet} , visité");
                                            Ordre.Add((voisin.Sommet));
                                            DFS.Push(voisin);
                                            Console.WriteLine($"Noeud : {voisin.Sommet}, rajouté dans la pile");
                                            voisin.Pred[voisin.Sommet] = y.Sommet;

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
            return Ordre;

        }

        /// <summary>
        /// Afficahge de l'ordre de visite d'une DFS
        /// </summary>
        /// <param name="DFS"></param>
        public void Affichage_Ordre_DFS (List<int> DFS)
        {
            string orden = "";
            foreach( int s in DFS)
            {
                orden += Convert.ToString(s)+"|";
            }
            
            Console.WriteLine();
            Console.WriteLine($"L'ordre de visite : {orden}");
        }

        /// <summary>
        /// Parcours en profondeur ou Deep First Search
        /// </summary>
        /// <param name="depart"></param>
        /// <returns></returns>
        public List<Noeud> DFS_Noeud(Noeud depart)
        {
            Stack<Noeud> DFS = new Stack<Noeud>();
            List<Noeud> Noeud_v = new List<Noeud>();
            List<int> Ordre = new List<int>();

            try
            {

                depart.Couleur = Color.Yellow;
                depart.date_Dec = depart.Decouverte();
                depart.Degre = depart.Calcul_Degre();
                Console.WriteLine($"Debut d'exploration de  : {depart.Sommet}");
                Noeud_v.Add(depart);
                Console.WriteLine($"Noeud :{depart.Sommet} , visité");
                Ordre.Add(depart.Sommet);
                DFS.Push(depart);
                Console.WriteLine("Noeud rajouté dans la pile");

                while (DFS.Count > 0)
                {
                    Noeud y = DFS.Pop();
                    y.Degre = y.Calcul_Degre();
                    y.Couleur = Color.Red;
                    y.date_Fin = y.Fin();
                    Console.WriteLine($"Fin d'exploration de : {y.Sommet}");
                    if (Succ[y.Sommet].Count > 0)
                    {
                        foreach (int voisinage in Succ[y.Sommet])
                        {
                            if (Succ.TryGetValue(voisinage, out List<int>? value))
                            {
                                if (Succ[voisinage].Count > 0)
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
                                            Console.WriteLine($"Debut d'exploration de  : {voisin.Sommet}");
                                            Noeud_v.Add(voisin);
                                            Console.WriteLine($"Noeud :{voisin.Sommet} , visité");
                                            Ordre.Add((voisin.Sommet));
                                            DFS.Push(voisin);
                                            Console.WriteLine($"Noeud : {voisin.Sommet}, rajouté dans la pile");
                                            voisin.Pred[voisin.Sommet] = y.Sommet;

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
            return Noeud_v;

        }


        /// <summary>
        /// Affichage d'une DFS en fonction des Noeuds
        /// </summary>
        /// <param name="DFS"></param>
        public void Affichage_Ordre_DFS_Noeud(List<Noeud> DFS)
        {
            Console.WriteLine();
            string[] orden = new string [7];
            foreach (Noeud n in DFS)
            {
                
             for(int i = 0; i < 7; i++)
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

        /// <summary>
        /// Parcours en largeur ou Breath First Search
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public (List<int>,List<Noeud>)BFS (Noeud depart)
        {
            Queue<Noeud> BFS = new Queue<Noeud>();
            List<Noeud> Noeud_v = new List<Noeud>();
            List<int> Ordre = new List<int>();

            try
            {

                depart.Couleur = Color.Yellow;
                depart.date_Dec = depart.Decouverte();
                depart.Degre = depart.Calcul_Degre(); ;
                Console.WriteLine($"Debut d'exploration de  : {depart.Sommet}");
                Noeud_v.Add(depart);
                Console.WriteLine($"Noeud :{depart.Sommet} , visité");
                Ordre.Add(depart.Sommet);
                BFS.Enqueue(depart);
                Console.WriteLine("Noeud rajouté dans la pile");

                while (BFS.Count > 0)
                {
                    Noeud y = BFS.Dequeue();
                    y.Degre = y.Calcul_Degre();
                    y.Couleur = Color.Red;
                    y.date_Fin = y.Fin();
                    Console.WriteLine($"Fin d'exploration de : {y.Sommet}");
                    if (Succ[y.Sommet].Count > 0)
                    {
                        foreach (int voisinage in Succ[y.Sommet])
                        {
                            if (Succ.TryGetValue(voisinage, out List<int>? value))
                            {
                                if (Succ[voisinage].Count > 0)
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
                                            Console.WriteLine($"Debut d'exploration de  : {voisin.Sommet}");
                                            Noeud_v.Add(voisin);
                                            Console.WriteLine($"Noeud :{voisin.Sommet} , visité");
                                            Ordre.Add((voisin.Sommet));
                                            BFS.Enqueue(voisin);
                                            Console.WriteLine($"Noeud : {voisin.Sommet}, rajouté dans la pile");
                                            voisin.Pred[voisin.Sommet] = y.Sommet;

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
            return (Ordre,Noeud_v);


        }

        /// <summary>
        /// BFS d'un chemin(duo de noeuds)
        /// </summary>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        /// <returns></returns>
        public (bool,List<int>,List<Noeud>) Chemin(Noeud depart,Noeud arrivee)
        {
            Queue<Noeud> BFS = new Queue<Noeud>();
            List<Noeud> Noeud_v = new List<Noeud>();
            List<int> Ordre = new List<int>();
            bool chemin = false;

            try
            {

                depart.Couleur = Color.Yellow;
                depart.date_Dec = depart.Decouverte();
                depart.Degre = depart.Calcul_Degre(); ;
                Console.WriteLine($"Debut d'exploration de  : {depart.Sommet}");
                Noeud_v.Add(depart);
                Console.WriteLine($"Noeud :{depart.Sommet} , visité");
                Ordre.Add(depart.Sommet);
                BFS.Enqueue(depart);
                Console.WriteLine("Noeud rajouté dans la pile");

                while (BFS.Count > 0)
                {
                    Noeud y = BFS.Dequeue();
                    y.Degre = y.Calcul_Degre();
                    y.Couleur = Color.Red;
                    y.date_Fin = y.Fin();
                    Console.WriteLine($"Fin d'exploration de : {y.Sommet}");
                    if (Succ[y.Sommet].Count > 0)
                    {
                        foreach (int voisinage in Succ[y.Sommet])
                        {
                            if (Succ.TryGetValue(voisinage, out List<int>? value))
                            {
                                if (Succ[voisinage].Count > 0)
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
                                            Console.WriteLine($"Debut d'exploration de  : {voisin.Sommet}");
                                            Noeud_v.Add(voisin);
                                            Console.WriteLine($"Noeud :{voisin.Sommet} , visité");
                                            Ordre.Add((voisin.Sommet));
                                            BFS.Enqueue(voisin);
                                            Console.WriteLine($"Noeud : {voisin.Sommet}, rajouté dans la pile");
                                            voisin.Pred[voisin.Sommet] = y.Sommet;

                                        }
                                        if (Noeud_v[i].Egale(arrivee, Noeud_v[i]) || voisin.Egale(arrivee, Noeud_v[i]))
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
            return (chemin,Ordre,Noeud_v);


        }

        public void Affichage_Ordre_BFS(List<int> BFS)
        {
            string orden = "";
            foreach (int s in BFS)
            {
                orden += Convert.ToString(s) + "|";
            }

            Console.WriteLine();
            Console.WriteLine($"L'ordre de visite : {orden}");
        }

              
         /// <summary>
         /// Presence d'un Cycle(Circuit) avec exemple si possible
         /// </summary>
         /// <param name="depart"></param>
         /// <param name="arrivee"></param>
         /// <returns></returns>

        public (bool,List<int>) Existence_Circuit(Noeud depart)
        {
            bool c = false; 
            Stack<Noeud> DFS = new Stack<Noeud>();
            List<Noeud> Noeud_v = new List<Noeud>();
            List<int> Cycle = new List<int>();
            List<int> Cir = new List<int>();

            try
            {

                depart.Couleur = Color.Yellow;
                //Console.WriteLine($"Debut d'exploration de  : {depart.Sommet}");
                Noeud_v.Add(depart);
                //Console.WriteLine($"Noeud :{depart.Sommet} , visité");
                Cycle.Add(depart.Sommet);
                DFS.Push(depart);
                //Console.WriteLine("Noeud rajouté dans la pile");

                while (DFS.Count > 0 && Noeud_v.Count < Sommet.Count)
                {
                    Noeud y = DFS.Pop();
                    y.Couleur = Color.Red;
                    //Console.WriteLine($"Fin d'exploration de : {y.Sommet}");
                    if (Succ[y.Sommet].Count > 0)
                    {
                        foreach (int voisinage in Succ[y.Sommet])
                        {
                            if (Succ.TryGetValue(voisinage, out List<int>? value))
                            {
                                if (Succ[voisinage].Count > 0)
                                {

                                    Noeud voisin = new Noeud(voisinage);

                                    for (int i = 0; i < Noeud_v.Count; i++)
                                    {

                                        if (!Noeud_v[i].Egale(voisin, Noeud_v[i]) && voisin.Couleur == Color.Yellow  && Occurence(Noeud_v,depart) == 1) 
                                        {
                                            c = true;
                                            Cir = Cycle;

                                        }
                                        if (!Noeud_v[i].Egale(voisin, Noeud_v[i]) && Occurence(Noeud_v, voisin) < 1)
                                        {
                                            voisin.Couleur = Color.Yellow;
                                            //Console.WriteLine($"Debut d'exploration de  : {voisin.Sommet}");
                                            Noeud_v.Add(voisin);
                                            //Console.WriteLine($"Noeud :{voisin.Sommet} , visité");
                                            Cycle.Add((voisin.Sommet));
                                            DFS.Push(voisin);
                                            //Console.WriteLine($"Noeud : {voisin.Sommet}, rajouté dans la pile");
                                            voisin.Pred[voisin.Sommet] = y.Sommet;


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
                
                Console.WriteLine($" {ex.Source} , \n{ex.Message} ");
            }
            return (c,Cir);

        }
        /// <summary>
        /// Affichage d'un circuit(cycle)
        /// </summary>
        /// <param name="C"></param>
        public void Affichage_Ordre_Circuit((bool,List<int> Cycle)C)
        {
            string orden = "";
            foreach (int s in C.Item2)
            {
                orden += Convert.ToString(s) + "|";
            }
            Console.WriteLine();
            Console.WriteLine($"L'ordre de visite : {orden}");
        }


        /// <summary>
        ///  Forte Connexité et CFC(Composantes Connexes) du graphe
        /// </summary>
        /// <param name="d"></param>
        /// <param name="a"></param>
        /// <returns></returns>

        
        public List<int[]> Composantes_Connexe()
        {
            
            List<int[]> CFC = new List<int[]>();
            List<(int,int)>Inverse_Arcs = AreteList;
            int Nombre_CFC = 0;
            for(int i = 0;i<AreteList.Count;i++)
            {
                (int,int) t = AreteList[i];
                Permutation(t.Item1,t.Item2);
            }

            for(int i = 0; i < Sommet.Count; i++)
            {
                Noeud n = new Noeud(Sommet[i]);
                DFS(n);
                //Tri_Sommet_Date(Sommet);
                
            }

            return (CFC);





        }
        /// <summary>
        /// Connexite du graphe 
        /// </summary>
        /// <returns></returns>
        public bool Connexe()
        {
            bool connexe = false;
            int c = 0;
            List<int> Connexe = new List<int>();
            foreach( int s in Sommet)
            {
                Noeud n = new Noeud(s);
                Connexe = DFS(n);
                if(Sommet.Count == Connexe.Count)
                {
                    c++;
                }
            }
            if(c == 34)
            {
                connexe = true;
            }
            return connexe;

        }
            
        
        /// <summary>
        /// Existence d'un chemin entre deux Noeuds
        /// </summary>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        /// <returns></returns>
        


        static void Tri_Croissant(List<int> L)
        {
            static int Partionner(List<int> L, int fin, int debut, int pivot)
            {
                int resul;
                resul = L[pivot];
                L[pivot] = L[fin];
                L[debut] = resul;

                int j = debut;

                for (int i = debut; i < fin; i++)
                {
                    if (L[i] <= L[fin])
                    {
                        resul = L[j];
                        L[j] = L[i];
                        L[i] = resul;
                        j--;
                    }

                }
                resul = L[j];
                L[j] = L[debut];
                L[fin] = resul;

                return j;
            }
            static void Quick_Sort(List<int> L, int debut, int fin)
            {
                int pivot;
                if (debut < fin )
                {
                    pivot = (debut + fin) / 2;
                    pivot = Partionner(L, debut , fin, pivot);
                    Quick_Sort(L, debut, pivot - 1);
                    Quick_Sort(L, pivot + 1, fin);

                }
            }
        }

        static void Tri_Croissant(List<Noeud> L)
        {
            static int Partionner(List<Noeud> L, int fin, int debut, int pivot)
            {
                
                Noeud resul = new Noeud();
                resul = L[pivot];
                L[pivot] = L[fin];
                L[debut] = resul;

                int j = debut;

                for (int i = debut; i < fin; i++)
                {
                    if (L[i].Egale(L[debut], L[i]) || L[i].Inferieur(L[i], L[debut]))
                    {
                        resul = L[j];
                        L[j] = L[i];
                        L[i] = resul;
                        j--;
                    }

                }
                resul = L[j];
                L[j] = L[debut];
                L[fin] = resul;

                return j;
            }
            static void Quick_Sort(List<Noeud> L, int debut, int fin)
            {
                int pivot;
                if (debut < fin)
                {
                    pivot = (debut + fin) / 2;
                    pivot = Partionner(L, debut, fin, pivot);
                    Quick_Sort(L, debut, pivot - 1);
                    Quick_Sort(L, pivot + 1, fin);

                }
            }
        }


        static void Tri_Decroissant(List<int> L)
        {
            static int Partionner(List<int> L, int fin, int debut, int pivot)
            {
                int resul;
                resul = L[pivot];
                L[pivot] = L[debut];
                L[debut] = resul;

                int j = fin;

                for (int i = fin; i <= debut; i--)
                {
                    if (L[i] <= L[debut])
                    {
                        resul = L[j];
                        L[j] = L[i];
                        L[i] = resul;
                        j--;
                    }

                }
                resul = L[j];
                L[j] = L[debut];
                L[debut] = resul;

                return j;
            }
            static void Quick_Sort(List<int> L, int debut, int fin)
            {
                int pivot;
                if (debut < fin)
                {
                    pivot = (debut + fin) / 2;
                    pivot = Partionner(L, debut, fin, pivot);
                    Quick_Sort(L, debut, pivot - 1);
                    Quick_Sort(L, pivot + 1, fin);

                }
            }
        }

        static void Tri_Decroissant(List<Noeud> L)
        {
            static int Partionner(List<Noeud> L, int fin, int debut, int pivot)
            {
                Noeud resul;
                resul = L[pivot];
                L[pivot] = L[debut];
                L[debut] = resul;

                int j = fin;

                for (int i = fin; i <= debut; i--)
                {
                    if (L[i].Egale(L[debut], L[i]) || L[i].Inferieur(L[i], L[debut]))
                    {
                        resul = L[j];
                        L[j] = L[i];
                        L[i] = resul;
                        j--;
                    }

                }
                resul = L[j];
                L[j] = L[debut];
                L[debut] = resul;

                return j;
            }
            static void Quick_Sort(List<Noeud> L, int debut, int fin)
            {
                int pivot;
                if (debut < fin)
                {
                    pivot = (debut + fin) / 2;
                    pivot = Partionner(L, debut, fin, pivot);
                    Quick_Sort(L, debut, pivot - 1);
                    Quick_Sort(L, pivot + 1, fin);

                }
            }
        }


        /// <summary>
        /// Listes des Noeuds du Graphe(Sommet,Degre,Niveau)
        /// </summary>
        /// <returns></returns>
        public List<Noeud> N()
        {
            List<Noeud>noeud = new List<Noeud>(Sommet.Count);
            for(int  j = 0;  j < noeud.Count; j++) 
            {
                for(int i = 0; i < Sommet.Count;i++)
                {
                    noeud[j] = new Noeud(Sommet[i]);
                    noeud[j].Degre = noeud[j].Calcul_Degre();
                    
                }
            }
            return noeud;
        }
        
        
        public void Affichage_Date_Fin(List<DateTime> Date_2_Fin)
        {
            string orden = "";
            foreach (DateTime d in Date_2_Fin)
            {
                orden += Convert.ToString(d) + "|";
            }

            Console.WriteLine();
            Console.WriteLine($"La liste des dates de Fin : {orden}");
        }

        public void Affichage_Date_Decouverte(List<DateTime> Date_2_Decouverte)
        {
            string orden = "";
            foreach (DateTime d in Date_2_Decouverte)
            {
                orden += Convert.ToString(d) + "|";
            }

            Console.WriteLine();
            Console.WriteLine($"La liste des dates de Fin : {orden}");
        }


        /// <summary>
        /// Degre du graphe
        /// </summary>
        /// <returns></returns>
        public int Deg_Max()
        {
            int degre_maximum = 0;
            Tri_Decroissant(this.degre);
            //degre_maximum = this.degre.Max(); 
            return degre_maximum;
        }
        
        /// <summary>
        /// Occurence d'un noeud dans une liste de noeuds 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="n1"></param>
        /// <returns></returns>
        public int Occurence(List<Noeud> n , Noeud n1 )
        {
            int occ = 0 ;
            for(int i = 0; i < n.Count; i++)
            {
                if (n[i].Sommet == n1.Sommet)
                {
                    occ++;
                }
            }
            return occ;
        }
        /// <summary>
        /// Occurence du noeud de depart dans une liste de noeuds
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool Occ_Depart(List<Noeud> n)
        {
            int occ = 0;
            bool o = false;
            for (int i = 0; i < n.Count; i++)
            {
                if (n[0].Sommet == n[i].Sommet)
                {
                    occ++;
                    if(occ == 2)
                    {
                        return true;
                    }
                }
            }
            return o;
        }
    }
}
