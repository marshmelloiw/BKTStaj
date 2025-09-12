using System;
using System.Collections.Generic;

namespace KampusTek.Models;

public partial class RentingProcess
{
    public int ProcessId { get; set; }

    public int UserId { get; set; }

    public int BicycleId { get; set; }

    public int StartStationId { get; set; }

    public int? EndStationId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? ReturnTime { get; set; }

    public virtual Bicycle Bicycle { get; set; } = null!;

    public virtual Station? EndStation { get; set; }

    public virtual Station StartStation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
