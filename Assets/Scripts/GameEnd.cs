using UnityEngine;

public class GameEnd : Singleton<GameEnd>
{
	[SerializeField] private float _updateSpeed = 0.5f;
	[SerializeField] private ParticleSystem _firework;
	[SerializeField] private PlayerMovement _player;
	[SerializeField] private Animator _cameraAnim;
	private bool _checkVelocity = false;
	
	[Header("Dependencies")]
	[SerializeField] private GameObject VictoryPanel;
	
	[HideInInspector] public bool PlayerFirst = false;
	[HideInInspector] public bool firstAwailable = true;
	[HideInInspector] public bool playerArrive = false;
	
	public bool SetFirst(bool _playerFirst)
	{
		if(firstAwailable)
		{
			PlayerFirst = _playerFirst;
			firstAwailable = false;
			return true;
		}
		return false;
	}
	
	public void FinishRun()
	{
		if(PlayerFirst)
		{
			if(_checkVelocity == false)
			{
				_checkVelocity = true;
				InvokeRepeating(nameof(CheckVelocity), _updateSpeed, _updateSpeed);
				_player.Disable(true);
			}
		}
		else
		{
			_player.Disable(false);
			Invoke(nameof(SetLose), 2f);
		}
	}
	
	private void CheckVelocity()
	{
		if(_player.Rigi.velocity.magnitude >= -0.02f 
			&& _player.Rigi.velocity.magnitude <= 0.02f)
		{
			SetVictory();
		}
	}
	
	private void SetLose()
	{
		PlayerStats.Instance.ApplyBaseCoinMultiplier();
		_cameraAnim.SetTrigger("Rotation");
		LevelSystem.Instance.FinishLevel(false);
		Time.timeScale = 0f;
		UIManager.Instance.EnablePanel(VictoryPanel);
	}
	
	private void SetVictory()
	{
		CancelInvoke(nameof(CheckVelocity));
		_firework.Play();
		PlayerStats.Instance.ApplyBaseCoinMultiplier();
		_cameraAnim.SetTrigger("Rotation");
		LevelSystem.Instance.FinishLevel(true);
		Time.timeScale = 0f;
		UIManager.Instance.EnablePanel(VictoryPanel);
	}
	
	public void Reset()
	{
		playerArrive = false;
		PlayerFirst = false;
		_checkVelocity = false;
		firstAwailable = true;
	}
}
