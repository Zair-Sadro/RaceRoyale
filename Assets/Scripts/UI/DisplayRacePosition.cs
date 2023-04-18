using UnityEngine;
using TMPro;

public class DisplayRacePosition : MonoBehaviour
{
	[SerializeField] private TMP_Text _lable;
	
	private void OnEnable()
	{
		if(GameEnd.Instance.PlayerFirst)
		{
			_lable.text = "1 / 2";
		}
		else
		{
			_lable.text = "2 / 2";
		}
	}
}
