using System.Text.Json.Serialization;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("Mode")]
        public int ModeId {  get; set; }
        public int? MinWeight {  get; set; }
        public int? MaxWeight {  get; set; }
        public string Service {  get; set; }
        public string Description {  get; set; }
        public string Notes {  get; set; }
        public bool? Status { get; set; }
    }
}
