using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{
    public String tag;
    
    // Used Code from: https://www.youtube.com/watch?v=Xtfe5S9n4SI
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag(tag);
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
