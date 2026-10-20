using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float exist = 5f;
    [SerializeField] private Color[] bulletColors;

    private Renderer myRenderer;

    public int PeaIndex { get; private set; }

    private void Awake()
    {
        myRenderer = GetComponentInChildren<Renderer>();
    }

    // Called by the Peashooter right after spawning
    public void Init(int index)
    {
        PeaIndex = index;

        if (myRenderer != null && bulletColors != null && index >= 0 && index < bulletColors.Length)
            myRenderer.material.color = bulletColors[index];
    }

    private void Start()
    {
        Destroy(gameObject, exist);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}