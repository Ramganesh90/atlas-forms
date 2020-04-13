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

            title = "Atlas Residential & Commerical Services LLC - Job Activation CheckList";

            var model = new JobActivationChecklist();
            model.PRJID = Convert.ToInt32(prjid);
            model = ChecklistDal.getProjectActivationDetails(model);
            model = ChecklistDal.JobActivationLookup(model);

            // Set up the document and the MS to write it to and create the PDF writer instance
            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A4.Rotate(), 15, 15, 46, 42);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            writer.PageEvent = new PDFReportEventHelper(title);

            // Open the PDF document
            document.Open();

            document.Add(PDFUtil.HeaderSection("Project Information"));

            var projectTable = new PdfPTable(2);
            projectTable.HorizontalAlignment = 0;
            projectTable.WidthPercentage = 100;
            projectTable.SpacingBefore = 5;
            projectTable.SpacingAfter = 5;
            projectTable.DefaultCell.Border = 0;
            projectTable.DefaultCell.Padding = 30f;
            projectTable.SetWidths(new float[] { 3, 6 });

            projectTable.AddCell(PDFUtil.CreateCell("Atlas Job Number", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.JobNumber, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Atlas Company Name", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.CompanyName, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Customer/Contractor Name", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.CustomerProfile.Name, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Address, City, State, Zip", PDFUtil.font_body_bold, 2, false));
            string CustomerAddr = string.Format("{0} | {1} | {2} | {3}",
                                                        model.projectInformation.CustomerProfile.Address,
                                                        model.projectInformation.CustomerProfile.City,
                                                        model.projectInformation.CustomerProfile.State,
                                                        model.projectInformation.CustomerProfile.Zip);
            projectTable.AddCell(PDFUtil.CreateCell(CustomerAddr, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Phone Number, Extension", PDFUtil.font_body_bold, 2, false));
            string CustomerPhone = string.Format("{0} | {1}",
                                                        model.projectInformation.CustomerProfile.PhoneNumber,
                                                        model.projectInformation.CustomerProfile.Extension);
            projectTable.AddCell(PDFUtil.CreateCell(CustomerPhone, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Project Name", PDFUtil.font_body_bold, 2, false));
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.ProjectProfile.Name, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Address, City, State, Zip", PDFUtil.font_body_bold, 2, false));
            string ProjectAddr = string.Format("{0} | {1} | {2} | {3}",
                                                       model.projectInformation.ProjectProfile.Address,
                                                       model.projectInformation.ProjectProfile.City,
                                                       model.projectInformation.ProjectProfile.State,
                                                       model.projectInformation.ProjectProfile.Zip);
            projectTable.AddCell(PDFUtil.CreateCell(ProjectAddr, PDFUtil.spanNormalBlack, 0, false));
            projectTable.AddCell(PDFUtil.CreateCell("Contact Name/Phone Number, Ext", PDFUtil.font_body_bold, 2, false));
            string ProjectPhone = string.Format("{0} | {1} | {2}",
                                                        model.projectInformation.CustomerProfile.ContactName,
                                                        model.projectInformation.CustomerProfile.PhoneNumber,
                                                        model.projectInformation.CustomerProfile.Extension);
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
            projectTable.AddCell(PDFUtil.CreateCell(model.projectInformation.TypeOfLabour, PDFUtil.spanNormalBlack, 0, false));

            document.Add(projectTable);

            document.Add(PDFUtil.HeaderSection("Contract/Job Paperwork"));

            var ContractTable = new PdfPTable(4);
            ContractTable.HorizontalAlignment = 0;
            ContractTable.WidthPercentage = 98;
            ContractTable.SpacingBefore = 5;
            ContractTable.SpacingAfter = 5;
            ContractTable.DefaultCell.Border = 0;
            ContractTable.DefaultCell.Padding = 30f;
            ContractTable.SetWidths(new float[] { 3, 1, 1, 5 });

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

            document.Add(PDFUtil.HeaderSection("Bonding/Insurance/Labor "));
            var BondingTable = new PdfPTable(4);
            BondingTable.HorizontalAlignment = 0;
            BondingTable.WidthPercentage = 98;
            BondingTable.SpacingBefore = 5;
            BondingTable.SpacingAfter = 5;
            BondingTable.DefaultCell.Border = 0;
            BondingTable.DefaultCell.Padding = 30f;
            BondingTable.SetWidths(new float[] { 3, 1, 1, 5 });

            BondingTable.AddCell(PDFUtil.CreateCell("Received bond (if required) and necessary insurance certification", PDFUtil.font_body_bold, 2, false));
            BondingTable.AddCell(PDFUtil.CreateCell(model.ListResponses.
                First(i => i.ResponseId.ToString() == model.bondingInsurance.InsuranceCertification).Response, PDFUtil.spanNormalBlack, 0, false));
            BondingTable.AddCell(PDFUtil.CreateCell("Comments", PDFUtil.font_body_bold, 2, false));
            BondingTable.AddCell(PDFUtil.CreateCell(model.bondingInsurance.InsuranceCertificationComments, PDFUtil.spanNormalBlack, 0, false));

            document.Add(BondingTable);
            document.Add(PDFUtil.HeaderSection("Safety Requirements - For all 'Yes' answers, please provide additional details"));

            var SafetyTable = new PdfPTable(4);
            SafetyTable.HorizontalAlignment = 0;
            SafetyTable.WidthPercentage = 98;
            SafetyTable.SpacingBefore = 5;
            SafetyTable.SpacingAfter = 5;
            SafetyTable.DefaultCell.Border = 0;
            SafetyTable.DefaultCell.Padding = 30f;
            SafetyTable.SetWidths(new float[] { 3, 1, 1, 5 });

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
            document.Add(PDFUtil.HeaderSection("Other Important Factors"));

            var OtherImportantTable = new PdfPTable(2);
            OtherImportantTable.HorizontalAlignment = 0;
            OtherImportantTable.WidthPercentage = 98;
            OtherImportantTable.SpacingBefore = 5;
            OtherImportantTable.SpacingAfter = 5;
            OtherImportantTable.DefaultCell.Border = 0;
            OtherImportantTable.DefaultCell.Padding = 30f;
            OtherImportantTable.SetWidths(new float[] { 3, 7 });

            OtherImportantTable.AddCell(PDFUtil.CreateCell("Please fill in any other pertinant information", PDFUtil.font_body_bold, 2, false));
            OtherImportantTable.AddCell(PDFUtil.CreateCell(model.otherImportantFactors.OtherPertinentInformation, PDFUtil.spanNormalBlack, 0, false));
            document.Add(OtherImportantTable);

            document.Add(PDFUtil.GetLineSeparator());
            var footerTable = new PdfPTable(4);
            footerTable.HorizontalAlignment = 0;
            footerTable.WidthPercentage = 98;
            footerTable.SpacingBefore = 5;
            footerTable.SpacingAfter = 5;
            footerTable.DefaultCell.Border = 0;
            footerTable.DefaultCell.Padding = 30f;
            footerTable.SetWidths(new float[] { 1, 2, 1, 2 });

            footerTable.AddCell(PDFUtil.CreateCell("Estimator Name", PDFUtil.font_body_bold, 2, false));
            footerTable.AddCell(PDFUtil.CreateCell(model.projectInformation.Estimator, PDFUtil.spanNormalBlack, 0, false));
            footerTable.AddCell(PDFUtil.CreateCell("Date Completed", PDFUtil.font_body_bold, 2, false));
            footerTable.AddCell(PDFUtil.CreateCell(model.DateCompleted, PDFUtil.spanNormalBlack, 0, false));

            footerTable.AddCell(PDFUtil.CreateCell("Approved By", PDFUtil.font_body_bold, 2, false));
            footerTable.AddCell(PDFUtil.CreateCell(model.ApprovedBy, PDFUtil.spanNormalBlack, 0, false));
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


    }
}