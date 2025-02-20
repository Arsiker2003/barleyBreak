using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    int rightButtons = 0;
    string[][] buttonTextCoor;
    Button[][] buttonsGrid;
    [SerializeField]
    private Image endGameScreen;

    // Start is called before the first frame update
    void Start()
    {
        Initiate();
    }

    public void Initiate()
    {
        Button[] buttons = GetComponentsInChildren<Button>()
            .Where( b => b.GetComponentInChildren<TMP_Text>() != null).ToArray();
        buttonsGrid = new Button[4][];
        for (int i = 0; i < buttonsGrid.Length; i++)
        {
            buttonsGrid[i] = new Button[4];
            for (int j = 0; j < buttonsGrid[i].Length; j++)
            {
                buttonsGrid[i][j] = buttons[j + i * 4];
            }
        }
        string[] texts = new string[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            string temp = buttons[i].GetComponentInChildren<TMP_Text>().text;
            texts[i] = temp;
        }
        int n = texts.Length;
        
        while (n > 0)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n);
            (texts[n], texts[k]) = (texts[k], texts[n]);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TMP_Text>().text = texts[i];
        }

        RectTransform[] rows = GetComponentsInChildren<RectTransform>().Where(i => i.name.Contains("Row")).ToArray();
        buttonTextCoor = new string[rows.Length][];
        for (int i = 0; i < buttonTextCoor.Length; i++)
        {
            buttonTextCoor[i] = new string[rows[i].GetComponentsInChildren<Button>().ToArray().Length];
        }

        for (int i = 0; i < buttonTextCoor.Length; i++)
        {
            buttons = rows[i].GetComponentsInChildren<Button>().ToArray();
            for (int j = 0; j < buttonTextCoor[i].Length; j++)
            {
                buttonTextCoor[i][j] = buttons[j].GetComponentInChildren<TMP_Text>().text;
            }
        }
        
    }
    public void OnCLick(string BTNName)
    {

        
        Button button = GetComponentsInChildren<Button>().Where(b => b.name == BTNName).First();
        Button[] BTNArr = Array.Find(buttonsGrid,b => b.Contains<Button>(button));
        int i = Array.IndexOf(buttonsGrid, BTNArr);
        int j = Array.IndexOf(
            Array.Find(buttonsGrid, 
            b => b.Contains<Button>(button)), button);
        
        Button[] neighbourBTNs = new Button[4];
        switch (i)
        {
            case 0:
                neighbourBTNs[0] = buttonsGrid[i + 1][j];
                neighbourBTNs[1] = buttonsGrid[i + 1][j];
                break;
            case 3:
                neighbourBTNs[1] = buttonsGrid[i - 1][j];
                neighbourBTNs[0] = buttonsGrid[i - 1][j];
                break;
                default:
                neighbourBTNs[0] = buttonsGrid[i + 1][j];
                neighbourBTNs[1] = buttonsGrid[i - 1][j];
                break;
        }
        switch (j)
        {
            case 0:
                neighbourBTNs[2] = buttonsGrid[i][j + 1];
                neighbourBTNs[3] = buttonsGrid[i][j + 1];
                break;
            case 3:
                neighbourBTNs[3] = buttonsGrid[i][j - 1];
                neighbourBTNs[2] = buttonsGrid[i][j - 1];
                break;
            default:
                neighbourBTNs[2] = buttonsGrid[i][j + 1];
                neighbourBTNs[3] = buttonsGrid[i][j - 1];
                break;
        }
        Button swapButton = Array.Find(neighbourBTNs,
            b => b.GetComponentInChildren<TMP_Text>().text == "  ");
        if (swapButton != null)
        {
            swapButton.GetComponentInChildren<TMP_Text>().text =
                button.GetComponentInChildren<TMP_Text>().text;
            button.GetComponentInChildren<TMP_Text>().text = "  ";
        }

        if (swapButton.name.Contains(swapButton.GetComponentInChildren<TMP_Text>().text))
        {
            swapButton.image.color = Color.green;
            rightButtons++;
        }
        else
        {
            if (swapButton.image.color == Color.green)
            {
                rightButtons--;
            }
            Color col = Color.white;
            col.a = 0.5f;
            swapButton.image.color = col;
        }
        if (button.name.Contains(button.GetComponentInChildren<TMP_Text>().text))
        {
            button.image.color = Color.green;
            rightButtons++;

        }
        else
        {
            if (button.image.color == Color.green)
            {
                rightButtons--;
            }
            Color col = Color.white;
            col.a = 0.5f;
            button.image.color = col;
        }
        if (rightButtons == 15)
        {
            StartCoroutine(Nextscene());
        }
    }


    private IEnumerator Nextscene()
    {
        endGameScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(1);
    }
}
