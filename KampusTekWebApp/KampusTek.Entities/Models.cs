namespace KampusTek.Entities
{
        public class User
        {
            public int Id { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string CellNumber { get; set; } = null!;
            public int UserTypeId { get; set; }
            public virtual UserType UserType { get; set; } = null!;
            public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        }

        public class Bicycle
        {
            public int Id { get; set; }
            public string BicycleCode { get; set; } = null!;
            public string? Model { get; set; }
            public string? Color { get; set; }
            public string? Status { get; set; }
            public int? CurrentStationId { get; set; }
            public virtual Station? CurrentStation { get; set; }
            public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
            public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        }

        public class Station
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string? Location { get; set; }
            public int Capacity { get; set; }
            public virtual ICollection<Bicycle> Bicycles { get; set; } = new List<Bicycle>();
            public virtual ICollection<Rental> RentalsAsStart { get; set; } = new List<Rental>();
            public virtual ICollection<Rental> RentalsAsEnd { get; set; } = new List<Rental>();
        }

        public class Rental
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int BicycleId { get; set; }
            public int StartStationId { get; set; }
            public int? EndStationId { get; set; }

            public DateTime StartTime { get; set; }
            public DateTime? ReturnTime { get; set; }
            public virtual User User { get; set; } = null!;
            public virtual Bicycle Bicycle { get; set; } = null!;
            public virtual Station StartStation { get; set; } = null!;
            public virtual Station? EndStation { get; set; }
        }

        public class Maintenance
        {
            public int Id { get; set; }
            public int BicycleId { get; set; }
            public string? Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public virtual Bicycle Bicycle { get; set; } = null!;
        }

        public class UserType
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public virtual ICollection<User> Users { get; set; } = new List<User>();
        }
}
