using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Change : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject eggPrefab;
    public GameObject chickenPrefab;
    public PetFeeder petFeeder; // PetFeeder 스크립트 참조

    private Dictionary<string, GameObject> currentInstances = new Dictionary<string, GameObject>();
    private Dictionary<string, float> spawnTimers = new Dictionary<string, float>();
    private HashSet<string> usedMarkers = new HashSet<string>();

    void Update()
    {
        foreach (var trackedImage in trackedImageManager.trackables)
        {
            string imageName = trackedImage.referenceImage.name;

            if (usedMarkers.Contains(imageName))
                continue;

            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if (!currentInstances.ContainsKey(imageName))
                {
                    GameObject egg = Instantiate(eggPrefab);
                    egg.transform.SetParent(trackedImage.transform);
                    egg.transform.localPosition = Vector3.zero;
                    egg.transform.localRotation = Quaternion.identity;
                    egg.transform.localScale = Vector3.one;

                    currentInstances[imageName] = egg;
                    spawnTimers[imageName] = Time.time;
                }
                else
                {
                    float elapsed = Time.time - spawnTimers[imageName];
                    if (elapsed >= 5f)
                    {
                        Destroy(currentInstances[imageName]);

                        GameObject chicken = Instantiate(chickenPrefab);
                        chicken.transform.SetParent(trackedImage.transform);
                        chicken.transform.localPosition = Vector3.zero;
                        chicken.transform.localRotation = Quaternion.identity;
                        chicken.transform.localScale = Vector3.one;

                        currentInstances[imageName] = chicken;
                        spawnTimers.Remove(imageName);
                        usedMarkers.Add(imageName);

                        petFeeder.SetCurrentPet(chicken, trackedImage.transform, startStage: -1); // -1로 시작
                    }
                }
            }
        }
    }
}
