using YoutubeComments.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace YoutubeComments.Controllers
{
    public class HomeController : Controller
    {

        private IMongoCollection<Comments> collection;
        double positive_number = 0;
        double neutral_number = 0;
        double negative_number = 0;
        double total_number = 0;

        int pos_perc = 0;
        int neu_perc = 0;
        int neg_perc = 0;



        List<DateTime> createdAt = new List<DateTime>();
        List<int> polarity = new List<int>();
        public HomeController()
        {
            var client = new MongoClient("MONGO API KEY");
            IMongoDatabase db = client.GetDatabase("DATABASE NAME");
            this.collection = db.GetCollection<Comments>("COLLECTION NAME");

            var model = collection.Find(FilterDefinition<Comments>.Empty).ToList();

            foreach (var m in model)
            {
                if (m.sentiment == 1)
                {
                    positive_number++;
                    total_number++;
                }
                else if (m.sentiment == 0)
                {
                    neutral_number++;
                    total_number++;
                }
                else if (m.sentiment == -1)
                {
                    negative_number++;
                    total_number++;
                }
                polarity.Add(m.sentiment);
            }

            pos_perc = Convert.ToInt32(positive_number / total_number * 100);
            neg_perc = Convert.ToInt32(negative_number / total_number * 100);
            neu_perc = Convert.ToInt32(neutral_number / total_number * 100);
        }

        public ActionResult Index()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            
            dataPoints.Add(new DataPoint("Positive", pos_perc));
            dataPoints.Add(new DataPoint("Neutral", neu_perc));
            dataPoints.Add(new DataPoint("Negative", neg_perc));
            

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            
            
            List<DataPoint> dataPointPos = new List<DataPoint>();
            List<DataPoint> dataPointNeu = new List<DataPoint>();
            List<DataPoint> dataPointNeg = new List<DataPoint>();
            
            

            ViewBag.DataPointsPos = JsonConvert.SerializeObject(dataPointPos);
            ViewBag.DataPointsNeu = JsonConvert.SerializeObject(dataPointNeu);
            ViewBag.DataPointsNeg = JsonConvert.SerializeObject(dataPointNeg);
            



            List<DataPoint2> dataPoints2 = new List<DataPoint2>();
            dataPoints2.Add(new DataPoint2("Positive", positive_number));
            dataPoints2.Add(new DataPoint2("Negative", negative_number));
            dataPoints2.Add(new DataPoint2("Neutral", neutral_number));

            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);

            List<DataPoint2> dataPointPos2 = new List<DataPoint2>();
            List<DataPoint2> dataPointNeu2 = new List<DataPoint2>();
            List<DataPoint2> dataPointNeg2 = new List<DataPoint2>();
            


            ViewBag.DataPointsPos2 = JsonConvert.SerializeObject(dataPointPos2);
            ViewBag.DataPointsNeu2 = JsonConvert.SerializeObject(dataPointNeu2);
            ViewBag.DataPointsNeg2 = JsonConvert.SerializeObject(dataPointNeg2);
            

            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
    }
}