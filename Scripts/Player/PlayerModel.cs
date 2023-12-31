using System;
using UniRx;

public class PlayerModel
{
    //Value could be Subcribed
    private readonly ReactiveProperty<float> _xProp;
    public IReadOnlyReactiveProperty<float> XProp => _xProp;

    private readonly ReactiveProperty<float> _yProp;
    public IReadOnlyReactiveProperty<float> YProp => _yProp;

    private readonly ReactiveProperty<PlayerEnum.PlayerDirection> _direction;
    public IReadOnlyReactiveProperty<PlayerEnum.PlayerDirection> Direction => _direction;





    //Calc Value
    public float X => _xProp.Value;
    public float Y => _yProp.Value;

    public PlayerEnum.PlayerDirection direction => _direction.Value;

    private const float _moveSpeed = 300f;

    private bool _checkBoundry = false;




    public PlayerModel()
    {

        _xProp = new ReactiveProperty<float>(PlayerConst.PlayerStartX);
        _yProp = new ReactiveProperty<float>(PlayerConst.PlayerStartY);
        _direction = new ReactiveProperty<PlayerEnum.PlayerDirection>(PlayerEnum.PlayerDirection.None);


    }


    private void CalcX(float x, PlayerEnum.PlayerDirection currentDirection, float deltaTime)
    {

        float dx;

        dx = _moveSpeed * deltaTime;

        switch (currentDirection)
        {

            case PlayerEnum.PlayerDirection.Left:

                if (_xProp.Value - dx < PlayerConst.XLeftBoundry)
                {
                    _xProp.Value = PlayerConst.XLeftBoundry;
                    break;
                }

                _xProp.Value -= dx;
                break;

            case PlayerEnum.PlayerDirection.Right:
                if (_xProp.Value + dx > PlayerConst.XRightBoundry)
                {
                    _xProp.Value = PlayerConst.XRightBoundry;
                    break;
                }

                _xProp.Value += dx;
                break;

            default:
                break;

        }


    }

    private void UpdateX(float deltaTime)
    {

        CalcX(X, direction, deltaTime);

    }

    private void CalcY(float y, PlayerEnum.PlayerDirection currentDirection, float deltaTime)
    {

        float dy;
        dy = _moveSpeed * deltaTime;

        switch (currentDirection)
        {

            case PlayerEnum.PlayerDirection.Down:
                if (_yProp.Value - dy < PlayerConst.YDownBoundry)
                {
                    _yProp.Value = PlayerConst.YDownBoundry;
                    break;
                }

                _yProp.Value -= dy;
                break;

            case PlayerEnum.PlayerDirection.Up:
                if (_yProp.Value + dy > PlayerConst.YUpBoundry)
                {
                    _yProp.Value = PlayerConst.YUpBoundry;
                    break;
                }
                _yProp.Value += dy;
                break;
            default:
                break;

        }

    }

    public void ManualUpdate(float deltaTime)
    {

        UpdateMove(deltaTime);

    }

    private void UpdateY(float deltaTime)
    {

        CalcY(Y, direction, deltaTime);

    }

    public void UpdateMove(float deltaTime)

    {

    
        _checkBoundry = IsBoundry();
        if ((_checkBoundry == true && X == PlayerConst.XLeftBoundry && _direction.Value == PlayerEnum.PlayerDirection.Left) ||
            (_checkBoundry == true && X == PlayerConst.XRightBoundry && _direction.Value == PlayerEnum.PlayerDirection.Right) ||
            (_checkBoundry == true && Y == PlayerConst.YUpBoundry && _direction.Value == PlayerEnum.PlayerDirection.Up)||
            (_checkBoundry == true && Y == PlayerConst.YDownBoundry && _direction.Value == PlayerEnum.PlayerDirection.Down))
        {
            return;
        }
        else
        {

            _checkBoundry = false;

            UpdateX(deltaTime);
            UpdateY(deltaTime);



        }
        

    }

    public void SetDirection(PlayerEnum.PlayerDirection currentDirection)
    {

        _direction.Value = currentDirection;


    }

    private bool IsBoundry()
    {

        if (X <= PlayerConst.XLeftBoundry || X >= PlayerConst.XRightBoundry
            || Y <= PlayerConst.YDownBoundry || Y >= PlayerConst.YUpBoundry)
        {
            return true;


        }
        else
        {
            return false;

        }

        

    }

    public void Reset()
    {

        _xProp.Value = PlayerConst.PlayerStartX;
        _yProp.Value = PlayerConst.PlayerStartY;

        SetDirection(PlayerEnum.PlayerDirection.None);
        


    }


}
