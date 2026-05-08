using UnityEngine;

public class ChangeSource : MonoBehaviour
{
    public Texture[] TextureImage = new Texture[2];
    Renderer render;
    int imageIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        render = GetComponent<Renderer>();
        imageIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeImage(){
        imageIndex++;
        if(imageIndex > 1){
            imageIndex = 0;
        }
        render.material.SetTexture("_BaseMap", TextureImage[imageIndex]);
    }
}
