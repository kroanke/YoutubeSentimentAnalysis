##from getComments import video_comments
from getComments2 import get_comments
from Analyze import getComments, analyze

def main():
    #Function to get youtube comments
##    get_comments()

    #Function to analyze the youtube comments
    analyze(getComments())

main()
