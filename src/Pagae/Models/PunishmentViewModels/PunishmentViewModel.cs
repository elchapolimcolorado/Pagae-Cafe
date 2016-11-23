using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pagae.Models.HomeViewModels;

namespace Pagae.Models.PunishmentViewModels
{
    public class PunishmentViewModel
    {
        [Required(ErrorMessage = "Select the user to punish")]
        public string PunishedId { get; set; }
        public List<UserViewModel> Users { get; set; }
        [Required(ErrorMessage = "What is the reason?")]
        public string Reason { get; set; }

        public PunishmentViewModel()
        {
            Users = new List<UserViewModel>();
        }
    }
}
