
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;


        // Ваши сохранения

        public int coins = 0;
        public int level = 1;
        public List<(int, Car.Type )> purchasedCars = new List<(int, Car.Type)> 
        {
                    { (0, Car.Type.Bike)  },
                    { (0, Car.Type.Boat) },
                    { (0, Car.Type.Air) },
                    { (0, Car.Type.Sportcar) },
                    { (0, Car.Type.Suv) },
                    { (0, Car.Type.Truck) }
        };
        public Dictionary<Car.Type, int> selectedCar = new Dictionary<Car.Type, int>
        {
                    { Car.Type.Bike, 0 },
                    { Car.Type.Boat, 0 },
                    { Car.Type.Air, 0 },
                    { Car.Type.Sportcar, 0 },
                    { Car.Type.Suv, 0 },
                    { Car.Type.Truck, 0 }
        };

// Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
// Пока выявленное ограничение - это расширение массива


// Вы можете выполнить какие то действия при загрузке сохранений
public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

           

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }
    }
}
