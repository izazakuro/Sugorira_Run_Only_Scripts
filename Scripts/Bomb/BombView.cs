using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;

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

    //Explosion Array
    [SerializeField]
    private Sprite[] _explosionSprites;

    private int _explosionIndex;

    private int _arrayIndex;

    private float _spriteChangeTimer;

    //Transform Cache
    private Transform _cachedTransform;


    public void Initialize()
    {
        _cachedTransform = transform;
        gameObject.SetActive(false);
        Reset();

    }

    public void Reset()
    {

        _arrayIndex = 0;
        _spriteChangeTimer = 0f;
    
    }

    public void ManualUpdate(float deltaTime)
    {

        UpdateView(deltaTime);

    }


    private void UpdateView(float deltaTime)
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

    public void SetX(float x)
    {

        var position = _cachedTransform.localPosition;
        position.x = x;
        _cachedTransform.localPosition = position;


    }

    public void SetY(float y)
    {

        var position = _cachedTransform.localPosition;
        position.y = y;
        _cachedTransform.localPosition = position;

    }

    public async UniTaskVoid ExplosionAsync()
    {

        for(int i = 0; i < _explosionSprites.Length; i++)
        {

            _bombImage.sprite = _explosionSprites[i];

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

        }

       

    }



}
