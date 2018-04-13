using System.ComponentModel.DataAnnotations;

namespace TcmHMS.Doctors.Dto
{
    public class CreateOrUpdateDoctorInput
    {
        [Required]
        public DoctorEditDto Doctor { get; set; }

        public bool SendActivationEmail { get; set; }

        public bool SetRandomPassword { get; set; }
    }
}
