using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private Type _obstacleType;
	[SerializeField] private bool _pushLeft = true;
	[SerializeField] private float _pushForce = 5f;
	private Rigidbody _rigi;
	
	private void Start()
	{
		_rigi = GetComponent<Rigidbody>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(enabled && other.TryGetComponent(out Car car))
		{
			Rigidbody carRigi = car._movement.Rigi;
			if((car.CanBreakWalls && _obstacleType == Type.Wall)
				|| (car.CanBreakObstacles && _obstacleType == Type.Obstacle))
			{
				EnablePhys();
				Destroy(gameObject, 1f);
				enabled = false;
			}
			else
			{
				carRigi.velocity = Vector3.zero;
				carRigi.AddForce(transform.right * _pushForce, ForceMode.Impulse);
			}
		}
	}
	
	public void OnCollisionEnter(Collision collisionInfo)
	{
		if(_obstacleType != Type.Unbreakable && collisionInfo.gameObject.TryGetComponent(out PlayerMovement player))
		{
			_rigi.AddForce(transform.forward * 5f * (_pushLeft ? -1f : 1f), ForceMode.Impulse);
			_rigi.AddForce(transform.right * -2f, ForceMode.Impulse);
		}
	}
	
	public void EnablePhys()
	{
		_rigi.isKinematic = false;
	}
    
	public enum Type
	{
		Obstacle,
		Wall,
		Unbreakable
	}
}
