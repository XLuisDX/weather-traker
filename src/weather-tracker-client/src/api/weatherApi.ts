import axios from "axios";
import { WeatherData, LocationSearch } from "../types/weather";

const API_BASE_URL = "https://localhost:7238/api/weather";

const weatherApi = {
  getCurrentWeather: async ({
    city,
    countryCode,
  }: LocationSearch): Promise<WeatherData> => {
    const response = await axios.get<WeatherData>(
      `${API_BASE_URL}/${city}/${countryCode}`
    );
    return response.data;
  },

  getForecast: async ({
    city,
    countryCode,
  }: LocationSearch): Promise<WeatherData[]> => {
    const response = await axios.get<WeatherData[]>(
      `${API_BASE_URL}/forecast/${city}/${countryCode}`
    );
    return response.data;
  },
};

export default weatherApi;
