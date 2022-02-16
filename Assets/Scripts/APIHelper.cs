// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// System.Net is needed when dealing with HTTP web responses
    using System.Net;
// reader to read our response
    using System.IO;

public static class APIHelper
{
    public static Joke GetNewJoke()
    {
        // GET request we send out to gather the info:
        // pass URL inside .Create
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.chucknorris.io/jokes/random");
        // interrogate the request and find its response:
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        // need to read that response by passing it through a stream reader:
        StreamReader reader = new StreamReader(response.GetResponseStream());
        // get the json response from the StreamReader then pass that response into our 'joke'
            // create a string called 'json'
            string json = reader.ReadToEnd();
            // turn the json response into a 'joke' object
            // 'return' the joke
            // you can use any Json Utility library that you would like 
            // .FromJson => <variable type that we want to pass it to>(json string)
            return JsonUtility.FromJson<Joke>(json);
    }
}
