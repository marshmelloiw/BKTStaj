using System;
using System.Collections.Generic;

namespace KampusTek.Models;

public partial class Station
{
    public int StationId { get; set; }

    public string NameOfStation { get; set; } = null!;

    public string? Location { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<Bicycle> Bicycles { get; set; } = new List<Bicycle>();

    public virtual ICollection<RentingProcess> RentingProcessEndStations { get; set; } = new List<RentingProcess>();

    public virtual ICollection<RentingProcess> RentingProcessStartStations { get; set; } = new List<RentingProcess>();
}
