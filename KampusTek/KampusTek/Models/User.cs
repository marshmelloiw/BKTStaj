using System;
using System.Collections.Generic;

namespace KampusTek.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string CellNumber { get; set; } = null!;

    public int UserTypeId { get; set; }

    public virtual ICollection<RentingProcess> RentingProcesses { get; set; } = new List<RentingProcess>();

    public virtual UserType UserType { get; set; } = null!;
}
