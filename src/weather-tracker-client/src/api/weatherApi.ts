import axios from "axios";
import { WeatherData, LocationSearch } from "../types/weather";

const API_BASE_URL =
  "http://weather-tracker-backend.nicemeadow-ebda215e.canadaeast.azurecontainerapps.io/api/weather";

// Create axios instance with default config
const axiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
});

const weatherApi = {
  getCurrentWeather: async ({
    city,
    countryCode,
  }: LocationSearch): Promise<WeatherData> => {
    try {
      const response = await axiosInstance.get<WeatherData>(
        `/${city}/${countryCode}`
      );
      return response.data;
    } catch (error) {
      console.error('Error fetching current weather:', error);
      throw error;
    }
  },

  getForecast: async ({
    city,
    countryCode,
  }: LocationSearch): Promise<WeatherData[]> => {
    try {
      const response = await axiosInstance.get<WeatherData[]>(
        `/forecast/${city}/${countryCode}`
      );
      return response.data;
    } catch (error) {
      console.error('Error fetching forecast:', error);
      throw error;
    }
  },
};

export default weatherApi;