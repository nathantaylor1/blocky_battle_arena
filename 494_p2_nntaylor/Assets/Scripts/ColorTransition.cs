using UnityEngine;

public class ColorTransition : MonoBehaviour
{
    public float lerpTime = 0.02f;
    public Color[] colors;
    
    private SpriteRenderer _spriteRenderer;
    private int _currentColorIndex;
    private float _timePassed = 0f;
    private int len;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentColorIndex = 0;
        _spriteRenderer.color = colors[0];
        len = colors.Length;
    }
    
    private void Update()
    {
        _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, colors[_currentColorIndex], 
            lerpTime * Time.deltaTime);
        _timePassed = Mathf.Lerp(_timePassed, 1f, lerpTime * Time.deltaTime);
        if (_timePassed > 0.9f)
        {
            _currentColorIndex++;
            _timePassed = 0f;
            _currentColorIndex = (_currentColorIndex >= len) ? 0 : _currentColorIndex;
        }
    }
}