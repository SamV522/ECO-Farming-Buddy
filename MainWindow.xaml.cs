using ECO_Farming_Buddy.Extensions;
using ECO_Farming_Buddy.Helpers;
using ECO_Farming_Buddy.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace ECO_Farming_Buddy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenFileDialog cropFileDialog;

        private IEnumerable<Crop> m_FilteredCrops => CropHelper.Crops.Where(crop => IsWithinFilters(crop));

        private decimal m_CropTemperatureFilter => decimal.Parse(txt_Temperature.Text);
        private decimal m_CropRainfallFilter => decimal.Parse(txt_Rainfall.Text) / 100;

        public MainWindow()
        {
            InitializeComponent();
            cropFileDialog = new OpenFileDialog
            {
                Filter = "Csv Files (*.csv)|*.csv|Txt Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true
            };

            cropFileDialog.FileOk += CropFileDialog_FileOk;

            /* Handle the event raised for unhandled exceptions */
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            /* Log the exception */
            LogHelper.Log(e.Exception);
        }

        private void btn_OpenCropFileDialog_Click(object sender, RoutedEventArgs e)
        {
            cropFileDialog.ShowDialog();
        }

        private void CropFileDialog_FileOk(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                CropHelper.LoadCrops(cropFileDialog.FileName);
                RefreshFilters();
                dataGrid_Crops.ItemsSource = m_FilteredCrops;
                txt_CropFile.Text = cropFileDialog.FileName;
            }
            catch (IOException ex) when (ex.Message.Substring(0, 35) == "The process cannot access the file ")
            {
                MessageBox.Show("The file you have selected is in use, please close the file and try again", "File in use", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(cropFileDialog.FileName))
            {
                try
                {
                    CropHelper.SaveCrops(cropFileDialog.FileName);
                    MessageBox.Show("Your changes were saved successfully!", "Saved Changes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                MessageBox.Show("The file you have specified does not exist", "File does not exist", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshFilters()
        {
            CropHelper.CalculateSuitability(m_CropTemperatureFilter, m_CropRainfallFilter);
            dataGrid_Crops.ItemsSource = new ObservableCollection<Crop>(m_FilteredCrops);
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshFilters();
        }

        private bool IsWithinFilters(Crop crop)
        {
            bool rainRange = m_CropRainfallFilter.Within(crop.RainfallMinimum, crop.RainfallMaximum);
            bool tempRange = m_CropTemperatureFilter.Within(crop.TemperatureMinimum, crop.TemperatureMaximum);

            bool plantableFilter = chck_OnlyPlantable.IsChecked.Value;
            bool plantable = !string.IsNullOrEmpty(crop.PlantableSeeds);

            bool optimalFilter = chck_OptimalOnly.IsChecked.Value;
            bool optimal = crop.Optimal;

            return !chck_FilterCrops.IsChecked.Value || 
                    (rainRange && tempRange
                    && (plantable || !plantableFilter)
                    && (optimal || !optimalFilter));
        }

        private void chck_FilterCrops_Toggled(object sender, RoutedEventArgs e)
        {
            RefreshFilters();
        }

        private void chck_PlantableOnly_Toggled(object sender, RoutedEventArgs e)
        {
            RefreshFilters();
        }

        private void chck_OptimalOnly_Toggled(object sender, RoutedEventArgs e)
        {
            RefreshFilters();
        }
    }
}
