using UnityEngine;
using DG.Tweening;
using TMPro;

public class VictoryPanel : MonoBehaviour
{
	[SerializeField] private float _scaleTime = 1f;
	[SerializeField] private TMP_Text _topText;
	
	protected void OnEnable()
	{
		if(GameEnd.Instance.PlayerFirst)
			_topText.text = "Level COMPLETED!";
		else
			_topText.text = "Try again!";
		
		transform.localScale = Vector3.zero;
		transform.DOKill();
		transform.DOScale(Vector3.one, _scaleTime).SetUpdate(true);
	}
}
