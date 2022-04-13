using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadScript : MonoBehaviour
{
  Material mMaterial;
  MeshRenderer mMeshRenderer;

    float[] mPoints;
    int mHitCount;
    public LayerMask heatMapLayer;
  
    public float mDelay;
    private float mTimer;

  void Start()
  {
        mTimer = mDelay;

    mMeshRenderer = GetComponent<MeshRenderer>();
    mMaterial = mMeshRenderer.material;

    mPoints = new float[256 * 3]; //256 point 

  }

  void Update()
  {    
    // //consistantly clone new projectiles
    // mDelay -= Time.deltaTime;
    // if (mDelay <=0)
    // {
    //   GameObject go = Instantiate(Resources.Load<GameObject>("breadcrumb"));
    //   go.transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 2f), Random.Range(-1f, -1f));

    //   mDelay = 0.5f;
    // }
  }

  private void OnCollisionStay(Collision collision)
  {
        if (mTimer > 0)
        {
            mTimer -= Time.deltaTime;
            return;
        }

        foreach(ContactPoint cp in collision.contacts)
        {
            // Debug.Log("Contact with object " + cp.otherCollider.gameObject.name);

            Vector3 StartOfRay = cp.point - cp.normal;
            Vector3 RayDir = cp.normal;

            Ray ray = new Ray(StartOfRay, RayDir);
            RaycastHit hit;

            bool hitit = Physics.Raycast(ray, out hit, 10f, heatMapLayer);

            if (hitit)
            {
                // Debug.Log("Hit Object " + hit.collider.gameObject.name);
                // Debug.Log("Hit Texture coordinates = " + hit.textureCoord.x + "," + hit.textureCoord.y);
                addHitPoint(hit.textureCoord.x*4-2, hit.textureCoord.y*4-2);
            }
            // Destroy(cp.otherCollider.gameObject);
        }

        mTimer = mDelay;

    }

  public void addHitPoint(float xp,float yp)
  {
    mPoints[mHitCount * 3] = xp;
    mPoints[mHitCount * 3 + 1] = yp;
    mPoints[mHitCount * 3 + 2] = Random.Range(1f, 3f);

    mHitCount++;
    mHitCount %= 256;

    mMaterial.SetFloatArray("_Hits", mPoints);
    mMaterial.SetInt("_HitCount", mHitCount);

  }

}
