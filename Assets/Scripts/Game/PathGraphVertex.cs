using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;

namespace Game
{
    public class PathGraphVertex
    {
        private Vector2 position;

        private PathGraphVertex top = null;
        
        private PathGraphVertex bottom = null;
        
        private PathGraphVertex left = null ;
        
        private PathGraphVertex right = null;
        
        
        public PathGraphVertex(Vector2 point)
        {
            position = point;
        }

        public void intersectWith(Line line)
        {
            if (line.myVertex1.position.x == position.x)
            {
                if (line.myVertex1.position.y < position.y)
                {
                    line.myVertex1.top = this;
                    line.myVertex2.bottom = this;
                    top = line.myVertex2;
                    bottom = line.myVertex1;
                }
                else
                {
                    line.myVertex2.top = this;
                    line.myVertex1.bottom = this;
                    top = line.myVertex1;
                    bottom = line.myVertex2;
                }
            }
            else
            {
                if (line.myVertex1.position.x < position.x)
                {
                    line.myVertex1.left = this;
                    line.myVertex2.right = this;
                    left = line.myVertex2;
                    right = line.myVertex1;
                }
                else
                {
                    line.myVertex2.left = this;
                    line.myVertex1.right = this;
                    left = line.myVertex1;
                    right = line.myVertex2;
                }
            }
        }
        
       public void addNeighbour(PathGraphVertex neighbour)
        {
            if (neighbour.position.x == position.x)
            {
                if (neighbour.position.y < position.y)
                {
                    bottom = neighbour;
                    neighbour.top = this;
                }
                else
                {
                    top = neighbour;
                    neighbour.bottom = this;
                }
            }
            else
            {
                if (neighbour.position.x < position.x)
                {
                    left = neighbour;
                    neighbour.right = this;
                }
                else
                {
                    right = neighbour;
                    neighbour.left = this;
                }
            }
        }
        
    }
}