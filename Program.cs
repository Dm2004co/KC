// See https://aka.ms/new-console-template for more information
using KC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
Console.Clear();
Console.Title = "Les relations des membres d'une association de Karaté";
Console.WriteLine(value: Console.Title.ToUpper() + Environment.NewLine);
Graphe g = new Graphe();

    Console.WriteLine("Entrer  :\n 0 : Propriétés \n 1 : Algorithmes de Parcours \n 2 : Modes : (Matrices et Listes)  \n 3 : Coloration du graphe \n 4 : PCC et son implementation \n 5 : Visualisation du graphe ");
    int i = Convert.ToInt32(Console.ReadLine());
    switch (i)
    {
        case 0:
        Console.WriteLine("Entrer  :\n 0 : Connexité du graphe \n 1 : Detection de circuit  \n 2 : Orientation du graphe  \n 3 : Ponderation du graphe \n 4 : Ordre du graphe \n 5 : Taille du graphe");
        int c = Convert.ToInt32(Console.ReadLine());
        switch(c)
        {
            case 0:
                Console.WriteLine("Propriete(s) du graphe : ");//Facultatif
                                                               //g.Connexe();//Obligatoire
                Console.WriteLine("Connexité du graphe : \n 0 : Connexe \n 1 : Composantes Connexes  ");
                int d = Convert.ToInt32(Console.ReadLine());
                switch (d)
                {
                    case 0:
                        
                        Console.WriteLine($"\nLe graphe est connexe : {g.Connexe()}");
                        
                        break;
                    case 1:
                        Console.Write("");
                        break;
                }
                break;

            case 1:
                Console.WriteLine("Circuit : \n 0 : Existence \n 1 : Exemple  ");
                int p = Convert.ToInt32(Console.ReadLine());
                switch(p)
                {
                    case 0:
                            Existence();
                        void Existence()
                        {
                            Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                            Noeud n = new Noeud();
                            Console.WriteLine($"Existence d'au moins un cycle dans le graphe : {(g.Existence_Circuit(n).Item1)}");//Obligatoire
                        }
                        
                        
                        break;
                    case 1: Exemple();
                        void Exemple()
                        {
                            for(int i = 0; i < 33; i++)
                            {
                                Noeud n = new Noeud(g.Sommet[i]);
                                if((g.Existence_Circuit(n).Item1) == true)
                                {
                                    g.Affichage_Ordre_Circuit((g.Existence_Circuit(n)));
                                }
                                
                            }
                           Console.WriteLine("Il n'y a pas de circuit dans le graphe");
                        }
                        
                        
                        break ;
                }
                
                break ;

            case 2: Console.WriteLine($"Orientation du graphe (O)  : {g.Orientation_Arc} ");
                break;
            case 3: Console.WriteLine($"Pondération du graphe (W) : {g.Ponderation_Arc}");
                break;
            case 4: Console.WriteLine($"Ordre du graphe (V) : {g.Sommet.Count}");
                break;
            case 5: Console.WriteLine($"Taille du graphe (E) : {g.AreteList.Count}");
                break;

        }
            
        
            
            //g.Simple;
            //g.Complet;
            //g.Regulier;
            //g.Multigraphe;
            break;
        case 1:
        Console.WriteLine("Algorithme de parcours : BFS(0) ou  DFS(1) ");//Obligatoire
        int imput = Convert.ToInt32(Console.ReadLine());
        switch (imput)
        {
            case 0:
                Console.WriteLine("Choix de parcours : BFS \n 0 : BFS \n 1 : Chemin ");
                int bfs = Convert.ToInt32(Console.ReadLine());
                switch(bfs)
                {

                    case 0: Console.WriteLine("Selection  :  BFS ");
                            BFS();
                       
                        
                        void BFS()
                        {
                            Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                            Noeud n = new Noeud();
                            g.Affichage_Ordre_BFS(g.BFS(n).Item1);
                        }
                       

                        break;
                    case 1:
                        Console.WriteLine(" Selection  :  Chemin \n 0 : Existence de Chemin \n 1 : Affichage de Chemin ");
                        int chemin = Convert.ToInt32(Console.ReadLine());
                        switch(chemin)
                        {
                            case 0:
                                Console.WriteLine(" Selection  : Existence de Chemins ");
                                Existence_Chemin();
                                break;
                            case 1:
                                Console.WriteLine(" Selection  : Affichage de Chemin ");
                                Chemin();
                                break;
                            case 2:
                                Console.WriteLine(" Selection  : Nombre de Chemins ");
                                Nb_Chemins();
                                break;
                        }
                        void Existence_Chemin()
                        {
                            Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                            Noeud n = new Noeud();
                            Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                            Noeud m = new Noeud();
                            Console.WriteLine($"Existence d'au moins un chemin dans le graphe : {(g.Chemin(n,m).Item1)}");
                        }
                        void Chemin()
                        {
                            Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                            Noeud n = new Noeud();
                            Console.WriteLine(" Choissisez un noeud d'arrive entre 1 et 34 (compris) : ");
                            Noeud m = new Noeud();
                            g.Affichage_Ordre_BFS(g.Chemin(n, m).Item2);
                            Console.WriteLine();
                            g.Affichage_Ordre_BFS_Noeud(g.Chemin(n, m).Item3);
                        }
                        void Nb_Chemins()
                        {
                            Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                            Noeud depart = new Noeud();
                            Console.WriteLine(" Choissisez un noeud d'arrivee entre 1 et 34 (compris) : ");
                            Noeud arrivee = new Noeud();
                            Console.WriteLine("Choisssez la longueur de votre chemin");
                            int longueur = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine($"Nombre de chemins de {depart.Sommet} à {arrivee.Sommet} est  : {g.Nb_2_Chemin(depart,arrivee,longueur)}");
                        }
                        break;
                }
                    
                    break;
            case 1:
                    Console.WriteLine("Choix de parcours : DFS ");
                    Console.WriteLine("DFS : \n 0 : Ordre de visite \n 1 : Noeuds visités  ");
                    int dfs = Convert.ToInt32(Console.ReadLine());
                    switch (dfs)
                    {
                        case 0:
                                DFS();
                        void DFS()
                            {
                                Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                                Noeud n = new Noeud();
                                g.Affichage_Ordre_DFS(g.DFS(n));
                                
                            }
                           
                            break;

                        case 1:
                                DFS_Noeud();

                        void DFS_Noeud()
                            {
                                Console.WriteLine(" Choissisez un noeud de départ entre 1 et 34 (compris) : ");
                                Noeud n = new Noeud();
                                g.Affichage_Ordre_DFS_Noeud(g.DFS_Noeud(n));
                            }
                            
                            break;

                }
                break;
        }
            
            break;
        case 2:
            Console.WriteLine("Modes : Liste d'adjacence (0) , \n Matrice d'adjacence (1) , \n Matrice d'incidence (2) , \n Liste des Predecesseurs(3), \n Listes des Noeuds(4) , \n Listes des Poids(5)");//Obligatoire
            int choix = Convert.ToInt32(Console.ReadLine());
            switch (choix)
            {

                case 0:
               
                           foreach( var key in g.Succ.Keys)
                        {
                            foreach( int value in g.Succ[key])
                            {
                        
                                Console.WriteLine($"Succeseur(s) de {key} sont : {value} ");
                            }
                            Console.WriteLine();
                    
                    
                        }
                    break;
                case 1:
                        Console.WriteLine("Matrice d'Adjacence : ");
                        g.Afficher_Adjacence(g.Matrice_ADJ);
                        break;
                case 2: Console.WriteLine("Matrice d'incidence : ");
                        g.Afficher_Incidence(g.Matrice_Incidence);
                        break;
                case 3: Console.WriteLine("Liste des Predecesseurs ");

                        foreach (var key in g.Pred.Keys)
                        {
                            foreach (int value in g.Pred[key])
                            {

                                Console.WriteLine($"Predecesseur(s) de {key} sont : {value} ");
                            }
                            Console.WriteLine();


                        }
                break;
                
                case 4:
                        Console.WriteLine("Liste des Noeuds(sommet,degre,niveau) : ");
                break;
                case 5:
                        Console.WriteLine("Liste des Poids : ");
                break;

            

        }
            break;
        case 3: Coloration color = new Coloration();
                break;
        case 4:
        Console.WriteLine("PCC : Dijkstra (0) , Bellman-Ford (1) , Floyd-Marshall (2)");
        int selection = Convert.ToInt32(Console.ReadLine());
        switch(selection)
        {
            case 0: Console.WriteLine("Choix de PCC : Dijkstra");
                    Dijkstra();
                void Dijkstra()
                {

                }
                
                break;
                case 1: Console.WriteLine("Choix de PCC : Bellman-Ford");
                        Bellman_Ford();
                void Bellman_Ford()
                {

                }
                break;
                case 2: Console.WriteLine("Choix de PCC : Floyd");
                        Floyd();
                void Floyd()
                {

                }
                break;
               
        }
        break;
        case 5: Dessin dessin = new Dessin();
        break;

    
}
