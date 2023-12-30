using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;


public class BombModel
{
    //X of Bomb
    private readonly ReactiveProperty<float> _xProp;

    public IReadOnlyReactiveProperty<float> XProp => _xProp;


    //Y of Bomb

    private readonly ReactiveProperty<float> _yProp;

    public IReadOnlyReactiveProperty<float> YProp => _yProp;

    private float Y => _yProp.Value;

    private float X => _xProp.Value;

    private bool _isHitOne;

    public bool IsHitOne => _isHitOne;

    public event Action AddScoreCallback;

    public BombModel(float time)
    {
        _xProp = new ReactiveProperty<float>(GenerateRandomX());
        _yProp = new ReactiveProperty<float>(BombGenerateConst.BombGenerateY 
                                            + time * BombGenerateConst.BombMoveSpeed);

        _isHitOne = false;

    }


    private float GenerateRandomX()
    {

        Random random = new Random();

        return (float)(BombGenerateConst.BombLeftBoundry + (Math.Abs(BombGenerateConst.BombLeftBoundry) + BombGenerateConst.BombRightBoundry) * random.NextDouble());

    }

    private void Move(float deltaTime)
    {

        var recentY = Y - BombGenerateConst.BombMoveSpeed * deltaTime;

        if(recentY < BombGenerateConst.BombRecycleLine)
        {

            recentY = BombGenerateConst.BombGenerateY;
            _xProp.Value = GenerateRandomX();
            AddScoreCallback?.Invoke();
        }


        _yProp.Value = recentY;
        


    }

    public bool IsHit(float playerX, float playerY)
    {

        bool hitX = Math.Abs(X - playerX) < (BombConst.BombWidth + PlayerConst.PlayerWidth) / 2;

        bool hitY = Math.Abs(Y - playerY) < (BombConst.BombHeight + PlayerConst.PlayerHeight) / 2;

        if(hitX && hitY)
        {
            _isHitOne = true;

            return true;
        }
        else
        {
            return false;
        }



    }

    public void ManualUpdate(float deltaTime)
    {

        Move(deltaTime);


    }

    public void Reset(float time)
    {
        _xProp.Value = GenerateRandomX();
        _yProp.Value = (BombGenerateConst.BombGenerateY
                       + time * BombGenerateConst.BombMoveSpeed);
        _isHitOne = false;

    }
    
}
