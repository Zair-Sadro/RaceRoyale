using System.Collections;
using UnityEngine;

public class ClaimButton : MonoBehaviour
{
	[SerializeField] private float _closeRouletteDelay = 1.5f;
	[SerializeField] private LevelRoulette _roulette;
	[SerializeField] private ParticleEmmiter _particles;
	[SerializeField] private DisplayCoins _displayCoins;
	
	public void TryClaim()
	{
		if(_roulette.IsRouletteActive)
		{
			_particles.Play();
			_roulette.Roll(OnRollEnd);
		}
		else
		{
			LevelSystem.Instance.NextLevel(GameEnd.Instance.PlayerFirst);
		}
	}
	
	private void OnRollEnd(float reward)
	{
		_displayCoins.PlayMultiplyAnim(reward);
		StartCoroutine(CloseRoulette(reward));
	}
	
	private IEnumerator CloseRoulette(float reward)
	{
		yield return new WaitForSecondsRealtime(_closeRouletteDelay);
		PlayerStats.Instance.AdditionScoreMultiplier = reward;
		LevelSystem.Instance.NextLevel(GameEnd.Instance.PlayerFirst);
	}
}
