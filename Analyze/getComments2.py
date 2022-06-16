from googleapiclient.discovery import build
from pymongo import MongoClient
from langdetect import detect_langs
from langdetect import detect
import re
import string
from cleantext import clean
from databaseConnection import addToDatabase

api_key = 'YOUTUBE API KEY'

def clean_comment(text):
##    text = re.sub("s+"," ", text)
    text = re.sub("[^-9A-Za-z ]", "" , text)
    text = "".join([i.lower() for i in text if i not in string.punctuation])
    text = clean(text, no_emoji = True)
##    text = re.sub(r"http\S+", "", text)
##    text = re.sub(r"www.\S+", "", text)
##    text = re.sub('[()!?]', ' ', text)
##    text = re.sub('\[.*?\]', ' ', text)
##    text = re.sub(r'^https?:\/\/.*[\r\n]*', '', text)
##    text = re.sub("([^0-9A-Za-z \t])|(\w+:\/\/\S+)", "", text)
    return text

def get_comments(part='snippet', 
                 maxResults=100, 
                 textFormat='plainText',
                 order='time',
                 videoId='SlozEKPYBPo'
                 ):

    authorList, commentList = [], []
    service = build('youtube', 'v3',
                        developerKey=api_key)

    response = service.commentThreads().list(
        part=part,
        maxResults=maxResults,
        textFormat=textFormat,
        order=order,
        videoId=videoId
    ).execute()

    while response:
        for item in response['items']:
            comment = item['snippet']['topLevelComment']
            text = comment['snippet']['textDisplay']
            author = comment["snippet"]["authorDisplayName"]

            text = clean_comment(text)
            if (len(text) != 0):
                if text.isupper() or text.islower() == True:
                
                    
                    language_detect = detect(text)

                    if language_detect == "en":
                        if text in commentList:
                            continue
                        else:
                            
                            print(author)
                            print(text)
                            print(len(commentList))
                            print("-----")
                            commentList.append(text)
                            authorList.append(author)
                            addToDatabase(videoId, author, text)
                            
            
                


        if 'nextPageToken' in response:
            response = service.commentThreads().list(
                part=part,
                maxResults=maxResults,
                textFormat=textFormat,
                order=order,
                videoId=videoId,
                pageToken=response['nextPageToken']
            ).execute()
        else:
            break
        
