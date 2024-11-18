using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChanger : MonoBehaviour
{
    
    public float knockbackForce = 10f;


   

    private void OnCollisionEnter(Collision collision)
    {
        ApplyKnockback(collision);

        Destroy(gameObject);
    }

    private void ChangeObject()
    {
    
      

      
      

      
        Destroy(gameObject);
    }

    private void ApplyKnockback(Collision collision)
    {
        
        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            
            Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;

          
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            
        }
      
    }
}
