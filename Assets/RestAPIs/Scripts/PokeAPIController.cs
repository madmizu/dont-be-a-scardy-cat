using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// for the unity web request class
    using UnityEngine.Networking;
// Added simpleJSON script in plugins folder
    using SimpleJSON;
using UnityEngine.UI;
// to use Textmesh pro
    using TMPro;

public class PokeAPIController : MonoBehaviour
{
    // declare variables for different UI components
    public RawImage pokeRawImage;
    public TextMeshProUGUI pokeNameText, pokeNumText;
    // array in text
    public TextMeshProUGUI[] pokeTypeTextArray;
    // refers to the API base url
    private readonly string basePokeURL = "https://pokeapi.co/api/v2/";

    private void Start()
    {
        // reset each UI to blank
        pokeRawImage.texture = Texture2D.blackTexture;
        pokeNameText.text = "";
        pokeNumText.text = "";

        foreach (TextMeshProUGUI pokeTypeText in pokeTypeTextArray)
        {
            pokeTypeText.text = "";
        }
    }

    public void OnButtonRandomPokemon()
    {
        // this generates a random poke index number
        // Random.Range has Min: inclusive, Max: exclusive
        int randomPokeIndex = Random.Range(1, 808);
        // blank out raw image
        pokeRawImage.texture = Texture2D.blackTexture;
        // change Name text to loading... while the API request is working
        pokeNameText.text = "Loading...";
        // the pokemon number will show because that has already been determined byt he randomPokeIndex and that is what is being used for the fetch request
        pokeNumText.text = "#" + randomPokeIndex;

        foreach (TextMeshProUGUI pokeTypeText in pokeTypeTextArray)
        {
            pokeTypeText.text = "";
        }
        
        StartCoroutine(GetPokemonAtIndex(randomPokeIndex));
    }

    // Get Pokemon Info. This is the entire json string from the response.
    IEnumerator GetPokemonAtIndex(int pokemonIndex)
    {
        // declare variable for the URL, which is the base plus additional strings as neeeded, plus pokemonIndex changed to a string.
            // Example URL: https://pokeapi.co/api/v2/pokemon/151
            string pokemonURL = basePokeURL + "pokemon/" + pokemonIndex.ToString();

        // create a new Unity web request called 'pokeInfoRequest'
        // GET requests =>
            // UnityWebRequest.Get(URL)
        UnityWebRequest pokeInfoRequest = UnityWebRequest.Get(pokemonURL);
        // we want our coroutine to return the request response
        // running .SendWebRequest() on our get request.
        // Sends the web request out to the url and once we get the response, the IEnumerator will continue to the following 'if' statement below 
            yield return pokeInfoRequest.SendWebRequest();
        // error handling:
        if (pokeInfoRequest.isNetworkError || pokeInfoRequest.isHttpError)
        {
            //only shows error in console.
            // !!! NEED to add error messages to render on page!!!
            Debug.LogError(pokeInfoRequest.error);
            // yeild break stops the coroutine. It will not continue after this.
            yield break;
        }
        // if there are no errors, we can parse through our json for the info we need:
        // JSONNode and JSON.Parse is included int he JSON script saved in 'utility' folder.
        JSONNode pokeInfo = JSON.Parse(pokeInfoRequest.downloadHandler.text);
        //declaring variables for the info we needs:
        // pokeInfo is the name of our returned object
        // "name" is a key
            string pokeName = pokeInfo["name"];
        //This is a nested json object. We want the front_default key value from the sprites key from the PokeInfo:
            string pokeSpriteURL = pokeInfo["sprites"]["front_default"];
        // Create a new JSONNode to equal the previous JSONNode's "types" key
            JSONNode pokeTypes = pokeInfo["types"];
            // create an string array empty string array that equals the length of the "types" array in the JSON info.
                string[] pokeTypeNames = new string[pokeTypes.Count];
        // do a for loop across poke types for the "type" and "name"
        for (int i = 0, j = pokeTypes.Count - 1; i < pokeTypes.Count; i++, j--)
        {
            pokeTypeNames[j] = pokeTypes[i]["type"]["name"];
        }

        // Get Pokemon Sprite
        // UnityWebRequest to get an image from a url
        // UnityWebRequestTexture for images:
        UnityWebRequest pokeSpriteRequest = UnityWebRequestTexture.GetTexture(pokeSpriteURL);

        yield return pokeSpriteRequest.SendWebRequest();

        // error handling
        if (pokeSpriteRequest.isNetworkError || pokeSpriteRequest.isHttpError)
        {
            Debug.LogError(pokeSpriteRequest.error);
            yield break;
        }
            
        // Set UI Objects

        pokeRawImage.texture = DownloadHandlerTexture.GetContent(pokeSpriteRequest);

        // the sprites are pixel art. There is no fitering applied to the images.
        pokeRawImage.texture.filterMode = FilterMode.Point;
        
        // capitalize the first letter for each
        pokeNameText.text = CapitalizeFirstLetter(pokeName);

        for (int i = 0; i < pokeTypeNames.Length; i++)
        {
            pokeTypeTextArray[i].text = CapitalizeFirstLetter(pokeTypeNames[i]);
        }
        
    }

    private string CapitalizeFirstLetter(string str)
    {
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
