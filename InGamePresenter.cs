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


    /// <summary>
    /// Bomb Array
    /// </summary>
    private BombPresenter[] _bombs;

    /// <summary>
    /// Bomb Prefab
    /// </summary>
    [SerializeField]
    private GameObject _bombPrefab;

    [SerializeField]
    private ScorePresenter _score;

    /// <summary>
    /// Finish
    /// </summary>
    [SerializeField]
    private InGameFinishView _finishView;


    // Start is called before the first frame update
    void Start()

    {

        _model = new InGameModel();

        _view.Initialize();


        //Start Initialize
        _view.Initialize();

        _player.Initialize(_model.State);

        _score.Initialize();

        _finishView.Initilize();

        SpawnBombsAsync().Forget();

        SetEvents();

        //Start of Background and Title
        _view.FadeIn().Forget();

        Bind();


    }

    private void Bind()
    {

        _model.StateProp.Where(state => state == InGameEnum.State.Hit)
            .Subscribe(_ =>

                StartCoroutine(EndState())

            )
            .AddTo(gameObject);


    }

    private IEnumerator EndState()
    {

        _player.DeadAnimation();
        _score.Hide();
        foreach (var bomb in _bombs)
        {
            if (bomb._isHitOne == true)
                bomb.Explosion();
        }

        yield return new WaitForSeconds(1f);

        _finishView.SetResultScore(_score.Score);
        _finishView.ShowAsync().Forget();



    }

    private void SetEvents()
    {

        foreach (var bomb in _bombs)
        {
            bomb.AddScoreCallback += () => _score.AddScore();
        }



        _view.OnTapButtonClicked
            .Subscribe(_ =>
            {
                _view.FadeOut().Forget();
                GameStart();

            })
            .AddTo(this);

        _finishView.RestartButton.OnClickAsObservable()
            .Subscribe(_ => GameRestart())
            .AddTo(gameObject);

        _finishView.ReturnButton.OnClickAsObservable()
            .Subscribe(_ => ReturnTitle())
            .AddTo(gameObject);

    }

    private void GameStart()
    {

        _model.SetState(InGameEnum.State.Run);
        _score.Show();


    }

    private async UniTaskVoid SpawnBombsAsync()
    {

        _bombs = new BombPresenter[InGameConst.NumberOfBombs];
        for (int i = 0; i < InGameConst.NumberOfBombs; i++)
        {
            var bombClone = Instantiate(_bombPrefab, _view.BombContainer);
            _bombs[i] = bombClone.GetComponent<BombPresenter>();
            _bombs[i].Initialize(InGameConst.TimeToGenerateBomb * i);

        }

        await UniTask.WaitUntil(() => _model.State == InGameEnum.State.Run);

        for (int i = 0; i < InGameConst.NumberOfBombs; i++)
        {
            _bombs[i].gameObject.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(InGameConst.TimeToGenerateBomb));
        }


    }









    // Update is called once per frame
    void Update()
    {

        var deltaTime = Time.deltaTime;

        _view.ManualUpdate(deltaTime);

        if (_model.State == InGameEnum.State.WaitStart || _model.State == InGameEnum.State.Run)
            _player.ManualUpdate(deltaTime,_model.State);

        _model.CheckHit(_bombs, _player.X, _player.Y);

        foreach(var bomb in _bombs)
        {
            if(_model.State == InGameEnum.State.Run)
                bomb.ManualUpdate(deltaTime);
        }
           
        

    }


    private void GameRestart()
    {

        ResetAll();

        _model.SetState(InGameEnum.State.Run);

        _score.Show();



    }

    private void ReturnTitle()
    {


        ResetAll();

        _model.SetState(InGameEnum.State.WaitStart);

        _score.Hide();

        _view.FadeIn().Forget();

  

    }

    private void ResetAll()
    {

        for (int i = 0; i < _bombs.Length; i++)
        {
            _bombs[i].Reset(i * InGameConst.TimeToGenerateBomb);
        }

        _player.Reset();

        _score.Reset();

        _finishView.Hide();


    }


}
