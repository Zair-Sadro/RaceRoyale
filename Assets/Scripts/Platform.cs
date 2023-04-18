using UnityEngine;

public class Platform : MonoBehaviour
{
	public Type RoadType;
	public Transform Connection;
	[SerializeField] private float _destroyDelay = 1f;
	[SerializeField] private Pickup _fuel;
	private bool _isArrived = false;
	
	public static event System.Action<Platform> OnPlayerEnter;
    
	private void OnTriggerEnter(Collider other)
	{
		Car car;
		if(!_isArrived && other.TryGetComponent(out car))
		{
			_isArrived = true;
			car.SetPlatform(this);
			if(car.IsPlayer)
			{
				if(_fuel != null)
					_fuel.Kill(_destroyDelay);
				OnPlayerEnter?.Invoke(this);
			}
		}
	}
	
	public enum Type
	{
		Flat,
		Obstacles,
		Walls,
		Water,
		Ravine,
		Offroad,
		Bonus
	}
}
