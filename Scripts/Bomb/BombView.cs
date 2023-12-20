using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombView : MonoBehaviour
{
    //Time to Change Sprite
    private const float BombSpriteChange = 0.1f;

    //Image of Bomb
    [SerializeField]
    private Image _bombImage;

    //Sprites Array
    [SerializeField]
    private Sprite[] _bombSprites;

    private int _arrayIndex;

    private float _spriteChangeTimer;


    public void Initialize()
    {

        Reset();

    }

    public void Reset()
    {

        _arrayIndex = 0;
        _spriteChangeTimer = 0f;
    
    }

    public void ManualUpdate(float deltaTime, InGameEnum.State state)
    {

        UpdateView(deltaTime, state);

    }


    private void UpdateView(float deltaTime, InGameEnum.State state)
    {

        _spriteChangeTimer -= deltaTime;
        if (_spriteChangeTimer < 0f)
        {
            _spriteChangeTimer += BombSpriteChange;
            _arrayIndex = Repeat(_arrayIndex, 1, _bombSprites.Length);
            _bombImage.sprite = _bombSprites[_arrayIndex];

        }

    }

    private int Repeat(int self, int value, int max)
    {
        return (self + value + max) % max;
    }


}
