using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class TestPathReceiver : MonoBehaviour {

    private Pathfinding pathfinding = null;
    private Stack<Node> path;

    private Grid m_grid;
    private Node startNode = null;
    private Node endNode = null;

    float counter = 0;
    Node prevNode = null;

    void Start ()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
	}

    void Update()
    {
        if (path != null)
        {
            if (path.Count > 0)
            {
                foreach (Node n in path)
                {
                    n.SetColour(Colors.RosyBrown);
                }

                if (counter >= 0.4f)
                {
                    counter = 0;
                    if (prevNode != null)
                    {
                        prevNode.ResetColour();
                    }

                    prevNode = path.Pop();
                    prevNode.SetColour(Colors.Orange);
                }
                counter += Time.deltaTime;
            }
        }
    }

    public void SetPathNode(Node node)
    {
        if (startNode == null)
        {
            startNode = node;
            m_grid = startNode.GetComponentInParent<Grid>();
            node.SetColour(Colors.Red);
        }
        else if (endNode == null)
        {
            endNode = node;
            path = pathfinding.GetPath(m_grid, startNode, endNode); // Asks for a path here (grid, start and end node are passd in)
            node.SetColour(Colors.Yellow);
        }
        else
        {
            startNode.ResetColour();
            endNode.ResetColour();
            startNode = node;
            endNode = null;
            node.SetColour(Colors.Red);
        }
    }
}
