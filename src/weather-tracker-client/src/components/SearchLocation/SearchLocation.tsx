import React, { useState } from "react";
import { Paper, TextField, Button, Grid } from "@mui/material";
import { Search } from "@mui/icons-material";
import { LocationSearch } from "../../types/weather";

interface SearchLocationProps {
  onSearch: (location: LocationSearch) => void;
}

const SearchLocation: React.FC<SearchLocationProps> = ({ onSearch }) => {
  const [city, setCity] = useState("");
  const [countryCode, setCountryCode] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch({ city, countryCode });
  };

  return (
    <Paper sx={{ p: 2, mb: 3 }}>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={2} alignItems="center">
          <Grid item xs={12} sm={5}>
            <TextField
              fullWidth
              label="City"
              value={city}
              onChange={(e) => setCity(e.target.value)}
            />
          </Grid>
          <Grid item xs={12} sm={5}>
            <TextField
              fullWidth
              label="Country Code"
              value={countryCode}
              onChange={(e) => setCountryCode(e.target.value)}
              placeholder="e.g., US, UK, CA"
            />
          </Grid>
          <Grid item xs={12} sm={2}>
            <Button
              fullWidth
              type="submit"
              variant="contained"
              startIcon={<Search />}
            >
              Search
            </Button>
          </Grid>
        </Grid>
      </form>
    </Paper>
  );
};

export default SearchLocation;
