using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    private bool _reloading = false;
    private float _waitTime = 2.0f;
    private float _t = 0.0f;
    
    private void Update()
    {
        if (_reloading)
        {
            _t += Time.deltaTime;
            
            if (_t >= _waitTime)
            {
                _t = 0.0f;
                _reloading = false;
            }

            return;
        }
        if (Input.GetAxisRaw("Cancel") != 0)
        {
            _reloading = true;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
