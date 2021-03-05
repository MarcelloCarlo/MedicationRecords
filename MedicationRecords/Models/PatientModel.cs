using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicationRecords.Models
{
  public class PatientModel
  {

    [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
    [Required]
    public string Patients { get; set; }

    [StringLength(50, ErrorMessage = "Drug name cannot be longer than 50 characters.")]
    [Required]
    public string Drug { get; set; }

    [RegularExpression(@"^(?=.*[1-9])\d{0,7}(?:\.\d{0,4})?$", ErrorMessage = "Invalid dosage amount.")]
    [Required]
    public decimal Dosage { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

  }
}
