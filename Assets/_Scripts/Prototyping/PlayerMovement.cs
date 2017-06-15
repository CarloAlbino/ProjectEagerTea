using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;


public class PlayerMovement : MonoBehaviour {

    //Reference to the Grid2 script
	private Grid m_grid;
    //2D Vector
	public Vector2 m_position;


    //reference to Pathfinding and NodeScript
    private Pathfinding pathfinding = null; // reference to pathfinding script and setting it to null
    private Stack<Node> path;

    private Node startNode = null; //startNode and endNode set to null at the start
    private Node endNode = null;


    float counter = 0;
    Node prevNode = null;
    private GameObject player;

    // Use this for initialization
    void Start () {

		m_grid = FindObjectOfType<Grid>();
        pathfinding = FindObjectOfType<Pathfinding>();

    }
	

	// Update is called once per frame
	void Update () {

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



    public void SetPathNode(Node node)
    {
        //First click only
        if (startNode == null)
        {
            //Reference "this" class
            if (node == m_grid.NodeFromWorldPoint(this.transform.position))
            {

                //Set the start node as the position of the player
                startNode = node;
                m_grid = startNode.GetComponentInParent<Grid>();
                node.SetColour(Colors.Red); //Starting node gets set to Red


             }

            else if (endNode == null)
             {
                //Range //Lists (return a list of nodes)


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

            //----Work in Progress----
        }

        /*
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
        */
    }




    //The Range Function (How far the player can move)
    public void Range()
    {
        /*
         -We will Create a List here and we will use the 'Count'
         Property to get the number of elements in the list(The nodes).
        
        -If the amount of nodes are the Range limit, the player cannot move
        any further then that.
         
        - We will use the 'Capacity' Property to set the Players Range (How far the player can move)
        */

        //1. Declare the List
        List<Node> playerRange = new List<Node>();

        //2. Get the Capacity
              //code

        //3. Set the Capacity
              //Code
    }


   

    

}



