using System;
using System.Collections.Generic;

namespace KampusTek.Models;

public partial class UserType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
