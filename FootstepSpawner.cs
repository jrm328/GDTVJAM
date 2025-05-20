using UnityEngine;

public class FootstepSpawner : MonoBehaviour
{
    public GameObject footstepPrefab;
    public Transform footstepOrigin;
    public float spawnInterval = 0.1f; // tighter for trail feel
    private float lastSpawnTime;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            if (Time.time - lastSpawnTime > spawnInterval)
            {
                SpawnFootstep();
                lastSpawnTime = Time.time;
            }
        }
    }

    void SpawnFootstep()
    {
        if (footstepPrefab == null || footstepOrigin == null) return;

        Vector3 position = footstepOrigin.position;
        Quaternion rotation = Quaternion.Euler(90f, transform.eulerAngles.y, 0f); // face up

        GameObject step = Instantiate(footstepPrefab, position, rotation);
        step.transform.localScale = Vector3.one * 0.2f; // tiny bird feet
    }
}
