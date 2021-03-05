using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationEntity
{
  public class Entity
  {

  }

  public class Medication : Entity
  {
    public int Id { get; set; }
    public string Patients { get; set; }
    public string Drug { get; set; }
    public decimal Dosage { get; set; }
    public DateTime Date { get; set; }

  }
}
