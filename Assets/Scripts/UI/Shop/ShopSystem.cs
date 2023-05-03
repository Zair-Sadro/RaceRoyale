using System.Collections.Generic;
using UnityEngine;
using YG;
using CarType = Car.Type;


public class ShopSystem : Singleton<ShopSystem>
{
    [SerializeField] private Sprite[] _suvIcons;
    [SerializeField] private Sprite[] _airIcons;
    [SerializeField] private Sprite[] _boatIcons;
    [SerializeField] private Sprite[] _truckIcons;
    [SerializeField] private Sprite[] _bikeIcons;
    [SerializeField] private Sprite[] _carIcons;


    [SerializeField] private SkinButton[] _skinButtons;
    public List<(int, CarType)> skinBought = new List<(int, CarType)>();

    protected void Awake()
    {

    }

    public void UpdateSkinButtons(Car.Type type)
    {
        for (int i = 0; i < _skinButtons.Length; i++)
        {
            _skinButtons[i].UpdateUI(type, i, GetIcons(type)[i]);
        }
    }

    public bool IsSkinAvailable(int id, CarType type)
    {
        if (skinBought.Contains((id,type)))
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

    public bool TryBuySkin(int id, CarType type)
    {
        if (IsSkinAvailable(id,type))
            return false;

        int cost = SkinCosts.Instance.GetSkinCost(id, type);
        if (PlayerStats.Instance.TrySpendCoin(cost))
        {
            AddSkin(id,type);
            return true;
        }
        return false;
    }

    private void AddSkin(int id, CarType type)
    {
        if (skinBought.Contains((id,type)) == false)
            skinBought.Add((id, type));
    }

    //public ShopSystem.E_SkinType GetSkinType(Car.Type type, int skin)
    //{
    //    ShopSystem.E_SkinType skinType = (ShopSystem.E_SkinType)((int)type * 3 + skin);
    //    return skinType;
    //}

    //public int GetSkinIndex(Car.Type type, ShopSystem.E_SkinType skin)
    //{
    //    int index = (int)skin - (int)type * 3;
    //    return index;
    //}
    //public List<int> GetWehicleList(Car.Type type)
    //{
    //    switch (type)
    //    {
    //        case Car.Type.Sportcar:
    //            return SportCarID;
    //        case Car.Type.Bike:
    //            return BikeID;
    //        case Car.Type.Truck:
    //            return TruckID;
    //        case Car.Type.Boat:
    //            return BoatID;
    //        case Car.Type.Air:
    //            return AirID;
    //        case Car.Type.Suv:
    //            return SuvID;
    //    }
    //    return null;
    //}

    private Sprite[] GetIcons(Car.Type type)
    {
        switch (type)
        {
            case Car.Type.Bike:
                return _bikeIcons;
            case Car.Type.Boat:
                return _boatIcons;
            case Car.Type.Air:
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




    //public enum E_SkinType
    //{
    //    Sportcar_1,
    //    Sportcar_2,
    //    Sportcar_3,
    //    Bike_1,
    //    Bike_2,
    //    Bike_3,
    //    Truck_1,
    //    Truck_2,
    //    Truck_3,
    //    Boat_1,
    //    Boat_2,
    //    Boat_3,
    //    Air_1,
    //    Air_2,
    //    Air_3,
    //    SUV_1,
    //    SUV_2,
    //    SUV_3,
    //}
}
