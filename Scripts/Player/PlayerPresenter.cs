
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


    /// <summary>
    /// Update
    /// </summary>
    public void ManualUpdate(float deltaTime, InGameEnum.State state)
    {

        _playerView.ManualUpdate(deltaTime, state);
        _playerModel.ManualUpdate(deltaTime, state);
        CheckDirection(state);


    }



    public void Initialize(InGameEnum.State state)
    {

        _playerModel = new PlayerModel();

        _playerView.Initialized();

        EventDirectionChanged();

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
    private void EventDirectionChanged()
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
        if (state == InGameEnum.State.WaitStart || state == InGameEnum.State.Dead) return;


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

}
