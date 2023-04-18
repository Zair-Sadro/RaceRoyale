using UnityEngine;

public class PlayerMovement : Movement
{
	[Header("Settings")]
	[SerializeField] private float _speedParticlesVelocity = 8f;
	[SerializeField] private ParticleSystem _firework;
	[SerializeField] private ParticleSystem _speedEffect;
	
	[Header("Dependencies")]
	[SerializeField] private Transform _playerStart;

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		
		if(_speedEffect.isStopped && Rigi.velocity.magnitude >= _speedParticlesVelocity)
		{
			_speedEffect.Play();
		}
		else if(_speedEffect.isPlaying && Rigi.velocity.magnitude < _speedParticlesVelocity)
		{
			_speedEffect.Stop();
		}
	}
	
	public override void ResetPosition()
	{
		_firework.Stop();
		enabled = true;
		transform.position = _playerStart.position;
		Rigi.velocity = Vector3.zero;
		Rigi.drag = 0.5f;
	}
    
	public override void Disable(bool makeDush = true)
	{
		if(enabled == false)
			return;
		if(makeDush)
		{
			Rigi.drag = 1f;
			Rigi.velocity = transform.forward * ((PlayerStats.Instance.Fuel + 1) * _finishSpeedMultiplier);
		}
		else
		{
			Rigi.velocity *= 0.3f;
		}
		base.Disable();
	}
}
