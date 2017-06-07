using UnityEngine;

namespace AI
{
    public enum ENodeTypes
    {               // Notation in map files
        None,       // n
        Exit,       // e
        Path,       // p
        Ground,     // g
        Water,      // w
        Hill,       // h
        Tree,       // t
        Building,   // b
        Wall,       // x
        TypeCount
    }

    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Node : MonoBehaviour, IHeapItem<Node>
    {
        // Grid variables
        public ENodeTypes nodeType;
        public Vector3 worldPosition { get; private set; }
        public int gridX { get; private set; }
        public int gridY { get; private set; }

        // Visuals
        private string m_fileName;
        private string m_spriteNum;
        private Sprite m_sprite;

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
                case ENodeTypes.Path:
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

        //Mouse hover
        void OnMouseOver()
        {
            SetColour(Colors.Purple);

        }

        //Mouse on exit
        void OnMouseExit()
        {
            ResetColour();
        }


        void OnMouseDown()
        {
            //FindObjectOfType<Pathfinding>().SetPathNode(this);
            FindObjectOfType<TestPathReceiver>().SetPathNode(this);

            

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
            //Color newColor = colour;
            //newColor.a = 0.5f;
            //m_spriteRenderer.color += newColor;
            m_spriteRenderer.color = colour;
        }

        public void ResetColour()
        {
            m_spriteRenderer.color = Colors.White;
            //switch (nodeType)
            //{
            //    case ENodeTypes.Path:
            //        m_spriteRenderer.color = Colors.White;
            //        break;
            //    case ENodeTypes.Water:
            //        m_spriteRenderer.color = Colors.DodgerBlue;
            //        break;
            //    case ENodeTypes.Wall:
            //        m_spriteRenderer.color = Colors.DarkGray;
            //        break;
            //    case ENodeTypes.None:
            //    default:
            //        m_spriteRenderer.color = Colors.NavajoWhite;
            //        break;
            //}
        }

        public void SetSprite(string fileName, string tileNum)
        {
            m_fileName = fileName;
            m_spriteNum = tileNum;

            Sprite[] spriteAll = Resources.LoadAll<Sprite>("MapTiles/" + m_fileName);
            m_sprite = spriteAll[int.Parse(tileNum)];

            m_spriteRenderer.sprite = m_sprite;

            Destroy(GetComponent<BoxCollider2D>());
            this.gameObject.AddComponent<BoxCollider2D>();
        }
    }
}
