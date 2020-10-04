using System.Collections.Generic;

namespace UtilityBelt.Models
{
    class SpacePersonModel
    {
        public int Number { get; set; }
        public List<SpacePerson> People { get; set; }
        public string Message { get; set; }
    }
    public class SpacePerson
    {
        public string Craft { get; set; }
        public string Name { get; set; }
    }
}
