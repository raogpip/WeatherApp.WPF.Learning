using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.WPF.Learning.Model;
using WeatherApp.WPF.Learning.ViewModel.Commands;
using WeatherApp.WPF.Learning.ViewModel.Helpers;

namespace WeatherApp.WPF.Learning.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set  
            {
                query = value;
                OnPropertyChanged("Query");    
            }
        }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            { 
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private City selectedCity; 

        public City SelectedCity
        {
            get { return selectedCity; }
            set 
            { 
                selectedCity = value; 
                OnPropertyChanged("SelectedCity"); 
                GetCurrentConditions(); 
            }
        }

        public SearchCommand SearchCommand { get; set; }


        public WeatherVM()
        {
            if(DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "Kyiv"
                };
                CurrentConditions = new CurrentConditions()
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature()
                    {
                        Metric = new Units()
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);

            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();

            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(query);

            Cities.Clear();
            foreach (var city in cities) 
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
