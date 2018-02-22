using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class LevelParser
{
    public static TileType[,] TextToTiles(string filePath)
    {
        Debug.Log(Application.dataPath + "/StreamingAssets");
        LinkedList<string> lines = new LinkedList<string>();
        using (StreamReader streamReader = new StreamReader(Application.dataPath + "/StreamingAssets/Levels/" + filePath))
        {
            while (!streamReader.EndOfStream)
            {
                string s = streamReader.ReadLine();
                lines.AddFirst(s);
            }
        }
        TileType[,] mapArray = new TileType[lines.ElementAt(0).Length, lines.Count];
        for (int i = 0; i < mapArray.GetLength(0); i++)
        {
            for (int j = 0; j < mapArray.GetLength(1); j++)
            {
                mapArray[i, j] = (TileType)(Char.GetNumericValue(lines.ElementAt(j)[i]));
            }
        }
        return mapArray;
    }
}
