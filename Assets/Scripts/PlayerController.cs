using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public TextMeshProUGUI litterText;

    int litterCounter;
    LayerMask litterLayer;
    // Start is called before the first frame update
    void Start()
    {
        litterLayer = LayerMask.GetMask("Litter");
        litterCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForLitterCollision();
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
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, litterLayer);

        foreach (Collider hit in hits)
        {
            if(hit.GetComponent<Litter>() != null)
            {
                Destroy(hit.gameObject);
                litterCounter++;

                litterText.text = litterCounter.ToString(); 
            }
        }
    }
}
