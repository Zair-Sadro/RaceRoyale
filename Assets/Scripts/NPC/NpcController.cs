using UnityEngine;

public class NpcController : Movement
{
	[Header("Settings")]
	[SerializeField] private float _reactMultiplyPerLevel = 0.9f;
	[SerializeField] private float _minReactTime = 0f;
	[SerializeField] private float _maxReactTime = 1f;
	[SerializeField] private ParticleSystem _firework;
	public CarSwitcher _switcher;
	private Platform targetPlatform;
	
	public void Init()
	{
		InitReactTime();
		SelectCar();
	}
	
	private void InitReactTime()
	{
		int level = LevelSystem.Instance.CurrentLevel;
		if(_minReactTime > 0f)
		{
			for(int i = 0; i < level; i++)
			{
				_minReactTime *= _reactMultiplyPerLevel;
			}
		}
		if(_maxReactTime > 0f)
		{
			for(int i = 0; i < level; i++)
			{
				_maxReactTime *= _reactMultiplyPerLevel;
			}
		}
	}
	 
	public override void Disable(bool makeDush = true)
	{
		if(enabled == false)
			return;
		if(makeDush)
		{
			Rigi.drag = 1f; 
			Rigi.velocity = transform.forward * (9 * _finishSpeedMultiplier);
		}
		else
		{
			Rigi.velocity *= 0.2f;
		}
		
		base.Disable();
	}
	
	public override void ResetPosition()
	{
		_firework.Stop();
		enabled = true;
		Rigi.velocity = Vector3.zero;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(targetPlatform == null && other.TryGetComponent(out targetPlatform) )
		{
			SelectCar();
		}
	}
	
	public void SelectCar()
	{
		Invoke(nameof(SelectBestCar), Random.Range(_minReactTime, _maxReactTime));
	}
	
	private void SelectBestCar()
	{
		_switcher.SetCar(GetFastestCar(targetPlatform));
		targetPlatform = null;
	}
	
	private Car.Type GetFastestCar(Platform platform)
	{
		Car fastest = null;
		Platform.Type platformType;
		if(platform == null)
			platformType = Platform.Type.Flat;
		else
			platformType = platform.RoadType;
			
		foreach(Car c in _switcher.availableCars)
		{
			if(c.FastRoad == platformType)
			{
				return c.carType;
			}
			if(fastest == null || c.GetSpeed(platformType) > fastest.GetSpeed(platformType))
			{
				fastest = c;
			}
		}
		return fastest.carType;
	}
}
