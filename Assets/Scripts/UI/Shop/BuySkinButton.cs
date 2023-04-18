using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BuySkinButton : MonoBehaviour
{
	public ShopSystem.E_SkinType selectedSkin;
	public TMP_Text costText;
	public Image background;
	public Color disabledColor = Color.grey;
	
	public event System.Action<ShopSystem.E_SkinType> OnBuySkin;
	
	protected void OnEnable()
	{
		UpdateUI();
	}
	
	public void UpdateUI()
	{
		if(selectedSkin != null)
		{
			background.color = Color.white;
			gameObject.SetActive(false);
		}
		else
		{
			background.color = disabledColor;
			costText.text = "Разблокировать: " + SkinCosts.Instance.GetSkinCost(selectedSkin).ToString();
		}
	}
	
	public void SelectSkin(ShopSystem.E_SkinType skin)
	{
		selectedSkin = skin;
		UpdateUI();
	}
	
	public void TryBuy()
	{
		if(selectedSkin != null && ShopSystem.Instance.TryBuySkin(selectedSkin))
		{
			OnBuySkin?.Invoke(selectedSkin);
			UpdateUI();
		}
	}
}
