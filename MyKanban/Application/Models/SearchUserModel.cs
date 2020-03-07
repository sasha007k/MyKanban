using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class SearchUserModel
    {
        public string Id { get; set; }

        public string SelectedUserId { get; set; }

        public IEnumerable<User> Users { get; set; }

        public List<string> SelectedUsers { get; set; }
    }
}
