using System;
using System.Collections.Generic;

namespace AdotAqui.Models.Entities
{
    public partial class AdoptionLogs
    {
        public int AdoptionLogId { get; set; }
        public int AdoptionStateId { get; set; }
        public int? AdoptionRequestId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }

        public AdoptionRequests AdoptionRequest { get; set; }
        public AdoptionStates AdoptionState { get; set; }
        public User User { get; set; }
    }
}
