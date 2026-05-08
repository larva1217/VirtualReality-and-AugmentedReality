using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using System.Collections;

public class PetEvolutionManager : MonoBehaviour
{
    public GameObject[] evolutionPrefabs; // Cat, Dog, Pinguin, Deer, Hourse, Tiger 순서로 연결
    public GameObject evolutionPopupPanel; // 진화 팝업 패널 (Canvas 하위에 비활성화 상태로 배치)
    public TextMeshProUGUI popupText; // TextMeshProUGUI로 변경
    public AudioClip oiiaiiClip; // 오이아이 음악
    public VideoPlayer videoPlayer; // 오이아이 영상
    public GameObject heartParticlePrefab; // 하트 파티클 프리팹
    private AudioSource audioSource;
    private int feedCount = 0;
    private int currentStage = -1; // -1: Chicken, 0: Cat, ...
    private GameObject currentPet;

    public AudioClip feedSoundClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // 만약 AudioSource 컴포넌트가 없다면 추가해줍니다.
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        currentPet = GameObject.Find("Chicken");
    }

    public void Feed()
    {
        Debug.Log("FEED BUTTON CLICK");

        if (currentPet != null)
        {
            //밥 먹는 소리 추가
            if (feedSoundClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(feedSoundClip);
            }
            
            // 밥먹는 애니메이션
            Animator animator = currentPet.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Eat");
            }

            currentPet.transform.localScale *= 1.2f;
            feedCount++;

            if (IsPowerOfTwo(feedCount) && currentStage + 1 < evolutionPrefabs.Length)
            {
                ShowEvolutionPopup();
            }
        }
    }

    bool IsPowerOfTwo(int x)
    {
        return x > 0 && (x & (x - 1)) == 0;
    }

    void ShowEvolutionPopup()
    {
        if (popupText != null)
            popupText.text = "진화하시겠습니까?";
        if (evolutionPopupPanel != null)
            evolutionPopupPanel.SetActive(true);
    }

    public void OnEvolutionYes()
    {
        if (evolutionPopupPanel != null)
            evolutionPopupPanel.SetActive(false);
        Evolve();
    }

    public void OnEvolutionNo()
    {
        if (evolutionPopupPanel != null)
            evolutionPopupPanel.SetActive(false);
    }

    void Evolve()
    {
        if (currentStage + 1 < evolutionPrefabs.Length)
        {
            Vector3 pos = currentPet.transform.position;
            Quaternion rot = currentPet.transform.rotation;
            Destroy(currentPet);

            currentStage++;
            SpawnPet(currentStage, pos, rot);
        }
    }

    void SpawnPet(int stage, Vector3? pos = null, Quaternion? rot = null)
    {
        if (currentPet != null)
        {
            Destroy(currentPet);
        }
        Vector3 spawnPos = pos ?? Vector3.zero;
        Quaternion spawnRot = rot ?? Quaternion.identity;
        currentPet = Instantiate(evolutionPrefabs[stage], spawnPos, spawnRot);
    }

    // ====== Play 버튼 기능 ======
    public void PlayWithPet()
    {
        if (currentPet != null)
        {
            // 1. 하트 파티클 생성
            if (heartParticlePrefab != null)
            {
                Instantiate(heartParticlePrefab, currentPet.transform.position + Vector3.up * 0.5f, Quaternion.identity);
            }
            // 2. Dance 애니메이션 트리거
            Animator animator = currentPet.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Dance");
            }
        }
    }

    // ====== Secret Gimmick ======
    public void SecretGimmick()
    {
        if (currentPet != null)
        {
            StartCoroutine(RotatePetDynamic());

            // 비디오 소리 음소거
            if (videoPlayer != null)
            {
                videoPlayer.SetDirectAudioMute(0, true); // 첫 번째 오디오 트랙 음소거
                videoPlayer.Stop();
                videoPlayer.Play();
                StartCoroutine(DisableVideoAfterEnd());
            }

            // 오디오 재생 (10초까지만)
            if (oiiaiiClip != null && audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = oiiaiiClip;
                audioSource.Play();
                StartCoroutine(StopAudioAfterSeconds(10f));
            }
        }
    }

    IEnumerator DisableVideoAfterEnd()
    {
        if (videoPlayer != null)
        {
            // 영상 길이만큼 대기 (10초)
            yield return new WaitForSeconds((float)videoPlayer.length);
            videoPlayer.Stop();
            videoPlayer.targetCameraAlpha = 0f; // 화면에서 안 보이게
        }
    }

    IEnumerator RotatePetDynamic()
    {
        float duration = 10f;
        float elapsed = 0f;
        Quaternion startRot = currentPet.transform.rotation;
        float totalY = 720f; // 2바퀴
        Vector3 startPos = currentPet.transform.position;
        float bounceHeight = 0.2f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            // Ease-in -> Ease-out (느리게 시작, 빠르게 끝)
            float speedCurve = Mathf.Pow(t, 2.5f); // 초반 느림, 후반 빠름
            float yRot = Mathf.Lerp(0, totalY, speedCurve);
            currentPet.transform.rotation = startRot * Quaternion.Euler(0, yRot, 0);

            // 바운스(상하 진동)
            float bounce = Mathf.Sin(t * Mathf.PI * 6) * bounceHeight * (1 - t * 0.5f); // 점점 바운스 줄어듦
            Vector3 pos = startPos + new Vector3(0, bounce, 0);
            currentPet.transform.position = pos;

            elapsed += Time.deltaTime;
            yield return null;
        }
        // 마지막 위치/회전 보정
        currentPet.transform.rotation = startRot * Quaternion.Euler(0, totalY, 0);
        currentPet.transform.position = startPos;
    }

    IEnumerator StopAudioAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

}
