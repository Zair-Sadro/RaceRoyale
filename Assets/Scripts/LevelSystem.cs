using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelSystem : Singleton<LevelSystem>
{
	[Header("Settings")]
	public int CurrentLevel = 1; 
	public int LevelPlayCount = 0; 
	
	[Header("Dependencies")]
	[SerializeField] private MapGenerator _generator;
	[SerializeField] private GameEnd _gameEnd;
	[SerializeField] private CarSwitcher _player;
	[SerializeField] private Animator _cameraAnim;
	[SerializeField] private SelectVehicleButton[] _selectButtons;
	public int levelProgress = 0;
	public Car[] availableCars;
	public bool IsVictoryLast = false;
	
	[SerializeField] private float _levelTimer = 0f;
	
	public static event System.Action OnLevelFinish;
	public static event System.Action<int> OnLevelChange;
	public static event System.Action<int, int> OnLevelProgressChange;
	
	#region Private Methods
	private void InvokeLevelChange() => OnLevelChange?.Invoke(CurrentLevel);
	
	private void OnEnable()
	{ 
		PlayerStats.OnSceneLoad += InvokeLevelChange;
		Platform.OnPlayerEnter += OnPlatformEnter;
	}
	
	private void OnDisable()
	{
		PlayerStats.OnSceneLoad -= InvokeLevelChange;
		Platform.OnPlayerEnter -= OnPlatformEnter;
	}
	
	private void Start()
	{
		PrepareScene();
		Time.timeScale = 0f;
		DontDestroyOnLoad(gameObject);
	}
	
	private void OnPlatformEnter(Platform p)
	{
		levelProgress++;
		OnLevelProgressChange?.Invoke(levelProgress, _generator._platformPerLevel);
	}
	
	private void ResetGame()
	{
		IsVictoryLast = false;
		_levelTimer = 0f;
		_gameEnd.Reset();
		ClearLevelProgress();
		_player.ResetCar();
		PlayerStats.Instance.Reset();
	}
	
	private Car[] GetRandomCars(int amount = 3)
	{
		List<Car> allCars = new List<Car>();
		allCars.AddRange(_player._cars);
		
		Car[] cars = new Car[3];
		for(int i = 0; i < amount; i++)
		{
			int index = Random.Range(0, allCars.Count);
			cars[i] = allCars[index];
			allCars.RemoveAt(index);
		}
		return cars;
	}
	
	public void FinishLevel(bool victory, bool leave = false)
	{
		IsVictoryLast = victory;
		StopCoroutine(LevelTimer());
		//SendFinishLevelMetrics(victory, leave);
	}
	
	private void RunLevel()
	{
		LevelPlayCount++;
		ResetGame();
		StartCoroutine(LevelTimer());
		//SendStartLevelMetrics();
		UIManager.Instance.EnableGameUI();
	}
	
	private void SendFinishLevelMetrics(bool victory, bool leave)
	{
		Dictionary<string, object > parameters = new Dictionary<string, object>();
		parameters.Add("level_number", CurrentLevel);
		parameters.Add("level_count", LevelPlayCount);
		parameters.Add("level_type", CurrentLevel % 5 == 0 ? "bonus" : "normal");
		parameters.Add("result", leave ? "leave" : (victory ? "win" : "lose"));
		parameters.Add("time", _levelTimer.ToString("0.00"));
		AppMetrica.Instance.ReportEvent("level_finish", parameters);
		AppMetrica.Instance.SendEventsBuffer();
	}
	
	private void SendStartLevelMetrics()
	{
		Dictionary<string, object > parameters = new Dictionary<string, object>();
		parameters.Add("level_number", CurrentLevel);
		parameters.Add("level_count", LevelPlayCount);
		parameters.Add("level_type", CurrentLevel % 5 == 0 ? "bonus" : "normal");
		AppMetrica.Instance.ReportEvent("level_start", parameters);
		AppMetrica.Instance.SendEventsBuffer();
	}
	
	private IEnumerator LevelTimer()
	{
		while(true)
		{
			yield return new WaitForEndOfFrame();
			_levelTimer += Time.deltaTime;
		}
	}
	#endregion
	
	#region Public Methods
	public void StartLevel()
	{
		//PrepareScene();
		RunLevel();
	}
	
	public void PrepareScene()
	{
		availableCars = GetRandomCars(3);
		for(int i = 0; i < _selectButtons.Length; i++)
		{
			_selectButtons[i].Init(availableCars[i].carType);
		}
		_player.SetAvailableCars(availableCars);
		_generator.GenerateTrack(availableCars);
	}
	
	public void NextLevel(bool victory)
	{
		if(victory)
			CurrentLevel++;
		Time.timeScale = 1f;
		InvokeLevelChange();
		ClearLevelProgress();
		PlayerStats.Instance.ClaimCoins();
		_player.ResetCar();
		_cameraAnim.SetTrigger("ToIdle");
		_cameraAnim.transform.rotation= Quaternion.identity;
		PrepareScene();
		UIManager.Instance.EnableMainMenu();
		OnLevelFinish?.Invoke();
	}
	
	public void ClearLevelProgress()
	{
		levelProgress = 0;
		OnLevelProgressChange?.Invoke(levelProgress, _generator._platformPerLevel);
	}
	#endregion
	
    
	#region DEBUG
	protected void Update()
	{
		if(Input.GetKeyDown(KeyCode.N))
			NextLevel(true);
	}
	#endregion
}
