using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DisplayLevel : MonoBehaviour
{
	[SerializeField] private TMP_Text _currentLevel;
	[SerializeField] private TMP_Text _nextLevel;
	[SerializeField] private Image _nextLevelBackground;
	[SerializeField] private Color _baseColor;
	[SerializeField] private Color _rouletteLevelColor;
	
	private void OnEnable()
	{
		_baseColor = _nextLevelBackground.color;
		LevelSystem.OnLevelChange += OnLevelChange;
		OnLevelChange(LevelSystem.Instance.CurrentLevel);
	}
	
	private void OnDisable()
	{
		_nextLevelBackground.color = _baseColor;
		LevelSystem.OnLevelChange -= OnLevelChange;
	}
	
	private void OnLevelChange(int level)
	{
		_currentLevel.text = $"{level}";
		
		if((level + 1) % 5 == 0)
			_nextLevelBackground.color = _rouletteLevelColor;
		else
			_nextLevelBackground.color = _baseColor;
		_nextLevel.text = $"{level + 1}";
	}
}