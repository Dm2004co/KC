using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SkiaSharp;
//using SkiaSharp.Views.Desktop;
namespace KC
{
    internal class Dessin
    {
      static Graphe g = new Graphe();
      static Random p = new Random();
        public Dessin ()
        {
            Visualisation();
        }
        static void Visualisation()
        {
            int longueur = 2000;
            int largeur = 2000;
            SKBitmap bitmap = new SKBitmap(largeur, longueur);
            SKCanvas canvas = new SKCanvas(bitmap);

            canvas.Clear(SKColors.White);
            SKPoint[] sommets = new SKPoint[g.Sommet.Count+1];
            int nb_Sommets = g.Sommet.Count;
            
           for(int i = 0;  i < nb_Sommets; i++)
            {
               
                sommets[i].X = p.Next(0,largeur - 10);
                sommets[i].Y = p.Next(0,(longueur + largeur)/4 + 750);
            }
          
            Tuple<int, int>[] arcs =  new Tuple<int, int>[g.AreteList.Count];
            int nb_arc = g.AreteList.Count;
               for(int i = 0; i < nb_arc; i++)
                {
                    arcs[i] = Tuple.Create(g.AreteList[i].Item1,g.AreteList[i].Item2) ;
                }
            
            SKPaint paint = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2
            };
            foreach( var arc in g.AreteList)
            {
                canvas.DrawLine(sommets[arc.Item1], sommets[arc.Item2], paint);
            }
            paint.Style = SKPaintStyle.Fill;
            SKColor[] colors = new SKColor[nb_Sommets];
            Random r = new Random();

            for(int i = 0; i < colors.Length-1; i++)
            {
                if (colors[i] == colors[i+1])
                {
                    string s = r.Next(0x1000000).ToString("X6");
                    colors[i] = SKColor.Parse($"#{s}");
                }
                
            }
            for(int i  = 0;  i < nb_Sommets; i++)
            {
                paint.Color = colors[i];
                canvas.DrawCircle(sommets[i], 10, paint);
            }
            
            

            SKImage image = SKImage.FromBitmap(bitmap) ;
            using (FileStream stream = File.OpenWrite("C:\\Users\\Darrell Messi\\source\\repos\\KC\\graphe.bmp"))
            {
                
                SKData data = image.Encode();
                data.SaveTo(stream);
                
            };
               
            
            
            
            
        }
        void Optimisation()
        {
                    /*-Disposition circulaire 
                - Attribution des niveaux (check)
                - Red des croisements pour les sommets au niveau 1 
                - Les sommets d'un même niveau sont sur le meme cercle .
                */

        }
        void Sugiyama()
        {
            /*- Suppression des cycles/circuits 
- Attribution des niveaux (check)
- Red des croisements pour les sommets au niveau 1 
- Les sommets d'un même niveau ont la même coordonnée y.
Les coordonnées x sont déterminées en fonction de l'ordre des sommets et de l'espacement souhaité.
*/

        }



    }
}
