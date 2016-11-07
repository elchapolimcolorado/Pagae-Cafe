using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pagae.Models.HomeViewModels
{
    public class PaySomeoneElseViewModel
    {
        [Required(ErrorMessage = "Select the user who is paying")]
        public string PayerId { get; set; }
        public List<UserViewModel> Users { get; set; }

        public PaySomeoneElseViewModel()
        {
            Users = new List<UserViewModel>();
        }
    }
}
