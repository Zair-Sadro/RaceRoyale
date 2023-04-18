using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] protected float _speed = 1f;
	[SerializeField] protected float _finishSpeedMultiplier = 2.8f;
	[SerializeField] protected float _speedMultiplier = 1f;
	[HideInInspector] public Car CurrentCar = null;
	public Platform _currentPlatform = null;
	
	[Header("Fly Settings")]
	protected bool _flyMoving = false;
	[SerializeField] protected float _targetHeight = 3.15f;
	[SerializeField] protected float _speedY = 1f;

	private Rigidbody _rigi;
	public Rigidbody Rigi 
	{
		get
		{
			if(_rigi == null) _rigi = GetComponent<Rigidbody>();
			return _rigi;
		}
		set{_rigi = value;}
	}
	
	public virtual void ResetPosition(){}
	
	protected virtual void FixedUpdate()
	{
		if(_flyMoving)
		{
			if(transform.position.y < _targetHeight)
				Rigi.AddForce(transform.up * _speedY, ForceMode.Acceleration);
			else
				Rigi.AddForce(-transform.up * _speedY, ForceMode.Acceleration);
		}
		float maximumVelocity = _speed * 3f;
		if(Rigi.velocity.magnitude < maximumVelocity)
		{
			Rigi.AddForce(transform.forward * _speed * _speedMultiplier, ForceMode.Acceleration);
		}
	}
	
	protected void UpdateSpeed()
	{
		if(_currentPlatform != null)
			_speed = CurrentCar.GetSpeed(_currentPlatform.RoadType);
		else
			_speed = CurrentCar.GetSpeed(Platform.Type.Flat);
	}
	
	
	public void InitCar(Car car)
	{
		CurrentCar = car;
		_flyMoving = CurrentCar.CanFly;
		Rigi.useGravity = !_flyMoving;
		Rigi.velocity *= 0.6f;
		UpdateSpeed();
	}
	
	public void SetPlatform(Platform platform)
	{
		_currentPlatform = platform;
		UpdateSpeed();
	}
	
	public virtual void Disable(bool makeDush = true)
	{
		enabled = false;
	}
}
