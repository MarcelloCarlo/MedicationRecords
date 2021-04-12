using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Medication_Entity
{
  public class MedicationEntity : Entity
  {
    public int Id { get; set; }
    public string Patients { get; set; }
    public string Drug { get; set; }
    public string Dosage { get; set; }
    public DateTime Date { get; set; }
  }
}
