using UnityEngine;

public class Destructable : MonoBehaviour
{
	[SerializeField] private GameObject _model;
	[SerializeField] private Debris _debris;
	[SerializeField] private bool _pushLeft = true;
	[SerializeField] private float _pushForce = 5f;
	[SerializeField] private float _destroyDelay = 2f;
	
	private void OnTriggerEnter(Collider other)
	{
		if(enabled && other.TryGetComponent(out Car car))
		{
			car._movement.Rigi.velocity *= 0.65f;
			enabled = false;
			_model.SetActive(false);
			SpawnDebris();
			Destroy(gameObject, _destroyDelay);
		}
	}
	
	public void SpawnDebris()
	{
		Debris debris = Instantiate(_debris, transform.position, Quaternion.identity, transform);
		debris.Init(transform.forward * 5f * (_pushLeft ? -1f : 1f));
	}
}
