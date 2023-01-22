using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    public float VerticalImageOffset;
    public float HorizontalImageOffset;
    public float ImageScale = 1f;
    public float rows;
    public float columns;
    public GameObject UIPanel;
    public GameObject ImageTemplate;
    public GameObject PreviousButton;
    public GameObject NextButton;
    public CreationsHandler ch;
    public bool isOpen = false;
    int pageNum = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadImages()
    {
        float baseIndex = (pageNum - 1) * (rows * columns);
        float addedIndex = 0;
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= columns; j++)
            {
                Creation data = ch.GetCreationAtIndex((int) (baseIndex + addedIndex));
                if (data == null) continue;

                Vector2 location = new Vector2(HorizontalImageOffset * (j - 3), VerticalImageOffset * (3 - i));
                GameObject newImage = Instantiate(ImageTemplate);
                newImage.transform.SetParent(UIPanel.transform);
                newImage.GetComponent<RectTransform>().localPosition = location;
                newImage.GetComponent<RectTransform>().localScale = Vector3.one * ImageScale;

                //LOGIC TO DISPLAY PROPER IMAGE
                ImageData id = newImage.GetComponent<ImageData>();
                id.Name = data.IsSecret ? "???" : data.Name;
                id.Quip = data.TimesMade > 0 ? data.Quip : "";
                id.Recipe = data.TimesMade > 0 ? data.Recipe : "";
                id.TimesMade = data.TimesMade;

                //IMAGE COLOR LOGIC
                if (data.TimesMade < 1)
                {
                    newImage.GetComponent<Image>().color = Color.black;
                }
                else
                {
                    newImage.GetComponent<Image>().color = data.Color;
                }

                //DEBUG STUFF
                newImage.GetComponent<Image>().color = data.Color;

                //Update added index
                addedIndex++;
            }
        }
    }

    public void OpenUI()
    {
        isOpen = true;
        UIPanel.SetActive(true);
        PreviousButton.SetActive(false);
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
        int maxNumPages = Mathf.CeilToInt(ch.GetCreationListSize() / (rows * columns));
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
