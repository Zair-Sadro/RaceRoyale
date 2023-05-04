public class SkinCosts : Singleton<SkinCosts>
{
    public SkinCost[] SporCarSkins;
    public SkinCost[] BoatSkins;
    public SkinCost[] AirSkins;
    public SkinCost[] SUVSkins;
    public SkinCost[] BikeSkins;
    public SkinCost[] TruckSkins;

    public int GetSkinCost(int id, Car.Type type)
    {
        SkinCost[] skins = default;
        switch (type)
        {
            case Car.Type.Sportcar:
                skins = SporCarSkins;
                break;
            case Car.Type.Bike:
                skins = BikeSkins;             
                break;
            case Car.Type.Truck:
                skins = TruckSkins;
               
                break;
            case Car.Type.Boat:
                skins = BoatSkins;
              
                break;
            case Car.Type.Air:
                skins = AirSkins;             
                break;
            case Car.Type.Suv:
                skins = SUVSkins;
                break;

               
        }
        return skins[id].Coins;
    }

    [System.Serializable]
    public class SkinCost
    {
        public int Coins;

    }
}
