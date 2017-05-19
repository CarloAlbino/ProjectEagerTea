using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class playerTestClass : MonoBehaviour {


    public GameObject player;

    //Reference to the grid class
    private Grid t_grid;


    //Reference to Node script
    private Node m_node;

    //Coordinates on the grid 
    private Vector2 coordinates;

    
    //Range for how far the player can move
    public int m_range;

   
    
    // Note: get the world pos from the grid


    // Use this for initialization
	void Start () {

        //Reference to the Grid Class
        t_grid = FindObjectOfType<Grid>(); 
        m_node = FindObjectOfType<Node>();




    }

    // Update is called once per frame
    void Update () {
        SpawnPlayer();
	}






    //Spawn player in the world
    public void SpawnPlayer()
    {
        GameObject m_player = Instantiate(player, t_grid.GetNodeWorldPosition(1, 1), Quaternion.identity);


        
    }





    //Range for how far the player can move
    public void moveRange()
    {

    }


    //We want the "Path" to be highlighted from the "current point" all the way to the "end point"
    //Mouse over method

    //Highlight the current "Grid Space" to a different colour
    void OnMouseOver(Node node)
    {
        node.SetColour(Colors.Purple);

    }




}
