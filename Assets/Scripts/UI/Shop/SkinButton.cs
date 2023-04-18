using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkinButton : MonoBehaviour
{
	[SerializeField] private ChangePlayerSkin _skinChanger;
	[SerializeField] private GameObject _selectedImage;
	[SerializeField] private ShopSystem.E_SkinType _type;
	[SerializeField] private Car.Type _carType;
	private int _skinID;
	public Image background;
	public Image _icon;
	public Color disabledColor = Color.grey;
	private bool _available = false;
	
	public GameObject _costPanel;
	public TMP_Text _costText;
	
	public void UpdateUI(Car.Type carType, ShopSystem.E_SkinType type, int skinID, Sprite icon = null)
	{
		_skinID = skinID;
		_carType = carType;
		_type = type;
		if(icon != null)
			_icon.sprite = icon;
		_available = ShopSystem.Instance.IsSkinAvailable(_type);
		
		if(_available)
		{
			background.color = Color.white;
			_icon.color = Color.white;
			_costPanel.SetActive(false);
		}
		else
		{
			background.color = disabledColor;
			_icon.color = disabledColor;
			_costText.text = SkinCosts.Instance.GetSkinCost(_type).ToString();
			_costPanel.SetActive(true);
		}
		
		if(ShopSystem.Instance.IsSelected(_carType, _skinID))
		{
			_selectedImage.SetActive(true);
		}
		else
		{
			_selectedImage.SetActive(false);
		}
	}
	
	public void SelectOrBuySkin()
	{
		if(_available)
		{
			_skinChanger.SetSkin(_carType, _skinID);
			ShopSystem.Instance.SetSkin(_carType, _skinID);
			ShopSystem.Instance.UpdateSkinButtons(_carType);
		}
		else
		{
			//Buy skin
			if(ShopSystem.Instance.TryBuySkin(_type))
			{
				UpdateUI(_carType, _type, _skinID);
			}
		}
	}
}
