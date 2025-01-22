using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TownGrid : MonoBehaviour
{
    #region Tilemap Mangement
    private static TileBase[] GetTileBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y];
        int index = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, z:0);
            array[index] = tilemap.GetTile(pos);
            index++;
        }
        return array;
    }
    private static void SetTileBase(BoundsInt area, TileBase tileBase, Tilemap tilemap)
    {
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y];
        FileTiles(tileArray, tileBase);
        tilemap.SetTilesBlock(area, tileArray); // Corrected to use BoundsInt and Tilemap
    }
    private static void FileTiles(TileBase[] arr, TileBase tileBase)
    {
        for (int i = 0; i < arr.Length;i++)
        {
            arr[i] = tileBase;
        }
    }
    public void ClearArea(BoundsInt area, Tilemap tilemap)
    {
        SetTileBase(area, null, tilemap);
    }
    #endregion
}