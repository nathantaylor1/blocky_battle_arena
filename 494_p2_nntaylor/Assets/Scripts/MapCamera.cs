using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public static MapCamera instance;
    
    private Camera[] _cameras;
    private Camera _mainCam;
    private Camera _mapCam;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _cameras = FindObjectsOfType<Camera>();
        for (int i = 0; i < _cameras.Length; ++i)
        {
            Camera cam = _cameras[i];
            if (cam.CompareTag("MainCamera"))
            {
                cam.enabled = true;
                _mainCam = cam;
            }
            else if (cam.CompareTag("MapCamera"))
            {
                cam.enabled = false;
                _mapCam = cam;
                int halfWidth = (int)MapGen.instance.mapWidth / 2, 
                    halfHeight = (int)MapGen.instance.mapHeight / 2;
                _mapCam.transform.position = new Vector3(halfWidth, halfHeight, -10.0f);
                _mapCam.orthographicSize = halfHeight;
            }
            else
            {
                cam.enabled = false;
            }
        }
    }

    private bool _mapCamEnabled = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _mapCamEnabled = !_mapCamEnabled;
            _mainCam.enabled = !_mapCamEnabled;
            _mapCam.enabled = _mapCamEnabled;
        }
    }
}
