using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSkin : MonoBehaviour
{
	[SerializeField] private CarSkins[] _cars;
	
	private Dictionary<Car.Type, int> _selectedSkins;
	[HideInInspector] public Dictionary<Car.Type, int> SelectedSkins
	{
		get
		{
			if(_selectedSkins == null)
			{
				_selectedSkins = new Dictionary<Car.Type, int>();
				_selectedSkins.Add(Car.Type.Bike, 0);
				_selectedSkins.Add(Car.Type.Boat, 0);
				_selectedSkins.Add(Car.Type.Helicopter, 0);
				_selectedSkins.Add(Car.Type.Sportcar, 0);
				_selectedSkins.Add(Car.Type.Suv, 0);
				_selectedSkins.Add(Car.Type.Truck, 0);
			}
			return _selectedSkins;
		}
	}
	
	public void SetSkin(Car.Type car, int skin)
	{
		SelectedSkins[car] = skin;
		_cars[(int)car].SetSkin(SelectedSkins[car]);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		if(Input.GetKeyDown(KeyCode.F))
		foreach(var s in SelectedSkins)
		{
			print(s.Key.ToString() + ":   " + s.Value.ToString());
		}
	}
}
