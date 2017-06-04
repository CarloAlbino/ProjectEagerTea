using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapCreator : EditorWindow
{
    private enum EnvironmentType
    {
        Forest,
        Snow,
        Desert
    }

    [Range(4, 32)]
    private int width = 4;
    [Range(4, 32)]
    private int height = 4;

    private EnvironmentType environmentType = EnvironmentType.Forest;

    private string saveFolder = "Resources/Maps";
    private string mapName = "Map";


    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/MapCreator")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MapCreator));
    }

    void OnGUI()
    {
        GUILayout.Label("Map Settings", EditorStyles.boldLabel);
        width = EditorGUILayout.IntSlider("Map Width", width, 4, 32);
        height = EditorGUILayout.IntSlider("Map Height", height, 4, 32);
        environmentType = (EnvironmentType)EditorGUILayout.EnumPopup("Environment Type", environmentType);
        saveFolder = EditorGUILayout.TextField("Save Folder", saveFolder);
        mapName = EditorGUILayout.TextField("Map Name", mapName);

        if (GUILayout.Button("Create"))
        {
            Debug.Log("Create a map");
            MapData.NewMap(width, height, Resources.Load<Texture2D>("Editor/" + environmentType.ToString()), mapName, saveFolder);

            string map = "";
            for (int y = 0; y < MapData.tempMap.GetLength(1); y++)
            {
                for (int x = 0; x < MapData.tempMap.GetLength(0); x++)
                {
                    map += MapData.tempMap[x, y] + " ";
                }
                map += "\n";
            }
            //Debug.Log(map);
            EditorWindow.GetWindow(typeof(MapEditor));
            //EditorWindow.GetWindow(typeof(MapDisplay));
            Close();
        }
    }
}

public class MapEditor : EditorWindow
{
    public enum Scale
    {
        x1, x2, x3, x4, x5
    }

    private Scale scale;    // Scale to display the texture

    private Vector2 currentSelection = Vector2.zero;    // Currently selected tile
    private Vector2 pickerHover = Vector2.zero;      // Position of tile the mouse is over picker

    // For scroll bars
    public Vector2 scrollPosition = Vector2.zero;

    private Vector2 mapViewOffset = Vector2.zero;
    private Vector2 mapHover = Vector2.zero;      // Position of tile the mouse is over map

    void OnGUI()
    {
        var texture2D = MapData.texture;    // Get the map name to load

        if(texture2D != null){
            #region GUI Display
            // Scale the texture view based on the selected choice from the popup
            scale = (Scale)EditorGUILayout.EnumPopup("Zoom", scale);
            var newScale = ((int)scale) + 1;
            var newTextureSize = new Vector2(texture2D.width, texture2D.height) * newScale;
            var offset = new Vector2(10, 65);
            mapViewOffset = new Vector2(10, newTextureSize.y + 85);

            // Export Button
            if (GUILayout.Button("Save Map"))
            {
                MapData.ExportMap();
                Close();
            }

            // Manages the scroll bar if needed
            EditorGUILayout.LabelField("Tile Picker");
            var viewPort = new Rect(0, 0, position.width - 5, position.height - 5);
            var contentSize = new Rect(0, 0, newTextureSize.x + offset.x, newTextureSize.y + offset.y);
            scrollPosition = GUI.BeginScrollView(viewPort, scrollPosition, contentSize);
            GUI.DrawTexture(new Rect(offset.x, offset.y, newTextureSize.x, newTextureSize.y), texture2D);

            // Size of a tile
            Vector2 tile = new Vector2(15, 15) * newScale;

            tile.x += newScale;
            tile.y += newScale;

            /////////////////
            // Picker view //
            /////////////////

            // Set the dimensions of the texture grid
            var grid = new Vector2(newTextureSize.x / tile.x, newTextureSize.y / tile.y);

            // Set the position of the selected tile and where the mouse is hovering
            var selectionPos = new Vector2(tile.x * currentSelection.x + offset.x, tile.y * currentSelection.y + offset.y);
            pickerHover = new Vector2(tile.x * pickerHover.x + offset.x, tile.y * pickerHover.y + offset.y);

            // Setup the box texture and it's style
            var boxTex = new Texture2D(1, 1);
            boxTex.SetPixel(0, 0, Colors.Aqua);
            boxTex.Apply();
            var style = new GUIStyle(GUI.skin.customStyles[0]);
            style.normal.background = boxTex;

            // Render the selected box
            GUI.Box(new Rect(selectionPos.x, selectionPos.y, tile.x, tile.y), "", style);

            // Change the box texture properties
            boxTex.SetPixel(0, 0, Colors.Purple * 0.5f);
            boxTex.Apply();
            style.normal.background = boxTex;

            // Render the hover box with the modified properties
            GUI.Box(new Rect(pickerHover.x, pickerHover.y, tile.x, tile.y), "", style);

            //////////////
            // Map view //
            //////////////

            // Label
            EditorGUI.LabelField(new Rect(0, mapViewOffset.y - 20, 50, 25), "Map");

            // Set the dimensions of the texture grid
            var map = new Vector2(MapData.mapDimensions.x, MapData.mapDimensions.y);

            // For map view hover
            mapHover = new Vector2(tile.x * mapHover.x + mapViewOffset.x, tile.y * mapHover.y + mapViewOffset.y);

            // Draw map grid
            for (int y = 0; y < map.y; y++)
            {
                for (int x = 0; x < map.x; x++)
                {
                    //GUI.DrawTextureWithTexCoords(new Rect(x * tile.x + mapViewOffset.x, y * tile.y + mapViewOffset.y, tile.x, tile.y), texture2D,
                    //                               new Rect(((int)(MapData.tempMap[x, y] / MapData.textureDimension.x) + 1) * 16, ((int)(MapData.tempMap[x, y] % MapData.textureDimension.y) + 1) * 16, 16, 16));
                    //GUI.DrawTextureWithTexCoords(new Rect(x * tile.x + mapViewOffset.x, y * tile.y + mapViewOffset.y, tile.x, tile.y), texture2D,
                    //                               new Rect(MapData.currentSelectedCoords.x * 16 + 1, MapData.currentSelectedCoords.y * 16 + 1, 16, 16));
                    GUI.DrawTextureWithTexCoords(new Rect(x * tile.x + mapViewOffset.x, y * tile.y + mapViewOffset.y, tile.x, tile.y), texture2D,
                                                  new Rect(35, 35, 16, 16));
                }
            }
            #endregion GUI Display

            #region GUI Input
            // Get the current GUI event and the mouse position
            var cEvent = Event.current;
            Vector2 mousePos = new Vector2(cEvent.mousePosition.x - offset.x, cEvent.mousePosition.y - offset.y);

            if (mousePos.y < mapViewOffset.y - 62 * newScale)
            {
                // Snap the position to the grid
                pickerHover.x = Mathf.Floor((mousePos.x + scrollPosition.x) / tile.x);
                pickerHover.y = Mathf.Floor((mousePos.y + scrollPosition.y) / tile.y);

                // Make sure the selection is within bounds
                if (pickerHover.x > grid.x - 1)
                    pickerHover.x = grid.x - 1;
                else if (pickerHover.x < 0)
                    pickerHover.x = 0;

                if (pickerHover.y > grid.y - 1)
                    pickerHover.y = grid.y - 1;
                else if (pickerHover.y < 0)
                    pickerHover.y = 0;

                // Mouse click in select area
                if (cEvent.type == EventType.mouseDown && cEvent.button == 0 && mousePos.y < offset.y + newTextureSize.y)
                {
                    currentSelection = pickerHover;

                    // Set the currently selected sprite accordingly
                    MapData.currentSprite = (int)(currentSelection.y * grid.x + currentSelection.x);
                    MapData.currentSelectedCoords = currentSelection;
                    Debug.Log("X: " + MapData.currentSelectedCoords.x * 16 + " Y: " + MapData.currentSelectedCoords.y * 16);
                }
            }
            else
            {
                // Re-calculate mouse position
                mousePos = new Vector2(cEvent.mousePosition.x - mapViewOffset.x, cEvent.mousePosition.y - mapViewOffset.y);

                // Snap the position to the grid
                mapHover.x = Mathf.Floor((mousePos.x + scrollPosition.x) / tile.x);
                mapHover.y = Mathf.Floor((mousePos.y + scrollPosition.y) / tile.y);

                // Make sure the selection is within bounds
                if (mapHover.x > map.x - 1)
                    mapHover.x = map.x - 1;
                else if (mapHover.x < 0)
                    mapHover.x = 0;

                if (mapHover.y > map.y - 1)
                    mapHover.y = map.y - 1;
                else if (mapHover.y < 0)
                    mapHover.y = 0;

                // Draw Box
                GUI.Box(new Rect(mapHover.x * tile.x + mapViewOffset.x, mapHover.y * tile.y + mapViewOffset.y, tile.x, tile.y), "", style);

                // Mouse click in map area
                if (cEvent.type == EventType.mouseDown && cEvent.button == 0)
                {
                    // Set new tile
                    MapData.SetTile((int)mapHover.x, (int)mapHover.y);
                }
            }
            #endregion GUI Input

            // End Scroll View
            GUI.EndScrollView();

            // Rerender
            Repaint();
        }
    }
}
