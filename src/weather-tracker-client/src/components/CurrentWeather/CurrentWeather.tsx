import React from "react";
import {
  Paper,
  Typography,
  Grid,
  Box,
  Chip,
  Divider,
  Skeleton,
} from "@mui/material";
import {
  ThermostatOutlined,
  WaterDropOutlined,
  AccessTimeOutlined,
  LocationOnOutlined,
} from "@mui/icons-material";
import { WeatherData } from "../../types/weather";

interface CurrentWeatherProps {
  data?: WeatherData;
  isLoading: boolean;
}

const CurrentWeather: React.FC<CurrentWeatherProps> = ({ data, isLoading }) => {
  if (isLoading) {
    return (
      <Paper sx={{ p: 3, mb: 3 }}>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <Skeleton variant="text" width="60%" height={40} />
            <Skeleton variant="text" width="40%" height={30} />
          </Grid>
          <Grid item xs={12}>
            <Skeleton variant="rectangular" height={100} />
          </Grid>
        </Grid>
      </Paper>
    );
  }

  if (!data) return null;
  console.log(data);
  return (
    <Paper sx={{ p: 3, mb: 3 }}>
      <Box sx={{ mb: 2 }}>
        <Typography
          variant="h5"
          sx={{ display: "flex", alignItems: "center", mb: 1 }}
        >
          <LocationOnOutlined sx={{ mr: 1 }} />
          {data.city}, {data.country}
        </Typography>
        <Typography
          variant="body2"
          color="text.secondary"
          sx={{ display: "flex", alignItems: "center" }}
        >
          <AccessTimeOutlined sx={{ mr: 1, fontSize: "1rem" }} />
          {new Date(data.timestamp).toLocaleString()}
        </Typography>
      </Box>

      <Divider sx={{ my: 2 }} />

      <Grid container spacing={3}>
        <Grid item xs={12} sm={6}>
          <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
            <ThermostatOutlined sx={{ mr: 1, color: "primary.main" }} />
            <Typography variant="h3">
              {Math.round(data.temperature)}°C
            </Typography>
          </Box>
          <Chip
            label={`Feels like ${Math.round(data.feelsLike)}°C`}
            variant="outlined"
            size="small"
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
            <Typography variant="h6" sx={{ textTransform: "capitalize" }}>
              {data.description}
            </Typography>
            <Box sx={{ display: "flex", alignItems: "center" }}>
              <WaterDropOutlined sx={{ mr: 1, color: "primary.main" }} />
              <Typography>Humidity: {data.humidity}%</Typography>
            </Box>
          </Box>
        </Grid>
      </Grid>
    </Paper>
  );
};

export default CurrentWeather;
