using UnityEngine;
using System;

public class DestroyZone : MonoBehaviour
{
	public static event Action<Platform> OnPlatformDestroy;
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.transform.parent != null && other.transform.parent.TryGetComponent(out Platform platform))
		{
			OnPlatformDestroy?.Invoke(platform);
			Destroy(platform.gameObject);
		}
	}
}
