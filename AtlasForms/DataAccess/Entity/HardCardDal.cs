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
    public class HardCardDal
    {
        private static String exepurl;
        private static string _myConnection;

        static HardCardDal()
        {
            DataObject dataObject = new DataObject();
            _myConnection = dataObject.getConnection();
        }

        public static HardCard getHardCardLookUpList(HardCard objHardCard)
        {
            try
            {
                


                var resultSet = SqlHelper.ExecuteDataset(_myConnection, CommandType.StoredProcedure,
                                     "spATL_PRJ_HardCard_LkUp",
                                     new SqlParameter("@PRJID", objHardCard.ProjectHeaderId));

                if (objHardCard.JobInformationDetails == null)
                {
                    objHardCard.JobInformationDetails = new JobInformation();
                }
                if (objHardCard.ContractInformationDetails == null)
                {
                    objHardCard.ContractInformationDetails = new ContractorInformation();
                }
                if (objHardCard.InstallationDetails == null)
                {
                    objHardCard.InstallationDetails = new Installation();
                }
                if (objHardCard.BuildChecklistDetails == null)
                {
                    objHardCard.BuildChecklistDetails = new BuildChecklist();
                }

                if (resultSet.Tables[0].Rows.Count > 0)
                {
                    var item = resultSet.Tables[0].Rows[0];

                    objHardCard.JobInformationDetails.EstimatorsName = Convert.ToString(item["EstimatorsName"]);
                    objHardCard.JobInformationDetails.ContractPhoneNumber = Convert.ToString(item["ContractPhoneNumber"]);
                    objHardCard.JobInformationDetails.JobName = Convert.ToString(item["JobName"]);
                    objHardCard.JobInformationDetails.JobAddress = Convert.ToString(item["JobAddress"]);
                    objHardCard.JobInformationDetails.JobCity = Convert.ToString(item["JobCity"]);
                    objHardCard.JobInformationDetails.JobState = Convert.ToString(item["JobState"]);
                    objHardCard.JobInformationDetails.JobZip = Convert.ToString(item["JobZip"]);
                    objHardCard.JobInformationDetails.JobContact = Convert.ToString(item["JobContact"]);
                    objHardCard.JobInformationDetails.JobPhone = Convert.ToString(item["JobPhone"]);
                    objHardCard.JobInformationDetails.Atlas_Job_Number = Convert.ToString(item["ProjectHeaderId"]);
                    objHardCard.JobInformationDetails.BidItemHeaderId = Convert.ToInt32(item["BidItemHeaderId"]);
                    objHardCard.JobInformationDetails.ListJobInfoResponse = new List<DOC10_Responses>();
                    objHardCard.JobInformationDetails.ListJobInfoResponse = getResponseList(resultSet);
                }
                if (resultSet.Tables[0].Rows.Count > 0)
                {
                    var item = resultSet.Tables[0].Rows[0];
                    objHardCard.ContractInformationDetails.ContractorName = Convert.ToString(item["ContName"]);
                    objHardCard.ContractInformationDetails.PrimaryContact = Convert.ToString(item["ContContact"]);
                    objHardCard.ContractInformationDetails.PrimaryPhone = Convert.ToString(item["ContPhone"]);
                    objHardCard.ContractInformationDetails.ListFenceTypes = new List<SYS07_FenceTypes>();
                    objHardCard.ContractInformationDetails.ListFenceTypes = getFenceTypes(resultSet);

                }


                objHardCard.InstallationDetails.ListInstallationResponses = new List<DOC10_Responses>();
                objHardCard.InstallationDetails.ListInstallationResponses = getResponseList(resultSet);

                objHardCard.InstallationDetails.ListDigType = getDigTypes(resultSet);

                objHardCard.InstallationDetails.ListGateInstallation = new List<DOC05_GateInstallation>();
                objHardCard.InstallationDetails.ListGateInstallation = getGateInstallation(resultSet);


                objHardCard.BuildChecklistDetails.ListBuildResponses = new List<DOC10_Responses>();
                objHardCard.BuildChecklistDetails.ListBuildResponses = getResponseList(resultSet);

                objHardCard.BuildChecklistDetails.ListFenceDirection = new List<DOC04_FenceDirection>();
                objHardCard.BuildChecklistDetails.ListFenceDirection = getFenceDirections(resultSet);

                objHardCard.BuildChecklistDetails.ListFenceInstall = new List<DOC03_FenceInstall>();
                objHardCard.BuildChecklistDetails.ListFenceInstall = getFenceInstalls(resultSet);

                objHardCard.BuildChecklistDetails.ListTearOutType = new List<DOC06_TearOutTypes>();
                objHardCard.BuildChecklistDetails.ListTearOutType = getTearOutTypes(resultSet);

                return objHardCard;

            }
            catch(Exception ex)
            {
                Logger.SaveErr(ex);
                return objHardCard;
            }
        }

        internal static void getHardCardDetails(HardCard model)
        {
            var resultSet = SqlHelper.ExecuteDataset(_myConnection, CommandType.StoredProcedure,
                                     "spATL_PRJ_HardCard_GetDtls",
                                     new SqlParameter("@HardCardID", model.HardCardId)).Tables[0];
           if(resultSet.Rows.Count > 0)
            {
                var rowItem = resultSet.Rows[0];
                model.ProjectHeaderId = Convert.ToInt32(rowItem["ProjectHeaderId"]);
                if(model.JobInformationDetails == null)
                {
                    model.JobInformationDetails = new JobInformation();
                }
                if(model.ContractInformationDetails == null)
                {
                    model.ContractInformationDetails = new ContractorInformation();
                }
                if(model.InstallationDetails == null)
                {
                    model.InstallationDetails = new Installation();
                }
                if(model.BuildChecklistDetails == null)
                {
                    model.BuildChecklistDetails = new BuildChecklist();
                }
                model.JobInformationDetails.Atlas_Job_Number = Convert.ToString(rowItem["ProjectHeaderId"]);
                model.JobInformationDetails.BidItemHeaderId = Convert.ToInt32(rowItem["BidItemHeaderId"]);
                model.JobInformationDetails.CallInRoute = Convert.ToString(rowItem["CallInRoute"]);
                model.ContractInformationDetails.FenceType = Convert.ToString(rowItem["FenceTypeId"]);
                model.ContractInformationDetails.SplNotes = Convert.ToString(rowItem["SpecialComments"]);
                model.ContractInformationDetails.Directions = Convert.ToString(rowItem["LocationDirections"]);
                model.InstallationDetails.PremakeGate = Convert.ToInt32(rowItem["PremakeGate"]);
                model.InstallationDetails.CBYD = Convert.ToInt32(rowItem["CBYD"]);
                model.InstallationDetails.CBYDDate = Convert.ToDateTime(rowItem["CBYDDate"]).ToShortDateString();
                model.InstallationDetails.CBYDNumber = Convert.ToString(rowItem["CBYDNumber"]);
                model.InstallationDetails.StartDate = Convert.ToDateTime(rowItem["StartDate"]).ToShortDateString();
                model.InstallationDetails.FinishDate = Convert.ToDateTime(rowItem["FinishDate"]).ToShortDateString();
                model.InstallationDetails.HardDate = Convert.ToInt32(rowItem["HardDate"]);
                model.InstallationDetails.GateDescription1 = Convert.ToString(rowItem["GateDescription1"]);
                model.InstallationDetails.GateInstallationID1 = Convert.ToInt32(rowItem["GateInstallationID1"]);
                model.InstallationDetails.GateDescription2 = Convert.ToString(rowItem["GateDescription2"]);
                model.InstallationDetails.GateInstallationID2 = Convert.ToInt32(rowItem["GateInstallationID2"]);
                model.InstallationDetails.GateDescription3 = Convert.ToString(rowItem["GateDescription3"]);
                model.InstallationDetails.GateInstallationID3 = Convert.ToInt32(rowItem["GateInstallationID3"]);
                model.InstallationDetails.GateDescription4 = Convert.ToString(rowItem["GateDescription4"]);
                model.InstallationDetails.GateInstallationID4 = Convert.ToInt32(rowItem["GateInstallationID4"]);
                model.InstallationDetails.GateDescription5 = Convert.ToString(rowItem["GateDescription5"]);
                model.InstallationDetails.GateInstallationID5 = Convert.ToInt32(rowItem["GateInstallationID5"]);
                model.InstallationDetails.EquipmentRequired = Convert.ToString(rowItem["EquipmentRequired"]);
                model.InstallationDetails.DigTypeID = Convert.ToInt32(rowItem["DigTypeID"]);
                model.InstallationDetails.WaterAvailible = Convert.ToInt32(rowItem["WaterAvailible"]);
                model.InstallationDetails.ElectricityAvailible = Convert.ToInt32(rowItem["ElectricityAvailible"]);
                model.InstallationDetails.MeasureByInstaller = Convert.ToInt32(rowItem["MeasureByInstaller"]);
                model.InstallationDetails.LeaveSamplesByInstaller = Convert.ToInt32(rowItem["LeaveSamplesByInstaller"]);
                model.InstallationDetails.Scope = Convert.ToString(rowItem["Scope"]);
                model.InstallationDetails.BudgetedInstallDays = Convert.ToDecimal(rowItem["BudgetedInstallDays"]);

                model.BuildChecklistDetails.PoolCode = Convert.ToInt32(rowItem["PoolCode"]);
                model.BuildChecklistDetails.BuildForRack = Convert.ToInt32(rowItem["BuildForRack"]);
                model.BuildChecklistDetails.SteppingTemp = Convert.ToInt32(rowItem["SteppingTemp"]);
                model.BuildChecklistDetails.Dowelled = Convert.ToInt32(rowItem["Dowelled"]);
                model.BuildChecklistDetails.Morticed = Convert.ToInt32(rowItem["Morticed"]);
                model.BuildChecklistDetails.NailOn = Convert.ToInt32(rowItem["NailOn"]);
                model.BuildChecklistDetails.SingleNailed = Convert.ToInt32(rowItem["SingleNailed"]);
                model.BuildChecklistDetails.BuildFullSections = Convert.ToInt32(rowItem["BuildFullSections"]);
                model.BuildChecklistDetails.FenceDirectionID = Convert.ToInt32(rowItem["FenceDirectionID"]);
                model.BuildChecklistDetails.FenceInstallID = Convert.ToInt32(rowItem["FenceInstallID"]);
                model.BuildChecklistDetails.Stain = Convert.ToInt32(rowItem["Stain"]);
                model.BuildChecklistDetails.StainColor = Convert.ToString(rowItem["StainColor"]);
                model.BuildChecklistDetails.StainBrand = Convert.ToString(rowItem["StainBrand"]);
                model.BuildChecklistDetails.TrimInField = Convert.ToInt32(rowItem["TrimInField"]);
                model.BuildChecklistDetails.PostPins = Convert.ToInt32(rowItem["PostPins"]);
                model.BuildChecklistDetails.TearOutTypeID = Convert.ToInt32(rowItem["TearOutTypeID"]);
                model.BuildChecklistDetails.SafetyOfficerOnsite = Convert.ToInt32(rowItem["SafetyOfficerOnsite"]);
                model.BuildChecklistDetails.SafteyMeetingOrintationOnsiteReq = Convert.ToInt32(rowItem["SafteyMeetingOrintationOnsiteReq"]);
                model.BuildChecklistDetails.SafteyInspectionReqBeforeJobStarts = Convert.ToInt32(rowItem["SafteyInspectionReqBeforeJobStarts"]);
                model.BuildChecklistDetails.PPERequired = Convert.ToInt32(rowItem["PPERequired"]);
                model.BuildChecklistDetails.EquipmentOperatorCertsReq = Convert.ToInt32(rowItem["EquipmentOperatorCertsReq"]);
                model.BuildChecklistDetails.OtherHazards = Convert.ToString(rowItem["OtherHazards"]);
                model.BuildChecklistDetails.Notes = Convert.ToString(rowItem["Notes"]);
               

            }

            
         }

        internal static object saveJobDetails(HardCard model)
        {
            int result = 0;
            try
            {
                List<SqlParameter> parametersList = new List<SqlParameter>();
                parametersList.Add(new SqlParameter("@HardCardID", model.HardCardId));
                parametersList.Add(new SqlParameter("@ProjectHeaderId", model.JobInformationDetails.Atlas_Job_Number));
                parametersList.Add(new SqlParameter("@BidItemHeaderId", model.JobInformationDetails.BidItemHeaderId));
                parametersList.Add(new SqlParameter("@CallInRoute", model.JobInformationDetails.CallInRoute));
                parametersList.Add(new SqlParameter("@FenceTypeId", model.ContractInformationDetails.FenceType));
                parametersList.Add(new SqlParameter("@SpecialComments", model.ContractInformationDetails.SplNotes));
                parametersList.Add(new SqlParameter("@LocationDirections", model.ContractInformationDetails.Directions));
                parametersList.Add(new SqlParameter("@Scope", model.InstallationDetails.Scope));
                parametersList.Add(new SqlParameter("@CBYD", model.InstallationDetails.CBYD));
                parametersList.Add(new SqlParameter("@CBYDDate", model.InstallationDetails.CBYDDate));
                parametersList.Add(new SqlParameter("@CBYDNumber", model.InstallationDetails.CBYDNumber));
                parametersList.Add(new SqlParameter("@EquipmentRequired", model.InstallationDetails.EquipmentRequired));
                parametersList.Add(new SqlParameter("@DigTypeID", model.InstallationDetails.DigTypeID));
                parametersList.Add(new SqlParameter("@WaterAvailible", model.InstallationDetails.WaterAvailible));
                parametersList.Add(new SqlParameter("@ElectricityAvailible", model.InstallationDetails.ElectricityAvailible));
                parametersList.Add(new SqlParameter("@MeasureByInstaller", model.InstallationDetails.MeasureByInstaller));
                parametersList.Add(new SqlParameter("@LeaveSamplesByInstaller", model.InstallationDetails.LeaveSamplesByInstaller));
                parametersList.Add(new SqlParameter("@StartDate", model.InstallationDetails.StartDate));
                parametersList.Add(new SqlParameter("@FinishDate", model.InstallationDetails.FinishDate));
                parametersList.Add(new SqlParameter("@HardDate", model.InstallationDetails.HardDate));
                parametersList.Add(new SqlParameter("@PremakeGate", model.InstallationDetails.PremakeGate));
                parametersList.Add(new SqlParameter("@GateDescription1", model.InstallationDetails.GateDescription1));
                parametersList.Add(new SqlParameter("@GateInstallationID1", model.InstallationDetails.GateInstallationID1));
                parametersList.Add(new SqlParameter("@GateDescription2", model.InstallationDetails.GateDescription2));
                parametersList.Add(new SqlParameter("@GateInstallationID2", model.InstallationDetails.GateInstallationID2));
                parametersList.Add(new SqlParameter("@GateDescription3", model.InstallationDetails.GateDescription3));
                parametersList.Add(new SqlParameter("@GateInstallationID3", model.InstallationDetails.GateInstallationID3));
                parametersList.Add(new SqlParameter("@GateDescription4", model.InstallationDetails.GateDescription4));
                parametersList.Add(new SqlParameter("@GateInstallationID4", model.InstallationDetails.GateInstallationID4));
                parametersList.Add(new SqlParameter("@GateDescription5", model.InstallationDetails.GateDescription5));
                parametersList.Add(new SqlParameter("@GateInstallationID5", model.InstallationDetails.GateInstallationID5));
                parametersList.Add(new SqlParameter("@BudgetedInstallDays", model.InstallationDetails.BudgetedInstallDays));
                parametersList.Add(new SqlParameter("@PoolCode", model.BuildChecklistDetails.PoolCode));
                parametersList.Add(new SqlParameter("@BuildForRack", model.BuildChecklistDetails.BuildForRack));
                parametersList.Add(new SqlParameter("@SteppingTemp", model.BuildChecklistDetails.SteppingTemp));
                parametersList.Add(new SqlParameter("@Dowelled", model.BuildChecklistDetails.Dowelled));
                parametersList.Add(new SqlParameter("@Morticed", model.BuildChecklistDetails.Morticed));
                parametersList.Add(new SqlParameter("@NailOn", model.BuildChecklistDetails.NailOn));
                parametersList.Add(new SqlParameter("@SingleNailed", model.BuildChecklistDetails.SingleNailed));
                parametersList.Add(new SqlParameter("@BuildFullSections", model.BuildChecklistDetails.BuildFullSections));
                parametersList.Add(new SqlParameter("@FenceDirectionID", model.BuildChecklistDetails.FenceDirectionID));
                parametersList.Add(new SqlParameter("@FenceInstallID", model.BuildChecklistDetails.FenceInstallID));
                parametersList.Add(new SqlParameter("@Stain", model.BuildChecklistDetails.Stain));
                parametersList.Add(new SqlParameter("@StainColor", model.BuildChecklistDetails.StainColor));
                parametersList.Add(new SqlParameter("@StainBrand", model.BuildChecklistDetails.StainBrand));
                parametersList.Add(new SqlParameter("@TrimInField", model.BuildChecklistDetails.TrimInField));
                parametersList.Add(new SqlParameter("@PostPins", model.BuildChecklistDetails.PostPins));
                parametersList.Add(new SqlParameter("@TearOutTypeID", model.BuildChecklistDetails.TearOutTypeID));
                parametersList.Add(new SqlParameter("@SafetyOfficerOnsite", model.BuildChecklistDetails.SafetyOfficerOnsite));
                parametersList.Add(new SqlParameter("@SafteyMeetingOrintationOnsiteReq", model.BuildChecklistDetails.SafteyMeetingOrintationOnsiteReq));
                parametersList.Add(new SqlParameter("@SafteyInspectionReqBeforeJobStarts", model.BuildChecklistDetails.SafteyInspectionReqBeforeJobStarts));
                parametersList.Add(new SqlParameter("@PPERequired", model.BuildChecklistDetails.PPERequired));
                parametersList.Add(new SqlParameter("@EquipmentOperatorCertsReq", model.BuildChecklistDetails.EquipmentOperatorCertsReq));
                parametersList.Add(new SqlParameter("@OtherHazards", model.BuildChecklistDetails.OtherHazards));
                parametersList.Add(new SqlParameter("@Notes", model.BuildChecklistDetails.Notes));


                var id = SqlHelper.ExecuteScalar(_myConnection, CommandType.StoredProcedure, "spATL_PRJ_HardCard_InsUpd",
                                     parametersList.ToArray());
                result = (Convert.ToInt32(id) == -1) ? 1 : result;
            }
            catch (Exception ex)
            {
                Logger.SaveErr(ex);
            }
            return result;
        }

        private static List<DOC10_Responses> getResponseList(DataSet resultSet)
        {
            List<DOC10_Responses> listResponse = new List<DOC10_Responses>();
            foreach (DataRow item in resultSet.Tables[1].Rows)
            {
                var response = new DOC10_Responses();
                response.ResponseId = Convert.ToInt32(item["ResponseId"]);
                response.Response = Convert.ToString(item["Response"]);
                listResponse.Add(response);
            }
            return listResponse;
        } 
        private static List<SYS07_FenceTypes> getFenceTypes(DataSet resultSet)
        {
            List<SYS07_FenceTypes> ListFenceTypes = new List<SYS07_FenceTypes>();
            foreach (DataRow item in resultSet.Tables[2].Rows)
            {
                var fenceType = new SYS07_FenceTypes();
                fenceType.FenceTypeId = Convert.ToInt32(item["FenceTypeId"]);
                fenceType.FenceTypeName = Convert.ToString(item["FenceTypeName"]);
                ListFenceTypes.Add(fenceType);
            }
            return ListFenceTypes;
        }

        private static List<SYS14_DigType> getDigTypes(DataSet resultSet)
        {
            List<SYS14_DigType> ListDigTypes = new List<SYS14_DigType>();
            foreach (DataRow item in resultSet.Tables[3].Rows)
            {
                var DigTypes = new SYS14_DigType();
                DigTypes.DigTypeId = Convert.ToInt32(item["DigTypeId"]);
                DigTypes.DigType = Convert.ToString(item["DigType"]);
                ListDigTypes.Add(DigTypes);
            }
            return ListDigTypes;
        }

        private static List<DOC05_GateInstallation> getGateInstallation(DataSet resultSet)
        {
            List<DOC05_GateInstallation> ListGateInstallation = new List<DOC05_GateInstallation>();
            foreach (DataRow item in resultSet.Tables[4].Rows)
            {
                var gateInstallation = new DOC05_GateInstallation();
                gateInstallation.GateInstallationID = Convert.ToInt32(item["GateInstallationID"]);
                gateInstallation.Description = Convert.ToString(item["Description"]);
                ListGateInstallation.Add(gateInstallation);
            }
            return ListGateInstallation;
        }


        private static List<DOC04_FenceDirection> getFenceDirections(DataSet resultSet)
        {
            List<DOC04_FenceDirection> ListFenceDirection = new List<DOC04_FenceDirection>();
            foreach (DataRow item in resultSet.Tables[5].Rows)
            {
                var fenceDirection = new DOC04_FenceDirection();
                fenceDirection.FenceDirectionID = Convert.ToInt32(item["FenceDirectionID"]);
                fenceDirection.Description = Convert.ToString(item["Description"]);
                ListFenceDirection.Add(fenceDirection);
            }
            return ListFenceDirection;
        }

        private static List<DOC03_FenceInstall> getFenceInstalls(DataSet resultSet)
        {
            List<DOC03_FenceInstall> ListFenceInstall = new List<DOC03_FenceInstall>();
            foreach (DataRow item in resultSet.Tables[6].Rows)
            {
                var fenceInstall = new DOC03_FenceInstall();
                fenceInstall.FenceInstallID = Convert.ToInt32(item["FenceInstallID"]);
                fenceInstall.Description = Convert.ToString(item["Description"]);
                ListFenceInstall.Add(fenceInstall);
            }
            return ListFenceInstall;
        }

        private static List<DOC06_TearOutTypes> getTearOutTypes(DataSet resultSet)
        {
            List<DOC06_TearOutTypes> ListTearOutTypes = new List<DOC06_TearOutTypes>();
            foreach (DataRow item in resultSet.Tables[7].Rows)
            {
                var tearoutType = new DOC06_TearOutTypes();
                tearoutType.TearOutTypeID = Convert.ToInt32(item["TearOutTypeID"]);
                tearoutType.Description = Convert.ToString(item["Description"]);
                ListTearOutTypes.Add(tearoutType);
            }
            return ListTearOutTypes;
        }
    }
}

