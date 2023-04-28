using UnityEngine;
using YG;


public class RewardButton : MonoBehaviour
{
    [SerializeField] private DisplayCoins _coins;
    
    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;


    void Rewarded(int id)
    {
        _coins.AddWatched();
    }

    // Метод для вызова видео рекламы
    public void MoreGoldAdReward(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}

