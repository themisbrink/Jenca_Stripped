
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Networking;
// using DarkTonic.MasterAudio;
using Newtonsoft.Json;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string _uri;

    List<Block> blockList;
    List<Block> sixthGrade;
    List<Block> seventhGrade;
    List<Block> eighthGrade;

    public Transform sixthParent;
    public Transform seventhParent;
    public Transform eighthParent;

    public GameObject secondCamera;

    public List<GameObject> stackFoc = new List<GameObject>();
    public List<GameObject> stacks = new List<GameObject>();
    public List<Rigidbody> blockRigids = new List<Rigidbody>();
    
    public bool rotating = false;
    public bool canRotate = true;
    public GameObject testButton;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        // FocusonStack(0);
        StartCoroutine(GetJson());
    }


    IEnumerator GetJson()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_uri))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var returnText = request.downloadHandler.text;
                Debug.Log(returnText);
                blockList = JsonConvert.DeserializeObject<List<Block>>(returnText);

                secondCamera.SetActive(true);
                yield return new WaitForSeconds(1.0f);
                // Handle List
                HandleList(blockList);

            }
        }
    }

    private void HandleList(List<Block> blockList)
    {
        Debug.Log(blockList.Count);
        sixthGrade = blockList.Where(x => x.grade == "6th Grade").ToList();
        seventhGrade = blockList.Where(x => x.grade == "7th Grade").ToList();
        eighthGrade = blockList.Where(x => x.grade == "8th Grade").ToList();



        StartCoroutine(CreateTower(sixthParent, sixthGrade, 0));
        StartCoroutine(CreateTower(seventhParent, seventhGrade, 1.2f));
        StartCoroutine(CreateTower(eighthParent, eighthGrade, 2.4f));

        canRotate = false;
    }

    private IEnumerator CreateTower(Transform parent, List<Block> blocks, float delay)
    {
        // Debug.Log(blocks.Count);
        blockRigids.Clear();

        float startHeight = 0;
        // Add Blocks
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < blocks.Count; i++)
        {
            // mastery 0, 1, 2 -> Glass, Wood, Stone
            GameObject blockObject = Instantiate(Resources.Load("Blocks/" + blocks[i].mastery, typeof(GameObject))) as GameObject;
            blockObject.SetActive(false);
            blockObject.transform.parent = parent;
            blockObject.transform.localScale = Vector3.one;

            blockRigids.Add(blockObject.GetComponent<Rigidbody>());
            Block block = blockObject.GetComponent<Block>();
            block.UpdateData(blocks[i]);
            // Add position & rotation
            if (i % 6 == 0)
            {
                blockObject.transform.localPosition = new Vector3(0, startHeight, 0);
                blockObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (i % 6 == 1)
            {
                blockObject.transform.localPosition = new Vector3(0, startHeight, -2);
                blockObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (i % 6 == 2)
            {
                blockObject.transform.localPosition = new Vector3(0, startHeight, 2);
                blockObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (i % 6 == 3)
            {
                blockObject.transform.localPosition = new Vector3(0, startHeight, 0);
                blockObject.transform.localEulerAngles = new Vector3(0, 90, 0);
            }
            if (i % 6 == 4)
            {
                blockObject.transform.localPosition = new Vector3(-2, startHeight, 0);
                blockObject.transform.localEulerAngles = new Vector3(0, 90, 0);
            }
            if (i % 6 == 5)
            {
                blockObject.transform.localPosition = new Vector3(2, startHeight, 0);
                blockObject.transform.localEulerAngles = new Vector3(0, 90, 0);
            }

            if (i % 3 == 2)
            {
                startHeight += 1.2f;
            }
            blockObject.SetActive(true);
            blockObject.transform.DOMoveY(blockObject.transform.position.y + startHeight + 10, 0.2f - (i / 100)).SetEase(Ease.Linear).From();
            yield return new WaitForSeconds(0.1f - (i / 100));
        }

        

        yield return new WaitForSeconds(1.5f);
        FocusonStack(0);
        
        yield return new WaitForSeconds(0.5f);
        
        // Close Phisics
        for (int i = 0; i < blockRigids.Count; i++)
        {
            if( blockRigids[i]!=null)
                blockRigids[i].isKinematic = true;
        }
        yield return new WaitForSeconds(0.5f);
        testButton.SetActive(true);
        canRotate = true;
    }

    [Button]
    public void ResetTower(int selection)
    {
        testButton.SetActive(false);
        switch (selection)
        {
            case 0:
                sixthGrade = sixthGrade.OrderBy(x => x.domain).ToList();
                seventhGrade = seventhGrade.OrderBy(x => x.domain).ToList();
                eighthGrade = eighthGrade.OrderBy(x => x.domain).ToList();
                break;

            case 1:
                sixthGrade = sixthGrade.OrderBy(x => x.cluster).ToList();
                seventhGrade = seventhGrade.OrderBy(x => x.cluster).ToList();
                eighthGrade = eighthGrade.OrderBy(x => x.cluster).ToList();
                break;
            case 2:
                sixthGrade = sixthGrade.OrderBy(x => x.standardid).ToList();
                seventhGrade = seventhGrade.OrderBy(x => x.standardid).ToList();
                eighthGrade = eighthGrade.OrderBy(x => x.standardid).ToList();
                break;
        }

        // Destroy all towers
        foreach (Transform child in sixthParent.transform) GameObject.Destroy(child.gameObject);
        foreach (Transform child in seventhParent.transform) GameObject.Destroy(child.gameObject);
        foreach (Transform child in eighthParent.transform) GameObject.Destroy(child.gameObject);

        // Create new Towers
        StartCoroutine(CreateTower(sixthParent, sixthGrade, 0));
        StartCoroutine(CreateTower(seventhParent, seventhGrade, 0f));
        StartCoroutine(CreateTower(eighthParent, eighthGrade, 0f));

        canRotate = false;
    }

    public void FocusonStack(int index)
    {
        if (rotating) return;
        for (int i = 0; i < stackFoc.Count; i++)
        {
            stackFoc[i].SetActive(false);
            stacks[i].transform.Find("parent").GetComponent<RotateObject>().focused = false;
        }

        stackFoc[index].SetActive(true);
        // focusedStack = stacks[index];
        stacks[index].transform.Find("parent").GetComponent<RotateObject>().focused = true;
    }

    public void TestStack() {
       
       if(canRotate)
        canRotate = false;
        else return;
        // Destroy Glass
        for (int i = 0; i < blockRigids.Count; i++)
        {
            if(blockRigids[i]== null) continue;
            if(blockRigids[i].gameObject.GetComponent<Block>().mastery == 0)
                Destroy(blockRigids[i].gameObject);
        }
        
        
        // Close Phisics
        for (int i = 0; i < blockRigids.Count; i++)
        {
            if( blockRigids[i]!=null)
            blockRigids[i].isKinematic = false;
        }
    }
}





