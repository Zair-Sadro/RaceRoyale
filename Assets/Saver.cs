using System;
using System.Collections.Generic;
using YG;


public class Saver : Singleton<Saver>
{
    public event Action DataLoaded;
    public ChangePlayerSkin _playerSkins;
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private SavesYG saveData => YandexGame.savesData;
    void Start()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void OnApplicationQuit()
    {
        if (YandexGame.SDKEnabled)
            Save();
    }
    public void Save()
    {
        ShopSystem shop = ShopSystem.Instance;
        saveData.coins = PlayerStats.Instance.Coins;
        saveData.level = LevelSystem.Instance.CurrentLevel;
        saveData.selectedCar = _playerSkins.SelectedSkins;

        saveData.purchasedCars = shop.skinBought;
        YandexGame.SaveProgress();

    }

    public void SaveSelectedSkin(Car.Type type,int id)
    {
        saveData.selectedCar[type] = id;
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {

        PlayerStats.Instance.Coins = saveData.coins;
        LevelSystem.Instance.CurrentLevel = saveData.level;
        ShopSystem.Instance.skinBought = saveData.purchasedCars;
        _playerSkins.InitFromSave(saveData.selectedCar);

        print($"Language - {saveData.language}\n" +
           $"First Session - {saveData.isFirstSession}\n" +
           $"Prompt Done - {saveData.promptDone}\n");
        DataLoaded?.Invoke();
    }
}
