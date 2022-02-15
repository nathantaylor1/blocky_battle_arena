using UnityEngine;
using UnityEngine.UI;

public class Player_BreakBlock : MonoBehaviour
{
    private Vector2 _currentDirection;
    private Collider2D _collider2D;
    private float _maxRaycastDist;

    public LayerMask wallLayer;
    public Text numBlocksCanBreakText;
    private uint numBlocksCanBreak;

    private void Awake()
    {
        numBlocksCanBreak = (MapGen.instance.mapWidth * MapGen.instance.mapHeight) / 200;
        UpdateText();
        
        _collider2D = GetComponent<Collider2D>();
        _maxRaycastDist = _collider2D.bounds.extents.x + 0.2f;
    }
    
    private void Update()
    {
        BreakBlock();
    }

    private void BreakBlock()
    {
        if (numBlocksCanBreak == 0 || Input.GetAxisRaw("Fire1") == 0) return;
        _currentDirection = Player_MovementController.instance.GetDirection();
        RaycastHit2D hit = Physics2D.Raycast(_collider2D.bounds.center, 
            _currentDirection, _maxRaycastDist, wallLayer);
        if (hit.transform != null && hit.transform.CompareTag("Wall"))
        {
            Breakable breakable = hit.transform.GetComponent<Breakable>();
            breakable.BreakThisBlock();
            numBlocksCanBreak--; 
            UpdateText();
        }
    }

    private void UpdateText()
    {
        numBlocksCanBreakText.text = numBlocksCanBreak.ToString();
    }

    public void AddNumBlocksCanBreak(int num)
    {
        numBlocksCanBreak += (uint)num;
        UpdateText();
    }
}
