
using UnityEngine;
using System;
using UniRx;

public class PlayerPresenter : MonoBehaviour
{
    /// <summary>
    /// Player View
    /// </summary>
    [SerializeField]
    private PlayerView _playerView;

    /// <summary>
    /// Player Model
    /// </summary>
    private PlayerModel _playerModel;

    public float X => _playerModel.X;

    public float Y => _playerModel.Y;


    /// <summary>
    /// Update
    /// </summary>
    public void ManualUpdate(float deltaTime, InGameEnum.State state)
    {

        _playerView.ManualUpdate(deltaTime);
        _playerModel.ManualUpdate(deltaTime);
        CheckDirection(state);


    }



    public void Initialize(InGameEnum.State state)
    {

        _playerModel = new PlayerModel();

        _playerView.Initialized();

        EventDirectionChanged(state);

        Bind();



    }

    private void Bind()
    {

        _playerModel.XProp
            .Subscribe(_playerView.SetX)
            .AddTo(gameObject);

        _playerModel.YProp
            .Subscribe(_playerView.SetY)
            .AddTo(gameObject);


    }


    //Subscribe the Change of Diection
    private void EventDirectionChanged(InGameEnum.State state)
    {


        _playerModel.Direction
            .Subscribe(direction => {
                float deltaTime = Time.deltaTime;
                _playerModel
                .UpdateMove(deltaTime);
            })
            .AddTo(this);


    }

    private void CheckDirection(InGameEnum.State state)
    {
        if (state == InGameEnum.State.WaitStart || state == InGameEnum.State.Hit) return;


        if (Input.GetKey(KeyCode.W))
            _playerModel.SetDirection(PlayerEnum.PlayerDirection.Up);
        else if (Input.GetKey(KeyCode.A))
            _playerModel.SetDirection(PlayerEnum.PlayerDirection.Left);
        else if (Input.GetKey(KeyCode.S))
            _playerModel.SetDirection(PlayerEnum.PlayerDirection.Down);
        else if (Input.GetKey(KeyCode.D))
            _playerModel.SetDirection(PlayerEnum.PlayerDirection.Right);
        else
            _playerModel.SetDirection(PlayerEnum.PlayerDirection.None);



    }

    public void DeadAnimation()
    {

        _playerView.DeadAnimation().Forget();

    }

    public void Reset()
    {

        _playerModel.Reset();
        _playerView.gameObject.SetActive(true);


    }

}
