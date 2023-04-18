using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	public GameObject ActivePanel = null;
	
	[Header("Dependencies")]
	public GameObject MainMenu = null;
	public GameObject GameUI = null;
	public GameObject VictoryPanel = null;
	
	public void EnablePanel(GameObject panel, bool stopTime = false)
	{
		if(ActivePanel != null)
	    	ActivePanel.SetActive(false);
	    ActivePanel = panel;
		ActivePanel.SetActive(true);
		Time.timeScale = stopTime ? 0f : 1f;
	}
    
	public void EnableMainMenu() => EnablePanel(MainMenu, true);
	public void EnableGameUI() => EnablePanel(GameUI, false);
	public void EnableVictoryPanel() => EnablePanel(VictoryPanel, true);
}
