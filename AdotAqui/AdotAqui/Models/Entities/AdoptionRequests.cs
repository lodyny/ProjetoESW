using System;
using System.Collections.Generic;

namespace AdotAqui.Models.Entities
{
    public partial class AdoptionRequests
    {
        public AdoptionRequests()
        {
            AdoptionLogs = new HashSet<AdoptionLogs>();
            AdoptionType = "Adoção";
        }

        public int AdoptionRequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string AdoptionType { get; set; }
        public int UserId { get; set; }
        public int AnimalId { get; set; }
        public DateTime ProposalDate { get; set; }
        public string Details { get; set; }

        public Animal Animal { get; set; }
        public User User { get; set; }
        public ICollection<AdoptionLogs> AdoptionLogs { get; set; }
    }
}
