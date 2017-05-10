using UnityEngine;

namespace AI
{
    public enum ENodeTypes
    {
        Floor,
        Water,
        None,
        Wall,
        TypeCount
    }

    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Node : MonoBehaviour, IHeapItem<Node>
    {

        // Grid variables
        public ENodeTypes nodeType;
        public Vector3 worldPosition;
        public int gridX;
        public int gridY;

        // A star variables
        public int gCost;
        public int hCost;
        // Estimated total cost of path though n to goal
        public int fCost { get { return gCost + hCost + (int)nodeType; } }
        public Node parent;

        private int m_heapIndex;
        private SpriteRenderer m_spriteRenderer;

        public void SetDefaults(ENodeTypes _type, Vector3 _worldPosition, int _gridX, int _gridY)
        {
            nodeType = _type;
            worldPosition = _worldPosition;
            gridX = _gridX;
            gridY = _gridY;
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            switch (nodeType)
            {
                case ENodeTypes.Floor:
                    m_spriteRenderer.color = Colors.ForestGreen;
                    break;
                case ENodeTypes.Water:
                    m_spriteRenderer.color = Colors.DodgerBlue;
                    break;
                case ENodeTypes.Wall:
                    m_spriteRenderer.color = Colors.DarkGray;
                    break;
                case ENodeTypes.None:
                default:
                    m_spriteRenderer.color = Colors.NavajoWhite;
                    break;
            }

            //transform.localScale = transform.localScale * 0.2f;
        }

        void Update()
        {

        }

        void OnMouseDown()
        {
            FindObjectOfType<Pathfinding>().SetPathNode(this);
        }

        public int heapIndex { get { return m_heapIndex; } set { m_heapIndex = value; } }

        public int CompareTo(Node otherNode)
        {
            int compare = fCost.CompareTo(otherNode.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(otherNode.hCost);
            }
            return -compare;
        }

        public void SetColour(Color colour)
        {
            Color newColor = colour;
            newColor.a = 0.5f;
            m_spriteRenderer.color += newColor;
        }

        public void ResetColour()
        {
            switch (nodeType)
            {
                case ENodeTypes.Floor:
                    m_spriteRenderer.color = Colors.ForestGreen;
                    break;
                case ENodeTypes.Water:
                    m_spriteRenderer.color = Colors.DodgerBlue;
                    break;
                case ENodeTypes.Wall:
                    m_spriteRenderer.color = Colors.DarkGray;
                    break;
                case ENodeTypes.None:
                default:
                    m_spriteRenderer.color = Colors.NavajoWhite;
                    break;
            }
        }
    }
}
