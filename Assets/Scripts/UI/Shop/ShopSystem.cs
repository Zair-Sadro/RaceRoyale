using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : Singleton<ShopSystem>
{
    [SerializeField] private Sprite[] _suvIcons;
    [SerializeField] private Sprite[] _airIcons;
    [SerializeField] private Sprite[] _boatIcons;
    [SerializeField] private Sprite[] _truckIcons;
    [SerializeField] private Sprite[] _bikeIcons;
    [SerializeField] private Sprite[] _carIcons;

    [SerializeField] private SkinButton[] _skinButtons;
    public List<ShopSystem.E_SkinType> skinBought = new List<ShopSystem.E_SkinType>();

    public static event System.Action<ShopSystem.E_SkinType> OnSkinBuy;

    protected void Awake()
    {
        // Add base skins to list
        AddSkin(ShopSystem.E_SkinType.Air_1);
        AddSkin(ShopSystem.E_SkinType.Bike_1);
        AddSkin(ShopSystem.E_SkinType.Boat_1);
        AddSkin(ShopSystem.E_SkinType.Sportcar_1);
        AddSkin(ShopSystem.E_SkinType.SUV_1);
        AddSkin(ShopSystem.E_SkinType.Truck_1);

        UpdateSkinButtons(Car.Type.Sportcar);
    }

    public void UpdateSkinButtons(Car.Type type)
    {
        for (int i = 0; i < _skinButtons.Length; i++)
        {
            _skinButtons[i].UpdateUI(type, GetSkinType(type, i), i, GetIcons(type)[i]);
        }
    }

    public bool IsSkinAvailable(ShopSystem.E_SkinType skin)
    {
        if (skinBought.Contains(skin))
        {
            return true;
        }
        return false;
    }

    public bool IsSelected(Car.Type car, int skinID)
    {
        if (Player.Instance.SkinChanger.SelectedSkins[car] == skinID)
        {
            return true;
        }
        return false;
    }

    public void SetSkin(Car.Type type, int skin)
    {
        Player.Instance.SkinChanger.SetSkin(type, skin);

    }

    public bool TryBuySkin(ShopSystem.E_SkinType skin)
    {
        if (IsSkinAvailable(skin))
            return false;

        int cost = SkinCosts.Instance.GetSkinCost(skin);
        if (PlayerStats.Instance.TrySpendCoin(cost))
        {
            AddSkin(skin);
            return true;
        }
        return false;
    }

    private void AddSkin(ShopSystem.E_SkinType skin)
    {
        if (skinBought.Contains(skin) == false)
            skinBought.Add(skin);
    }

    public ShopSystem.E_SkinType GetSkinType(Car.Type type, int skin)
    {
        ShopSystem.E_SkinType skinType = (ShopSystem.E_SkinType)((int)type * 3 + skin);
        return skinType;
    }

    public int GetSkinIndex(Car.Type type, ShopSystem.E_SkinType skin)
    {
        int index = (int)skin - (int)type * 3;
        return index;
    }

    private Sprite[] GetIcons(Car.Type type)
    {
        switch (type)
        {
            case Car.Type.Bike:
                return _bikeIcons;
            case Car.Type.Boat:
                return _boatIcons;
            case Car.Type.Helicopter:
                return _airIcons;
            case Car.Type.Sportcar:
                return _carIcons;
            case Car.Type.Suv:
                return _suvIcons;

            case Car.Type.Truck:
                return _truckIcons;

        }
        return null;
    }

    public enum E_SkinType
    {
        Sportcar_1,
        Sportcar_2,
        Sportcar_3,
        Bike_1,
        Bike_2,
        Bike_3,
        Truck_1,
        Truck_2,
        Truck_3,
        Boat_1,
        Boat_2,
        Boat_3,
        Air_1,
        Air_2,
        Air_3,
        SUV_1,
        SUV_2,
        SUV_3,
    }
}
