using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	//Reference to the Grid2 script
	private Grid2 m_grid;


	//2D Vector
	public Vector2 m_position;





	// Use this for initialization
	void Start () {

		m_grid = FindObjectOfType<Grid2>(); 

	}
	
	// Update is called once per frame
	void Update () {
		
		//Player Movement

		//Up
		if (Input.GetKeyDown (KeyCode.W)) {
			m_position.y = m_position.y + 1;
		}
	

		//Down
		if (Input.GetKeyDown (KeyCode.S)) {
			m_position.y = m_position.y - 1;
		}

		//Left
		if (Input.GetKeyDown (KeyCode.A)) {
			m_position.x = m_position.x - 1;
		}

		//Right
		if (Input.GetKeyDown (KeyCode.D)) {
			m_position.x = m_position.x + 1;
		}

		Move ();
	}


	//
	private void Move()
	{

		if(m_grid.getPosAt((int)m_position.x, (int) m_position.y).z != 1)
		{
			transform.position = m_grid.getPosAt((int)m_position.x, (int) m_position.y);
		}
	}





}



