using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving: PlayerCtrl
{
    public float playerSpeed;
    public bool endGame = false;

    [SerializeField] private Transform modelTransform;
    [SerializeField] private LayerMask layerBrick;
    [SerializeField] private LayerMask layerGround;
    [SerializeField] private LayerMask layerLine;
    [SerializeField] private LayerMask layerWinpos;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private GameObject brickSpawnPrefab;
    [SerializeField] private Transform brickContainer;

    private List<GameObject> collectBrick;
    private Vector3 mouseBeginPos;
    private Vector3 mouseEndPos;
    private Vector3 roadCtrl;
    private int numberofbrick = 0;

    private bool isGetInputDone = false;

    private void Awake()
    {
        collectBrick = new List<GameObject>();
    }

    private void Start()
    {
        this.playerSpeed = 5f;
    }
    private void Update()
    {
        getInput();
        Move();
    }


    private void getInput()
    {
        if (isGetInputDone) return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseBeginPos = Input.mousePosition;
        }


        if (Input.GetMouseButton(0))
        {
            roadCtrl = Input.mousePosition - mouseBeginPos;
            if (roadCtrl.magnitude > 30)
            {
                isGetInputDone = true;
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseEndPos = Input.mousePosition;
        }
    }
    private void Move()
    {
        if (!isGetInputDone) return;

        if (Vector3.Angle(Vector3.up, roadCtrl) <= 45f && CheckOnBrick() && CheckNextRoad(Vector3.forward))
        {            
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + Vector3.forward, playerSpeed);
            return;
        }

        else if (Vector3.Angle(Vector3.down, roadCtrl) <= 45f && CheckOnBrick() && CheckNextRoad(Vector3.back))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position - Vector3.forward, playerSpeed);
            return;
        }

        else if (Vector3.Angle(Vector3.right, roadCtrl) <= 45f && CheckOnBrick() && CheckNextRoad(Vector3.right))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + Vector3.right, playerSpeed);
            return;
        }

        else if (Vector3.Angle(Vector3.left, roadCtrl) <= 45f && CheckOnBrick() && CheckNextRoad(Vector3.left))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position - Vector3.right, playerSpeed);
            return;
        }
        isGetInputDone = false;
    }



    private bool CheckOnBrick()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, layerTarget))
        {
            endGame = true;
            return true;
        }        

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f, layerBrick))
        {
            Brick br = hit.collider.GetComponent<Brick>();            
            if (br.isActiveMesh)
            {
                numberofbrick += 1;
                br.DeActiveMesh();
                GameObject newbr = Instantiate(brickSpawnPrefab);
                collectBrick.Add(newbr);
                newbr.transform.SetParent(brickContainer);
                newbr.transform.position = new Vector3(brickContainer.position.x, brickContainer.position.y + 0.3f * numberofbrick, brickContainer.position.z);
                modelTransform.position += Vector3.up * 0.3f;
            }
            return true;
        }
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.3f, layerLine))
        {
            LineBrick br = hit.collider.GetComponent<LineBrick>();
            if (!br.isActiveMesh)
            {
                Destroy(collectBrick[collectBrick.Count - 1]);
                collectBrick.RemoveAt(collectBrick.Count - 1);
                br.SetActiveMesh(true);                
                modelTransform.position += Vector3.down * 0.3f;
            }
            return true;
        }
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, layerWinpos))
        {

            while(collectBrick.Count>0)
            {
                Destroy(collectBrick[0]);
                collectBrick.RemoveAt(0);
            }            
            modelTransform.position = new Vector3(modelTransform.position.x, 1.3f, modelTransform.position.z);

            return true;
        }
        return false;
    }
    
    private bool CheckNextRoad(Vector3 direct)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.down * 1.1f, direct , out hit, 1.2f, layerBrick))
        {
            return true;
        }
        if (Physics.Raycast(transform.position + Vector3.down * 1.1f, direct, out hit, 1.2f, layerLine))
        {
            return true;
        }

        if (Physics.Raycast(transform.position + Vector3.down * 3.8f, direct, out hit, 1.2f, layerWinpos))
        {
            return true;
        }
        if (Physics.Raycast(transform.position + Vector3.down * 3.8f, direct, out hit, 1.2f, layerTarget))
        {
            return true;
        }


        return false;
    }
}