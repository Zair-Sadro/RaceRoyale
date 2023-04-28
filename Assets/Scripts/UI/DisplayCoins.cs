using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class DisplayCoins : MonoBehaviour
{
	[SerializeField] private bool _displayCurrent = false;
	[SerializeField] private TMP_Text _text;
	[SerializeField] private float _multiplyAnimSpeed = 1f;
	
	private void OnEnable()
	{
		if(_displayCurrent)
		{
			_text.text = PlayerStats.Instance.CurrentCoins.ToString();
			PlayerStats.OnCurrentCoinChange += OnCoinChange;
		}
		else
		{
			_text.text = PlayerStats.Instance.Coins.ToString();
			PlayerStats.OnCoinChange += OnCoinChange;
		}
	}
	
	private void OnDisable()
	{
		PlayerStats.OnCoinChange -= OnCoinChange;
		PlayerStats.OnCurrentCoinChange -= OnCoinChange;
	}
	
	private void OnCoinChange(int amount)
	{
		_text.text = amount.ToString();
	}
	
	public void PlayMultiplyAnim(float targetValue)
	{
		StartCoroutine(MultiplyAnim(targetValue));
	}
    public void AddWatched()
    {
        PlayMultiplyAnim(2);
		PlayerStats.Instance.CurrentCoins *= 2;
    }
    private IEnumerator MultiplyAnim(float multiply)
	{
		float value = 0f;
		float currentCoins = PlayerStats.Instance.CurrentCoins;
		float diff = (currentCoins * multiply) - currentCoins;
		float coinPerSec = diff / _multiplyAnimSpeed;
		AudioManager.Instance.Coin();
		while(diff > 0)
		{
			diff -= coinPerSec * Time.unscaledDeltaTime;
			value += coinPerSec * Time.unscaledDeltaTime;
			if(diff < 0)
			{
				diff = 0;
				value = (currentCoins * multiply) - currentCoins;
			}
			_text.text = (currentCoins + value).ToString("0");
			yield return null;
		}
	}

  
}
