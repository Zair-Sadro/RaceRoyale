using UnityEngine;

public class SkinCosts : Singleton<SkinCosts>
{
	public SkinCost[] Skins;
	
	public int GetSkinCost(ShopSystem.E_SkinType type)
	{
		for(int i = 0; i < Skins.Length; i++)
		{
			if(Skins[i].type == type)
			{
				return Skins[i].Coins;
			}
		}
		return Skins[0].Coins;
	}
	
	[System.Serializable]
	public class SkinCost
	{
		public ShopSystem.E_SkinType type;
		public int Coins;
		
		public SkinCost(ShopSystem.E_SkinType _type, int _coins)
		{
			Coins = _coins;
			type = _type;
		}
	}
}
