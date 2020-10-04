using System;

namespace UtilityBelt.Models
{
    class CatFactModel
    {
        public bool Used { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public bool Deleted { get; set; }
        public string Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public int __v { get; set; }
        public CatFactStatus Status { get; set; }
    }
    public class CatFactStatus
    {
        public bool Verified { get; set; }
        public int SentCount { get; set; }
    }
}
