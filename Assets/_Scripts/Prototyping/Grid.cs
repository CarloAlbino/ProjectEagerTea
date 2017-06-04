using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private Node nodeTilePrefab;
        [SerializeField]
        private float squareSize = 1.0f;
        [SerializeField]
        private Vector2 gridDimensions = new Vector2(100, 100);

        private Node[,] m_grid;
        private int gridWidth, gridHeight;

        //public List<Node> path;
        public Stack<Node> path;
        public int maxSize { get { return gridWidth * gridHeight; } }

        void Start()
        {

        }

        float counter = 0;
        Node prevNode = null;
        void Update()
        {
            if (path != null)
            {
                //if (path.Count > 0)
                //{
                //    //foreach(Node n in path)
                //    //{
                //    //    n.SetColour(Colors.Orange);
                //    //}

                //    if (counter >= 0.4f)
                //    {
                //        counter = 0;
                //        if (prevNode != null)
                //        {
                //            prevNode.ResetColour();
                //        }

                //        prevNode = path.Pop();
                //        prevNode.SetColour(Colors.Orange);
                //    }
                //    counter += Time.deltaTime;
                //}
            }
        }

        public void CreateNewGrid(int x, int y)
        {
            gridWidth = x;// Mathf.RoundToInt(x / squareSize);
            gridHeight = y;// Mathf.RoundToInt(y / squareSize);
            squareSize /= 2;
            gridDimensions = new Vector2(gridWidth * squareSize, gridHeight * squareSize);

            // Create the grid here
            CreateGrid();
        }

        public void SetGridTile(int x, int y, ENodeTypes type)
        {
            m_grid[x, y].nodeType = type;
            m_grid[x, y].ResetColour();
        }

        private void CreateGrid()
        {
            m_grid = new Node[gridWidth, gridHeight];
            Vector3 gridTopLeft = transform.position - Vector3.right * gridDimensions.x / 2 - Vector3.down * gridDimensions.y / 2;

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Vector3 newNodePos = gridTopLeft + Vector3.right * (x * squareSize) + Vector3.down * (y * squareSize);
                    ENodeTypes newType = ENodeTypes.Path;

                    m_grid[x, y] = Instantiate(nodeTilePrefab, newNodePos, transform.rotation);
                    m_grid[x, y].SetDefaults(newType, newNodePos, x, y);
                    m_grid[x, y].transform.parent = this.transform;
                }
            }
        }

        // Use this to get the world position of a node.
        public Vector3 GetNodeWorldPosition(int x, int y)
        {
            return m_grid[x, y].worldPosition;
        }

        public bool IsWalkable(int x, int y)
        {
            if (m_grid[x, y].nodeType == ENodeTypes.Path ||
                m_grid[x, y].nodeType == ENodeTypes.Water)
                return true;
            else
                return false;
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = ((worldPosition.x + worldPosition.x) / 2) / gridDimensions.x;
            float percentY = ((worldPosition.y + worldPosition.y) / 2) / gridDimensions.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridWidth - 1) * percentX);
            int y = Mathf.RoundToInt((gridHeight - 1) * percentY);

            return m_grid[x, y];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            /*
            // Getting all the neighbours including diagonal neighbours
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;
                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                    {
                        neighbours.Add(m_grid[checkX, checkY]);
                    }
                }
            }
            */

            // Get only vertical and horizontal neighbours
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if ((x == 0 && y == 0) || (Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1))
                        continue;
                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                    {
                        neighbours.Add(m_grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }
    }
}
