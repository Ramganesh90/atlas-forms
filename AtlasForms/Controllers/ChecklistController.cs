using AtlasForms.DataAccess.Entity;
using AtlasForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasForms.Controllers
{
    public class ChecklistController : Controller
    {
        [Route("Project/JobActivation/Checklist/{PRJID}")]
        public ActionResult JobChecklist(string PRJID)
        {
            int ProjectHeaderID = 0;
            if (!string.IsNullOrWhiteSpace(PRJID) && Int32.TryParse(PRJID, out ProjectHeaderID))
            {
                Session["PRJID"] = ProjectHeaderID;
            }
            if (Session["PRJID"]!= null)
            {
                ViewBag.Title = string.Format("{0}", Session["PRJID"]);
                var model = new JobActivationChecklist();
                if (ProjectHeaderID > 0)
                {
                    model.PRJID = ProjectHeaderID;
                    model = ChecklistDal.getProjectActivationDetails(model);
                    LoadActivationLookup(model);
                }

                return View("index",model);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        [HttpPost]
        public ActionResult SaveActivation(JobActivationChecklist model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = ChecklistDal.saveJobActivation(model);
                    if (result > 0)
                    {
                        Session["PRJID"] = model.PRJID;

                        LoadActivationLookup(model);

                        return View("index", model);
                    }
                }
                LoadActivationLookup(model);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message + BusinessConstants.contactAdmin);
                LoadActivationLookup(model);
                return View(model);
            }
        }

        public void LoadActivationLookup(JobActivationChecklist model)
        {
            model = ChecklistDal.JobActivationLookup(model);


            if(model.ListLabourTypes == null)
            {
                model.ListLabourTypes = new List<SYS10_MhRates>();
            }

            IEnumerable<SelectListItem> ListLabourTypes = model.ListLabourTypes.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.MhRateId),
                Text = c.MhRateName

            });
            ViewBag.ListLabourTypes = ListLabourTypes;

            if (model.ListCustomersTypes == null)
            {
                model.ListCustomersTypes = new List<DOC08_CustomerType>();
            }
            IEnumerable<SelectListItem> ListCustomersType = model.ListCustomersTypes.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.CustomerTypeID),
                Text = c.Description

            });
            ViewBag.ListCustomersType = ListCustomersType;
            if (model.ListJobTypes == null)
            {
                model.ListJobTypes = new List<DOC09_JobTypes>();
            }
            IEnumerable<SelectListItem> ListJobType = model.ListJobTypes.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.JobTypeId),
                Text = c.JobTypeDesc

            });
            ViewBag.ListJobType = ListJobType;
            if (model.ListResponses == null)
            {
                model.ListResponses = new List<DOC10_Responses>();
            }

            IEnumerable<SelectListItem> ListResponses = model.ListResponses.Select(c => new SelectListItem
            {
                Value = Convert.ToString(c.ResponseId),
                Text = c.Response

            });
            ViewBag.ListResponses = ListResponses;
        }
    }
}