using UnityEngine;
using YG;


public class RewardButton : MonoBehaviour
{
    [SerializeField] private DisplayCoins _coins;
    [SerializeField] private AddShow _adShower;
    public bool TimerNeed;
    void Awake()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }


    void Rewarded(int id)
    {
        _coins.AddWatched();
    }

    // Ìועמה הכÿ גûחמגא גטהומ נוךכאלû
    public void MoreGoldAdReward(int id)
    {
        if(TimerNeed)
        _adShower.Show();
        else
            YandexGame.RewVideoShow(id);

    }
}

