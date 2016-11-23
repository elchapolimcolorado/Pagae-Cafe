using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pagae.Models.VoteViewModels
{
    public class VoteViewModel
    {
        public int PunishmentId { get; set; }
        public string Question { get; set; }
        public bool InFavor { get; set; }
    }
}
