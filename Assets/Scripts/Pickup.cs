using AndroidNativeCore;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
	[SerializeField] private float _destroyMultiplier = 1f;
	[SerializeField] private ParticleSystem _particles;
	
	private void OnTriggerEnter(Collider other)
	{
		Car car;
		if(other.TryGetComponent(out car))
		{
			if(car.IsPlayer)
			{
				PlayerStats.Instance.AddFuel(1);
				if(Settings.Instance.Vibration)
					Vibrator.Vibrate(100);
			}
			_particles.transform.parent = null;
			Destroy(gameObject);
		}
	}
	
	public void Kill(float _destroyDelay)
	{
		_particles.transform.parent = null;
		if(gameObject != null)
			Destroy(gameObject, _destroyDelay * _destroyMultiplier);
	}
	
	protected void OnDestroy()
	{
		if(_particles != null)
			_particles.Play();
	}
}
