using UnityEngine;

public class NpcSpawner : Singleton<NpcSpawner>
{
	[SerializeField] private NpcController _prefab;
	[HideInInspector] public NpcController Opponent;
	
	public void Spawn(Vector3 position, Transform parent, Car[] _cars)
	{
		Opponent = Instantiate(_prefab, position, _prefab.transform.rotation, parent);
		Opponent._switcher.SetAvailableCars(_cars);
		Opponent.Init();
	}
}
