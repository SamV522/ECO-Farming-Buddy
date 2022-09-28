using CsvHelper;
using ECO_Farming_Buddy.Extensions;
using ECO_Farming_Buddy.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ECO_Farming_Buddy.Helpers
{
    internal static class CropHelper
    {
        public static List<Crop> Crops { get; set; } = new List<Crop>();
        public static List<Crop> FilteredCrops { get; set; } = new List<Crop>();

        public static void LoadCrops(string Path)
        {
            using (var reader = new StreamReader(Path, new FileStreamOptions() { Access = FileAccess.Read }))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                Crops = csv.GetRecords<Crop>().ToList();
            }
        }

        public static void SaveCrops(string Path)
        {
            using (var writer = new StreamWriter(Path, new FileStreamOptions() { Access = FileAccess.ReadWrite }))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Crops);
            }
        }

        public static void CalculateSuitability(decimal temperature, decimal rainfall)
        {
            foreach(Crop crop in Crops)
            {
                if (crop.Plant == "Beans")
                {
                    //continue;
                }

                decimal tempDiff = crop.OptimalTemperature - temperature;
                decimal rainDiff = crop.OptimalRainfall - rainfall;

                decimal tempLerp = temperature.InverseLerpRangeUnclamped(crop.OptimalTemperature, crop.TemperatureMinimum, crop.TemperatureMaximum);
                decimal rainLerp = rainfall.InverseLerpRangeUnclamped(crop.OptimalRainfall, crop.RainfallMinimum, crop.RainfallMaximum);

                decimal tempSuitability = Math.Round(1 - Math.Abs(tempLerp), 2);
                decimal rainSuitability = Math.Round(1 - Math.Abs(rainLerp), 2);

                
                crop.Suitability = tempSuitability < 0 ? 0 : rainSuitability < 0 ? 0 : (tempSuitability + rainSuitability) / 2;

                /* Optimal = Within Optimal Min/Max*/
                bool isOptimalTemp = temperature.Within(crop.TemperatureOptimalMinimum, crop.TemperatureOptimalMaximum);
                bool isOptimalRain = rainfall.Within(crop.RainfallOptimalMinimum, crop.RainfallOptimalMaximum);
                crop.Optimal = isOptimalTemp && isOptimalRain;
            }
        }
    }
}
