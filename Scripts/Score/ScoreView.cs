using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{

    [SerializeField]
    private Text _scoreNum;

    [SerializeField]
    private Text _scoreText;


    public void Initialize()
    {

        Hide();

    }


    public void Hide()
    {

        gameObject.SetActive(false);


    }

    public void Show()
    {

        _scoreNum.color = new Color(_scoreText.color.r, _scoreText.color.g, _scoreText.color.b, 0);
        gameObject.SetActive(true);
        _scoreNum.DOFade(1, 2f);

    }

    public void SetScore(int score)
    {

        _scoreNum.text = score.ToString();

    }

}
