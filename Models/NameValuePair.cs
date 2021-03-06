﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevopsGenie.Reponook.Models
{
    [BsonIgnoreExtraElements]
    public class NameValuePair
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("value")]
        public string Value { get; set; }
    }
}
