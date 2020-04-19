using AtlasForms.DataAccess.Entity;
using AtlasForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasForms.Controllers
{
    public class HardCardController : Controller
    {
        // GET: HardCard
        [Route("Project/JobDetails")]
        [Route("Project/JobDetails/{PRJID}")]
        [Route("Project/JobDetails/{PRJID}/{HardCardID}")]
        public ActionResult Index(string PRJID ="", string HardCardID="")
        {
            int HardCardId = 0;
            int projectHeaderID = 0;
            if (!string.IsNullOrWhiteSpace(PRJID) && Int32.TryParse(PRJID, out projectHeaderID))
            {
                Session["PRJID"] = PRJID;
            }

            if (Session["PRJID"] != null)
            {
                var model = new HardCard();
                model.ProjectHeaderId = projectHeaderID;
                LookUpHardCardCombos(model);

                if (!string.IsNullOrWhiteSpace(HardCardID) && Int32.TryParse(HardCardID, out HardCardId))
                {
                    Session["HardCardId"] = HardCardId;
                    model.HardCardId = HardCardId;
                    HardCardDal.getHardCardDetails(model);
                }

                return View("index", model);

            }
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult SaveJobDetails(HardCard model)
        {
            string errors = String.Empty;
            try
            {
                if (model.HardCardId == 0)
                {
                    model.HardCardId = (Convert.ToInt32(Session["HardCardId"]));
                } 
                if (ModelState.IsValid)
                {
                    var result = Convert.ToInt32(HardCardDal.saveJobDetails(model));
                    if (result > 0)
                    {
                        LookUpHardCardCombos(model);

                        return View("index", model);
                    }
                }
                else
                {
                    errors = GetModelError(errors);
                }
                LookUpHardCardCombos(model);
                return View("index", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message + BusinessConstants.contactAdmin);
                return RedirectToAction("index", "home");
            }
        }


        private void LookUpHardCardCombos(HardCard model)
        {
            if(model.JobInformationDetails== null)
            {
                model.ProjectHeaderId = (Convert.ToInt32(Session["PRJID"]));
            }
            else
            {
                model.ProjectHeaderId = Convert.ToInt32(model.JobInformationDetails.Atlas_Job_Number);
            }
            HardCardDal.getHardCardLookUpList(model);


            IEnumerable<SelectListItem> ListJobInfoResponse = model.JobInformationDetails.ListJobInfoResponse.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.ResponseId),
                Text = c.Response

            });
            ViewBag.ListJobInfoResponse = ListJobInfoResponse;

            IEnumerable<SelectListItem> ListFenceTypes = model.ContractInformationDetails.ListFenceTypes.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.FenceTypeId),
                Text = c.FenceTypeName

            });
            ViewBag.ListFenceTypes = ListFenceTypes;

            IEnumerable<SelectListItem> ListInstallationResponses = model.InstallationDetails.ListInstallationResponses.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.ResponseId),
                Text = c.Response

            });
            ViewBag.ListInstallationResponses = ListInstallationResponses;

            IEnumerable<SelectListItem> ListGateInstallation = model.InstallationDetails.ListGateInstallation.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.GateInstallationID),
                Text = c.Description

            });
            ViewBag.ListGateInstallation = ListGateInstallation;

            IEnumerable<SelectListItem> ListDigType = model.InstallationDetails.ListDigType.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.DigTypeId),
                Text = c.DigType

            });
            ViewBag.ListDigType = ListDigType;

            IEnumerable<SelectListItem> ListBuildResponses = model.BuildChecklistDetails.ListBuildResponses.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.ResponseId),
                Text = c.Response

            });
            ViewBag.ListBuildResponses = ListBuildResponses;

            IEnumerable<SelectListItem> ListFenceDirection = model.BuildChecklistDetails.ListFenceDirection.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.FenceDirectionID),
                Text = c.Description

            });
            ViewBag.ListFenceDirection = ListFenceDirection;

            IEnumerable<SelectListItem> ListFenceInstall = model.BuildChecklistDetails.ListFenceInstall.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.FenceInstallID),
                Text = c.Description

            });
            ViewBag.ListFenceInstall = ListFenceInstall;


            IEnumerable<SelectListItem> ListTearOutType = model.BuildChecklistDetails.ListTearOutType.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.TearOutTypeID),
                Text = c.Description

            });
            ViewBag.ListTearOutType = ListTearOutType;
        }

        private string GetModelError(string errors)
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage;
                }
            }

            return errors;
        }

    }
}