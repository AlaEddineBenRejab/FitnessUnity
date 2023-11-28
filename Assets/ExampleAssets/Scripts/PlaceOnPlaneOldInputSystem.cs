using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceAndAnimate : MonoBehaviour
{
    [SerializeField] GameObject placedPrefab; // Prefab to be instantiated
    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    GameObject spawnedObject;

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && spawnedObject == null)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // Instantiate object at the center of the detected plane
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                spawnedObject.transform.Rotate(Vector3.up, 180.0f);


            }
        }
    }
}
