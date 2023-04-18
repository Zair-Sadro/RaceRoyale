using UnityEngine;

public class CarSkins : MonoBehaviour
{
	public GameObject[] Skins;
	[SerializeField] private GameObject _activeSkin;
	
	public void SetSkin(int skin)
    {
	    if(_activeSkin != null)
	    	_activeSkin.SetActive(false);
	    //print(skin);
	    _activeSkin = Skins[skin];
	    _activeSkin.SetActive(true);
    }
}
