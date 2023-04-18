using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : Singleton<PlayerStats>
{
	public float ScoreMultiplier = 0f;
	public int Coins = 0;
	public int CurrentCoins = 0;
	public int Fuel = 0;
	
	public float AdditionScoreMultiplier = 1f;
	
	public static Action<int> OnFuelChange;
	public static Action<int> OnCoinChange;
	public static Action<int> OnCurrentCoinChange;
	public static Action OnSceneLoad;
	
	public void Start()
	{
		OnSceneLoad?.Invoke();
	}
	
	public void AddFuel(int amount)
    {
	    Fuel += amount;
	    OnFuelChange?.Invoke(Fuel);
    }
    
	public void AddCoin(int amount)
	{
		Coins += amount;
		OnCoinChange?.Invoke(Coins);
	}
	
	public void AddCurrentCoin(int amount)
	{
		CurrentCoins += amount;
		OnCurrentCoinChange?.Invoke(CurrentCoins);
	}
	
	public bool TrySpendCoin(int amount)
	{
		if(Coins >= amount)
		{
			AddCoin(-amount);
			return true;
		}
		return false;
	}
	
	public void SetFuel(int amount)
	{
		Fuel = amount;
		OnFuelChange?.Invoke(Coins);
	}
	
	public void SetCoins(int amount, bool current = false)
	{
		if(current)
		{
			CurrentCoins = amount;
			OnCurrentCoinChange?.Invoke(CurrentCoins);
		}
		else
		{
			Coins = amount;
			OnCoinChange?.Invoke(Coins);
		}
	}
	
	public void ClaimCoins()
	{
		ApplyAddCoinMultiplier();
		if(CurrentCoins > 0)
			AddCoin(CurrentCoins);
		CurrentCoins = 0;
	}
	
	public void ApplyBaseCoinMultiplier()
	{
		CurrentCoins = Mathf.RoundToInt(CurrentCoins * ScoreMultiplier);
	}
	
	public void ApplyAddCoinMultiplier()
	{
		CurrentCoins = Mathf.RoundToInt(CurrentCoins * AdditionScoreMultiplier);
	}
	
	public void Reset()
	{
		AdditionScoreMultiplier = 1f;
		ScoreMultiplier = 0f;
		CurrentCoins = 0;
		SetFuel(0);
	}
}
