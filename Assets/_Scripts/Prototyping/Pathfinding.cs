using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Pathfinding : MonoBehaviour
    {
        private Grid m_grid;
        private Stack<Node> m_path = new Stack<Node>();

        //void Start()
        //{
        //    m_grid = GetComponent<Grid>();
        //}

        public Stack<Node> GetPath(Grid grid, Node startNode, Node targetNode)
        {
            m_grid = grid;
            FindPath(startNode, targetNode);
            return m_path;
        }

        private void FindPath(Node startNode, Node targetNode)
        {
            Heap<Node> openSet = new Heap<Node>(m_grid.maxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    // Retrace steps to get the path using parent
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (Node neighbour in m_grid.GetNeighbours(currentNode))
                {
                    if (neighbour.nodeType == ENodeTypes.Wall || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        private void RetracePath(Node startNode, Node endNode)
        {
            //List<Node> path = new List<Node>();
            Stack<Node> path = new Stack<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                //path.Add(currentNode);
                path.Push(currentNode);
                currentNode = currentNode.parent;
            }

            //path.Reverse();

            //m_grid.path = path;
            m_path = new Stack<Node>();
            m_path = path;
        }

        private int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY) + (int)nodeA.nodeType + (int)nodeB.nodeType;
            return 14 * dstX + 10 * (dstY - dstX) + (int)nodeA.nodeType + (int)nodeB.nodeType;
        }

        //private Node startNode = null;
        //private Node endNode = null;

        //public void SetPathNode(Node node)
        //{
        //    if (startNode == null)
        //    {
        //        startNode = node;
        //        node.SetColour(Colors.Red);
        //    }
        //    else if (endNode == null)
        //    {
        //        endNode = node;
        //        FindPath(startNode, endNode);
        //        node.SetColour(Colors.Yellow);
        //    }
        //    else
        //    {
        //        startNode.ResetColour();
        //        endNode.ResetColour();
        //        startNode = node;
        //        endNode = null;
        //        node.SetColour(Colors.Red);
        //    }
        //}
    }
}
