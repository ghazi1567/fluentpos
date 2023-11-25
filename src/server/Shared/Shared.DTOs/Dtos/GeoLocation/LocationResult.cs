using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Dtos.GeoLocation
{
    public class LocationResult
    {
        public string place_id { get; set; }

        public string licence { get; set; }

        public string osm_type { get; set; }

        public string osm_id { get; set; }

        public List<string> boundingbox { get; set; }

        public string lat { get; set; }

        public string lon { get; set; }

        public string display_name { get; set; }

        public string type { get; set; }

        public decimal importance { get; set; }

        public string icon { get; set; }
    }

    public class GeocodingLocationResult
    {
        public List<GeocodingResult> results { get; set; }

    }

    public class GeocodingResult
    {
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public GeoLocation Location { get; set; }
    }

    public class GeoLocation
    {
        public decimal Lat { get; set; }

        public decimal lng { get; set; }
    }
}