import requests
from threading import Thread
import time

class myClass():


    def MyThread1(self):
        # api-endpoint
        URL = "http://localhost:58271/api/scanner"
 
        # defining a param dict for the parameters to be sent to the API
        PARAMS = {'url':"http://ipv4.download.thinkbroadband.com/100MB.zip"}
        r = requests.post(url = URL, params = PARAMS)
        start = time.time()
        pastebin_url = r.text
        print("result for Thread1 is: %s"%pastebin_url)
        print("Response for Request1 has arrived in ", time.time()-start, 'seconds.')

    def MyThread2(self):
        URL2 = "http://localhost:58271/api/scanner"
 
        PARAMS2 = {'url':"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Grave_eend_maasmuur.jpg/1200px-Grave_eend_maasmuur.jpg"}
        PARAMS3 = {'url': "http://ipv4.download.thinkbroadband.com/10MB.zip"}

        for x in range(1, 50):
            r2 = requests.post(url = URL2, params = PARAMS2)
            start2 = time.time()
            r3 = requests.post(url = URL2, params = PARAMS3)
            start3 = time.time()
            pastebin_url = r2.text
            print("result for Thread2 is: %s"%pastebin_url)
            print("Response for Request2 has arrived in ", time.time()-start2, 'seconds.')
            pastebin_url = r3.text
            print("result for Thread2 is: %s"%pastebin_url)
            print("Response for Request3 has arrived in ", time.time()-start3, 'seconds.')


if __name__ == "__main__":
    Yep = myClass()
    thread = Thread(target = Yep.MyThread1)
    thread2 = Thread(target = Yep.MyThread2)
    thread.start()
    print('thread1 with a large file download started now')
    thread2.start()
    print('thread2 with 2 small files looping 50 times started now')
    thread.join()
    print('Finished')
