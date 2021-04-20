using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medication_Entity;
using Medication_DAL;
using System.Linq;

namespace Medication_BLL
{
    public class MedicationBLL
    {
        MedicationDAL _medicationDAL = null;
        public async Task<IEnumerable<MedicationEntity>> LoadRecords()
        {
            IEnumerable<MedicationEntity> medicationEntities;
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

        public async Task<MedicationEntity> ViewRecord(int id)
        {
            MedicationEntity medicationEntity;
            try
            {
                _medicationDAL = new MedicationDAL();
                medicationEntity = await _medicationDAL.ViewAsync(id);
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
                if (await CheckRecord(medicationEntity))
                {
                    result.IsSuccess = false;
                }
                else
                {
                    _medicationDAL = new MedicationDAL();
                    result = await _medicationDAL.InsertAsync(medicationEntity);
                }

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
                if (await CheckRecord(medicationEntity))
                {
                    result.IsSuccess = false;
                }
                else
                {
                    _medicationDAL = new MedicationDAL();
                    result.IsSuccess = await _medicationDAL.UpdateAsync(medicationEntity);
                }

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

        public async Task<bool> CheckRecord(MedicationEntity medicationEntity)
        {
            bool result;
            try
            {
                _medicationDAL = new MedicationDAL();
                IEnumerable<MedicationEntity> objmedicationEntities = await _medicationDAL.CheckAsync(medicationEntity);
                result = (objmedicationEntities != null) && (objmedicationEntities.Count() > 0);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

    }
}
