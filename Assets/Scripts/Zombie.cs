using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Color[] zomColors;
    public int zomIndex = 0;

    private Renderer Renderer;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        if (Renderer == null || zomColors == null || zomColors.Length == 0)
            return;

        int index = Random.Range(0, zomColors.Length);
        zomIndex = index;

        Renderer.material.color = zomColors[index];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        Bullet bullet = other.GetComponentInParent<Bullet>();
        Destroy(bullet.gameObject);

        if (bullet.PeaIndex == zomIndex)
            Destroy(gameObject);

    }
}