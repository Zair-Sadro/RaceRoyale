using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LevelButton : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private Color _completeColor;
	[SerializeField] private Button _button;
	[SerializeField] private TMP_Text _text;
	
	public void UpdateUI(float scale, Color color, bool interactable, string text)
	{
		_button.transform.localScale = Vector3.one * scale;
		_button.image.color = color;
		_button.interactable = interactable;
		_text.text = text;
	}
	
	public void SetUncomplete()
	{
		_image.color = Color.white;
	}
	
	public void SetComplete()
	{
		_image.color = _completeColor;
	}
}
