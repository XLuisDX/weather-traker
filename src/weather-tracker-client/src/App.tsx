import React, { useState } from "react";
import { ThemeProvider, CssBaseline, Alert, Snackbar } from "@mui/material";
import theme from "./theme";
import Layout from "./components/Layout/Layout";
import SearchLocation from "./components/SearchLocation/SearchLocation";
import CurrentWeather from "./components/CurrentWeather/CurrentWeather";
import WeatherForecast from "./components/WeatherForecast/WeatherForecast";
import { WeatherData, LocationSearch } from "./types/weather";
import weatherApi from "./api/weatherApi";

function App() {
  const [currentWeather, setCurrentWeather] = useState<WeatherData>();
  const [forecast, setForecast] = useState<WeatherData[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSearch = async (location: LocationSearch) => {
    setLoading(true);
    setError(null);

    try {
      const [currentData, forecastData] = await Promise.all([
        weatherApi.getCurrentWeather(location),
        weatherApi.getForecast(location),
      ]);

      setCurrentWeather(currentData);
      setForecast(forecastData);
    } catch (err) {
      setError(
        err instanceof Error
          ? err.message
          : "An error occurred while fetching weather data from the API"
      );
    } finally {
      setLoading(false);
    }
  };

  const handleCloseError = () => {
    setError(null);
  };

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Layout>
        <SearchLocation onSearch={handleSearch} />
        <CurrentWeather data={currentWeather} isLoading={loading} />
        <WeatherForecast data={forecast} isLoading={loading} />

        <Snackbar
          open={!!error}
          autoHideDuration={6000}
          onClose={handleCloseError}
          anchorOrigin={{ vertical: "top", horizontal: "center" }}
        >
          <Alert onClose={handleCloseError} severity="error" elevation={6}>
            {error}
          </Alert>
        </Snackbar>
      </Layout>
    </ThemeProvider>
  );
}

export default App;
