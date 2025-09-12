using System;
using System.Collections.Generic;

namespace KampusTek.Models;

public partial class Bicycle
{
    public int Id { get; set; }

    public string BicycleCode { get; set; } = null!;

    public string? Model { get; set; }

    public string? Color { get; set; }

    public string? Status { get; set; }

    public int? CurrentStationId { get; set; }

    public virtual Station? CurrentStation { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual ICollection<RentingProcess> RentingProcesses { get; set; } = new List<RentingProcess>();
}
