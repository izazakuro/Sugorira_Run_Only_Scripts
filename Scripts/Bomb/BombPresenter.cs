using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UniRx;
using System;

public class BombPresenter : MonoBehaviour
{

    //Bomb View
    [SerializeField]
    private BombView _bombView;

    //Bomb Model
    private BombModel _bombModel;

    public bool _isHitOne => _bombModel.IsHitOne;

    public event Action AddScoreCallback;

    //Bomb Initialize
    public void Initialize(float time)
    {

        _bombModel = new BombModel(time);
        _bombView.Initialize();
        SetEvent();

        Bind();


        
    }

    public void SetEvent()
    {

        _bombModel.AddScoreCallback += () => AddScoreCallback?.Invoke();

    }

    private void Bind()
    {

        _bombModel.XProp
            .Subscribe(_bombView.SetX)
            .AddTo(gameObject);

        _bombModel.YProp
            .Subscribe(_bombView.SetY)
            .AddTo(gameObject);


    }

    public void ManualUpdate(float deltaTime)
    {

        _bombModel.ManualUpdate(deltaTime);
        _bombView.ManualUpdate(deltaTime);

    }

    public bool IsHit(float playerX, float playerY)
    {


        return _bombModel.IsHit(playerX, playerY);

    }

    public void Explosion()
    {
        _bombView.ExplosionAsync().Forget();


    }

    public void Reset(float time)
    {

        _bombModel.Reset(time);

    }


}
