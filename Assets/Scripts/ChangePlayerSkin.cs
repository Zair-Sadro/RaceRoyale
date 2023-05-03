using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSkin : MonoBehaviour
{
	
	[SerializeField] private CarSkins[] _cars;
	
	private Dictionary<Car.Type, int> _selectedSkins;
	public Dictionary<Car.Type, int> SelectedSkins
	{
        //DONT CHANGE Orders of keys 
        get
        {
			if(_selectedSkins == null)
			{
                _selectedSkins = new Dictionary<Car.Type, int>
                {
                    { Car.Type.Sportcar, 0 },                    
                    { Car.Type.Bike, 0 },
                    { Car.Type.Truck, 0 },
                    { Car.Type.Boat, 0 },
                    { Car.Type.Air, 0 },                                                        
                    { Car.Type.Suv, 0 }
                };
            }
			return _selectedSkins;
		}
		set
		{
			_selectedSkins = new(value);
		}
	}
	
	public void SetSkin(Car.Type type, int id)
	{
		SelectedSkins[type] = id;
		_cars[(int)type].SetSkin(SelectedSkins[type]);

		Saver.Instance.SaveSelectedSkin(type, id);

	}

	public void InitFromSave(Dictionary<Car.Type, int> selectedCar)
	{
        SelectedSkins = selectedCar;
        var typeC = Car.Type.Sportcar;
        for (int i = 0; i < 6; i++)
		{
			_cars[i].SetSkin(selectedCar[typeC]);
			ShopSystem.Instance.UpdateSkinButtons(typeC);
			++typeC;
        }

    }
	
}
