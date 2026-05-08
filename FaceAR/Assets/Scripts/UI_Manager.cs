using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public ARFaceManager faceManager;

    public Material[] faceMats;
    public TMP_Text vertexIndex;

    int vertexNum = 0;
    int vertexCount = 468;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vertexIndex.text = vertexNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleMaskImage()
    {
        foreach (ARFace face in faceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
            {
                face.gameObject.SetActive(!face.gameObject.activeInHierarchy);
            }
        }
    }

    public void SwitchFaceMaterial(int num)
    {
        foreach (ARFace face in faceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
            {
                face.gameObject.GetComponent<MeshRenderer>().material = faceMats[num];
            }
        }
    }

    public void IndexIncrease()
    {
        int number = Mathf.Min(++vertexNum, vertexCount);
        vertexIndex.text = number.ToString();
    }

    public void IndexDecrease()
    {
        int number = Mathf.Max(--vertexNum, 0);
        vertexIndex.text = number.ToString();
    }
    
    
}
