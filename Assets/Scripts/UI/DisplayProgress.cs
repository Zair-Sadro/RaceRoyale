using UnityEngine.UI;
using UnityEngine;

public class DisplayProgress : MonoBehaviour
{
	[SerializeField] private Slider _slider;

	private void OnDisable() => LevelSystem.OnLevelProgressChange -= UpdateUI;
	private void OnEnable()
	{
		LevelSystem.OnLevelProgressChange += UpdateUI;
		_slider.value = 0f;
	}
	
	public void UpdateUI(int current, int maximum)
	{
		_slider.value = (float)current / (float)maximum;
	}
}
