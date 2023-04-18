using UnityEngine;

public class BonusPlatform : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Car car;
		if(enabled && other.TryGetComponent(out car))
		{
			enabled = false;
			if(car.IsPlayer)
			{
				bool first =  GameEnd.Instance.SetFirst(true);
				PlayerStats.Instance.ScoreMultiplier += 1f;
				if(GameEnd.Instance.playerArrive == false)
					GameEnd.Instance.FinishRun();
				GameEnd.Instance.playerArrive = true;
			}
			else
			{
				bool first = GameEnd.Instance.SetFirst(false);
				if(first)
				{
					car._movement.Disable(true);
				}
				else
				{
					car._movement.Disable(false);
				}
			}
		}
	}
}
