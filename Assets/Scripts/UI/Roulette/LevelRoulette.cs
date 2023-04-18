using System.Collections;
using UnityEngine;

public class LevelRoulette : MonoBehaviour
{
	[SerializeField] private Animator _arrow;
	[SerializeField] private GameObject _holder;
	[SerializeField] private RouletteChunk[] _chunks;
	[SerializeField] private int _activatePerLevel = 5;
	[HideInInspector] public bool IsRouletteActive;
	private float _reward = 1f;
	
	private void OnEnable()
	{
		if(LevelSystem.Instance.CurrentLevel % _activatePerLevel == 0 && LevelSystem.Instance.IsVictoryLast)
		{
			_holder.SetActive(true);
		}
		else
		{
			_holder.SetActive(false);
			return;
		}
		IsRouletteActive = _holder.activeSelf;
		_arrow.enabled = true;
		_arrow.SetTrigger("Play");
	}
	
	public void Roll(System.Action<float> callback)
	{
		_arrow.enabled = false;
		_arrow.SetTrigger("Stop");
		
		RouletteChunk closest = null;
		float smallestDistance = 9999f;
		foreach(var ch in _chunks)
		{
			float currentDistance = Vector3.Distance(ch.transform.position, _arrow.transform.position);
			if(closest == null || smallestDistance > currentDistance)
			{
				smallestDistance = currentDistance;
				closest = ch;
			}
		}
		_reward = closest.Reward;
		callback.Invoke(_reward);
	}
}