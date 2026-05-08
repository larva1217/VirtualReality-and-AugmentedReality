using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI; 

public class ChangeSrc : MonoBehaviour
{
    public VideoClip[] VideoSrc = new VideoClip[2];
    VideoPlayer player;
    int videoIndex;

    public StopVideo stopVideoScript; 
    public Toggle toggle;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        videoIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVideo(){
        videoIndex++;
        if(videoIndex > 1){
            videoIndex = 0;
        }
        player.clip = VideoSrc[videoIndex];

        if (!stopVideoScript.IsPlaying())
        {
            player.Play();
            toggle.isOn = false;
        }
    }
}
