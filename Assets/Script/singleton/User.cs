using Assets.Script.singleton;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script
{
    public class User
    {
        private static User _instance;
        public static User Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new User();
                }
                return _instance;
            }
        }

        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("username"), BsonRepresentation(BsonType.String)]
        public string Username { get; set; }
        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string Password { get; set; } 
    }
}
