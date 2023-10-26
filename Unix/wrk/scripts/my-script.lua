-- init random
math.randomseed(os.time())

-- the request function that will run at each request
request = function() 
   
   -- define the path that will search for q=%v 9%v being a random number between 0 and 1000)
   url_path = "/somepath/search?q=" .. math.random(0,1000)

   -- if we want to print the path generated
   print(url_path)

   -- Return the request object with the current URL path
   return wrk.format("GET", url_path)
end

done = function(summary, latency, requests)
      print("fin")

      print("Duration in μs: ", summary.duration)
      print("Total completed requests: ", summary.requests)
      print("Total bytes received: ", summary.bytes)
end
