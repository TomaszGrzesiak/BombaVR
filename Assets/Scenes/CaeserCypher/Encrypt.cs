using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Encrypt : MonoBehaviour
{
    public TMP_InputField encodedWordInputField; // Input field for the encoded word (read-only)
    public TextMeshProUGUI shiftText; // Text field showing the shift value
    public TextMeshProUGUI resultText; // Text field for displaying result messages
    public Button checkButton; // Button to check the user's answer
    public Button reloadButton; // Button to reload with a new case

    public LetterChooser letterChooser; // Reference to the LetterChooser class

    private string originalWord;
    private string encodedWord;
    private int shift;

    private readonly string[] predefinedWords = { "start", "clean", "plane" }; // Predefined words

    void Start()
    {
        // Initialize with a random word
        LoadRandomWord();
        resultText.text = "";

        // Add listeners to buttons
        checkButton.onClick.AddListener(CheckDecodedWord);
        reloadButton.onClick.AddListener(LoadRandomWord);
    }

    void LoadRandomWord()
    {
        resultText.text = ""; // Clear result message

        // Select a random word from the predefined list
        originalWord = predefinedWords[Random.Range(0, predefinedWords.Length)];

        // Generate a random shift
        shift = Random.Range(1, 26);

        // Encode the selected word
        encodedWord = EncodeWord(originalWord, shift);

        // Display the encoded word and shift value
        encodedWordInputField.text = encodedWord;
        shiftText.text = shift.ToString();

        // Reset the LetterChooser
        letterChooser.ResetChooser();
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
        string userDecodedWord = letterChooser.GetChosenWord(); // Get the word from LetterChooser
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
