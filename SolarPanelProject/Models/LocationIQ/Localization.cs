using Newtonsoft.Json;

namespace SolarPanelProject.Models.LocationIQ
{
    internal class Localization
    {
        [JsonProperty(PropertyName = "place_id")]
        internal long PlaceID { get; set; }

        internal string Licence { get; set; }

        [JsonProperty(PropertyName = "osm_type")]
        internal string OsmType { get; set; }

        [JsonProperty(PropertyName = "osm_id")]
        internal long OsmID { get; set; }

        internal float [] BoundingBox { get; set; }

        [JsonProperty(PropertyName = "lat")]
        internal float Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        internal float Longitude { get; set; }

        [JsonProperty(PropertyName = "display_name")]
        internal string DisplayName { get; set; }

        internal string Class { get; set; }

        internal string Type { get; set; }

        internal float Importance { get; set; }

        internal string Icon { get; set; }
    }
}
