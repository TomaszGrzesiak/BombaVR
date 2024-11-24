using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CaesarCypher : MonoBehaviour
{
    public TMP_InputField encodedWordInputField; // Encoded word display
    public TextMeshProUGUI shiftText; // Displays the shift value
    public TextMeshProUGUI resultText; // Displays result messages
    public LetterChooser letterChooser; // Reference to the WordLetterChooser script
    public Button checkButton; // Button to check the user's word
    public Button reloadButton; // Button to reload the word

    private string originalWord;
    private string encodedWord;
    private int shift;

    private const string RandomWordAPI = "https://random-word-api.herokuapp.com/word?number=1";

    void Start()
    {
        StartCoroutine(FetchRandomWord());

        // Add listeners to buttons
        checkButton.onClick.AddListener(CheckDecodedWord);
        reloadButton.onClick.AddListener(() => StartCoroutine(FetchRandomWord()));
    }

    IEnumerator FetchRandomWord()
    {
        resultText.text = "Loading...";
        UnityWebRequest request = UnityWebRequest.Get(RandomWordAPI);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            originalWord = jsonResponse.Trim(new char[] { '[', ']', '"' }); // Parse word from JSON

            // Generate a random shift and encode the word
            shift = Random.Range(1, 26);
            encodedWord = EncodeWord(originalWord, shift);

            // Update UI
            encodedWordInputField.text = encodedWord;
            shiftText.text = shift.ToString();
            resultText.text = "";

            // Reset letter chooser
            letterChooser.ResetChooser();
        }
        else
        {
            Debug.LogError("Failed to fetch word: " + request.error);
            resultText.text = "Error fetching word. Try again!";
        }
    }

    // Caesar cipher encoding
    private string EncodeWord(string word, int shift)
    {
        char[] encodedChars = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            char c = word[i];
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

    // Check if the chosen word matches the original word
    void CheckDecodedWord()
    {
        string userWord = letterChooser.GetChosenWord();
        if (userWord == originalWord)
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
