import React from "react";
import { Paper, Typography, Grid, Divider, Skeleton } from "@mui/material";
import { WeatherData } from "../../types/weather";
import { CalendarMonthOutlined } from "@mui/icons-material";

interface WeatherForecastProps {
  data?: WeatherData[];
  isLoading: boolean;
}

const WeatherForecast: React.FC<WeatherForecastProps> = ({
  data,
  isLoading,
}) => {
  if (isLoading) {
    return (
      <Paper sx={{ p: 3 }}>
        <Skeleton variant="text" width="40%" height={40} />
        <Grid container spacing={2} sx={{ mt: 1 }}>
          {[1, 2, 3, 4, 5].map((item) => (
            <Grid item xs={12} sm={6} md={2.4} key={item}>
              <Skeleton variant="rectangular" height={120} />
            </Grid>
          ))}
        </Grid>
      </Paper>
    );
  }

  if (!data?.length) return null;
  console.log(data);
  return (
    <Paper sx={{ p: 3 }}>
      <Typography
        variant="h6"
        sx={{ display: "flex", alignItems: "center", mb: 2 }}
      >
        <CalendarMonthOutlined sx={{ mr: 1 }} />
        5-Day Forecast
      </Typography>

      <Divider sx={{ mb: 3 }} />

      <Grid container spacing={2}>
        {data.map((day, index) => (
          <Grid item xs={12} sm={6} md={2.4} key={index}>
            <Paper
              elevation={0}
              sx={{
                p: 2,
                textAlign: "center",
                bgcolor: "background.default",
              }}
            >
              <Typography variant="subtitle2" sx={{ mb: 1 }}>
                {new Date(day.timestamp).toLocaleDateString(undefined, {
                  weekday: "short",
                  hour: "numeric",
                })}
              </Typography>

              <Typography variant="h4" sx={{ mb: 1 }}>
                {Math.round((day.temperature * 9) / 5 + 32)}°F
              </Typography>

              <Typography
                variant="body2"
                color="text.secondary"
                sx={{ textTransform: "capitalize" }}
              >
                {day.description}
              </Typography>

              <Typography
                variant="caption"
                display="block"
                color="text.secondary"
              >
                Humidity: {day.humidity}%
              </Typography>
            </Paper>
          </Grid>
        ))}
      </Grid>
    </Paper>
  );
};

export default WeatherForecast;
