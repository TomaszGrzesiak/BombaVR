using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Encrypt : MonoBehaviour
{
     public TMP_InputField encodedWordInputField; // Input field for the encoded word (read-only)
    public TMP_InputField decodedWordInputField; // Input field for user's decoded word
    public TextMeshProUGUI shiftText; // Text field showing the shift value
    public TextMeshProUGUI resultText; // Text field for displaying result messages
    public Button checkButton; // Button to check the user's answer
    public Button reloadButton; // Button to reload with a new case

    private string originalWord;
    private string encodedWord;
    private int shift;

    private const string RandomWordAPI = "https://random-word-api.herokuapp.com/word?number=1";

    void Start()
    {
        // Fetch a random word from the API
        StartCoroutine(FetchRandomWord());
        resultText.text = "";

        // Add listeners to buttons
        checkButton.onClick.AddListener(CheckDecodedWord);
        reloadButton.onClick.AddListener(() => StartCoroutine(FetchRandomWord()));
    }

    IEnumerator FetchRandomWord()
    {
        resultText.text = "Loading..."; // Indicate loading
        UnityWebRequest request = UnityWebRequest.Get(RandomWordAPI);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Parse the API response
            string jsonResponse = request.downloadHandler.text;
            originalWord = jsonResponse.Trim(new char[] { '[', ']', '"' }); // Clean up response

            // Generate a random shift
            shift = Random.Range(1, 26);

            // Encode the selected word
            encodedWord = EncodeWord(originalWord, shift);

            // Display the encoded word and shift value
            encodedWordInputField.text = encodedWord;
            shiftText.text = shift.ToString();

            // Clear input and result message
            decodedWordInputField.text = "";
            resultText.text = "";
        }
        else
        {
            Debug.LogError("Failed to fetch random word: " + request.error);
            resultText.text = "Error fetching word. Try again!";
            originalWord = "hello"; // Fallback word

            // Generate a random shift and encode fallback word
            shift = Random.Range(1, 26);
            encodedWord = EncodeWord(originalWord, shift);

            // Update UI with fallback
            encodedWordInputField.text = encodedWord;
            shiftText.text = shift.ToString();
        }
    }

    // Encode the word using Caesar cipher logic
    string EncodeWord(string word, int shift)
    {
        char[] encodedChars = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            char c = word[i];
            // Shift each character (only letters)
            if (char.IsLetter(c))
            {
                char baseChar = char.IsUpper(c) ? 'A' : 'a';
                encodedChars[i] = (char)(((c - baseChar + shift) % 26) + baseChar);
            }
            else
            {
                encodedChars[i] = c; // Leave non-letters unchanged
            }
        }
        return new string(encodedChars);
    }

    // Decode and validate user's input
    void CheckDecodedWord()
    {
        string userDecodedWord = decodedWordInputField.text.ToLower();
        if (userDecodedWord == originalWord)
        {
            resultText.text = "Correct! You decoded the word!";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "Incorrect. Try again!";
            resultText.color = Color.red;
        }
    }
}
