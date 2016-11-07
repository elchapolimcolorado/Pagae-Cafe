using System.Collections.Generic;

namespace Pagae.Models.HomeViewModels
{
    public class PayViewModel
    {
        public List<UserViewModel> Users { get; set; }

        public PayViewModel()
        {
            Users = new List<UserViewModel>();
        }
    }
}
