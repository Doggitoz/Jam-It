using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverCreation : MonoBehaviour
{
    // Modified from https://forum.unity.com/threads/how-to-detect-if-mouse-is-over-ui.1025533/
    public GameObject hoverInfo;

    public TMP_Text Name;
    public TMP_Text Quip;
    public TMP_Text Recipe;
    public TMP_Text TimesMade;

    int UILayer;
 
    private void Start()
    {
        Debug.Log("TODO: Change text recipe to image recipe");
        Debug.Log("TODO: Scale panel with element size");
        Debug.Log("TODO: This stopped working when I added in layers to the images for colors");
        UILayer = LayerMask.NameToLayer("UI");
        if (GameManager.GM.GetState() != GameState.Progress) DisableHoverUI();
    }
 
    private void Update()
    {
        if (GameManager.GM.GetState() != GameState.Progress) return;
        IsPointerOverImageElement();

        //put hover UI above or below based on mouse position
        Vector3 offset = Vector3.zero;
        offset.y = Input.mousePosition.y > Screen.height / 2.0f ? -10f : hoverInfo.GetComponent<RectTransform>().rect.height + 50f;
        hoverInfo.GetComponent<RectTransform>().localPosition = offset;
    }
 
    public void EnableHoverUI()
    {
        hoverInfo.SetActive(true);
    }

    public void DisableHoverUI()
    {
        hoverInfo.SetActive(false);
    }

    void UpdateUIData(ImageData id)
    {
        Name.text = id.Name;
        Quip.text = id.Quip;
        Recipe.text = "Recipe: " + id.Recipe; //CHANGE THIS TO IMAGES LATER. THIS STUFF COMES FROM PROGRESS UI
        TimesMade.text = "Times made: " + id.TimesMade;
    }

    #region Raycast UI
    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverImageElement()
    {
        return IsPointerOverImageElement(GetEventSystemRaycastResults());
    }
 
 
    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverImageElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
            {
                if (curRaysastResult.gameObject.GetComponent<ImageData>() != null)
                {
                    UpdateUIData(curRaysastResult.gameObject.GetComponent<ImageData>());
                    EnableHoverUI();
                    return true;
                }
            }
        }
        DisableHoverUI();
        return false;
    }
 

    
 
    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
    #endregion
}
