using AtlasForms.DataAccess.Entity;
using AtlasForms.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasForms.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports

        [Route("Report/JobActivation/id/{prjid}")]
        public FileStreamResult printJobActivation(string prjid)
        {
            string title = string.Empty;

            var model = new JobActivationChecklist();
            model.PRJID = Convert.ToInt32(prjid);
            model = ChecklistDal.getProjectActivationDetails(model);
            model = ChecklistDal.JobActivationLookup(model);

            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A4, 15, 10, 15, 35);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            writer.PageEvent = new PDFReportEventHelper(title);

            // Open the PDF document
            document.Open();
            document.Add(PDFUtil.HeaderSection("Job Activation Checklist"));

            var projectTable = PDFUtil.createTableWithHeader("Project Information", new float[] { 3, 6 });
            projectTable.AddCell(PDFUtil.CreateCell("Atlas Job Number", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.JobNumber, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Atlas Company Name", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.CompanyName, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Customer/Contractor Name", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.CustomerProfile.Name, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Address, City, State, Zip", PDFUtil.font_body_bold, 2, false));
            string CustomerAddr = string.Format("{0} | {1} | {2} | {3}",
                                                        model.projectInformation.CustomerProfile.Address ?? "-",
                                                        model.projectInformation.CustomerProfile.City ?? "-",
                                                        model.projectInformation.CustomerProfile.State ?? "-",
                                                        model.projectInformation.CustomerProfile.Zip ?? "-");
            projectTable.AddCell(PDFUtil.CreateCell(CustomerAddr, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Phone Number, Extension", PDFUtil.font_body_bold, 2, false));
            string CustomerPhone = string.Format("{0} | {1}",
                                                        AppUtil.formatPhoneNumber(model.projectInformation.CustomerProfile.PhoneNumber) ?? "-",
                                                         AppUtil.formatPhoneNumber(model.projectInformation.CustomerProfile.Extension) ?? "-");
            projectTable.AddCell(PDFUtil.CreateCell(CustomerPhone, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Project Name", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.ProjectProfile.Name, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Address, City, State, Zip", PDFUtil.font_body_bold, 2, false));
            string ProjectAddr = string.Format("{0} | {1} | {2} | {3}",
                                                       model.projectInformation.ProjectProfile.Address ?? "-",
                                                       model.projectInformation.ProjectProfile.City ?? "-",
                                                       model.projectInformation.ProjectProfile.State ?? "-",
                                                       model.projectInformation.ProjectProfile.Zip ?? "-");
            projectTable.AddCell(PDFUtil.CreateCell(ProjectAddr, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Contact Name/Phone Number, Ext", PDFUtil.font_body_bold, 2, false));
            string ProjectPhone = string.Format("{0} | {1} | {2}",
                                                        model.projectInformation.CustomerProfile.ContactName ?? "-",
                                                        AppUtil.formatPhoneNumber(model.projectInformation.CustomerProfile.PhoneNumber) ?? "-",
                                                        AppUtil.formatPhoneNumber(model.projectInformation.CustomerProfile.Extension) ?? "-");
            projectTable.AddCell(PDFUtil.CreateCell(ProjectPhone, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Customer Type", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.ListCustomersTypes.
                First(i => i.CustomerTypeID.ToString() == model.projectInformation.CustomerType).Description, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Job Type", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.ListJobTypes.
                First(i => i.JobTypeId.ToString() == model.projectInformation.Jobtype).JobTypeDesc, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Customer Bid/Job Reference #", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.CustomerBidReference, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Scope of Work to be performed", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.ScopeWorkToBePerformed, PDFUtil.spanNormalBlack, 0, false));

            projectTable.AddCell(PDFUtil.CreateCell("Type of Labor", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.ListLabourTypes.First(i => i.MhRateId.ToString() == model.projectInformation.TypeOfLabour).MhRateName, PDFUtil.spanNormalBlack, 0, false));

            document.Add(projectTable);

            var ContractTable = PDFUtil.createTableWithHeader("Contract/Job Paperwork", new float[] { 3, 1, 1.5f, 5 });
            ContractTable.AddCell(PDFUtil.CreateCell("Copy of Contract or PO", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.CopyOfContractorPO).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.CopyOfContractorPOComments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Broken scope into appropriate phases", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.BrokenScopephases).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.BrokenScopephasesComments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Bid Roll-up (For each BI being activated)", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.BidRollUp).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.BidRollUpComments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Pack CFS for each BI rollup", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.PackCFSPIRollUp).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.PackCFSPIRollUpcomments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Applicable quote(s) for special material", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.ApplicableQuote).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.ApplicableQuoteComments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Drawings/field conditions or grading reports", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.DrawingConditions).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.DrawingConditionsComments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Site Photos", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.SitePhotos).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.SitePhotosComments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Hard Card completely filled out", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.HardCard).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.HardCardComments, PDFUtil.spanNormalBlack, 0, false));

            ContractTable.AddCell(PDFUtil.CreateCell("Pay Envelope (residential only)", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.contractPaperWork.PayEnvelope).Response, PDFUtil.spanNormalBlack, 0, false));
            ContractTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            ContractTable.AddCell(PDFUtil.CreateCell(model.contractPaperWork.PayEnvelopeComments, PDFUtil.spanNormalBlack, 0, false));

            document.Add(ContractTable);

            var BondingTable = PDFUtil.createTableWithHeader("Bonding/Insurance/Labor ", new float[] { 3, 1, 1.5f, 5 });
            BondingTable.AddCell(PDFUtil.CreateCell("Received bond (if required) and necessary insurance certification", PDFUtil.font_body_bold, 2, false));
            BondingTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.bondingInsurance.InsuranceCertification).Response, PDFUtil.spanNormalBlack, 0, false));
            BondingTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            BondingTable.AddCell(PDFUtil.CreateCell(model.bondingInsurance.InsuranceCertificationComments, PDFUtil.spanNormalBlack, 0, false));

            document.Add(BondingTable);

            var SafetyTable = PDFUtil.createTableWithHeader("Safety Requirements - For all 'Yes' answers, please provide additional details",
                                        new float[] { 3, 1, 1.5f, 5 });
            SafetyTable.AddCell(PDFUtil.CreateCell("Is there a safety officer on site? If so, provide Contact Informatios there a safety officer on site? If so, provide Contact Information", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.safetyRequirements.SafetyOfficer).Response, PDFUtil.spanNormalBlack, 0, false));
            SafetyTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.safetyRequirements.SafetyOfficerComments, PDFUtil.spanNormalBlack, 0, false));

            SafetyTable.AddCell(PDFUtil.CreateCell("Any safety meetings/orientation or badging needed before the job starts?", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.safetyRequirements.SafetyMeeting).Response, PDFUtil.spanNormalBlack, 0, false));
            SafetyTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.safetyRequirements.SafetyMeetingComments, PDFUtil.spanNormalBlack, 0, false));

            SafetyTable.AddCell(PDFUtil.CreateCell("Any daily safety meetings, truck/tool inspections required?", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.safetyRequirements.DailySafetyMeeting).Response, PDFUtil.spanNormalBlack, 0, false));
            SafetyTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.safetyRequirements.DailySafetyMeetingComments, PDFUtil.spanNormalBlack, 0, false));

            SafetyTable.AddCell(PDFUtil.CreateCell("Any specific PPE needed (e.g. fall protection,flotation, fire clothing, etc.)", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.safetyRequirements.PPENeeded).Response, PDFUtil.spanNormalBlack, 0, false));
            SafetyTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.safetyRequirements.PPENeededComments, PDFUtil.spanNormalBlack, 0, false));

            SafetyTable.AddCell(PDFUtil.CreateCell("Is fall protection is required? Please provide details of the situation ?", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.safetyRequirements.FallProtection).Response, PDFUtil.spanNormalBlack, 0, false));
            SafetyTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.safetyRequirements.FallProtectionComments, PDFUtil.spanNormalBlack, 0, false));

            SafetyTable.AddCell(PDFUtil.CreateCell("Any equipment certifications required (e.g. Bobcats, Forklifts, High lift, etc)?", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.safetyRequirements.EquipmentCertification).Response, PDFUtil.spanNormalBlack, 0, false));
            SafetyTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.safetyRequirements.EquipmentCertificationComments, PDFUtil.spanNormalBlack, 0, false));

            SafetyTable.AddCell(PDFUtil.CreateCell("Other hazards (water, lane closure, dust/respirator, HEPA, vacuum, heavy lifting)", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.safetyRequirements.OtherHazards).Response, PDFUtil.spanNormalBlack, 0, false));
            SafetyTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            SafetyTable.AddCell(PDFUtil.CreateCell(model.safetyRequirements.OtherHazardsComments, PDFUtil.spanNormalBlack, 0, false));

            document.Add(SafetyTable);

            var OtherImportantTable = PDFUtil.createTableWithHeader("Other Important Factors",new float[] { 3, 7 });
            OtherImportantTable.AddCell(PDFUtil.CreateCell("Please fill in any other pertinant information", PDFUtil.font_body_bold, 2, false));
            OtherImportantTable.AddCell(PDFUtil.CreateCell(model.otherImportantFactors.OtherPertinentInformation, PDFUtil.spanNormalBlack, 0, false));
            document.Add(OtherImportantTable);

           // document.Add(PDFUtil.GetLineSeparator());

            var footerTable = PDFUtil.createTableWithHeader("", new float[] { 1, 2, 1, 2 },true);
            footerTable.AddCell(PDFUtil.CreateCell("Estimator Name", PDFUtil.font_body_bold, 2, false));
            footerTable.AddCell(PDFUtil.CreateCell(model.projectInformation.Estimator, PDFUtil.spanNormalBlack, 0, false));
            footerTable.AddCell(PDFUtil.CreateCell("Date Completed", PDFUtil.font_body_bold, 2, false));
            footerTable.AddCell(PDFUtil.CreateCell(model.DateCompleted, PDFUtil.spanNormalBlack, 0, false));

            footerTable.AddCell(PDFUtil.CreateCell("Approved By", PDFUtil.font_body_bold, 2, false));
            footerTable.AddCell(PDFUtil.CreateCell(model.ListUsers.First(i => i.UserId.ToString() == model.ApprovedBy).Name, PDFUtil.spanNormalBlack, 0, false));
            footerTable.AddCell(PDFUtil.CreateCell("Date Reviewed", PDFUtil.font_body_bold, 2, false));
            footerTable.AddCell(PDFUtil.CreateCell(model.DateReviewed, PDFUtil.spanNormalBlack, 0, false));
            document.Add(footerTable);

            document.Close();
            byte[] file = ms.ToArray();
            MemoryStream output = new MemoryStream();
            output.Write(file, 0, file.Length);
            output.Position = 0;

            HttpContext.Response.AddHeader("content-disposition", "inline; filename=JobActivation_" + prjid + ".pdf");
            return File(output, "application/pdf");
        }


        [Route("Report/JobHardDetails/id/{hid}")]
        public FileStreamResult printHardCardDetails(string hid)
        {
            string title = string.Empty;

            title = "";
            var model = new HardCard();
            model.HardCardId = Convert.ToInt32(hid);
            HardCardDal.getHardCardDetails(model);
            HardCardDal.getHardCardLookUpList(model);

            // Set up the document and the MS to write it to and create the PDF writer instance
            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A4,15, 10,15,35);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            writer.PageEvent = new PDFReportEventHelper(title);

            // Open the PDF document
            document.Open();
            document.Add(PDFUtil.HeaderSection("Job Card"));

            #region Job Info
            var jobInfo = PDFUtil.createTableWithHeader("Job Information", new float[] { 2, 2, 2, 2 });

            jobInfo.AddCell(PDFUtil.CreateCell("Estimator", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.EstimatorsName, PDFUtil.spanNormalBlack));
            jobInfo.AddCell(PDFUtil.CreateCell("Job Number", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.Atlas_Job_Number, PDFUtil.spanNormalBlack));

            jobInfo.AddCell(PDFUtil.CreateCell("Est.Phone #", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(AppUtil.formatPhoneNumber(model.JobInformationDetails.ContractPhoneNumber), PDFUtil.spanNormalBlack));
            jobInfo.AddCell(PDFUtil.CreateCell("BI Number", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.BidItemHeaderId, PDFUtil.spanNormalBlack));

            jobInfo.AddCell(PDFUtil.CreateCell("Job Name", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.JobName, PDFUtil.spanNormalBlack, colSpan: 3));

            jobInfo.AddCell(PDFUtil.CreateCell("Job Address", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.JobAddress, PDFUtil.spanNormalBlack, colSpan: 3));

            jobInfo.AddCell(PDFUtil.CreateCell("Job City", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.JobCity, PDFUtil.spanNormalBlack));
            jobInfo.AddCell(PDFUtil.CreateCell("Job State", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.JobState, PDFUtil.spanNormalBlack));

            jobInfo.AddCell(PDFUtil.CreateCell("Job Contact", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.JobContact, PDFUtil.spanNormalBlack));
            jobInfo.AddCell(PDFUtil.CreateCell("Call In Route", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(model.JobInformationDetails.ListJobInfoResponse.
                FirstOrDefault(i => i.ResponseId.ToString() == model.JobInformationDetails.CallInRoute)?.Response, PDFUtil.spanNormalBlack));

            jobInfo.AddCell(PDFUtil.CreateCell("Contact Phone", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(AppUtil.formatPhoneNumber(model.JobInformationDetails.JobPhone), PDFUtil.spanNormalBlack));
            jobInfo.AddCell(PDFUtil.CreateCell("Contact Cell", PDFUtil.font_body_bold, 2, false));
            jobInfo.AddCell(PDFUtil.CreateCell(AppUtil.formatPhoneNumber(model.JobInformationDetails.JobCell), PDFUtil.spanNormalBlack));

            document.Add(jobInfo);

            #endregion

            #region ContractorInfo
            var contractInfo = PDFUtil.createTableWithHeader("Contractor Information", new float[] { 2, 2, 2, 2 });
            contractInfo.AddCell(PDFUtil.CreateCell("Contractor Name", PDFUtil.font_body_bold, 2, false));
            contractInfo.AddCell(PDFUtil.CreateCell(model.ContractInformationDetails.ContractorName, PDFUtil.spanNormalBlack, colSpan: 3));


            contractInfo.AddCell(PDFUtil.CreateCell("Primary Contact Name", PDFUtil.font_body_bold, 2, false));
            contractInfo.AddCell(PDFUtil.CreateCell(model.ContractInformationDetails.PrimaryContact, PDFUtil.spanNormalBlack));
            contractInfo.AddCell(PDFUtil.CreateCell("Phone", PDFUtil.font_body_bold, 2, false));
            contractInfo.AddCell(PDFUtil.CreateCell(AppUtil.formatPhoneNumber(model.ContractInformationDetails.PrimaryPhone), PDFUtil.spanNormalBlack));

            contractInfo.AddCell(PDFUtil.CreateCell("Fence Type", PDFUtil.font_body_bold, 2, false));
            contractInfo.AddCell(PDFUtil.CreateCell(model.ContractInformationDetails.ListFenceTypes.
                First(i => i.FenceTypeId.ToString() == model.ContractInformationDetails.FenceType).FenceTypeName, PDFUtil.spanNormalBlack, colSpan: 3));

            contractInfo.AddCell(PDFUtil.CreateCell("Special Notes", PDFUtil.font_body_bold, 2, false));
            contractInfo.AddCell(PDFUtil.CreateCell(model.ContractInformationDetails.SplNotes, PDFUtil.spanNormalBlack));
            contractInfo.AddCell(PDFUtil.CreateCell("Directions", PDFUtil.font_body_bold, 2, false));
            contractInfo.AddCell(PDFUtil.CreateCell(model.ContractInformationDetails.Directions, PDFUtil.spanNormalBlack));

            document.Add(contractInfo);

            #endregion

            #region Installation
            var installDetails = PDFUtil.createTableWithHeader("Installation", new float[] { 2, 2, 2, 2, 2, 2 });
            installDetails.AddCell(PDFUtil.CreateCell("Pre Make Gates", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListInstallationResponses.
                First(i => i.ResponseId.ToString() == model.InstallationDetails.PremakeGate.ToString()).Response, PDFUtil.spanNormalBlack, colSpan: 5));


            installDetails.AddCell(PDFUtil.CreateCell("CBYD", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListInstallationResponses.
                First(i => i.ResponseId.ToString() == model.InstallationDetails.CBYD.ToString()).Response, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("CBYD Date", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.CBYDDate, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("CBYD Number", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.CBYDNumber, PDFUtil.spanNormalBlack));

            installDetails.AddCell(PDFUtil.CreateCell("Start Date", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.StartDate, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Finish Date", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.FinishDate, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Hard Date", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.HardDate, PDFUtil.spanNormalBlack));

            installDetails.AddCell(PDFUtil.CreateCell("Gate Description", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.GateDescription1, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Gate Installation", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListGateInstallation.
                FirstOrDefault(i => i.GateInstallationID.ToString() == model.InstallationDetails.GateInstallationID1)?.Description, PDFUtil.spanNormalBlack, colSpan: 3));

            installDetails.AddCell(PDFUtil.CreateCell("Gate Description", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.GateDescription2, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Gate Installation", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListGateInstallation.
                FirstOrDefault(i => i.GateInstallationID.ToString() == model.InstallationDetails.GateInstallationID2)?.Description, PDFUtil.spanNormalBlack, colSpan: 3));

            installDetails.AddCell(PDFUtil.CreateCell("Gate Description", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.GateDescription3, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Gate Installation", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListGateInstallation.
                FirstOrDefault(i => i.GateInstallationID.ToString() == model.InstallationDetails.GateInstallationID3)?.Description, PDFUtil.spanNormalBlack, colSpan: 3));

            installDetails.AddCell(PDFUtil.CreateCell("Gate Description", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.GateDescription4, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Gate Installation", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListGateInstallation.
                FirstOrDefault(i => i.GateInstallationID.ToString() == model.InstallationDetails.GateInstallationID4)?.Description, PDFUtil.spanNormalBlack, colSpan: 3));

            installDetails.AddCell(PDFUtil.CreateCell("Gate Description", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.GateDescription5, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Gate Installation", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListGateInstallation.
                FirstOrDefault(i => i.GateInstallationID.ToString() == model.InstallationDetails.GateInstallationID5)?.Description, PDFUtil.spanNormalBlack, colSpan: 3));


            installDetails.AddCell(PDFUtil.CreateCell("Equipment Required", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListInstallationResponses.
                First(i => i.ResponseId.ToString() == model.InstallationDetails.EquipmentRequired).Response, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Dig Type", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListDigType.
                First(i => i.DigTypeId == model.InstallationDetails.DigTypeID).DigType, PDFUtil.spanNormalBlack, colSpan: 3));

            installDetails.AddCell(PDFUtil.CreateCell("Water Available", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListInstallationResponses.
                First(i => i.ResponseId.ToString() == model.InstallationDetails.WaterAvailible.ToString()).Response, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Electricity Available", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListInstallationResponses.
                First(i => i.ResponseId == model.InstallationDetails.ElectricityAvailible).Response, PDFUtil.spanNormalBlack, colSpan: 3));

            installDetails.AddCell(PDFUtil.CreateCell("Measure By Installer", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListInstallationResponses.
                First(i => i.ResponseId.ToString() == model.InstallationDetails.MeasureByInstaller.ToString()).Response, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Leave Samples By Installer", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.ListInstallationResponses.
                First(i => i.ResponseId == model.InstallationDetails.LeaveSamplesByInstaller).Response, PDFUtil.spanNormalBlack, colSpan: 3));

            installDetails.AddCell(PDFUtil.CreateCell("Budgeted Install Days", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.BudgetedInstallDays, PDFUtil.spanNormalBlack));
            installDetails.AddCell(PDFUtil.CreateCell("Scope", PDFUtil.font_body_bold, 2, false));
            installDetails.AddCell(PDFUtil.CreateCell(model.InstallationDetails.Scope, PDFUtil.spanNormalBlack, colSpan: 3));
            document.Add(installDetails);

            #endregion

            #region BuildChecklist
            var buildList = PDFUtil.createTableWithHeader("Build Checklist", new float[] { 2, 2, 2, 2, 3, 2 });
            buildList.AddCell(PDFUtil.CreateCell("Pool Code", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId.ToString() == model.BuildChecklistDetails.PoolCode.ToString()).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Dowelled", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.Dowelled).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Safety Officer Onsite", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.SafetyOfficerOnsite).Response, PDFUtil.spanNormalBlack));

            buildList.AddCell(PDFUtil.CreateCell("Build For Rack", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId.ToString() == model.BuildChecklistDetails.BuildForRack.ToString()).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Morticed", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.Morticed).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Saftey Meeting/Orientation Onsite", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.SafteyMeetingOrintationOnsiteReq).Response, PDFUtil.spanNormalBlack));

            buildList.AddCell(PDFUtil.CreateCell("Stepping Temp", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId.ToString() == model.BuildChecklistDetails.SteppingTemp.ToString()).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Nail On", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.NailOn).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Saftey Inspection Before Job Starts", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.SafteyInspectionReqBeforeJobStarts).Response, PDFUtil.spanNormalBlack));

            buildList.AddCell(PDFUtil.CreateCell("Fence Direction", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListFenceDirection.
                First(i => i.FenceDirectionID.ToString() == model.BuildChecklistDetails.FenceDirectionID.ToString()).Description, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Single Nailed", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.SingleNailed).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("PPE Required", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.PPERequired).Response, PDFUtil.spanNormalBlack));

            buildList.AddCell(PDFUtil.CreateCell("Fence Install", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListFenceInstall.
                First(i => i.FenceInstallID.ToString() == model.BuildChecklistDetails.FenceInstallID.ToString()).Description, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Build Full Sections", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.BuildFullSections).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Equipment Operator Certs", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.EquipmentOperatorCertsReq).Response, PDFUtil.spanNormalBlack));

            buildList.AddCell(PDFUtil.CreateCell("Trim In Field", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.TrimInField).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Stain", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.Stain).Response, PDFUtil.spanNormalBlack, colSpan: 3));

            buildList.AddCell(PDFUtil.CreateCell("Post Pins", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListBuildResponses.
                First(i => i.ResponseId == model.BuildChecklistDetails.PostPins).Response, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Stain Color", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.StainColor, PDFUtil.spanNormalBlack, colSpan: 3));

            buildList.AddCell(PDFUtil.CreateCell("Tear Out Type", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.ListTearOutType.
                First(i => i.TearOutTypeID == model.BuildChecklistDetails.TearOutTypeID).Description, PDFUtil.spanNormalBlack));
            buildList.AddCell(PDFUtil.CreateCell("Stain Brand", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.StainBrand, PDFUtil.spanNormalBlack, colSpan: 3));

            buildList.AddCell(PDFUtil.CreateCell("Other Hazards", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.OtherHazards, PDFUtil.spanNormalBlack, colSpan: 2));
            buildList.AddCell(PDFUtil.CreateCell("Notes", PDFUtil.font_body_bold, 2, false));
            buildList.AddCell(PDFUtil.CreateCell(model.BuildChecklistDetails.Notes, PDFUtil.spanNormalBlack, colSpan: 3));

            document.Add(buildList);
            #endregion

            document.Close();
            byte[] file = ms.ToArray();
            MemoryStream output = new MemoryStream();
            output.Write(file, 0, file.Length);
            output.Position = 0;

            HttpContext.Response.AddHeader("content-disposition", "inline; filename=HardCard" + hid + ".pdf");
            return File(output, "application/pdf");

        }

    }
}