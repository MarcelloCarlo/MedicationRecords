using MedicationRecords.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using static BusinessLogicLibrary.MedicationBLL;

namespace MedicationRecords.Controllers
{
  public class PatientController : Controller
  {
    // GET: Patient
    public ActionResult Index()
    {
      var data = LoadRecords();
      List<MedicationModel> medicationRecords = new List<MedicationModel>();

      foreach ( var row in data )
      {
        medicationRecords.Add(new MedicationModel
        {
          Id = row.Id,
          txtPatients = row.Patients,
          txtDrug = row.Drug,
          txtDosage = row.Dosage,
          dtpDate = row.Date
        });
      }
      return View(medicationRecords);
    }

    // GET: Patient/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Patient/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,txtPatients,txtDrug,txtDosage,Date")] MedicationModel medicationModel)
    {

      if ( ModelState.IsValid )
      {

        CreateRecord(medicationModel.txtPatients,
           medicationModel.txtDrug,
           medicationModel.txtDosage,
           medicationModel.dtpDate);
        return RedirectToAction("Index");

      }

      return View(medicationModel);
    }

    // GET: Patient/Edit/5
    public ActionResult Edit(int? id)
    {
      if ( id == null )
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var data = SearchQueryById((int) id);
      List<MedicationModel> medicationRecords = new List<MedicationModel>();

      foreach ( var row in data )
      {
        medicationRecords.Add(new MedicationModel
        {

          Id = row.Id,
          txtPatients = row.Patients,
          txtDrug = row.Drug,
          txtDosage = row.Dosage,
          dtpDate = row.Date

        });
      }

      if ( medicationRecords == null )
      {
        return HttpNotFound();
      }



      return View(medicationRecords);
    }

    // POST: Patient/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,txtPatients,txtDrug,txtDosage,Date")] MedicationModel medicationModel)
    {
      if ( ModelState.IsValid )
      {

        UpdateRecord(medicationModel.Id,
           medicationModel.txtPatients,
           medicationModel.txtDrug,
           medicationModel.txtDosage,
           medicationModel.dtpDate);
        return RedirectToAction("Index");

      }
      return View(medicationModel);
    }

    // GET: Patient/Delete/5
    public ActionResult Delete(int? id)
    {

      if ( id == null )
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      var data = SearchQueryById((int) id);
      List<MedicationModel> medicationRecords = new List<MedicationModel>();

      foreach ( var row in data )
      {
        medicationRecords.Add(new MedicationModel
        {

          Id = row.Id,
          txtPatients = row.Patients,
          txtDrug = row.Drug,
          txtDosage = row.Dosage,
          dtpDate = row.Date

        });
      }

      if ( medicationRecords == null )
      {
        return HttpNotFound();
      }

      return View(medicationRecords);
    }

    // POST: Patient/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      DeleteRecord(id);
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if ( disposing )
      {

      }
      base.Dispose(disposing);
    }
  }
}
