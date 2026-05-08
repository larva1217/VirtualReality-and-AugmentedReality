using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StopVideo : MonoBehaviour
{
   public VideoClip[] VideoSrc = new VideoClip[2];
    VideoPlayer player;
   
    public Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAndPlayVideo()
    {

        if (player.isPlaying) 
        {
            
            player.Pause();
            text.text="Start";
        }

        else 
        {
           
            player.Play();
            text.text="Stop";
        }
    }

    public bool IsPlaying()
    {
        return player.isPlaying;
    }
}
