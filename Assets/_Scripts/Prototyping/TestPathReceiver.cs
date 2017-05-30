using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class TestPathReceiver : MonoBehaviour {

    private Pathfinding pathfinding = null; // reference to pathfinding script and setting it to null
    private Stack<Node> path;

    private Grid m_grid;   
    private Node startNode = null; //startNode and endNode set to null at the start
    private Node endNode = null;

    float counter = 0;
    Node prevNode = null;
    private GameObject player;

    void Start ()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
	}

    void Update()
    {
        if (path != null)
        {
            if (path.Count > 0) //If the path count is > than "0" colour of the path will change.
            {
                foreach (Node n in path)
                {
                    n.SetColour(Colors.RosyBrown); //Setting the colour of the path
                }

                if (counter >= 0.4f)
                {
                    counter = 0; //Set the counter to 0
                    if (prevNode != null)
                    {
                        prevNode.ResetColour(); //Colour gets reset
                    }

                    prevNode = path.Pop();

                    prevNode.SetColour(Colors.Orange);
                    
                    //Set the players position to the previous node(first thing)
                    player.transform.position = prevNode.transform.position;
                }
                counter += Time.deltaTime;
            }
        }
    }

    //Set PathNode function
    public void SetPathNode(Node node)
    {
        if (startNode == null)
        {
            startNode = node;
            m_grid = startNode.GetComponentInParent<Grid>();
            node.SetColour(Colors.Red); //Starting node gets set to Red
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


    //Set players position function
    public void setPlayer(GameObject p)
    {
        player = p;
    }


}
