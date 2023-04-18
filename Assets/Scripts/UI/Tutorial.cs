using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private float _turnOnDelay = 0.7f;
	[SerializeField] private GameObject _holder;
	
	private void OnEnable()
	{
		CarSwitcher.OnCarSwitch += OnCarSwitch;
	}
	
	private void OnDisable()
	{
		CarSwitcher.OnCarSwitch -= OnCarSwitch;
	}
	
	private void Start()
	{
		if(LevelSystem.Instance.CurrentLevel == 1)
		{
			Invoke(nameof(TurnOn), _turnOnDelay);
		}
    }

	private void TurnOn()
	{
		Time.timeScale = 0f;
	    _holder.SetActive(true);
	}
    
	private void TurnOff()
	{
		Time.timeScale = 1f;
		_holder.SetActive(false);
		gameObject.SetActive(false);
	}
    
	private void OnCarSwitch(Car.Type car)
	{
		if(_holder.activeSelf)
			TurnOff();
		//CancelInvoke(nameof(TurnOn));
	}
}
