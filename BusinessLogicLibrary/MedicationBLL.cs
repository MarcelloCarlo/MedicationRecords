using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medication_Entity;
using Medication_DAL;
namespace Medication_BLL
{
    public class MedicationBLL
    {
        MedicationDAL _medicationDAL = null;
        public async Task<IEnumerable<MedicationEntity>> LoadRecords()
        {
            IEnumerable<MedicationEntity> medicationEntities = null;

            try
            {
                _medicationDAL = new MedicationDAL();
                medicationEntities = await _medicationDAL.LoadAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return medicationEntities;
        }

        public async Task<MedicationEntity> ViewRecord(string query)
        {
            MedicationEntity medicationEntity = null;

            try
            {
                _medicationDAL = new MedicationDAL();
                medicationEntity = await _medicationDAL.ViewRecordAsync(query);
            }
            catch (Exception)
            {

                throw;
            }

            return medicationEntity;
        }

        public async Task<MedicationEntity> InsertRecord(MedicationEntity medicationEntity)
        {
            MedicationEntity result = new MedicationEntity
            {
                MessageList = new List<string>()
            };

            try
            {
                _medicationDAL = new MedicationDAL();
                result = await _medicationDAL.InsertAsync(medicationEntity);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public async Task<MedicationEntity> UpdateRecord(MedicationEntity medicationEntity) 
        {
            MedicationEntity result = new MedicationEntity
            {
                MessageList = new List<string>()
            };

            try
            {
                _medicationDAL = new MedicationDAL();
                result.IsSuccess = await _medicationDAL.UpdateAsync(medicationEntity);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public async Task<MedicationEntity> DeleteRecord(MedicationEntity medicationEntity)
        {
            MedicationEntity result = new MedicationEntity
            {
                MessageList = new List<string>()
            };

            try
            {
                _medicationDAL = new MedicationDAL();
                result.IsSuccess = await _medicationDAL.DeleteAsync(medicationEntity);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

    }
}
