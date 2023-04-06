using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class spawnOnPlanes : MonoBehaviour
{
  // create a field that allows us to assign a game object for placement
  [SerializeField]
  GameObject PlacedPrefab;

  // keep track of our spawned object
  GameObject spawnedObject;

  // a list of true values returned from a raycast hit
  static List<ARRaycastHit> hits = new List<ARRaycastHit>();

  // our raycast manager that contains information about hit objects
  ARRaycastManager m_rayCastManager;

    void Awake()
    {
        // assign our component from raycast manager
        m_rayCastManager = GetComponent<ARRaycastManager>();
    }

      // 
    bool GetTouch(out Vector2 touch_pos)
    {
        // if there is no touch just return with default
        // if there is a touch return with that position in the scene
        if(Input.touchCount > 0)
        {
          touch_pos = Input.GetTouch(0).position;
        return true;
        }
      touch_pos = default;
      return false;
    }
      // Update is called once per frame
    void Update()
    {
        // break out early if no touch
          if(GetTouch(out Vector2 touch_pos) == false) 
        {
          return;
        }

        // when there is a touch event we access the raycast game object and pass through the touch position, our hits list, and limit to trackables of type planes
          if(m_rayCastManager.Raycast(touch_pos, hits, TrackableType.Planes))
        {
              // pose is postion and rotation
              // our first index is the closest return of true from a hit
              var hitPose = hits[0].pose;
        
              // if there is no spawned object then we create a new one of our assigned gameObject at the location and rotation from our hit
              // otherwise we transform the position of our spawned object to a new position
              if (spawnedObject == null)
            {
                spawnedObject = Instantiate(PlacedPrefab, hitPose.position, hitPose.rotation);
            } else
            {
               spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}
