namespace BlazorPinballLocations
{
    /// <summary>
    /// Class generated from Paste Special -> Paste from JSON
    /// </summary>
    public class PinballLocation
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zip { get; set; }
        public string? phone { get; set; }
        public string? lat { get; set; }
        public string? lon { get; set; }
        public string? website { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object? zone_id { get; set; }
        public int? region_id { get; set; }
        public int? location_type_id { get; set; }
        public string? description { get; set; }
        public object? operator_id { get; set; }
        public string? date_last_updated { get; set; }
        public int? last_updated_by_user_id { get; set; }
        public bool? is_stern_army { get; set; }
        public string? country { get; set; }
        public bool? ic_active { get; set; }
        public float? distance { get; set; }
        public string? bearing { get; set; }
        public string[] machine_names { get; set; }
        public int[] machine_ids { get; set; }
        public int? num_machines { get; set; }
    }

}