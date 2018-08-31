ScannerApi Zeynep Aykal 11/07/2018


GENERAL USAGE NOTES
-------------------------------------------------


Python scripts for testing:

  - largerthan200mb.py  posts a request for a file that is 200mb and it should return an error message.
  - postrequest3.py posts a large file request in one thread, and then in second thread, it loops a request of 50 times 
of two simultaneous small files. Both of the threads execute at the same time.
It calculates the speed of the responses.

  - You don't normally need to enter any parameter into the python script, it has hardcoded URLs.
But you can change those for testing.
  - For sending a new request, please open postrequest3.py and enter to the PARAMS variables the URLs you wish. This way you can
also see the response speed and how is it doing with many requests at the same time.


----------------------------------------------------

ScannerApi solution:

  - Please install .NET Core if you dont have it on your computer: https://www.microsoft.com/net/download/windows
  - You can run the solution from ScannerApi\ScannerApi folder, with the command "dotnet run"
  - Default address will be: http://localhost:58271/api/scanner
  - However, if you run it from Visual Studio and click IIS Express, it will be http://localhost:58270/api/scanner . 
In this case you just need to do a small change in localhost address in python script's parameters.
  - Unit tests can be run from ScannerApi.Tests/PrimeWebDefaultRequestShould.cs


--------------------------------------------------------


 Additional information:

 - When I first wrote the app, I downloaded the files and read sha1 from there and then deleted them from disk. But later I changed 
it to read the sha1 from memory stream, without downloading the file.
 - Multi threading and using memory chunks are done automatically by Microsoft libraries, I did not have to do anything extra for this.
 - In the ScannerService.cs Checksum method, I was before reading it byte by byte. But then I decided to use another way and read it in bigger
bytes. It increased the response speed 6 times faster.
 - Dropbox files have erratic behaviour: 
1- They don't return the same sha1 each time
2- They always return a file size "-1" 
I'm thinking that Dropbox has taken some measures for ddos attacks.
