using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;
using YLocal = YG.YandexGame;
public class VictoryPanel : MonoBehaviour
{
    [SerializeField] private float _scaleTime = 1f;
    [SerializeField] private TMP_Text _topText;

    [SerializeField] private GameEnd _gameEnd;

    protected void OnEnable()
    {

        if (YandexGame.EnvironmentData.language != "ru")
        {
            if (_gameEnd.PlayerFirst)
                _topText.text = "Level COMPLETED!";
            else
                _topText.text = "Try again!";
        }
        else if (YLocal.EnvironmentData.language== "ru" || YLocal.EnvironmentData.language == "be" || YLocal.EnvironmentData.language == "kk" || YLocal.EnvironmentData.language == "uk" || YLocal.EnvironmentData.language == "uz")
        {
            if (_gameEnd.PlayerFirst)
                _topText.text = "Уровень ПРОЙДЕН!";
            else
                _topText.text = "Попробуй снова!";
        }


        transform.localScale = Vector3.zero;
        transform.DOKill();
        transform.DOScale(Vector3.one, _scaleTime).SetUpdate(true);
    }
}
