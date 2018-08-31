import requests
 
# api-endpoint
URL = "http://localhost:58271/api/scanner"
 
# defining a param dict for the parameters to be sent to the API
PARAMS = {'url':"http://ipv4.download.thinkbroadband.com/512MB.zip"}

 
# sending get request and saving the response as response object
r = requests.post(url = URL, params = PARAMS)
  
# extracting id, url, result and sha1 
# of the requests
pastebin_url = r.text
print("result is :%s"%pastebin_url)
