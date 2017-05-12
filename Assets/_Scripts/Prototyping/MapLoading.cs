using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class MapLoading : MonoBehaviour {

    public string m_mapFileName;

    private AI.Grid m_grid;

	// Use this for initialization
	void Start () {
        m_grid = GetComponent<AI.Grid>();
        LoadMap(m_mapFileName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Load map code taken from http://answers.unity3d.com/questions/279750/loading-data-from-a-txt-file-c.html
    private bool LoadMap(string fileName)
    {
        string line;
        // Create a new StreamReader, tell it which file to read and what encoding the file
        // was saved as
        StreamReader theReader = new StreamReader(Application.dataPath + "/" + fileName, Encoding.Default);
        // Immediately clean up the reader after this block of code is done.
        // You generally use the "using" statement for potentially memory-intensive objects
        // instead of relying on garbage collection.
        // (Do not confuse this with the using directive for namespace at the 
        // beginning of a class!)
        using (theReader)
        {
            line = string.Join("", File.ReadAllLines(Application.dataPath + "/" + fileName));
            if(line != null)
            {
                string[] data = line.Split('_');
                if (data.Length > 0)
                {
                    BuildMap(data);
                }
                else
                {
                    Debug.LogWarning("Nothing in the map file.");
                    theReader.Close();
                    return false;
                }
            }
                // While there's lines left in the text file, do this:
            //    do
            //{
            //    line = theReader.ReadLine();
            //    // Do whatever you need to do with the text line, it's a string now
            //    // In this example, I split it into arguments based on comma
            //    // deliniators, then send that array to DoStuff()
            //    string[] data = line.Split('_');
            //    if (data.Length > 0)
            //    {
            //        BuildMap(data);
            //    }
            //    else
            //    {
            //        Debug.LogWarning("Nothing in the map file.");
            //        theReader.Close();
            //        return false;
            //    }
            //}
            //while (line != null);
            // Done reading, close the reader and return true to broadcast success    
            theReader.Close();
            return true;
        }
    }

    private void BuildMap(string[] data)
    {


        int width;
        int height;
        int.TryParse(data[0], out width);
        int.TryParse(data[1], out height);

        m_grid.CreateNewGrid(width, height);

        string[] mapData = data[2].Split(',');
        if (mapData.Length > 0)
        {
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    AI.ENodeTypes newType = AI.ENodeTypes.Floor;
                    int n = x * width + y;
                    switch (mapData[y * width + x])
                    {
                        case "f":
                            newType = AI.ENodeTypes.Floor;
                            break;
                        case "w":
                            newType = AI.ENodeTypes.Water;
                            break;
                        case "-":
                            newType = AI.ENodeTypes.Wall;
                            break;
                        default:
                            newType = AI.ENodeTypes.Floor;
                            break;
                    }

                    m_grid.SetGridTile(x, y, newType);
                }
            }
        }
    }
}
