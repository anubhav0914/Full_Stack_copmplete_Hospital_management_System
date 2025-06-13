namespace Hospital.Persistence.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
        public DateTime CreationDate { get; set; }
        public int NoOfEmployees { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<EmployeeStaff> Employees { get; set; }
    }

}