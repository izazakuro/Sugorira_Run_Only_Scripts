using UnityEngine.UI;
using UnityEngine;
using UnityEditor.PackageManager.Requests;
using Unity.VisualScripting;

public class PlayerView : MonoBehaviour
{

    /// <summary>
    /// Time to Change Sprite
    /// </summary>
    private float _timeToChangeSprite = 0.1f;

    /// <summary>
    /// Image
    /// </summary>
    [SerializeField]
    private Image _image;

    /// <summary>
    /// Array of Sprites
    /// </summary>
    [SerializeField]
    private Sprite[] _sprites;

    private Transform _cacheTransform;

    /// <summary>
    /// Array Index
    /// </summary>
    private int _indexOfSprites;

    private float _spriteChangeTimer;


    /// <summary>
    /// Initialization
    /// </summary>
    public void Initialized()
    {

        Reset();

    }


    /// <summary>
    /// Reset of Array and Timer
    /// </summary>
    public void Reset()
    {
;
        _cacheTransform = transform;
        _indexOfSprites = 0;
        _spriteChangeTimer = 0;


    }

    public void ManualUpdate(float deltaTime, InGameEnum.State state)
    {

        UpdateView(deltaTime, state);

    }

    private void UpdateView(float deltaTime, InGameEnum.State state)
    {
        
        _spriteChangeTimer -= deltaTime;
        if(_spriteChangeTimer < 0f)
        {
            _spriteChangeTimer += _timeToChangeSprite;
            _indexOfSprites = Repeat(_indexOfSprites, 1, _sprites.Length);
            _image.sprite = _sprites[_indexOfSprites];

        }


        

    }


    private int Repeat(int self , int value, int max)
    {
        return (self + value + max) % max;
    }


    public void SetX(float x)
    {

        var position = _cacheTransform.localPosition;
        position.x = x;
        _cacheTransform.localPosition = position;

    }

    public void SetY(float y)
    {

        var position = _cacheTransform.localPosition;
        position.y = y;
        _cacheTransform.localPosition = position;

    }


}
