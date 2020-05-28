using System;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace pokemongenerator
{   
    struct Pos{
    public int x;
    public int y;
}
    class AsciiSorter
    {      
          static void Main(string[] args)
        {
            int width = Int32.Parse(args[0]);
            int height = Int32.Parse(args[1]);
            int houses = Int32.Parse(args[2]);
            Map map = new Map (width,height);
            Random rnd = new Random();

            int sum_x = 0;
            int sum_y = 0;
            int moy_x = 0;
            int moy_y = 0;
            var path_positions = new List<Pos>();


            //Houses
            for (int i = 0 ; i < houses ; i ++)
            {
                int x_start  = rnd.Next(0,width-3); // random start for house
                int y_start  = rnd.Next(0,height-3);
                map.set_object(x_start+1,y_start+2,"#"); // add path in front of house
                sum_x += x_start+1 ; // to get middle path
                sum_y += y_start+2 ; // to get middle path
                path_positions.Add(new Pos(){ x =x_start+1 , y =y_start+2 });
                
                for (int x = x_start ; x < x_start+3 ; x ++)
                {
                    for (int y = y_start ; y < y_start+2 ; y ++)
                     {
                    map.set_object (x,y, "█");
                    }
                }
            }


            moy_x = (int)(sum_x/houses);
            moy_y = (int)(sum_y/houses);
            
            foreach (var coord in path_positions){
                var cur_x = coord.x;
                var cur_y = coord.y;
                while (true){
                    if (cur_x > moy_x & (map.get_object(cur_x-1,cur_y)==" " | map.get_object(cur_x-1,cur_y)=="#")){
                        cur_x = cur_x-1;
                    }
                    else if (cur_x < moy_x & (map.get_object(cur_x+1,cur_y)==" " | map.get_object(cur_x+1,cur_y)=="#")){
                        cur_x = cur_x+1;
                    }
                    else if (cur_y > moy_y & (map.get_object(cur_x,cur_y-1)==" " | map.get_object(cur_x,cur_y-1)=="#")){
                        cur_y = cur_y-1;
                    }
                    else if (cur_y < moy_y & (map.get_object(cur_x,cur_y+1)==" " | map.get_object(cur_x,cur_y+1)=="#")){
                        cur_y = cur_y+1;
                    }
                    else if (cur_y == moy_y & cur_y == moy_y){
                        break;
                    }
                    map.set_object(cur_x,cur_y,"#");
                    //map.display();
                    }
 
            }
            //map.set_object (moy_x,moy_y, "#");


            /*bool processing = true ;
            for( int i = 0 ; i < 100 ; i ++ )  //parcours la map 100
            {

                for (int x = 0 ; x < width ; x ++) //parcours chaque item de la map pour créer des chemins
                {
                    for (int y = 0 ; y < height ; y ++)
                    {
                        if (map.get_object(x,y)=="#" & map.number_of_neighbor(x,y,"#")< 2)
                        {
                            map.random_set_empty_neighbor(x,y,"#");

                        }
                    }

                }
            }*/

            /*for ( int i = 0 ; i < 100 ; i ++ )  //parcours la map 100 pour supprimer les chemin inutile. Ensuite il faut retracer et rechercher de nouveaux
            {

            
                for (int x = 2 ; x < 58 ; x ++) //parcours chaque item de la map pour créer des chemins
                {
                    for (int y = 2 ; y < 58 ; y ++)
                    {   
                        if (map.get_object(x,y)=="#" & map.number_of_neighbor(x,y,"#")== 1 &  map.number_of_neighbor(x,y,"█") == 0)
                        {
                            map.set_object(x,y," ");
                        }
                    }

                }
            }*/
            map.display();
        }

    }

    class Map
    {

        public  List<List<String>> Map_objects = new List<List<String>>();
        public int height;
        public int width;
        public Map (int h , int w) {
            height = h;
            width = w ;
            for (int y = 0 ; y< h ; y++)
            {
                Map_objects.Add(new List<string>(w));
                for (int x = 0 ; x< w ; x++)
                {
                    Map_objects[y].Add(" ");
                }
            }
        }

        public String get_object (int x , int y){
            
            if (x<0 | x>width-1 | y<0 | y>height-1  )
            {
                String empty  = " ";
                return empty;
            }
            
            return Map_objects[y][x];
        }

        public void set_object (int x , int y , String value){
            if (x<0 | x>width-1 | y<0 | y>height-1  )
            {
                Console.WriteLine("trying to change out of the map object");
            }
            else {
                Map_objects[y][x] = value;

            }
        }

        public int number_of_neighbor( int x, int y , String neighbor )
        {
            int count = 0 ;
            if (get_object(x+1 , y) == neighbor )  {
                count += 1 ;
            }
            if (get_object(x-1 , y) == neighbor) {
                count += 1 ;
            }
            if (get_object(x , y+1) == neighbor) {
                count += 1 ;
            }
            if (get_object(x , y-1) == neighbor) {
                count += 1 ;
            }        

            
            return count;
        }

       



        public void random_set_empty_neighbor (int x , int y, String value)
        {
            Random rnd = new Random();
            int count = 0 ;
            while (true){
                count += 1;
                int rand  = rnd.Next(1,5);
                
                int x_mod = -1 ;
                int y_mod = 0 ;
                
                if (rand==1) {
                    x_mod = 1 ;
                    y_mod = 0 ;
                }
                else if (rand==2) {
                    x_mod = 0 ;
                    y_mod = 1 ;
                }
                else if (rand==3) {
                    x_mod = -1 ;
                    y_mod = 0 ;
                }
                else if (rand==4) {
                    x_mod = 0 ;
                    y_mod = -1 ;
                }

                


                if (get_object(x+x_mod , y+y_mod) == " "  & number_of_neighbor(x+x_mod,y+y_mod,"#")< 2)
                {
                    set_object(x+x_mod , y+y_mod , value) ;
                    break ;
                }
                if (count > 100)
                {
                    break ;
                }
            }
            

        }

        public void display (){

            foreach (List<String> list_y in Map_objects)
            {
                String line = "";
                foreach (String value in list_y)
                {
                    line = line + value ;
                }
                Console.WriteLine(line);
            }
        }
    }
}
