using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChanger : MonoBehaviour
{
    public GameObject newObjectPrefab;  // Prefab to replace the current object
    private bool hasCollidedOnce = false;  // Tracks if the object has already collided once

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has already collided once
        if (!hasCollidedOnce)
        {
            // First collision: Change the object
            ChangeObject();
            hasCollidedOnce = true;
        }
        else
        {
            // Second collision: Remove the object
            Destroy(gameObject);
        }
    }

    private void ChangeObject()
    {
        // Instantiate the new object at the current object's position and rotation
        GameObject newObject = Instantiate(newObjectPrefab, transform.position, transform.rotation);

        // Optionally, copy over the scale of the current object
        newObject.transform.localScale = transform.localScale;

        // Destroy the current object
        Destroy(gameObject);
    }
}
