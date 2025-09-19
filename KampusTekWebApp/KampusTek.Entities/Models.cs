using System.ComponentModel.DataAnnotations;

namespace KampusTek.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;
        
        [Required]
        [RegularExpression(@"^(\+90|0)?5\d{9}$", ErrorMessage = "Please enter a valid phone number (example: 05xxxxxxxxx).")]
        [Display(Name = "Phone Number")]
        public string CellNumber { get; set; } = null!;
        
        [Required]
        [Range(1, 2)]
        [Display(Name = "User Type")]
        public int UserTypeId { get; set; }
        
        public virtual UserType? UserType { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }

    public class Bicycle
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Bicycle Code")]
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
        
        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }
        
        [Required]
        [Display(Name = "Bicycle")]
        public int BicycleId { get; set; }
        
        [Required]
        [Display(Name = "Start Station")]
        public int StartStationId { get; set; }
        
        [Display(Name = "End Station")]
        public int? EndStationId { get; set; }
        
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        
        [Display(Name = "Return Time")]
        public DateTime? ReturnTime { get; set; }
        
        public virtual User? User { get; set; }
        public virtual Bicycle? Bicycle { get; set; }
        public virtual Station? StartStation { get; set; }
        public virtual Station? EndStation { get; set; }
    }

    public class Maintenance
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Bicycle")]
        public int BicycleId { get; set; }
        
        [Display(Name = "Description")]
        public string? Description { get; set; }
        
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        
        public virtual Bicycle? Bicycle { get; set; }
    }

    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
