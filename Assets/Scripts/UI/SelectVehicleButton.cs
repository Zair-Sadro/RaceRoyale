using UnityEngine.UI;
using UnityEngine;

public class SelectVehicleButton : MonoBehaviour
{
	[SerializeField] private Sprite[] _sprites;
	[SerializeField] private float _selectedScale = 1.2f;
	[SerializeField] private GameObject _selectedImage;
	[SerializeField] private Image _icon;
	[SerializeField] private CarSwitcher _carSwitcher;
	[SerializeField] private Car.Type _carType;
	[SerializeField] private KeyCode buttonName;

	private void OnEnable()
	{
		CarSwitcher.OnCarSwitch += OnCarSwitch;
	}
	
	private void OnDisable()
	{
		CarSwitcher.OnCarSwitch -= OnCarSwitch;
		
	}
    private void Update()
    {
        if(Input.GetKeyDown(buttonName))
		{
			SwitchCar();
		}
    }
    private void OnCarSwitch(Car.Type carType)
	{
		if(carType == _carType)
		{
			transform.localScale = Vector3.one * _selectedScale;
			_selectedImage.SetActive(true);
		}
		else
		{
			transform.localScale = Vector3.one;
			_selectedImage.SetActive(false);
		}
	}

	public void Init(Car.Type type)
	{
	    _carType = type;
		_icon.sprite = _sprites[(int)_carType];
    }
    
	public void SwitchCar()
	{
		_carSwitcher.SetCar(_carType);
		AudioManager.Instance.CarSwitch();
	}
}
