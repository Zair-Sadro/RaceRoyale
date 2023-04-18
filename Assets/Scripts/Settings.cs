using UnityEngine.UI;
using UnityEngine;

public class Settings : Singleton<Settings>
{
	public Toggle _toggle;	
	
	public bool Vibration = true;
	
	public void OnVibrationChange()
	{
		Vibration = _toggle.isOn;
	}
	
	public void SetVibration(bool enable)
	{
		_toggle.isOn = enable;
		Vibration = enable;
	}
}
