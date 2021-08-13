using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int seed, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000,100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float[,] noiseMap = new float[mapWidth, mapHeight];
        if(scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeigth = float.MinValue;
        float minNoiseHeigth = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeigth = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitute = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
               
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeigth) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) *2 -1;
                    noiseHeight += perlinValue * amplitute;

                    amplitute *= persistance;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeigth)
                {
                    maxNoiseHeigth = noiseHeight;
                }else if (noiseHeight < minNoiseHeigth)
                {
                    minNoiseHeigth = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeigth, maxNoiseHeigth, noiseMap[x, y]);
            }
        }
         return noiseMap;
    }




}
