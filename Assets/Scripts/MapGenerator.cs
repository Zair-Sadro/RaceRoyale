using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	[SerializeField] private Transform _opponentStartPos;
	[SerializeField] private Transform _opponentRoadPos;
	[SerializeField] private GameObject _finishPrefab;
	[SerializeField] private Platform[] _platformsPrefabs;
	public int _platformPerLevel = 5;
	private Platform _lastPlatform;
	private int _prevSpawnIndex = -1;
	private Transform _trackParent = null;
	private Transform _trackOppParent = null;
	private List<Platform> _prefabsForSpawn;
	private Car[] _cars;

	public void GenerateTrack(Car[] cars)
	{
		if(LevelSystem.Instance.CurrentLevel == 1)
			SpawnTutorialLevel(cars);
		else
			SpawnCommonLevel(cars);
	}
	
	public void SpawnTutorialLevel(Car[] cars)
	{
		_cars = cars;
		ChoiseAvailabelPlatforms(_cars);
		_prevSpawnIndex = -1;
		_lastPlatform = null;
		if(_trackParent != null)
			Destroy(_trackParent.gameObject);
		if(_trackOppParent != null)
			Destroy(_trackOppParent.gameObject);
		_trackParent = new GameObject("Map").transform;
		
		StartCoroutine(SpawnTutorialPlatforms());
	}
	
	public void SpawnCommonLevel(Car[] cars)
	{
		_cars = cars;
		ChoiseAvailabelPlatforms(_cars);
		_prevSpawnIndex = -1;
		_lastPlatform = null;
		if(_trackParent != null)
			Destroy(_trackParent.gameObject);
		if(_trackOppParent != null)
			Destroy(_trackOppParent.gameObject);
		_trackParent = new GameObject("Map").transform;
		StartCoroutine(SpawnPlatforms());
	}
	
	private IEnumerator SpawnTutorialPlatforms()
	{
		SpawnBasePlatform();
		SpawnPlatfromForCar(_cars[1]);
		for(int i = 0; i < _platformPerLevel - 1; i++)
		{
			yield return null;
			SpawnPlatform();
		}
		SpawnFinish();
		GenerateOppTrack();
		NpcSpawner.Instance.Spawn(_opponentStartPos.position, 
			_trackOppParent, _cars);
	}
	
	private IEnumerator SpawnPlatforms()
	{
		SpawnBasePlatform();
		for(int i = 0; i < _platformPerLevel; i++)
		{
			yield return null;
			SpawnPlatform();
		}
		SpawnFinish();
		GenerateOppTrack();
		NpcSpawner.Instance.Spawn(_opponentStartPos.position, 
			_trackOppParent, _cars);
	}
	
	private void GenerateOppTrack()
	{
		_trackOppParent = Instantiate(_trackParent);
		_trackOppParent.position = _opponentRoadPos.position;
	}
    
	private void ChoiseAvailabelPlatforms(Car[] cars)
	{
		_prefabsForSpawn = new List<Platform>();
		foreach(Platform p in _platformsPrefabs)
		{
			foreach(Car c in cars)
			{
				if(c.PassableRoadTypes.Contains(p.RoadType))
				{
					_prefabsForSpawn.Add(p);
					break;
				}
			}
		}
	}
    
	private void SpawnFinish()
	{
		Vector3 spawnPosition = _lastPlatform.Connection.position;
		_lastPlatform = Instantiate(_platformsPrefabs[0], spawnPosition, Quaternion.identity, _trackParent);
		spawnPosition = _lastPlatform.Connection.position;
		Instantiate(_finishPrefab, spawnPosition, Quaternion.identity, _trackParent);
	}
	
	private void SpawnBasePlatform()
	{
		Vector3 spawnPosition = _lastPlatform == null ? transform.position : _lastPlatform.Connection.position;
		_lastPlatform = Instantiate(_platformsPrefabs[0], spawnPosition, Quaternion.identity, _trackParent);
	}
	
	private void SpawnPlatfromForCar(Car car)
	{
		Platform platform = _prefabsForSpawn[0];
		int prefabIndex = 0;
		for(int i = 0; i < _prefabsForSpawn.Count; i++)
		{
			if(car.PassableRoadTypes.Contains(_prefabsForSpawn[i].RoadType))
			{
				platform = _prefabsForSpawn[i];
				prefabIndex = i;
				break;
			}
		}
		_prevSpawnIndex = prefabIndex;
		Vector3 spawnPosition = _lastPlatform == null ? transform.position : _lastPlatform.Connection.position;
		_lastPlatform = Instantiate(platform, spawnPosition, Quaternion.identity, _trackParent);
	}
    
	private void SpawnPlatform()
	{
		int prefabIndex = 0;
		do
		{
			prefabIndex = Random.Range(0, _prefabsForSpawn.Count);
		}
		while(_prevSpawnIndex == prefabIndex);
		_prevSpawnIndex = prefabIndex;
		Vector3 spawnPosition = _lastPlatform == null ? transform.position : (_lastPlatform.Connection.position + Vector3.down * 0.01f);
		_lastPlatform = Instantiate(_prefabsForSpawn[prefabIndex], spawnPosition, Quaternion.identity, _trackParent);
	}
}
