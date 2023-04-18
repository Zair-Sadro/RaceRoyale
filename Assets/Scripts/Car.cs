using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	public Type carType;
	public bool CanBreakWalls = false;
	public bool CanBreakObstacles = false;
	public bool CanFly = false;
	public float SpawnHeight = 1f;
	public Platform.Type FastRoad;
	public Passability[] Speed;
	public bool IsPlayer = false;
	[HideInInspector] public Movement _movement;
	public List<Platform.Type> PassableRoadTypes;
	
	public void SetPlatform(Platform platform) => _movement.SetPlatform(platform);
	public void Init(Movement movement)
	{
		_movement = movement;
		if(movement is PlayerMovement)
		{
			IsPlayer = true;
		}
	}
	
	public float GetSpeed(Platform.Type type)
	{
		foreach(var p in Speed)
		{
			if(p.RoadType == type)
			{
				return p.Speed;
			}
		}
		return Speed[0].Speed;
	}
	
	[System.Serializable]
	public struct Passability
	{
		public Platform.Type RoadType;
		public float Speed;
	}
	
	[System.Serializable]
	public enum Type
	{
		Sportcar = 0,
		Bike,
		Truck,
		Boat,
		Helicopter,
		Suv
	}
}
