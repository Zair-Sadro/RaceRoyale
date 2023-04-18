using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	private const int SKINS_AMOUNT = 18;
	private const int CAR_AMOUNT = 6;
	
	// Game Data
	private const string DATA_LEVEL = "DATA_LEVEL";
	private const string DATA_COINS = "DATA_COINS";
	private const string DATA_SKINS = "DATA_SKINS_";
	private const string DATA_SHOP_BOUGHT_SKIN = "SKIN_";
	
	// Settings
	private const string DATA_VIBRATION = "DATA_VIBRATION";
	
	private void Awake()
	{
	    LoadData();
	}
    
	protected void OnApplicationFocus(bool focus)
	{
		if(focus == false)
		{
			SaveData();
		}
	}
    
	//Temporary logic
	public void SaveData()
	{
		PlayerPrefs.SetInt(DATA_COINS, PlayerStats.Instance.Coins);
		PlayerPrefs.SetInt(DATA_LEVEL, LevelSystem.Instance.CurrentLevel);
		PlayerPrefs.SetInt(DATA_VIBRATION, Settings.Instance.Vibration ? 1 : 0);
		
		for(int i = 0; i < SKINS_AMOUNT; i++)
		{
			PlayerPrefs.SetInt(DATA_SHOP_BOUGHT_SKIN + (ShopSystem.E_SkinType)i, ShopSystem.Instance.IsSkinAvailable((ShopSystem.E_SkinType)i) ? 1 : 0);
		}
		
		var selectedSkins = Player.Instance.SkinChanger.SelectedSkins;
		for(int i = 0; i < CAR_AMOUNT; i++)
		{
			PlayerPrefs.SetInt(DATA_SKINS + (Car.Type)i, selectedSkins[(Car.Type)i]);
		}
		
		PlayerPrefs.Save();
	}
	
	public void LoadData()
	{
		PlayerStats.Instance.Coins = PlayerPrefs.GetInt(DATA_COINS, 0);
		LevelSystem.Instance.CurrentLevel = PlayerPrefs.GetInt(DATA_LEVEL, 1);
		
		Settings.Instance.SetVibration(PlayerPrefs.GetInt(DATA_VIBRATION) == 1 ? true : false);
		
		for(int i = 0; i < SKINS_AMOUNT; i ++)
		{
			if(PlayerPrefs.GetInt(DATA_SHOP_BOUGHT_SKIN + (ShopSystem.E_SkinType)i, 0) == 1)
			{
				ShopSystem.Instance.skinBought.Add((ShopSystem.E_SkinType)i);
			}
		}
		
		var selectedSkins = Player.Instance.SkinChanger.SelectedSkins;
		
		
		for(int i = 0; i < CAR_AMOUNT; i++)
		{
			int skinID = PlayerPrefs.GetInt(DATA_SKINS + (Car.Type)i, -44);
			if(skinID >= 0)
			{
				Player.Instance.SkinChanger.SetSkin((Car.Type)i, skinID);
			}
		}
	}
}
