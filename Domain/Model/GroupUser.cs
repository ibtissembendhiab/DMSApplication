using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    class GroupUser
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string Id { get; set; }
        public User User { get; set; }

        public GroupUser() { }
    }
}
