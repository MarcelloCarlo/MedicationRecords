using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicationRecords.Models
{
    public class MedicationModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [Required]
        [Display(Name = "Patient")]
        public string txtPatients { get; set; }

        [StringLength(50, ErrorMessage = "Drug name cannot be longer than 50 characters.")]
        [Required]
        [Display(Name = "Drug")]
        public string txtDrug { get; set; }

        [RegularExpression(@"^(?=.*[1-9])\d{0,7}(?:\.\d{0,4})?$", ErrorMessage = "Invalid dosage amount.")]
        [Required]
        [Display(Name = "Dosage")]
        public decimal txtDosage { get; set; }

    }

    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public bool IsListResult { get; set; }
        public object Result { get; set; }
    }
}