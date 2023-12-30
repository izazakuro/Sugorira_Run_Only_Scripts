using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

public class InGameFinishView : MonoBehaviour
{
    //GameOver Image
    [SerializeField]
    private Image _image;

    public Image Image => _image;

    [SerializeField]
    private Image _resultImage;

    [SerializeField]
    private Text _resultScore;


    //Restart Button
    [SerializeField]
    private Button _restartButton;
    public Button RestartButton => _restartButton;

    [SerializeField]
    private Button _returnButton;
    public Button ReturnButton => _returnButton;

    [SerializeField]
    private Image _restartButtonImage;

    [SerializeField]
    private Image _returnToTitle;

    private static readonly Color _transparent = new Color(1f, 1f, 1f, 0f);



    private float _fadeTime = 2f;


    public void Initilize()
    {

        Hide();

    }

    public void Hide()
    {
        _image.color = _transparent;
        _restartButtonImage.color = _transparent;
        _returnToTitle.color = _transparent;
        _resultImage.color = _transparent;
        _resultScore.color = _transparent;
        _restartButton.gameObject.SetActive(false);
        _returnButton.gameObject.SetActive(false);


    }

    public async UniTaskVoid ShowAsync()
    {
        Sequence fadeIn = DOTween.Sequence();
        await fadeIn.Append(_image.DOFade(1f, _fadeTime))
        .Append(_restartButtonImage.DOFade(1f, _fadeTime))
        .Join(_returnToTitle.DOFade(1f, _fadeTime))
        .Join(_resultImage.DOFade(1f, _fadeTime))
        .Join(_resultScore.DOFade(1f,_fadeTime))
        .Play()
        .OnComplete(() =>
        {
            _restartButton.gameObject.SetActive(true);
            _returnButton.gameObject.SetActive(true);

        });
        


    }

    public void SetResultScore(int score)
    {

        _resultScore.text = "Score:" + score.ToString();

    }


}
