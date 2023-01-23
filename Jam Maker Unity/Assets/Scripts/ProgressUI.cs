using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    public float VerticalImageOffset;
    public float HorizontalImageOffset;
    public float ImageScale = 1f;
    public int rows;
    public int columns;
    public GameObject UIPanel;
    public GameObject JamTemplate;
    public GameObject JuiceTemplate;
    public GameObject PreviousButton;
    public GameObject NextButton;
    public CreationsHandler ch;
    public bool isOpen = false;
    int pageNum = 1;
    GameObject[,] displayCatalog;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TODO: IT DOESNT DELETE THE IMAGES AFTER UNLOADING THEM LOL");
        displayCatalog = new GameObject[rows, columns];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadImages()
    {
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= columns; j++)
            {
                if (displayCatalog[i - 1, j - 1] != null)
                {
                    Destroy(displayCatalog[i - 1, j - 1]);
                }
            }
        }

                float baseIndex = (pageNum - 1) * (rows * columns);
        float addedIndex = 0;
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= columns; j++)
            {
                Creation data = ch.GetCreationAtIndex((int) (baseIndex + addedIndex));
                if (data == null) continue;

                Vector2 location = new Vector2(HorizontalImageOffset * (j - 3), VerticalImageOffset * (3 - i));
                GameObject newImage;
                if (data.Type == CreationType.Jam)
                {
                    newImage = Instantiate(JamTemplate);
                }
                else if (data.Type == CreationType.Juice)
                {
                    newImage = Instantiate(JuiceTemplate);
                }
                else
                {
                    Debug.LogError("SOMEHOW THIS GOT TO AN ERROR TYPE IDK MAN");
                    newImage = Instantiate(JamTemplate);
                }
                newImage.transform.SetParent(UIPanel.transform);
                newImage.GetComponent<RectTransform>().localPosition = location;
                newImage.GetComponent<RectTransform>().localScale = Vector3.one * ImageScale;

                //LOGIC TO DISPLAY PROPER IMAGE
                ImageData id = newImage.GetComponent<ImageData>();
                if (data.TimesMade > 0)
                {
                    id.Name = data.Name + " " + data.Type.ToString();
                }
                else
                {
                    id.Name = data.IsSecret ? "???" : data.Name + " " + data.Type.ToString();
                }
                
                id.Quip = data.TimesMade > 0 ? data.Quip : "";
                id.Recipe = data.TimesMade > 0 ? data.Recipe : "";
                id.TimesMade = data.TimesMade;

                //IMAGE COLOR LOGIC
                newImage.GetComponent<Image>().color = data.Color;

                if (data.TimesMade < 1)
                {
                    id.EnableCover();
                }

                //Update added index
                addedIndex++;

                //Store in array for deletion later
                displayCatalog[i - 1, j - 1] = newImage;
            }
        }
    }

    public void OpenUI()
    {
        isOpen = true;
        UIPanel.SetActive(true);
        PreviousButton.SetActive(false);
        NextButton.SetActive(true);
        pageNum = 1;
        LoadImages();
    }

    public void CloseUI()
    {
        isOpen = false;
        UIPanel.SetActive(false);
    }

    public void NextPage()
    {
        int maxNumPages = Mathf.CeilToInt(ch.GetCreationListSize() / (float)(rows * columns));
        if (pageNum >= maxNumPages) return;
        if (pageNum == maxNumPages - 1)
        {
            pageNum++;
            NextButton.SetActive(false);
        }
        else
        {
            pageNum++;
        }
        PreviousButton.SetActive(true);
        LoadImages();
    }

    public void PreviousPage()
    {
        if (pageNum <= 1) return;
        if (pageNum == 2)
        {
            pageNum--;
            PreviousButton.SetActive(false);
        }
        else
        {
            pageNum--;
        }
        NextButton.SetActive(true);
        LoadImages();
    }
}
