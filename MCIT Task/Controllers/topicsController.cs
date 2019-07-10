using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCIT_Task.Entites;
using System.IO;
using System.Diagnostics;
using MCIT_Task.ViewModel;

namespace MCIT_Task.Controllers
{
    public class topicsController : Controller
    {
        private McitContext db = new McitContext();


        public ActionResult ListAlltopics()
        {
            var result = db.Topics.Include(x => x.TopicDetails).ToList()
                                                .GroupBy(x => x.TopicDate)
                                                .OrderBy(g => g.Key);
            var m = new ListVM
            {
                TopicsSortedByDAte = result
            };
            return PartialView("_ListAlltopics", m);
        }

        //for Create And Update
        [HttpPost]
        public async Task<ActionResult> Save(topic topic, HttpPostedFileBase imgageFile)
        {

            string ImagePath = null;
            try
            {
                if (imgageFile != null)
                {
                    ImagePath = topic.TopicTitle + "_" + topic.TopicId + ".png";
                    string path = Path.Combine(Server.MapPath("~/TopicsImages"), Path.GetFileName(ImagePath));
                    imgageFile.SaveAs(path);
                    Debug.Write("Image Saved Successfully");
                }
                else
                {
                    var SelectedTopic = db.TopicsDetails.SingleOrDefault(x => x.TopicDetailsId == topic.TopicId);
                    ImagePath = SelectedTopic.Image;
                }
            }
            catch (Exception)
            {
                Debug.Write("Error while Image uploading.");
            }
            if (topic.TopicId == 0)
            {       //Create
                topic.TopicDetails.Image = ImagePath;
                db.Topics.Add(topic);
                await db.SaveChangesAsync();
                return RedirectToAction("ListAlltopics");
            }
            else
            {
                //Edit
                var Target_topic = await db.Topics.Include(d => d.TopicDetails).FirstOrDefaultAsync(I => I.TopicId == topic.TopicId);
                if (topic != null)
                {
                    Target_topic.TopicDate = topic.TopicDate;
                    Target_topic.TopicDetails = topic.TopicDetails;
                    Target_topic.TopicIsActive = topic.TopicIsActive;
                    Target_topic.TopicTitle = topic.TopicTitle;
                    Target_topic.TopicDetails.Image = ImagePath;
                    await db.SaveChangesAsync();
                    return RedirectToAction("ListAlltopics");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Save = "Create New";
            var m = db.Topics.Include(x => x.TopicDetails);
            return PartialView("_TopicFormModel");
        }
        [HttpGet]
        public ActionResult EditTopic(int id)
        {
            ViewBag.Save = "Update ";
            var m = db.Topics.Include(d => d.TopicDetails).SingleOrDefault(x => x.TopicId == id);
            if (m != null)
            {
                return PartialView("_TopicFormModel", m);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTopic(int id)
        {
            var post = db.Topics.Include(s => s.TopicDetails).SingleOrDefault(s => s.TopicId == id);
            if (post != null)
            {
                db.Topics.Remove(post);
                await db.SaveChangesAsync();
                return RedirectToAction("ListAlltopics");
            }
            return HttpNotFound();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
