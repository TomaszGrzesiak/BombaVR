using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Encrypt : MonoBehaviour
{
    public TMP_InputField encodedWordInputField; // Input field for the encoded word (read-only)
    public TMP_InputField decodedWordInputField; // Input field for user's decoded word
    public TextMeshProUGUI shiftText; // Text field showing the shift value
    public TextMeshProUGUI resultText; // Text field for displaying result messages
    public Button checkButton; // Button to check the user's answer

    private string originalWord = "hello";
    private string encodedWord;
    private int shift;

    void Start()
    {
        // Generate a random shift value between 1 and 25
        shift = Random.Range(1, 26);

        // Encode the word "hello"
        encodedWord = EncodeWord(originalWord, shift);

        // Display the encoded word and shift value
        encodedWordInputField.text = encodedWord;
        shiftText.text = shift.ToString();

        // Add listener to the button
        checkButton.onClick.AddListener(CheckDecodedWord);
    }

    // Encode the word using Caesar cipher logic
    string EncodeWord(string word, int shift)
    {
        char[] encodedChars = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
        {
            char c = word[i];
            // Shift each character
            char shiftedChar = (char)(((c - 'a' + shift) % 26) + 'a');
            encodedChars[i] = shiftedChar;
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
            resultText.color = Color.green; // Optional: Set color to green for correct answer
        }
        else
        {
            resultText.text = "Incorrect. Try again!";
            resultText.color = Color.red; // Optional: Set color to red for incorrect answer
        }
    }
}
