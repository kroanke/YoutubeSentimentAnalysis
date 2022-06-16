from nltk.sentiment.vader import SentimentIntensityAnalyzer

from pymongo import MongoClient

url = "MONGO API KEY"
client = MongoClient(url)
db = client.youtube ##Instead of "youtube" put your database name
dataBase = db["COLLECTION NAME"]
sentiments = []
#Retrieve comments from database
def getComments():
    collectionComment = dataBase.find({}, {'_id': 0, 'comment': 1, 'sentiment': 1})
    comments = []
    
    for comment in collectionComment:
        comments.append(comment["comment"])
        sentiments.append(comment["sentiment"])
    return comments

def analyze(comments):
    polarity = 0

    neutral_list = []
    negative_list = []
    positive_list = []
    for comment,sentiment in zip(comments, sentiments):
        score = SentimentIntensityAnalyzer().polarity_scores(comment)
        neg = score['neg']
        neu = score['neu']
        pos = score['pos']
##        print(comment)
##        print(sentiment)
        ##TODO: eger sentiment 2 ise update'le
        if neg > pos:
            print("neg")
            negative_list.append(comment)
            db.youtubeComments.update_one({'comment' : comment}, {"$set" : {"sentiment": -1}})
        elif pos > neg:
            print("pos")
            positive_list.append(comment)
            db.youtubeComments.update_one({'comment' : comment}, {"$set" : {"sentiment": 1}})
        elif pos == neg:
            print("neu")
            neutral_list.append(comment)
            db.youtubeComments.update_one({'comment' : comment}, {"$set" : {"sentiment": 0}})
##    print("negative")
##    print("--------")
##    print(len(negative_list))
##    print("positive")
##    print("--------")
##    print(len(positive_list))
##    print("neutral")
##    print("--------")
##    print(len(neutral_list))


