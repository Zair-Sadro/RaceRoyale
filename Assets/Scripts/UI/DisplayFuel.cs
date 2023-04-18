using UnityEngine;
using TMPro;

public class DisplayFuel : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	
	private void OnEnable()
	{
		_text.text = 0.ToString();
		PlayerStats.OnFuelChange += OnFuelChange;
	}
	
	private void OnDisable()
	{
		PlayerStats.OnFuelChange += OnFuelChange;
	}
	
	private void OnFuelChange(int amount)
	{
		_text.text = amount.ToString();
	}
}
