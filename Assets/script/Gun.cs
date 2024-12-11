using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 50f;
    public float range = 100f;

    public Camera fpsCam;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyController target = hit.transform.GetComponent<EnemyController>();
            if (target != null)
            {
                target.TakeDamage(damage);
                PlayerStats.points = PlayerStats.points + 50;
                Debug.Log(PlayerStats.points);
                
                
            }
        }
    }
}
