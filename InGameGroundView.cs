using UnityEngine;
using UnityEngine.UI;

public class InGameGroundView : MonoBehaviour
{

    
    private float  _moveSpeed = 120f;

    public float MoveSpeed => _moveSpeed;


    private Vector2 _startPoint;

    [SerializeField]
    private RectTransform _sizeofBar;

    private float _totalHeight;


    public void Initialize()
    {
        
        _startPoint = transform.position;
        if (_sizeofBar != null)
        {
            
            _totalHeight = _sizeofBar.rect.height * 0.6f; 

        }

    }

    /// <summary>
    /// Background Update
    /// </summary>
    public void ManualUpdate(float deltaTime)
    {
        Move(deltaTime);
    }

    /// <summary>
    /// Scroll
    /// </summary>
    private void Move(float deltaTime)
    {

        float newPosition = Mathf.Repeat(Time.time * MoveSpeed, _totalHeight);
        transform.position = _startPoint + Vector2.down * newPosition;
    }
}

