//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using PriorityQueues;
//using System.Diagnostics.CodeAnalysis;

//namespace Rogue2.Helpers.Pathfinding
//{
//    static class PathfindingAlgorithm
//    {
//        public delegate int CalculateGValue(Vector2 position);
//        public static Stack<Vector2> GetPath(Vector2 startPosition, Vector2 endPosition, IPathfindingData pathfindingData, int maxIterations = 9999)
//        {
//            IPriorityQueue<Node, int> openList = new BinaryHeap<Node, int>(PriorityQueueType.Minimum);
//            Node startingNode = new Node(startPosition, 0, GetDistance(startPosition, endPosition), null);
//            openList.Enqueue(startingNode, startingNode.f);

//            while (openList.Count != 0)
//            {
//                Node currentNode = openList.Dequeue();
//                currentNode.closed = true;

//                if (currentNode.position == endPosition)
//                {
//                    break;
//                }

//                List<Vector2> adjacentPositions = new List<Vector2>()
//                {
//                    new Vector2(currentNode.position.X - 1, currentNode.position.Y),
//                    new Vector2(currentNode.position.X + 1, currentNode.position.Y),
//                    new Vector2(currentNode.position.X, currentNode.position.Y - 1),
//                    new Vector2(currentNode.position.X, currentNode.position.Y + 1)
//                };

//                foreach (var adjacentPosition in adjacentPositions)
//                {
//                    if (closedList.Contains))
//                    {
//                        continue;
//                    }

//                    int gvalue = currentNode.g;
//                    gvalue += pathfindingData.CalculateWeightValue(adjacentPosition);

//                    if (!openList.Any(x => x.position == adjacentPosition))
//                    {
//                        Node newNode = new Node(adjacentPosition, gvalue, GetDistance(adjacentPosition, endPosition), currentNode);
//                        openList.Enqueue(newNode, newNode.f);
//                    }

//                    else
//                    {
//                        Node node = openList.First(x => x.position == adjacentPosition);

//                        if (gvalue + node.h < node.f)
//                        {
//                            node.g = gvalue;
//                            node.f = node.g + node.h;
//                            node.parent = currentNode;
//                        }
//                    }
//                }
//            }

//            return Traceback(endPosition, closedList);
//        }
//        private static Stack<Vector2> Traceback(Vector2 endPosition, HashSet<Node> closedList)
//        {
//            Node traceback = closedList.FirstOrDefault(x => x.position == endPosition);
//            Stack<Vector2> path = new Stack<Vector2>();
//            while (traceback != null)
//            {
//                path.Push(traceback.position);
//                traceback = traceback.parent;
//            }

//            return path;
//        }
//        public static int GetDistance(Vector2 vector1, Vector2 vector2)
//        {
//            return (int)(Math.Abs(vector1.X - vector2.X) + Math.Abs(vector1.Y - vector2.Y));
//        }
//        class Node
//        {
//            public Vector2 position;

//            public int f;
//            public int g;
//            public int h;

//            public Node parent;

//            public bool closed = false;

//            public Node(Vector2 position, int g, int h, Node parent)
//            {
//                this.position = position;
//                this.g = g;
//                this.h = h;
//                this.f = g + h;

//                this.parent = parent;
//            }

//            public Node(int x, int y, int g, int h, Node parent)
//            {
//                this.position.X = x;
//                this.position.Y = y;
//                this.g = g;
//                this.h = h;
//                this.f = g + h;

//                this.parent = parent;


//            }
//        }
//    }
//}

