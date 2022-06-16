from pymongo import MongoClient
from getComments2 import *

# Database connection

def addToDatabase(videoId, author, comment):

    url = "MONGO API KEY"    
    client = MongoClient(url)
    db = client.youtube ##Instead of "youtube" put your database name
    dataBase = db["COLLECTION NAME"]
    
        
    data = {
            "video_id" : videoId,
            "author" : author,
            "comment" : comment,
            "sentiment" : 2
        }
    if db.youtubeComments.count_documents({"comment": comment}) > 1:
        return True
    else:
        result = db.youtubeComments.insert_one(data)

##        print(video)
##        print(comment)
##    print(videoId)
##    for i in authorList:
##        print(i)
