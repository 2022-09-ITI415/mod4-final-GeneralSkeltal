using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject Level_1;
    public GameObject Level_2;

    bool changingLevel;

    public Inventory inventory;

    public GameObject blackScreen;
    public GameObject winUI;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        if (inventory.animalCount == 1 && !changingLevel && Level_2.active == false)
        {
            changingLevel = true;
            StartCoroutine(ChangeToLevel2());
        }

        if (inventory.animalCount == 3 && !changingLevel)
        {
            changingLevel = true;
            LevelEnd();
        }
    }

    public void LevelEnd()
    {
        FindObjectOfType<FirstPersonController>().enabled = false;
        winUI.SetActive(true);
        Invoke("RestartGame", 5f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator ChangeToLevel2()
    {
        yield return new WaitForSeconds(2f);
        blackScreen.SetActive(true);
        Level_2.SetActive(true);
        Level_1.SetActive(false);
        yield return new WaitForSeconds(2f);
        blackScreen.SetActive(false);
    }
}
