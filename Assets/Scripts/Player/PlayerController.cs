using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Transform playerSpawn;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int spawnDelay;

    [SerializeField]
    public TextMeshProUGUI litterCounterText;
    [SerializeField]
    public TextMeshProUGUI spawnCounterText;

    [SerializeField]
    public int maxLitter;

    int litterCount;
    float spawnCooldown;

    LayerMask litterLayer;
    LayerMask binLayer;
    LayerMask obstacleLayer;


    // Start is called before the first frame update
    void Start()
    {
        litterLayer = LayerMask.GetMask("Litter");
        binLayer = LayerMask.GetMask("Bin");
        obstacleLayer = LayerMask.GetMask("Obstacle");
        litterCount = 0;

        if(maxLitter == 0)
        {
            Debug.Log("Max Litter needs a value");
        }

        if(litterCounterText != null)
        {
            litterCounterText.text = "Litter: " + litterCount;
        }
        else
        {
            Debug.Log("No Litter Text");
        }

        if(spawnCounterText != null)
        {
            spawnCounterText.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No Spawn Counter Text");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnCooldown > 0)
        {
            if (spawnCounterText != null)
            {
                spawnCounterText.gameObject.SetActive(true);
                spawnCounterText.text = spawnCooldown.ToString("F2");
            }

            spawnCooldown -= Time.deltaTime;
            return;
        }

        if(spawnCounterText != null && spawnCounterText.IsActive() == true)
        {
            spawnCounterText.gameObject.SetActive(false);
        }

        Move();
        CheckForLitterCollision();
        CheckForBinsCollision();
        CheckForObstacleCollision();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        move.Normalize();

        transform.position += move * speed * Time.deltaTime;
    }

    void CheckForLitterCollision()
    {
        if (litterCount >= maxLitter)
        {
            return;
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, litterLayer);

        foreach (Collider hit in hits)
        {
            if(hit.GetComponent<Litter>() != null)
            {
                Destroy(hit.gameObject);
                litterCount++;

                if (litterCounterText != null)
                {
                    litterCounterText.text = "Litter: " + litterCount;
                }
            }
        }
    }
    void CheckForBinsCollision()
    {
        if (litterCount <= 0)
        {
            return;
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, binLayer);

        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<Bins>() != null)
            {
                hit.GetComponent<Bins>().BinLitter(litterCount);
                litterCount = 0;

                if (litterCounterText != null)
                {
                    litterCounterText.text = "Litter: " + litterCount;
                }
            }
        }
    }
    void CheckForObstacleCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, obstacleLayer);

        foreach (Collider hit in hits)
        {
            if (hit.GetComponentInParent<Customer>() != null)
            {
                litterCount = 0;

                if (litterCounterText != null)
                {
                    litterCounterText.text = "Litter: " + litterCount;
                }

                spawnCooldown = spawnDelay;
                transform.position = playerSpawn.position;
            }
        }
    }
}
