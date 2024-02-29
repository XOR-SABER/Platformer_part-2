using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject starPrefab;
    public GameObject waterPrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }
        int row = 0;
        // Go through the rows from bottom to top
        char[] prevLine = null;
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                if(letter == ' ') continue;
                Vector3 newPos = new Vector3(column, row, 0f);
                switch (letter)
                {
                    case 'x':
                        Instantiate(rockPrefab, newPos, Quaternion.identity, environmentRoot);
                        break;
                    case '?':
                        Instantiate(questionBoxPrefab, newPos, Quaternion.identity, environmentRoot);
                        break;
                    case 'b':
                        Instantiate(brickPrefab, newPos, Quaternion.identity, environmentRoot);
                        break;
                    case 's':
                        Instantiate(stonePrefab, newPos, Quaternion.identity, environmentRoot);
                        break;
                    case 't':
                        Instantiate(starPrefab, newPos, Quaternion.Euler(0f, 0f, 180f), environmentRoot);
                        break;
                    case 'w':
                        // Instantiate the water prefab
                        GameObject water = Instantiate(waterPrefab, newPos, Quaternion.identity, environmentRoot);
                        // Get the Renderer component of the instantiated water object
                        Renderer renderer = water.GetComponent<Renderer>();
                        if (prevLine == null || prevLine[column] == 'w') renderer.material.mainTextureScale = new Vector2(1f, 0f);
                        else renderer.material.mainTextureScale = new Vector2(1f, 0.5f);
                        break;
                }
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
            }
            row++;
            prevLine = letters;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
