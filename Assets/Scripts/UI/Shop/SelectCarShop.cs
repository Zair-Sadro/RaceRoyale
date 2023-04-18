using UnityEngine;

public class SelectCarShop : MonoBehaviour
{
	public Car.Type _type;
	
	public void Select()
	{
		ShopSystem.Instance.UpdateSkinButtons(_type);
	}
}
