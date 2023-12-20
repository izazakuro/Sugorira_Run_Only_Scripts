using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using UniRx;

public class InGamePresenter : MonoBehaviour
{

    [SerializeField]
    private InGameView _view;


    private InGameModel _model;

    /// <summary>
    /// Player
    /// </summary>
    [SerializeField]
    private PlayerPresenter _player;


    // Start is called before the first frame update
    void Start()

    {

        _model = new InGameModel();

        _view.Initialize();


        //Start Initialize
        _view.Initialize();

        _player.Initialize(_model.State);

        SetEvents();


        //Start of Background and Title
        _view.FadeIn().Forget();

        


    }

    private void SetEvents()
    {

        _view.OnTapButtonClicked
            .Subscribe(_ =>
            {
                _view.FadeOut().Forget();
                GameStart();
                
            })
            .AddTo(this);


    }

    private void GameStart()
    {

        _model.SetState(InGameEnum.State.Run);
        

    }

    // Update is called once per frame
    void Update()
    {

        var deltaTime = Time.deltaTime;

        _view.ManualUpdate(deltaTime);

        _player.ManualUpdate(deltaTime,_model.State);
        

    }






}
