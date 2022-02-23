using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private float _t = 0.0f;
    void Update()
    {
        _t += Time.deltaTime;
        if (_t > 1.0f) Destroy(this.gameObject);
    }
}
