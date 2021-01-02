import tweepy

auth = tweepy.OAuthHandler('8E40q4o9fB0hD2QwswEHRdI5C', 'GR1n5be91In5sLzMZ3VzF8Ww1CG4AJ8dFQfApPrPigk5dNSR2r')
auth.set_access_token('1214934665846497289-ckXeSIzHnnv5luSl2D3epaSmKH0xB4 ', 'XUwSMbTFnR0kyKuy3wjOWr22zBfcGHL6oDX6RNZENMJQh')

api = tweepy.API(auth)

public_tweets = api.home_timeline()
user = api.me()
print(user.name)