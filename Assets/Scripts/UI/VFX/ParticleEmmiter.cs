using System.Collections;
using UnityEngine;

public class ParticleEmmiter : MonoBehaviour
{
	[Header("Settings")]
	public bool IsActive = false;
	[SerializeField] private float _followSpeed = 0.3f;
	[SerializeField] private float _followDelay = 1f;
	[SerializeField] private Transform _destination = null;
	
	[Header("Dependency")]
	[SerializeField] private ParticleSystem _system;
	private ParticleSystem.Particle[] _particles;
	
	private void Awake()
	{
		if (_particles == null || _particles.Length < _system.main.maxParticles)
			_particles = new ParticleSystem.Particle[_system.main.maxParticles];
	}
	
	public void Play()
	{
		_system.Play();
		StartCoroutine(Activate());
	}
	
	private IEnumerator Activate() 
	{
		yield return new WaitForSecondsRealtime(_followDelay);
		IsActive = true;
	}
    
	private void LateUpdate()
	{
		if(IsActive == false || _destination == null)
			return;
			
		int numParticlesAlive = _system.GetParticles(_particles);

		Vector3 direction;
		Vector3 vector;

		for (int i = 0; i < numParticlesAlive; i++)
		{
			vector = _destination.position - _particles[i].position;
			if(vector.magnitude <= 0.02f)
			{
				_particles[i].remainingLifetime = 0;
			}
			else
			{
				direction = Vector3.Normalize(vector);
				_particles[i].velocity = direction * _followSpeed;
			}
		}

		_system.SetParticles(_particles, numParticlesAlive);
	}
}
