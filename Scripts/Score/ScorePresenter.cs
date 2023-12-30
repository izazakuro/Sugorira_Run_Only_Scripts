using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class ScorePresenter : MonoBehaviour
{

    private ScoreModel _scoreModel;

    [SerializeField]
    private ScoreView _scoreView;

    public int Score => _scoreModel.ScoreProp.Value;


    public void Initialize()
    {

        _scoreModel = new ScoreModel();
        _scoreView.Initialize();

        Bind();
        
    }

    private void Bind()
    {
        _scoreModel.ScoreProp
            .Subscribe(_scoreView.SetScore)
            .AddTo(gameObject);

    }

    public void AddScore()
    {

        _scoreModel.AddScore();       

    }


    public void Reset()
    {

        _scoreModel.Reset();

    }

    public void Show()
    {

        _scoreView.Show();

    }

    public void Hide()
    {

        _scoreView.Hide();

    }



}
