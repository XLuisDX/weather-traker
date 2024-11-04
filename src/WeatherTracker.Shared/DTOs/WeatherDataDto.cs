public record WeatherDataDto
{
    public string City { get; init; }
    public string Country { get; init; }
    public double Temperature { get; init; }
    public double FeelsLike { get; init; }
    public int Humidity { get; init; }
    public string Description { get; init; }
    public DateTime Timestamp { get; init; }
}
