using System.Collections;
using UnityEngine;

public class Peashooter : MonoBehaviour, ISwitchable
{
    [SerializeField] private Material peaMart;
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private Color[] peaColor;
    [SerializeField] private float fireTime = 1f;
    public int peaIndex = 0;
    private Transform targetZom;

    private void Start()
    {
        ApplyColor();
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if (targetZom == null)
            return;

        Vector3 lookSpot = new Vector3(targetZom.position.x, transform.position.y, targetZom.position.z);
        transform.LookAt(lookSpot);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something entered: " + other.name + " | tag: " + other.tag);

        if (targetZom == null && other.CompareTag("Rotate"))
            targetZom = other.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (targetZom == null && other.CompareTag("Rotate"))
            targetZom = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (targetZom == other.transform)
            targetZom = null;
    }

    public void Switch()
    {
        if (peaColor == null || peaColor.Length == 0)
            return;

        peaIndex = (peaIndex + 1) % peaColor.Length;
        ApplyColor();
    }

    private void ApplyColor()
    {
        if (peaMart == null || peaColor == null || peaColor.Length == 0)
            return;

        peaMart.color = peaColor[peaIndex];
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireTime);

            if (bulletObj == null)
                continue;

            GameObject newBul = Instantiate(bulletObj, transform.position, transform.rotation);
            newBul.GetComponent<Bullet>().Init(peaIndex);
        }
    }
}