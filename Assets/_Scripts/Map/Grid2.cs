using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2 : MonoBehaviour {


    //Player referenes
    public GameObject player;

    //Instance of the grid (only 1)
    public static Grid2 instance;

    //Grass prefab Reference
    public GameObject grassPrefab;

    //Reference to the Array 
    public GameObject[,] grassTiles;

    //Size of the map(2D vector)
    public Vector2 mapSize;

    //Outline percentage
    public float outlinePercent;
    
    // Use this for initialization
	void Start () {

        //Call Generate Grid function
        genGrid();        
	}
	

    void Awake()
    {
        //Instance of "this" grid class on Awake
        instance = this;

    }


    // Update is called once per frame
	void Update () {
      
	}



    //Generate the Grid
    public void genGrid()
    {
        //Create the new Gameobject
        grassTiles = new GameObject[10,10];

        //Tiles for the X axis
        for(int x = 0; x < mapSize.x; x++)
        {
            //Tiles for T axis
            for (int y = 0; y < mapSize.y; y++)
            {
                //Position of the tile when spawned into the world
                // with the most of the edge of tile
                Vector3 tilePos = new Vector3(-mapSize.x / 2 + 0.5f + x, -mapSize.y / 2 + 0.5f + y, 0);

                //Setting the GrassPrefab and Pos as an gameobject to instantiate
                GameObject go = GameObject.Instantiate(grassPrefab, tilePos, Quaternion.Euler(Vector3.zero)) as GameObject;

                //Setting grassTiles as the Gameobject to Instantiate
                grassTiles [x,y] = go;

               


            }
         
        }
             

		spawnPlayer ();

    }




    //Spawn Player in Grid
    public void spawnPlayer()
    {
        
		//Instantiate(player,grassTiles[4,5].transform.position, Quaternion.identity);

		//The player GameObject
		GameObject p = Instantiate(player,grassTiles[4,5].transform.position, Quaternion.identity);
		p.GetComponent<PlayerMovement>().m_position = new Vector2 (4, 5);
       

    }

    
    



	//Method for getting world position
	public Vector3 getPosAt(int x, int y)
	{
		
		if (x > -1 && y > -1 && x < grassTiles.GetLength (0) && y < grassTiles.GetLength (1))
			return grassTiles [x, y].transform.position;
		else
			return Vector3.forward;

	}






}
