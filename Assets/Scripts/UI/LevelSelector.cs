using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelSelector : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float _currentButtonSize = 1.2f;
	[SerializeField] private Color _currentButtonColor = Color.yellow;
	[SerializeField] private Color _defaultButtonColor = Color.white;
	[SerializeField] private Color _rouletteButtonColor = Color.white;
	
	[Header("Dependencies")]
	[SerializeField] private LevelButton[] _levelButtons;
	private int _currentLevelButton = 0;
	
	private void OnEnable()
    {
	    UpdateDisplay();
    }
    
	private void UpdateDisplay()
	{
		CalculateButtonIndex();
		EnableCurrentButton();
	}
	
	private void EnableCurrentButton()
	{
		int currentLevel = LevelSystem.Instance.CurrentLevel;
		for(int i = 0; i < _levelButtons.Length; i++)
		{
			if(i == _currentLevelButton)
			{
				_levelButtons[i].UpdateUI(_currentButtonSize, _currentButtonColor, true, currentLevel.ToString());
				_levelButtons[i].SetUncomplete();
			}
			else if((currentLevel - _currentLevelButton + i) % 5 == 0)
			{
				if(i < _currentLevelButton)
				{
					_levelButtons[i].SetComplete();
				}
				else
				{
					_levelButtons[i].SetUncomplete();
				}
				_levelButtons[i].UpdateUI(1f, _rouletteButtonColor, false, (currentLevel - _currentLevelButton + i).ToString());
			}
			else
			{
				_levelButtons[i].UpdateUI(1f, _defaultButtonColor, false, (currentLevel - _currentLevelButton + i).ToString());
				if(i < _currentLevelButton)
				{
					_levelButtons[i].SetComplete();
				}
				else
				{
					_levelButtons[i].SetUncomplete();
				}
			}
		}
	}
	
	private void CalculateButtonIndex()
	{
		int i = LevelSystem.Instance.CurrentLevel;
		
		if(i == 1)
			_currentLevelButton = 0;
		else if(i == 2)
			_currentLevelButton = 1;
		else
			_currentLevelButton = 2;
	}
}
