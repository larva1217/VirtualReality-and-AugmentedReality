using System.Collections.Generic;
using UnityEngine;

public class PetFeeder : MonoBehaviour
{
    public List<GameObject> evolutionPrefabs; // [Cat, Dog, ...] 순서
    private int currentStage = 0;
    private int feedCount = 0;

    private GameObject currentPet;

    public AudioClip feedSoundClip;
    private AudioSource audioSource;

    public GameObject heartParticlePrefab;

    public float scaleIncreasePerFeed = 0.1f;

    private Transform arAnchorTransform;

    public AudioClip evolutionSoundClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void SetCurrentPet(GameObject pet, Transform anchorTransform, int startStage = 0)
    {
        currentPet = pet;
        arAnchorTransform = anchorTransform;
        currentStage = startStage;
        feedCount = 0;
    }

    public void Feed()
    {
        Debug.Log("Feed 버튼 누름.");

        if (currentPet == null || arAnchorTransform == null)
        {
            return;
        }

        if (feedSoundClip != null)
        {
            audioSource.PlayOneShot(feedSoundClip);
        }

        currentPet.transform.localScale += Vector3.one * scaleIncreasePerFeed;
        feedCount++;

        if (feedCount >= 5)
        {
            if (currentStage + 1 < evolutionPrefabs.Count)
            {
                currentStage++; // 다음 진화 단계로 증가

                Transform parent = currentPet.transform.parent;
                Destroy(currentPet);

                currentPet = Instantiate(evolutionPrefabs[currentStage]);

                Collider newPetCollider = currentPet.GetComponent<Collider>();
                if (newPetCollider == null)
                {
                    currentPet.transform.position = arAnchorTransform.position;
                }
                else
                {
                    float petHalfHeight = newPetCollider.bounds.extents.y;
                    currentPet.transform.position = arAnchorTransform.position + Vector3.up * petHalfHeight;

                    Rigidbody newPetRb = currentPet.GetComponent<Rigidbody>();
                    if (newPetRb != null)
                    {
                        newPetRb.isKinematic = true;
                        newPetRb.useGravity = false;
                    }
                }

                currentPet.transform.rotation = arAnchorTransform.rotation;
                currentPet.transform.localScale = Vector3.one;
                currentPet.transform.SetParent(parent);

                if (heartParticlePrefab != null)
                {
                    GameObject particle = Instantiate(heartParticlePrefab, currentPet.transform.position, Quaternion.identity);
                    Destroy(particle, 2.0f);
                }

                if (evolutionSoundClip != null)
                {
                    audioSource.PlayOneShot(evolutionSoundClip);
                }

                feedCount = 0;
            }
        }
    }
}
