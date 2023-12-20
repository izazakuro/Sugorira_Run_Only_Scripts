using System;
using UniRx;
using UnityEngine;

public class InGameModel
{

    private ReactiveProperty<InGameEnum.State> _stateProp;
    public IReadOnlyReactiveProperty<InGameEnum.State> StateProp => _stateProp;


    public InGameEnum.State State => _stateProp.Value;

    public InGameModel()
    {

        _stateProp = new ReactiveProperty<InGameEnum.State>(InGameEnum.State.WaitStart);


    }


    public void Reset()
    {


        SetState(InGameEnum.State.WaitStart);

    }


    public void SetState(InGameEnum.State state)
    {

        _stateProp.Value = state;

    }


}
