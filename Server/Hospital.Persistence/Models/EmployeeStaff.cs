namespace Hospital.Persistence.Models
{
    public class EmployeeStaff
    {
        public int EmpId { get; set; }
        public int? DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int Salary { get; set; }
        public DateTime JoiningDate { get; set; }

        public string Password { get; set; }

        public Department Department { get; set; }
    }

}