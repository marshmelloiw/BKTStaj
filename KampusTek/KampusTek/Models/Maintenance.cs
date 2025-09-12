using System;
using System.Collections.Generic;

namespace KampusTek.Models;

public partial class Maintenance
{
    public int MaintenanceId { get; set; }

    public int BicycleId { get; set; }

    public DateTime MaintenanceStartDate { get; set; }

    public DateTime? MaintenanceEndDate { get; set; }

    public string? MaintenanceNotes { get; set; }

    public virtual Bicycle Bicycle { get; set; } = null!;
}
