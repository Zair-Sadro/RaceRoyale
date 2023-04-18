using AndroidNativeCore;
using UnityEngine;

public class CarSwitcher : MonoBehaviour
{
	public Car[] _cars;
	[SerializeField] private LayerMask _platformMask;
	[SerializeField] private ParticleSystem _switchParticles;
	
	[HideInInspector] public Car[] availableCars;
	private Car.Type _selectedType;
	private Movement _movement;
	private Car _currentCar;
	
	public static event System.Action<Car.Type> OnCarSwitch;
	
	private void Awake()
	{
		_movement = GetComponent<Movement>();
		InitCars();
	}
    
	private void InitCars()
	{
		foreach(Car c in _cars)
		{
			c.Init(_movement);
		}
	}
	
	public void SetAvailableCars(Car[] _cars)
	{
		availableCars = _cars;
		ResetCar();
	}
	
	public void ResetCar()
	{
		SetCar(availableCars[0].carType);
		_movement.ResetPosition();
	}
	
	public void SetCarByIndex(int index)
	{
		if(index >= availableCars.Length)
			index = availableCars.Length - 1;
		SetCar(availableCars[index].carType);
	}

	public void SetCar(int type)
	{
		if(type > 5)
			type = 5;
		SetCar((Car.Type)type);
	}
	
	public void SetCar(Car.Type type)
	{
		int index = (int)type;
		if(_cars[index] != _currentCar)
		{
			// Disable current car
			_currentCar?.gameObject.SetActive(false);
			
			// Init new car
			_currentCar = _cars[index];
			_currentCar.gameObject.SetActive(true);
			_movement.InitCar(_currentCar);
			if(_movement is PlayerMovement)
				OnCarSwitch?.Invoke(_currentCar.carType);
			
			// Particles
			if(_switchParticles != null)
				_switchParticles.Play();
			
			// Translate new car to new height
			RaycastHit hit;
			if(Physics.Raycast(transform.position + transform.forward * 1.2f, Vector3.down, out hit, float.PositiveInfinity, _platformMask))
			{
				Vector3 newPos = transform.position;
				newPos.y = hit.point.y + _currentCar.SpawnHeight;
				transform.position = newPos;
			}
			else transform.position += Vector3.up * 0.4f;
		}
	}
}
