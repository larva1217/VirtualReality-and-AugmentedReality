using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color color = Color.green;
    public float radius = 0.02f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnDrawGizmos(){
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position,radius);
    }
}
