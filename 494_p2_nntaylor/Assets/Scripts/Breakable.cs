using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public void BreakThisBlock()
    {
        GameObject _floorTile = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Floor");
        Transform parentTransform = GetComponentInParent<Transform>();
        GameObject go = Instantiate(_floorTile, parentTransform);
        go.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
