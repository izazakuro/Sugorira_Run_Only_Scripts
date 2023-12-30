using System;
using UniRx;
public class ScoreModel
{

    private readonly ReactiveProperty<int> _scoreProp;
    public IReadOnlyReactiveProperty<int> ScoreProp => _scoreProp;


    public ScoreModel()
    {

        _scoreProp = new ReactiveProperty<int>(0);

    }

    public void AddScore()
    {

        _scoreProp.Value += ScoreConst.AdditionScore;

    }


    public void Reset()
    {

        _scoreProp.Value = 0;

    }



}
