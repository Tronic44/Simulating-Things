using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseMap,ColourMap };
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeigth;

    [Range(2, 100)]
    public float noiseScale;

    [Range(1, 10)]
    public int octaves;
    [Range(0,1)]
    public float persistance;
    [Range(1, 2)]
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public TerrainType[] regions;
    
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeigth, noiseScale, seed ,octaves,persistance,lacunarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeigth];

        for( int y = 0; y < mapHeigth; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeigth = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeigth <= regions[i].heigth)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
            
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeigthMap(noiseMap));
        } else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeigth));
        }


    }
    private void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if(mapHeigth < 1)
        {
            mapHeigth = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float heigth;
    public Color colour;
}