using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoutubeComments.Models;
using MongoDB.Driver;
using PagedList;

namespace AnalyzingTweets.Controllers
{
    public class CommentsController : Controller
    {
        double positive_number = 0;
        double neutral_number = 0;
        double negative_number = 0;
        double total_number = 0;

        private IMongoCollection<Comments> collection;
        public CommentsController()
        {
            var client = new MongoClient("MONGO API KEY");
            IMongoDatabase db = client.GetDatabase("DATABASE NAME");
            this.collection = db.GetCollection<Comments>("COLLECTION NAME");
        }

        // GET: Comments
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var model = collection.Find(FilterDefinition<Comments>.Empty).ToList();

            foreach (var m in model)
            {
                if (m.sentiment == 1)
                {
                    m.sentimentString = "Positive";
                    positive_number++;
                    total_number++;
                }
                else if (m.sentiment == 0)
                {
                    m.sentimentString = "Neutral";
                    neutral_number++;
                    total_number++;
                }
                else if (m.sentiment == -1)
                {
                    m.sentimentString = "Negative";
                    negative_number++;
                    total_number++;
                }
            }

            Comments.positive_number = positive_number;
            Comments.neutral_number = neutral_number;
            Comments.negative_number = negative_number;
            Comments.total_number = total_number;

            Comments.pos_perc = (positive_number / total_number) * 100;
            Comments.neu_perc = Convert.ToInt32((neutral_number / total_number) * 100);
            Comments.neg_perc = Convert.ToInt32((negative_number / total_number) * 100);

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        
    }
}