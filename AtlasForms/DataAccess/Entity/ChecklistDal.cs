using AtlasForms.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AtlasForms.DataAccess.Entity
{
    public class ChecklistDal
    {
        private static string _myConnection;

        static ChecklistDal()
        {
            DataObject dataObject = new DataObject();
            _myConnection = dataObject.getConnection();
        }

        internal static int saveJobActivation(JobActivationChecklist model)
        {
            int result = 0;
            try
            {
                List<SqlParameter> parametersList = new List<SqlParameter>();
                parametersList.Add(new SqlParameter("@PRJID", model.PRJID));
                parametersList.Add(new SqlParameter("@CustomerTypeID", model.projectInformation.CustomerType));
                parametersList.Add(new SqlParameter("@JobTypeId", model.projectInformation.Jobtype));
                parametersList.Add(new SqlParameter("@CustomerBid_JobNum", model.projectInformation.CustomerBidReference));
                parametersList.Add(new SqlParameter("@Scope", model.projectInformation.ScopeWorkToBePerformed));
                parametersList.Add(new SqlParameter("@LabourType", model.projectInformation.TypeOfLabour));
                parametersList.Add(new SqlParameter("@Contract", model.contractPaperWork.CopyOfContractorPO));
                parametersList.Add(new SqlParameter("@ContractComments", model.contractPaperWork.CopyOfContractorPOComments));
                parametersList.Add(new SqlParameter("@ScopePhased", model.contractPaperWork.BrokenScopephases));
                parametersList.Add(new SqlParameter("@ScopePhasedComments", model.contractPaperWork.BrokenScopephasesComments));
                parametersList.Add(new SqlParameter("@BIRollups", model.contractPaperWork.BidRollUp));
                parametersList.Add(new SqlParameter("@BIRollupsComments", model.contractPaperWork.BidRollUpComments));
                parametersList.Add(new SqlParameter("@Packslip", model.contractPaperWork.PackCFSPIRollUp));
                parametersList.Add(new SqlParameter("@PackslipComments", model.contractPaperWork.PackCFSPIRollUpcomments));
                parametersList.Add(new SqlParameter("@Quotes", model.contractPaperWork.ApplicableQuote));
                parametersList.Add(new SqlParameter("@QuotesComments", model.contractPaperWork.ApplicableQuoteComments));
                parametersList.Add(new SqlParameter("@Drawings", model.contractPaperWork.DrawingConditions));
                parametersList.Add(new SqlParameter("@DrawingsComments", model.contractPaperWork.DrawingConditionsComments));
                parametersList.Add(new SqlParameter("@Photos", model.contractPaperWork.SitePhotos));
                parametersList.Add(new SqlParameter("@PhotoComments", model.contractPaperWork.SitePhotosComments));
                parametersList.Add(new SqlParameter("@HardCard", model.contractPaperWork.HardCard));
                parametersList.Add(new SqlParameter("@HardCardComments", model.contractPaperWork.HardCardComments));
                parametersList.Add(new SqlParameter("@PayEnvelope", model.contractPaperWork.PayEnvelope));
                parametersList.Add(new SqlParameter("@PayEnvelopeComments", model.contractPaperWork.PayEnvelopeComments));
                parametersList.Add(new SqlParameter("@Bonding", model.bondingInsurance.InsuranceCertification));
                parametersList.Add(new SqlParameter("@BondingComments", model.bondingInsurance.InsuranceCertificationComments));
                parametersList.Add(new SqlParameter("@SafetyOfficer", model.safetyRequirements.SafetyOfficer));
                parametersList.Add(new SqlParameter("@SafetyOfficerComments", model.safetyRequirements.SafetyOfficerComments));
                parametersList.Add(new SqlParameter("@PreStartSafetyMeeting", model.safetyRequirements.SafetyMeeting));
                parametersList.Add(new SqlParameter("@PreStartSafetyMeetingComments", model.safetyRequirements.SafetyMeetingComments));
                parametersList.Add(new SqlParameter("@DailySafetyMeetings", model.safetyRequirements.DailySafetyMeeting));
                parametersList.Add(new SqlParameter("@DailySafetyMeetingComments", model.safetyRequirements.DailySafetyMeetingComments));
                parametersList.Add(new SqlParameter("@PPE", model.safetyRequirements.PPENeeded));
                parametersList.Add(new SqlParameter("@PPEComments", model.safetyRequirements.PPENeededComments));
                parametersList.Add(new SqlParameter("@Fall", model.safetyRequirements.FallProtection));
                parametersList.Add(new SqlParameter("@FallComments", model.safetyRequirements.FallProtectionComments));
                parametersList.Add(new SqlParameter("@EquipCerts", model.safetyRequirements.EquipmentCertification));
                parametersList.Add(new SqlParameter("@EquipCertComments", model.safetyRequirements.EquipmentCertificationComments));
                parametersList.Add(new SqlParameter("@OtherHaz", model.safetyRequirements.OtherHazards));
                parametersList.Add(new SqlParameter("@OtherHazComments", model.safetyRequirements.OtherHazardsComments));
                parametersList.Add(new SqlParameter("@OtherComments", model.otherImportantFactors.OtherPertinentInformation));
                parametersList.Add(new SqlParameter("@ApprovedBy", Convert.ToInt32(model.ApprovedBy)));
                parametersList.Add(new SqlParameter("@DateCompleted", model.DateCompleted));
                parametersList.Add(new SqlParameter("@DateReviewed", model.DateReviewed));

                var id = SqlHelper.ExecuteNonQuery(_myConnection, CommandType.StoredProcedure, "spATL_PRJ_JobActChkLst_InsUpd",
                                      parametersList.ToArray());
                result = (Convert.ToInt32(id) == -1) ? 1 : result;
            }
            catch (Exception ex)
            {
                Logger.SaveErr(ex);
            }
            return result;
        }

        public static JobActivationChecklist JobActivationLookup(JobActivationChecklist objActivation)
        {
            try
            {
                var resultSet = SqlHelper.ExecuteDataset(_myConnection, CommandType.StoredProcedure,
                                     "spATL_PRJ_JobActChkLst_LkUp",
                                     new SqlParameter("@PRJID", objActivation.PRJID));

                objActivation.ListCustomersTypes = new List<DOC08_CustomerType>();
                foreach (DataRow item in resultSet.Tables[0].Rows)
                {
                    var customer = new DOC08_CustomerType();
                    customer.CustomerTypeID = Convert.ToInt32(item["CustomerTypeId"]);
                    customer.Description = Convert.ToString(item["Description"]);
                    objActivation.ListCustomersTypes.Add(customer);
                }

                objActivation.ListJobTypes = new List<DOC09_JobTypes>();
                foreach (DataRow item in resultSet.Tables[1].Rows)
                {
                    var jobType = new DOC09_JobTypes();
                    jobType.JobTypeId = Convert.ToInt32(item["JobTypeId"]);
                    jobType.JobTypeDesc = Convert.ToString(item["JobTypeDesc"]);
                    objActivation.ListJobTypes.Add(jobType);
                }

                objActivation.ListResponses = new List<DOC10_Responses>();
                foreach (DataRow item in resultSet.Tables[2].Rows)
                {
                    var response = new DOC10_Responses();
                    response.ResponseId = Convert.ToInt32(item["ResponseId"]);
                    response.Response = Convert.ToString(item["Response"]);
                    objActivation.ListResponses.Add(response);
                }

                objActivation.ListLabourTypes = new List<SYS10_MhRates>();
                foreach (DataRow item in resultSet.Tables[3].Rows)
                {
                    var labourType = new SYS10_MhRates();
                    labourType.MhRateId = Convert.ToString(item["MhRateId"]);
                    labourType.MhRateName = Convert.ToString(item["MhRateName"]);
                    objActivation.ListLabourTypes.Add(labourType);
                }

                objActivation.ListUsers = new List<Sys_UsersData>();
                foreach (DataRow item in resultSet.Tables[4].Rows)
                {
                    var usersData = new Sys_UsersData();
                    usersData.UserId = Convert.ToInt32(item["UserId"]);
                    usersData.Name = Convert.ToString(item["Name"]);
                    objActivation.ListUsers.Add(usersData);
                }

                foreach (DataRow item in resultSet.Tables[5].Rows)
                {
                    objActivation.PRJID = Convert.ToInt32(item["ProjectHeaderId"]);
                    objActivation.projectInformation.JobNumber = Convert.ToString(item["Atlas_Job_Number"]);
                    objActivation.projectInformation.CompanyName = Convert.ToString(item["Atlas_Company_Name"]);
                    objActivation.projectInformation.CustomerProfile = new Profile();
                    objActivation.projectInformation.CustomerProfile.Name = Convert.ToString(item["Contractor_Name"]);
                    objActivation.projectInformation.CustomerProfile.Address = Convert.ToString(item["Contractor_Address"]);
                    objActivation.projectInformation.CustomerProfile.City = Convert.ToString(item["Contractor_City"]);
                    objActivation.projectInformation.CustomerProfile.State = Convert.ToString(item["Contractor_State"]);
                    objActivation.projectInformation.CustomerProfile.Zip = Convert.ToString(item["Contractor_Zip"]);
                    objActivation.projectInformation.CustomerProfile.PhoneNumber = Convert.ToString(item["Contractor_Phone"]);
                    //TODO: Check Ext, Contact
                    //objActivation.projectInformation.CustomerProfile.Extension = Convert.ToString(item["Contractor_PhoneExt"]);
                    //objActivation.projectInformation.CustomerProfile.ContactName = Convert.ToString(item["Contractor_Contact"]);
                    objActivation.projectInformation.ProjectProfile = new Profile();
                    objActivation.projectInformation.ProjectProfile.Name = Convert.ToString(item["JobName"]);
                    objActivation.projectInformation.ProjectProfile.Address = Convert.ToString(item["JobAddress"]);
                    objActivation.projectInformation.ProjectProfile.City = Convert.ToString(item["JobCity"]);
                    objActivation.projectInformation.ProjectProfile.State = Convert.ToString(item["JobState"]);
                    objActivation.projectInformation.ProjectProfile.Zip = Convert.ToString(item["JobZip"]);
                    objActivation.projectInformation.ProjectProfile.PhoneNumber = Convert.ToString(item["JobPhone"]);
                    //TODO: 
                    //objActivation.projectInformation.ProjectProfile.Extension = Convert.ToString(item["Job_PhoneExt"]);
                    objActivation.projectInformation.ProjectProfile.ContactName = Convert.ToString(item["JobContact"]);
                    objActivation.projectInformation.Estimator = Convert.ToString(item["EstimatorsName"]);
                }

                if(resultSet.Tables[5].Rows.Count == 0)
                {
                    objActivation.PRJID = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErr(ex);
                throw ex;
            }
            return objActivation;
        }

        internal static JobActivationChecklist getProjectActivationDetails(JobActivationChecklist objActivation)
        {
            try
            {
                var resultSet = SqlHelper.ExecuteDataset(_myConnection, CommandType.StoredProcedure,
                                    "spATL_PRJ_JobActChkLst_GetDtls",
                                    new SqlParameter("@PRJID", objActivation.PRJID));
                objActivation.projectInformation = new ProjectInformation();
                objActivation.contractPaperWork = new ContractPaperWork();
                objActivation.bondingInsurance = new BondingInsurance();
                objActivation.safetyRequirements = new SafetyRequirements();
                objActivation.otherImportantFactors = new OtherImportantFactors();
                foreach (DataRow item in resultSet.Tables[0].Rows)
                {
                    objActivation.recordExists = new Guid().ToString();
                    objActivation.projectInformation.CustomerType = Convert.ToString(item["CustomerTypeID"]);
                    objActivation.projectInformation.Jobtype = Convert.ToString(item["JobTypeId"]);

                    objActivation.projectInformation.CustomerBidReference = Convert.ToString(item["CustomerBid_JobNum"]);

                    objActivation.projectInformation.ScopeWorkToBePerformed = Convert.ToString(item["Scope"]);
                    objActivation.projectInformation.TypeOfLabour = Convert.ToString(item["LabourType"]);

                    objActivation.contractPaperWork.CopyOfContractorPO = Convert.ToString(item["Contract"]);
                    objActivation.contractPaperWork.CopyOfContractorPOComments = Convert.ToString(item["ContractComments"]);

                    objActivation.contractPaperWork.BrokenScopephases = Convert.ToString(item["ScopePhased"]);
                    objActivation.contractPaperWork.BrokenScopephasesComments = Convert.ToString(item["ScopePhasedComments"]);

                    objActivation.contractPaperWork.BidRollUp = Convert.ToString(item["BIRollups"]);
                    objActivation.contractPaperWork.BidRollUpComments = Convert.ToString(item["BIRollupsComments"]);

                    objActivation.contractPaperWork.PackCFSPIRollUp = Convert.ToString(item["Packslip"]);
                    objActivation.contractPaperWork.PackCFSPIRollUpcomments = Convert.ToString(item["PackslipComments"]);

                    objActivation.contractPaperWork.ApplicableQuote = Convert.ToString(item["Quotes"]);
                    objActivation.contractPaperWork.ApplicableQuoteComments = Convert.ToString(item["QuotesComments"]);

                    objActivation.contractPaperWork.SitePhotos = Convert.ToString(item["Photos"]);
                    objActivation.contractPaperWork.SitePhotosComments = Convert.ToString(item["PhotoComments"]);

                    objActivation.contractPaperWork.DrawingConditions = Convert.ToString(item["Drawings"]);
                    objActivation.contractPaperWork.DrawingConditionsComments = Convert.ToString(item["DrawingsComments"]);

                    objActivation.contractPaperWork.HardCard = Convert.ToString(item["HardCard"]);
                    objActivation.contractPaperWork.HardCardComments = Convert.ToString(item["HardCardComments"]);

                    objActivation.contractPaperWork.PayEnvelope = Convert.ToString(item["PayEnvelope"]);
                    objActivation.contractPaperWork.PayEnvelopeComments = Convert.ToString(item["PayEnvelopeComments"]);

                    objActivation.bondingInsurance.InsuranceCertification = Convert.ToString(item["Bonding"]);
                    objActivation.bondingInsurance.InsuranceCertificationComments = Convert.ToString(item["BondingComments"]);

                    objActivation.safetyRequirements.SafetyOfficer = Convert.ToString(item["SafetyOfficer"]);
                    objActivation.safetyRequirements.SafetyOfficerComments = Convert.ToString(item["SafetyOfficerComments"]);

                    objActivation.safetyRequirements.SafetyMeeting = Convert.ToString(item["PreStartSafetyMeeting"]);
                    objActivation.safetyRequirements.SafetyMeetingComments = Convert.ToString(item["PreStartSafetyMeetingComments"]);

                    objActivation.safetyRequirements.DailySafetyMeeting = Convert.ToString(item["DailySafetyMeetings"]);
                    objActivation.safetyRequirements.DailySafetyMeetingComments = Convert.ToString(item["DailySafetyMeetingComments"]);

                    objActivation.safetyRequirements.PPENeeded = Convert.ToString(item["PPE"]);
                    objActivation.safetyRequirements.PPENeededComments = Convert.ToString(item["PPEComments"]);

                    objActivation.safetyRequirements.FallProtection = Convert.ToString(item["Fall"]);
                    objActivation.safetyRequirements.FallProtectionComments = Convert.ToString(item["FallComments"]);

                    objActivation.safetyRequirements.EquipmentCertification = Convert.ToString(item["EquipCerts"]);
                    objActivation.safetyRequirements.EquipmentCertificationComments = Convert.ToString(item["EquipCertComments"]);

                    objActivation.safetyRequirements.OtherHazards = Convert.ToString(item["OtherHaz"]);
                    objActivation.safetyRequirements.OtherHazardsComments = Convert.ToString(item["OtherHazComments"]);

                    objActivation.otherImportantFactors.OtherPertinentInformation = Convert.ToString(item["OtherComments"]);
                    objActivation.ApprovedBy = Convert.ToString(item["Approved"]);
                    objActivation.DateCompleted = Convert.ToDateTime(item["DateCompleted"]).ToShortDateString();
                    objActivation.DateReviewed = Convert.ToDateTime(item["ApprovedDate"]).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                objActivation.recordExists = string.Empty;
                Logger.SaveErr(ex);
            }
            return objActivation;
        }
    }
}
