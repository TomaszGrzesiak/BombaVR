using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterChooser : MonoBehaviour
{
    public TextMeshProUGUI[] letterDisplays; // Letter display for each position
    public Button[] upButtons; // Up arrow buttons
    public Button[] downButtons; // Down arrow buttons

    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    void Start()
    {
        // Add listeners to up and down buttons
        for (int i = 0; i < letterDisplays.Length; i++)
        {
            int index = i; // Capture the index in the loop
            upButtons[i].onClick.AddListener(() => IncrementLetter(index));
            downButtons[i].onClick.AddListener(() => DecrementLetter(index));
        }
        ResetChooser();
    }

    public void ResetChooser()
    {
        // Reset all letters to 'A'
        foreach (var letterDisplay in letterDisplays)
        {
            letterDisplay.text = "A";
        }
    }

    public string GetChosenWord()
    {
        string word = "";
        foreach (var letterDisplay in letterDisplays)
        {
            word += letterDisplay.text;
        }
        return word.ToLower(); // Return the word in lowercase
    }

    private void IncrementLetter(int index)
    {
        char currentChar = GetCurrentLetter(index);
        letterDisplays[index].text = GetNextLetter(currentChar, 1).ToString();
    }

    private void DecrementLetter(int index)
    {
        char currentChar = GetCurrentLetter(index);
        letterDisplays[index].text = GetNextLetter(currentChar, -1).ToString();
    }

    private char GetCurrentLetter(int index)
    {
        string text = letterDisplays[index].text.ToUpper();
        return string.IsNullOrEmpty(text) ? 'A' : text[0];
    }

    private char GetNextLetter(char current, int step)
    {
        int currentIndex = Alphabet.IndexOf(current);
        if (currentIndex == -1) currentIndex = 0;

        int newIndex = (currentIndex + step + Alphabet.Length) % Alphabet.Length;
        return Alphabet[newIndex];
    }
}
