import axios from "axios";
import { WeatherData, LocationSearch } from "../types/weather";

const API_BASE_URL =
  process.env.REACT_APP_API_URL ||
  "http://weather-tracker-backend.nicemeadow-ebda215e.canadaeast.azurecontainerapps.io/api/weather";

// For debugging
console.log("API Base URL:", API_BASE_URL);

const weatherApi = {
  getCurrentWeather: async ({
    city,
    countryCode,
  }: LocationSearch): Promise<WeatherData> => {
    try {
      const response = await axios.get<WeatherData>(
        `${API_BASE_URL}/${city}/${countryCode}`
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching current weather:", error);
      throw error;
    }
  },

  getForecast: async ({
    city,
    countryCode,
  }: LocationSearch): Promise<WeatherData[]> => {
    try {
      const response = await axios.get<WeatherData[]>(
        `${API_BASE_URL}/forecast/${city}/${countryCode}`
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching forecast:", error);
      throw error;
    }
  },
};

export default weatherApi;