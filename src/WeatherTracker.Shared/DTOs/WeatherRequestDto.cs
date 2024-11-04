public record WeatherRequestDto
{
    public string City { get; init; }
    public string CountryCode { get; init; }
}