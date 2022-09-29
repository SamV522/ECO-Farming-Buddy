using CsvHelper.Configuration.Attributes;
using ECO_Farming_Buddy.Utilities;

namespace ECO_Farming_Buddy.Models
{
    internal class Crop
    {
        [Name("Plant")]
        public string Plant { get; set; }
        [Name("Temp Min")]
        public decimal TemperatureMinimum { get; set; }
        [Name("Temp Max")]
        public decimal TemperatureMaximum { get; set; }
        [Name("Temp Opt Min")]
        public decimal TemperatureOptimalMinimum { get; set; }
        [Name("Temp Opt Max")]
        public decimal TemperatureOptimalMaximum { get; set; }
        [Ignore]
        public decimal OptimalTemperature => MathUtilities.Middle(TemperatureOptimalMinimum, TemperatureOptimalMaximum);
        [Name("Rainfall Min")]
        public decimal RainfallMinimum { get; set; }
        [Name("Rainfall Max")]
        public decimal RainfallMaximum { get; set; }
        [Name("Rainfall Opt Min")]
        public decimal RainfallOptimalMinimum { get; set; }
        [Name("Rainfall Opt Max")]
        public decimal RainfallOptimalMaximum { get; set; }
        [Ignore]
        public decimal OptimalRainfall => MathUtilities.Middle(RainfallOptimalMinimum, RainfallOptimalMaximum);
        [Name("Plantable Seed")]
        public string PlantableSeeds { get; set; }
        [Name("Base Level Time To Maturity")]
        public decimal BaseTimeToMaturity { get; set; }
        [Ignore]
        public decimal Suitability { get; set; } = 0;
        [Ignore]
        public bool Optimal { get; set; } = false;
    }
}
