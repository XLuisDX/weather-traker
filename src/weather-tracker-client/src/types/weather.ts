export interface WeatherData {
  city: string;
  country: string;
  temperature: number;
  feelsLike: number;
  humidity: number;
  description: string;
  timestamp: string;
}

export interface LocationSearch {
  city: string;
  countryCode: string;
}
