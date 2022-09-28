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
                decimal tempDiff = Math.Abs(temperature.DifferenceFromRange(crop.TemperatureOptimalMinimum, crop.TemperatureOptimalMaximum));
                decimal rainDiff = Math.Abs(rainfall.DifferenceFromRange(crop.RainfallOptimalMinimum, crop.RainfallOptimalMaximum));
                crop.Suitability = 1 - (tempDiff + rainDiff);

                bool optimalTemp = temperature.Between(crop.TemperatureOptimalMinimum, crop.TemperatureOptimalMaximum);
                bool optimalRain = rainfall.Between(crop.RainfallOptimalMinimum, crop.RainfallOptimalMaximum);
                crop.Optimal = optimalTemp && optimalRain;
            }
        }
    }
}
