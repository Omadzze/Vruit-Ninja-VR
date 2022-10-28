using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.SceneManagement;

public class ObjectSlicer : MonoBehaviour
{
    // start and end of our katana
    public Transform startSlicingPoint;
    public Transform endSlicingPoint;
    
    // layers that katana can slice
    public LayerMask sliceableLayer;

    public VelocityEstimator velocityEstimator;
    // after object will sliced it will change to another material inside
    public Material slicedMaterial;

    public float slicedObjectInitialVelocity = 100;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // middle of our katana
        Vector3 slicingDirection = endSlicingPoint.position - startSlicingPoint.position;
        // checking if katana was hit this gameobject
        bool hasHit = Physics.Raycast(startSlicingPoint.position, slicingDirection, out hit, slicingDirection.magnitude, sliceableLayer);
        if (hasHit)
        {
            // if we sliced bomb then restart game
            if (hit.transform.gameObject.layer == 7)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Slice(hit.transform.gameObject, hit.point, velocityEstimator.GetVelocityEstimate());   
            }
        }
    }

    void Slice(GameObject target, Vector3 planePosition, Vector3 slicerVelocity)
    {
        audioSource.Play();
        Debug.Log("WE SLICE THE OBJECT");
        Vector3 slicingDirection = endSlicingPoint.position - startSlicingPoint.position;
        Vector3 planeNormal = Vector3.Cross(slicerVelocity, slicingDirection);
        SlicedHull hull = target.Slice(planePosition, planeNormal, slicedMaterial);
        if (hull != null)
        {
            DisplayScore.score++;
            
            // creating material how upper and lower part of our sliced gameobject will look like
            GameObject upperHull = hull.CreateUpperHull(target, slicedMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, slicedMaterial);
            
            CreateSlicedComponent(upperHull);
            CreateSlicedComponent(lowerHull);
            
            Destroy(target);
        }
    }

    void CreateSlicedComponent(GameObject slicedHull)
    {
        Rigidbody rb = slicedHull.AddComponent<Rigidbody>();
        MeshCollider collider = slicedHull.AddComponent<MeshCollider>();
        collider.convex = true;
        
        rb.AddExplosionForce(slicedObjectInitialVelocity, slicedHull.transform.position, 1);
        
        Destroy(slicedHull, 4);
    }
}
