using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class InGameView : MonoBehaviour
{
    [SerializeField]
    private InGameGroundView _ground;

    public InGameGroundView Ground => _ground;

    [SerializeField]
    private Image _titleImage;

    [SerializeField]
    private Image _buttonImage;

    [SerializeField]
    private Button _tapButton;

    public IObservable<Unit> OnTapButtonClicked => _tapButton.OnClickAsObservable();

    private float fadeTime = 2f;

    private float outTime = 2f;




    public void Initialize()
    {
        Color buttonColor = _buttonImage.color;
        buttonColor.a = 0f;
        _buttonImage.color = buttonColor;

        Color titleColor = _titleImage.color;
        titleColor.a = 0f;
        _titleImage.color = titleColor;
        _ground.Initialize();

    }

    public async UniTask FadeIn()
    {
        _tapButton.gameObject.SetActive(true);
        Sequence fadeIn = DOTween.Sequence();
        await fadeIn.Append(_titleImage.DOFade(1f, fadeTime))
        .Append(_buttonImage.DOFade(1f, fadeTime))
        .Play();


    }

    public async UniTask FadeOut()
    {
        _tapButton.gameObject.SetActive(false);
        Sequence fadeOut = DOTween.Sequence();


        await fadeOut.Append(_titleImage.DOFade(0f, outTime))
        .Join(_buttonImage.DOFade(0f, outTime))
        .Play()
        .OnComplete(() =>
        {

            _tapButton.gameObject.SetActive(false);

        });

    }


    public void ManualUpdate(float deltaTime)
    {
        _ground.ManualUpdate(deltaTime);


    }


}
