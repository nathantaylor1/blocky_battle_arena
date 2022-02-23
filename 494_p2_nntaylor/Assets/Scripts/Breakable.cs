using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public void BreakThisBlock()
    {
        GameObject _floorTile = Resources.Load<GameObject>("PreFabs/Tiles/Tile_Floor");
        Transform parentTransform = GetComponentInParent<Transform>();
        
        GameObject go = Instantiate(_floorTile);
        go.transform.position = transform.position;
        
        int pct = Random.Range(0, 100);
        print(pct);
        if (pct <= 5)
        {
            // Assets/Resources/PreFabs/Collectables/HealthUp.prefab
            GameObject healthDrop = Resources.Load<GameObject>("PreFabs/Collectables/HealthUp");
            GameObject inst = Instantiate(healthDrop);
            inst.transform.position = transform.position;
        }
        
        gameObject.SetActive(false);
    }
}
