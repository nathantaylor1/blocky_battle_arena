using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public KeyCode attackKey = KeyCode.X;
    public Vector2 lastDirection;
    public GameObject swordGO;
    public float animTime = 0.5f;

    private bool _swinging = false;
    private float _t = 0.0f;
    private float _startAngle, _endAngle;
    
    private void Update()
    {
        if (!Player_Inventory.instance.HasSword()) return;
        lastDirection = Player_MovementController.instance.GetLastDirection();
        if (_swinging)
        {
            print('x');
            _t += Time.deltaTime;
            float pctg = _t / animTime;
            float angle = Mathf.Lerp(_startAngle, _endAngle, pctg);
            print(angle);
            swordGO.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            
            if (_t > animTime)
            {
                _swinging = false;
                _t = 0;
                swordGO.SetActive(false);
            }

            return;
        }
        
        if (!_swinging && Input.GetKeyDown(attackKey))
        {
            swordGO.SetActive(true);
            _swinging = true;
            if (lastDirection == Vector2.up)
            {
                swordGO.transform.localRotation = Quaternion.Euler(0f, 0f, -45f);
                _startAngle = -45f;
                _endAngle = 45f;
            }
            else if (lastDirection == Vector2.right)
            {
                swordGO.transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
                _startAngle = 225f;
                _endAngle = 315f;
            }
            else if (lastDirection == Vector2.down)
            {
                swordGO.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                _startAngle = 135f;
                _endAngle = 225f;
            }
            else if (lastDirection == Vector2.left)
            {
                swordGO.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                _startAngle = 45f;
                _endAngle = 135f;
            }
        }
    }
}
