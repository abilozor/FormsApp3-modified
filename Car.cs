using System;

namespace FormsApp2
{
    public class Car : CarSample
    {
        public Car()
        { }

        public Car(string modelName, string manufacturer, int yearOfProduction, 
            double engineCapacity, int seatCount, bool isElectric, double averageFuelConsumption)
        {
            ModelName = modelName;
            Manufacturer = manufacturer;
            YearOfProduction = yearOfProduction;
            EngineCapacity = engineCapacity;
            SeatCount = seatCount;
            IsElectric = isElectric;
            AverageFuelConsumption = averageFuelConsumption;
        }

        public double GetFuelConsumptionPer100Km()
        {
            return AverageFuelConsumption;
        }

        public override int GetCarAge()
        {
            return DateTime.Now.Year - YearOfProduction;
        }
    }
}
