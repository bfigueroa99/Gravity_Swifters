using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public float daño = 1f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
            if (bossObject != null)
            {
                Boss boss = bossObject.GetComponent<Boss>();
                if (boss != null)
                {
                   
                    boss.RecibirDaño(daño);
                }
                else
                {
                    Debug.LogError("El objeto Boss no tiene el script Boss adjunto.");
                }
            }
            else
            {
                Debug.LogError("No se encontró ningún objeto con el tag 'Boss'.");
            }

            Destroy(gameObject);
            Debug.Log("Agarraste el objeto");
        }
    }
}