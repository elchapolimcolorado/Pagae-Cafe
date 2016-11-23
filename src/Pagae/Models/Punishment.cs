using System;

namespace Pagae.Models
{
    public class Punishment
    {
        public int Id { get; set; }
        public string PunishedId { get; set; }
        public ApplicationUser Punished { get; set; }
        public string Reason { get; set; }
        public PunishmentStatus Status { get; set; }
        public DateTime Date { get; set; }
    }

    public class Vote
    {
        public int Id { get; set; }
        public int PunishmentId { get; set; }
        public Punishment Punishment { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool InFavor { get; set; }
    }

    public enum PunishmentStatus
    {
        Voting = 1,
        Approved = 2,
        Rejected = 3
    }
}
