using Darnton.Blazor.DeviceInterop.Geolocation;
using System.Text.Json;

namespace BlazorPinballLocations.Components.Pages
{
    public partial class Pinball
    {
        Location location = new();
        IList<PinballLocation>? pinballLocations = null;

        private async Task Submit()
        {               
            pinballLocations = new List<PinballLocation>();
            string matches = await GetClosestMachinesByLatLon(location.Latitude, location.Longitude);
            pinballLocations = GetPinballLocations(matches);
        }

        // Services layer
        private async Task<string> GetClosestMachinesByLatLon(double latitude, double longitude)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri($"https://pinballmap.com/api/v1/locations/closest_by_lat_lon.json?lat={latitude}&lon={longitude}&max_distance=50&send_all_within_distance=1")
            };
            var response = await Http.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody!;
        }

        // Business layer
        private IList<PinballLocation> GetPinballLocations(string json)
        {
            pinballLocations?.Clear();
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                if (string.IsNullOrEmpty(json))
                {
                    return Enumerable.Empty<PinballLocation>().ToList();
                }
                else
                {
                    JsonElement root = document.RootElement;
                    JsonElement locationElement = root.TryGetProperty(propertyName: "locations", out JsonElement value) ? value : new();
                    if (locationElement.ValueKind != JsonValueKind.Undefined)
                    {
                        foreach (JsonElement location in locationElement.EnumerateArray())
                        {
                            pinballLocations?.Add(JsonSerializer.Deserialize<PinballLocation>(location)!);
                        }

                        return pinballLocations ?? Enumerable.Empty<PinballLocation>().ToList();
                    }
                    else
                    {
                        return Enumerable.Empty<PinballLocation>().ToList();
                    }
                }
            }
        }

        // Helpers
        private async Task GetUserCoordinates()
        {
            var service = new GeolocationService(JS);
            var currentPositionResult = await service.GetCurrentPosition();
            location.Latitude = currentPositionResult.Position.Coords.Latitude;
            location.Longitude = currentPositionResult.Position.Coords.Longitude;
            pinballLocations = null;
        }
    }
}