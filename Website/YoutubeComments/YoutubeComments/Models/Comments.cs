﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoutubeComments.Models
{
    public class Comments
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public string video_id { get; set; }

        [BsonElement]
        public string comment { get; set; }

        [BsonElement]
        public string author { get; set; }

        [BsonElement]
        public int polarity { get; set; }

        [BsonElement]
        public int sentiment { get; set; }

        public string sentimentString { get; set; }
        
        public static double positive_number { get; set; }
        public static double neutral_number { get; set; }
        public static double negative_number { get; set; }
        public static double total_number { get; set; }

        public static double pos_perc { get; set; }
        public static int neu_perc { get; set; }
        public static int neg_perc { get; set; }
        
    }
}