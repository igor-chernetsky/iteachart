using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class IdNameParentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<IdNameModel> Childs { get; set; }
    }

    public class IdNameModel
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
    }
}