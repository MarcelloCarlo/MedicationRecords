using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicationRecords.Models;
using Medication_BLL;
using Medication_Entity;

namespace MedicationRecords.Controllers
{
    public class MedicationController : Controller
    {
        private MedicationBLL _medicationBLL = new MedicationBLL();
        private MedicationEntity _medicationEntity = new MedicationEntity();
        private MedicationModel _medicationViewModel = new MedicationModel();
        private readonly ResultModel _resultModel = new ResultModel();
        // GET: Medication
        public async Task<ActionResult> Index()
        {
            return View(await _medicationBLL.LoadRecords());
        }

        // GET: Medication/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _medicationEntity = await _medicationBLL.ViewRecord((int)id);
            if (_medicationEntity == null)
            {
                return HttpNotFound();
            }

            _medicationBLL = null;

            return View(_medicationEntity);
        }

        // GET: Medication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medication/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "txtPatients,txtDrug,txtDosage")] MedicationModel medicationModel)
        {
            if (ModelState.IsValid)
            {
                _medicationEntity.Patients = medicationModel.txtPatients;
                _medicationEntity.Drug = medicationModel.txtDrug;
                _medicationEntity.Dosage = medicationModel.txtDosage.ToString();

                MedicationEntity result = new MedicationEntity();

                result = await _medicationBLL.InsertRecord(_medicationEntity);

                if (result.Id != 0)
                {
                    UpdateResultModel(true, false, result);
                }
                else
                {
                    UpdateResultModel(false, false, "Cannot add same drug to a patient.");
                }

                //return RedirectToAction("Index");
            }

            _medicationBLL = null;

            return View(_resultModel);
        }

        [HttpPost]
        public async Task<JsonResult> JsonCreate(MedicationEntity medicationEntity)
        {
            if (ModelState.IsValid)
            {
                MedicationEntity result = new MedicationEntity();

                result = await _medicationBLL.InsertRecord(medicationEntity);

                if (result.Id != 0)
                {
                    UpdateResultModel(true, false, result);
                }
                else
                {
                    UpdateResultModel(false, false, "Cannot add same drug to a patient.");
                }

                //return RedirectToAction("Index");
            }

            _medicationBLL = null;

            return Json(_resultModel);
        }

        // GET: Medication/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _medicationEntity = null;

            _medicationEntity = await _medicationBLL.ViewRecord((int)id);

            if (_medicationEntity == null)
            {
                return HttpNotFound();
            }

            _medicationViewModel.Id = _medicationEntity.Id;
            _medicationViewModel.txtPatients = _medicationEntity.Patients;
            _medicationViewModel.txtDrug = _medicationEntity.Drug;
            _medicationViewModel.txtDosage = Convert.ToDecimal(_medicationEntity.Dosage.Trim());

            _medicationBLL = null;

            return View(_medicationViewModel);
        }

        // POST: Medication/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,txtPatients,txtDrug,txtDosage,dtpDate")] MedicationModel medicationModel)
        {
            if (ModelState.IsValid)
            {
                _medicationEntity.Id = medicationModel.Id;
                _medicationEntity.Patients = medicationModel.txtPatients;
                _medicationEntity.Drug = medicationModel.txtDrug;
                _medicationEntity.Dosage = medicationModel.txtDosage.ToString();

                await _medicationBLL.UpdateRecord(_medicationEntity);
                return RedirectToAction("Index");
            }

            _medicationBLL = null;

            return View(medicationModel);
        }

        [HttpPost]
        public async Task<JsonResult> JsonEdit(MedicationEntity medicationEntity)
        {
            if (ModelState.IsValid)
            {
                MedicationEntity result = new MedicationEntity();

                result = await _medicationBLL.UpdateRecord(medicationEntity);

                if (result.IsSuccess)
                {
                    UpdateResultModel(true, false, result);
                }
                else
                {
                    UpdateResultModel(false, false, "Cannot add same drug to a patient.");
                }

                //return RedirectToAction("Index");
            }

            _medicationBLL = null;

            return Json(_resultModel);
        }

        // GET: MedicationModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _medicationEntity = await _medicationBLL.ViewRecord((int)id);
            if (_medicationEntity == null)
            {
                return HttpNotFound();
            }

            _medicationBLL = null;

            return View(_medicationEntity);
        }

        // POST: MedicationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            _medicationEntity.Id = id;
            await _medicationBLL.DeleteRecord(_medicationEntity);
            _medicationBLL = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> JsonDelete(MedicationEntity medicationEntity)
        {
            if (ModelState.IsValid)
            {
                MedicationEntity result = new MedicationEntity();

                result = await _medicationBLL.DeleteRecord(medicationEntity);

                if (result.IsSuccess)
                {
                    UpdateResultModel(true, false, result);
                }
                else
                {
                    UpdateResultModel(false, false, "Record deletion failed.");
                }

            }

            _medicationBLL = null;

            return Json(_resultModel);
        }

        public ResultModel UpdateResultModel(bool isSuccess, bool isListResult, object resultObject)
        {
            _resultModel.IsSuccess = isSuccess;
            _resultModel.IsListResult = isListResult;
            _resultModel.Result = resultObject;

            return _resultModel;
        }

    }

}
