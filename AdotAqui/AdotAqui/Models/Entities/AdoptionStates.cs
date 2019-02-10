using System;
using System.Collections.Generic;

namespace AdotAqui.Models.Entities
{
    /// <summary>
    /// Represents the state of a adoption request
    /// </summary>
    public partial class AdoptionStates
    {
        public AdoptionStates()
        {
            AdoptionLogs = new HashSet<AdoptionLogs>();
        }

        public int AdoptionStateId { get; set; }
        public string Name { get; set; }
        public string NamePt { get; set; }

        public ICollection<AdoptionLogs> AdoptionLogs { get; set; }
    }
}
