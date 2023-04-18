using UnityEngine;

public class Debris : MonoBehaviour
{
	[SerializeField] private Rigidbody[] _debris;
	
	public void Init(Vector3 impulse)
	{
		foreach(var rigi in _debris)
		{
			rigi.AddForce(impulse, ForceMode.Impulse);
		}
	}
}
