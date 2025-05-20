using UnityEngine;

public class Footstep : MonoBehaviour
{
    public float lifetime = 3f;
    private float timer = 0f;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (mat != null)
        {
            Color c = mat.color;
            c.a = Mathf.Lerp(1f, 0f, timer / lifetime);
            mat.color = c;
        }

        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
