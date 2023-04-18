using UnityEngine;

public class CoinPerDistance : MonoBehaviour
{
	[SerializeField] private int _metersPerCoin = 5;
	[SerializeField] private float _updateSpeed = 0.1f;
	[SerializeField] private float _startPos = -15f;
	private PlayerMovement _movement;
	private int _maxCoins = 0;
	
	protected void OnEnable()
	{
		LevelSystem.OnLevelFinish += Reset;
	}
	
	protected void OnDisable()
	{
		LevelSystem.OnLevelFinish -= Reset;
	}
	
	private void Reset()
	{
		_maxCoins = 0;
	}
	
    private void Start()
    {
	    _movement = GetComponent<PlayerMovement>();
	    InvokeRepeating(nameof(CalculateCoins), _updateSpeed, _updateSpeed);
    }

	private void CalculateCoins()
	{
		if(_movement.enabled && transform.position.x < _startPos)
		{
			int coins = Mathf.Abs((int)(transform.position.x - _startPos) / _metersPerCoin);
			
			if(coins > _maxCoins)
			{
		    	_maxCoins = coins;
			}
			PlayerStats.Instance.SetCoins(_maxCoins, true);
		}
    }
}