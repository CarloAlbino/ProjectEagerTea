using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapData{
    public static Texture2D texture;
    public static Vector2 textureDimension;
    public static Vector2 mapDimensions;
    public static int currentSprite = 0;
    public static Vector2 currentSelectedCoords = Vector2.zero;

    public static string saveDestination;
    public static string mapName;

    public static int[,] tempMap;
    public static Vector2[,] tempMapCoords;
    private static string currentMap;

    private static int[] exitInts = { 17, 32 };
    private static int[] pathInts = { 114, 115, 116, 117, 118, 131, 132, 133, 135, 149, 150, 151, 152};
    private static int[] groundInts = { 0, 1, 2, 3, 18, 19, 20, 35, 36, 37, 157 };
    private static int[] waterInts = { 10, 11, 12, 13, 14, 15, 16, 27, 28, 30, 31, 32, 33, 44, 45, 46, 47, 48, 49, 50 };
    private static int[] hillInts = { 107, 108, 109, 124, 125, 126, 140, 141, 142, 143, 174 };
    private static int[] treeInts = { 4, 21, 55, 72 };
    private static int[] buildingInts = { 52, 53, 172, 188 };
    private static int[] wallInts = { 5, 6, 7, 8, 9, 22, 23, 24, 25, 26, 38, 39, 40, 41, 56, 57, 58, 59, 60, 62, 63, 64, 65, 66, 67,
                                73, 74, 75, 76, 77, 79, 81, 82, 84, 89, 90, 91, 92, 96, 97, 98, 99, 100, 101, 158, 159, 160, 161, 162,
                                171, 175, 176, 177, 178, 179, 189, 191, 192, 193, 194};

    public static void NewMap(int x, int y, Texture2D t, string name, string dest)
    {
        mapDimensions = new Vector2(x, y);
        texture = t;
        var newTextureSize = new Vector2(texture.width, texture.height);
        textureDimension = new Vector2(newTextureSize.x / 16, newTextureSize.y / 16);
        currentSprite = 0;
        currentSelectedCoords = Vector2.zero;
        tempMapCoords = new Vector2[x, y];
        tempMap = new int[x, y];
        currentMap = "";
        mapName = name;
        saveDestination = dest;
    }

    public static void SetTile(int x, int y)
    {
        tempMap[x, y] = currentSprite;
    }

    public static void ExportMap()
    {
        currentMap = "";
        currentMap += mapDimensions.x + "_" + mapDimensions.y + "_";
        currentMap += texture.name + "_\n";

        bool found = false;
        for(int y = 0; y < mapDimensions.x; y++)
        {
            for(int x = 0; x < mapDimensions.y; x++)
            {
                for(int i = 0; i < wallInts.Length; i++)
                {
                    if(tempMap[x, y] == wallInts[i])
                    {
                        currentMap += "x=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                for (int i = 0; i < buildingInts.Length; i++)
                {
                    if (tempMap[x, y] == buildingInts[i])
                    {
                        currentMap += "b=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                for (int i = 0; i < treeInts.Length; i++)
                {
                    if (tempMap[x, y] == treeInts[i])
                    {
                        currentMap += "t=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                for (int i = 0; i < hillInts.Length; i++)
                {
                    if (tempMap[x, y] == hillInts[i])
                    {
                        currentMap += "h=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                for (int i = 0; i < waterInts.Length; i++)
                {
                    if (tempMap[x, y] == waterInts[i])
                    {
                        currentMap += "w=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                for (int i = 0; i < groundInts.Length; i++)
                {
                    if (tempMap[x, y] == groundInts[i])
                    {
                        currentMap += "g=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                for (int i = 0; i < pathInts.Length; i++)
                {
                    if (tempMap[x, y] == pathInts[i])
                    {
                        currentMap += "p=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                for (int i = 0; i < exitInts.Length; i++)
                {
                    if (tempMap[x, y] == exitInts[i])
                    {
                        currentMap += "e=" + tempMap[x, y];
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    currentMap += "g=0";
                }
                currentMap += ",";
            }
            currentMap += "\n";
        }
        currentMap = currentMap.Substring(0, currentMap.Length - 2);
        Debug.Log(currentMap);
        // Put export here
        System.IO.File.WriteAllText("Assets/" + saveDestination + "/" + mapName + ".txt", currentMap);
    }

    public static void AddNewCoords(int x, int y, Vector2 coord)
    {
        tempMapCoords[x, y] = new Vector2(coord.x, (int)Mathf.Abs(coord.y - textureDimension.y + 1));
    }

}
