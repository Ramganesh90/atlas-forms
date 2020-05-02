using AtlasForms.DataAccess.Entity;
using AtlasForms.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasForms.Controllers
{
    [HandleError(View = "error")]
    public class HardCardController : Controller
    {
        // GET: HardCard
        [Route("Project/JobDetails")]
        [Route("Project/JobDetails/{PRJID}")]
        public ActionResult Index(string PRJID)
        {
            var model = new ProjectBidViewModel();

            int prjid = 0;
            if (!string.IsNullOrWhiteSpace(PRJID) && Int32.TryParse(PRJID, out prjid))
            {
                Session["PRJID"] = prjid;
                model.ProjectHeaderId = Convert.ToString(prjid);
                model.BidItemsList = HardCardDal.getBidItems(PRJID);
                if (model.BidItemsList.Count > 0)
                {
                    IEnumerable<SelectListItem> ListBidItems = model.BidItemsList.Select(c => new SelectListItem
                    {
                        Value = Convert.ToString(c.BidItemHeaderId),
                        Text = String.Format("{0} - {1}", c.BidItemHeaderId, c.BidItemName)

                    });
                    ViewBag.ListBidItems = ListBidItems;
                }
                else
                {
                    ViewBag.ListBidItems = Enumerable.Empty<SelectListItem>();
                }
            }
            else
            {
                ViewBag.ListBidItems = Enumerable.Empty<SelectListItem>();
            }
               
            return View("index", model);
        }

        [Route("Project/JobDetails/SearchJob")]
        [Route("Project/JobDetails/SearchBids")]
        public JsonResult SearchJob(string prjid, string bidid)
        {
            var model = new ProjectBidViewModel();
            if (!string.IsNullOrWhiteSpace(prjid))
            {
                model.BidItemsList = HardCardDal.getBidItems(prjid);
                model.ProjectHeaderId = prjid;
                IEnumerable<SelectListItem> ListBidItems = model.BidItemsList.Select(c => new SelectListItem
                {
                    Value = Convert.ToString(c.BidItemHeaderId),
                    Text = String.Format("{0} - {1}", c.BidItemHeaderId, c.BidItemName)

                });
            }

            if (!string.IsNullOrWhiteSpace(bidid))
            {

                if (!string.IsNullOrWhiteSpace(prjid) && !string.IsNullOrWhiteSpace(bidid))
                {
                    var results = HardCardDal.getHardCardItem(prjid, bidid);
                    if (results.Rows.Count > 0)
                    {
                        foreach (DataRow row in results.Rows)
                        {
                            model.BidItemId = Convert.ToString(row["BidItemHeaderId"]);
                            model.HardCardId = Convert.ToString(row["HardCardID"]);
                            model.ProjectHeaderId = Convert.ToString(row["ProjectHeaderId"]);
                        }
                    }
                    else
                    {
                        model.BidItemId = bidid;
                        model.ProjectHeaderId = prjid;
                    }
                }
            }
            return  Json(model);
        }

        [Route("Project/JobDetails/create/{PRJID}/bid/{bidid}")]
        public ActionResult Create(string PRJID ="", string BIDID="")
        {
            int BIDId = 0;
            int projectHeaderID = 0;
            var model = new HardCard();
            if (!string.IsNullOrWhiteSpace(PRJID) && Int32.TryParse(PRJID, out projectHeaderID))
            {
                Session["PRJID"] = PRJID;
                model.ProjectHeaderId = projectHeaderID;
            }

            if (!string.IsNullOrWhiteSpace(BIDID) && Int32.TryParse(BIDID, out BIDId))
            {
                Session["BIDID"] = BIDId;
                model.BidItemHeaderid = BIDId;
            }
            if(Session["BIDID"] != null && Session["BIDID"] != null)
            {
                LookUpHardCardCombos(model);
                if (model.ProjectHeaderId == 0 || model.BidItemHeaderid == 0)
                {
                    return View("create", model);
                }

                var hardCardItem = HardCardDal.getHardCardItem(PRJID, BIDID);
                if(hardCardItem.Rows.Count ==1)
                {
                    model.HardCardId = Convert.ToInt32(hardCardItem.Rows[0]["HardCardID"]);
                    HardCardDal.getHardCardDetails(model);
                }

                return View("create", model);
            }
            return RedirectToAction("error");
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
                        model.HardCardId = result;
                        LookUpHardCardCombos(model);

                        return View("create", model);
                    }
                }
                else
                {
                    errors = GetModelError(errors);
                }
                LookUpHardCardCombos(model);
                return View("create", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message + BusinessConstants.contactAdmin);
                return RedirectToAction("error", ex); ;
            }
        }


        private void LookUpHardCardCombos(HardCard model)
        {
            if(model.JobInformationDetails== null)
            {
                model.ProjectHeaderId = (Convert.ToInt32(Session["PRJID"]));
                model.BidItemHeaderid = (Convert.ToInt32(Session["BIDID"]));
            }
            HardCardDal.getHardCardLookUpList(model);
             

            IEnumerable<SelectListItem> ListJobInfoResponse = model.JobInformationDetails?.ListJobInfoResponse?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.ResponseId),
                Text = c.Response

            });
            ViewBag.ListJobInfoResponse = ListJobInfoResponse;

            IEnumerable<SelectListItem> ListFenceTypes = model.ContractInformationDetails?.ListFenceTypes?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.FenceTypeId),
                Text = c.FenceTypeName

            });
            ViewBag.ListFenceTypes = ListFenceTypes;

            IEnumerable<SelectListItem> ListInstallationResponses = model.InstallationDetails?.ListInstallationResponses?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.ResponseId),
                Text = c.Response

            });
            ViewBag.ListInstallationResponses = ListInstallationResponses;

            IEnumerable<SelectListItem> ListGateInstallation = model.InstallationDetails?.ListGateInstallation?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.GateInstallationID),
                Text = c.Description

            });
            ViewBag.ListGateInstallation = ListGateInstallation;

            IEnumerable<SelectListItem> ListDigType = model.InstallationDetails?.ListDigType?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.DigTypeId),
                Text = c.DigType

            });
            ViewBag.ListDigType = ListDigType;

            IEnumerable<SelectListItem> ListBuildResponses = model.BuildChecklistDetails.ListBuildResponses?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.ResponseId),
                Text = c.Response

            });
            ViewBag.ListBuildResponses = ListBuildResponses;

            IEnumerable<SelectListItem> ListFenceDirection = model.BuildChecklistDetails.ListFenceDirection?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.FenceDirectionID),
                Text = c.Description

            });
            ViewBag.ListFenceDirection = ListFenceDirection;

            IEnumerable<SelectListItem> ListFenceInstall = model.BuildChecklistDetails.ListFenceInstall?.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.FenceInstallID),
                Text = c.Description

            });
            ViewBag.ListFenceInstall = ListFenceInstall;


            IEnumerable<SelectListItem> ListTearOutType = model.BuildChecklistDetails.ListTearOutType?.Select(c => new SelectListItem
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