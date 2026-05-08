using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;



using Unity.Collections;
using TMPro;

public class FindDetection : MonoBehaviour
{
    public ARFaceManager faceManager;
    public GameObject smallCube;
    public TMP_Text vertexIndex;

    List<GameObject> testCubes = new List<GameObject>();




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<3; i++){
            GameObject go = Instantiate(smallCube);
            testCubes.Add(go);
            go.SetActive(false);
        }

        faceManager.trackablesChanged.AddListener(onDetectFaceAll);
        
    }

    void onDetectThreePoints(ARTrackablesChangedEventArgs<ARFace> changes){

    }

    void onDetectFaceAll(ARTrackablesChangedEventArgs<ARFace> changes){
        foreach(var face in changes.updated){
            //Vector3 vertexPos = face.vertices[100];
            int num = int.Parse(vertexIndex.text);
            Vector3 vertexPos = face.vertices[num];
            vertexPos = face.transform.TransformPoint(vertexPos);
            testCubes[0].SetActive(true);
            testCubes[0].transform.position = vertexPos;
        }
        foreach(var face in changes.removed){
            testCubes[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
