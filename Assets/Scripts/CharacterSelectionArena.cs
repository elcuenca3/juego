using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionArena : MonoBehaviour
{
    
    public GameObject[] playerObjects;
    public int selectedCharacter = 0;

    public string gameScene = "arena2.1";

    private string selectedCharacterDataName = "SelectedCharacter";

    void Start()
    {

        HideAllCharacters();

        //selectedCharacter = PlayerPrefs.GetInt(selectedCharacterDataName, 0);

        playerObjects[selectedCharacter].SetActive(true);
    }


    private void HideAllCharacters()
    {
        foreach (GameObject g in playerObjects)
        {
            g.SetActive(false);
        }
    }

    public void NextCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= playerObjects.Length)
        {
            selectedCharacter = 0;
        }
        playerObjects[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        playerObjects[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = playerObjects.Length-1;
        }
        playerObjects[selectedCharacter].SetActive(true);
    }
    

    public void StartGame()
    {
        PlayerPrefs.SetInt("IDArena",selectedCharacter);
        switch (selectedCharacter){
            case 0:
            SceneManager.LoadScene("arena2.1");
            break;
            case 1:
            SceneManager.LoadScene("arenanew");
            break;
            default:
            break;

            
        }
        Debug.Log("dddddd"+PlayerPrefs.GetInt("IDCARRO"));
        //SceneManager.LoadScene("seleccionMapa");
        //PlayerPrefs.SetInt(selectedCharacterDataName, selectedCharacter);
        //SceneManager.LoadScene(gameScene);
    }

}
