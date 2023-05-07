using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    private SceneType sceneType;
    private PartController partController;
    private List<GameObject> childs;
    private GameObject tempChild;
    private Rigidbody tempChildRigidbody;
    private Transform center;
    private int obstacleCount;

    void Start()
    {
        obstacleCount = 0;
        childs = new List<GameObject>();
    }

    public void Init(SceneType sceneType, Transform obstacleTransform)
    {
        this.sceneType = sceneType;
        obstacleCount++;
        transform.position=obstacleTransform.position+new Vector3(0,-0.475F,0);
        transform.rotation = obstacleTransform.rotation;
        transform.eulerAngles += new Vector3(0, 8, 0);
        if (PlayerPrefs.GetInt("level") < 10)
        {

            if (Random.Range(0,10)==0)
            {
                transform.eulerAngles += Vector3.up * 90;
            }
        }
        else
        {

            if (Random.Range(0, 5) == 0)
            {
                transform.eulerAngles += Vector3.up * 90;
            }
        }
        if (transform.Find("Cylinder"))
        {
            transform.Find("Cylinder").GetComponent<PartController>().Init(PartType.Finish, sceneType);

        }
        else
        {
            int unsafeMaterialCount = Random.Range(0, sceneType.obstacleType.transform.childCount);
            for (int i = 0; i < sceneType.obstacleType.transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<PartController>().Init(PartType.Safe, sceneType);
            }
            for (int i = 0; i < unsafeMaterialCount; i++)
            {
                transform.GetChild(i).GetComponent<PartController>().Init(PartType.Unsafe, sceneType);
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.SetParent(FindObjectOfType<RotateManager>().transform);
            }
        }
    }

    public void DestroyParts()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i).gameObject);
        }
        if (childs[0].name != "Cylinder")
        {
            foreach (var item in childs)
            {
                item.transform.GetComponent<MeshCollider>().enabled = false;
                item.transform.SetParent(null);
                tempChildRigidbody = item.GetComponent<Rigidbody>();
                tempChildRigidbody.isKinematic = false;
                center = item.transform.Find("Center");
                tempChildRigidbody.AddForce((-transform.position + center.position).normalized * 1000);
            }
            StartCoroutine(DestroyEnumeratorParts());
        }
    }

    IEnumerator DestroyEnumeratorParts()
    {

        yield return new WaitForSecondsRealtime(1F);
        Destroy(gameObject);
        int destroyCount = childs.Count;
        for (int i = 0; i < destroyCount; i++)
        {
            Destroy(childs[i]);
        }
    }


}
