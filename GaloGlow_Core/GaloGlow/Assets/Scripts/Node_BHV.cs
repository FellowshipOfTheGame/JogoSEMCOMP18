using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node_BHV : MonoBehaviour {

    public GameObject God;
    public GameObject Spark;
    public GameObject[] ConnectionArt;
    public GameObject ConnectorArt;
    public Material DrawMaterial;
    public Material DestroyMaterial;

    public bool CanBeConnected = true;
    public bool CanBeDisconnected = true;
    public bool CanConnect = true;
    public bool CanDisconnect = true;
    public float ConnectionRadius = 2.0f;

    public List<GameObject> NextNodes; //Public just for inspector. Please do not use it in code.
    private List<int> ArrivingSparks;
    private bool DragingWire = false;
    private bool DragingDestroy = false;
    public int ExternalConnectionsCount = 0;

    private void RefreshArt(){

        if (NextNodes.Count > 0 || HasArrivingConnections() || DragingWire){

            ConnectorArt.GetComponent<MeshRenderer>().enabled = true;

        }
        else{

            ConnectorArt.GetComponent<MeshRenderer>().enabled = false;

        }

        for (int i = 0; i < ConnectionArt.Length; i++){

            if (i < NextNodes.Count){

                ConnectionArt[i].GetComponent<LineRenderer>().enabled = true;
                ConnectionArt[i].GetComponent<LineRenderer>().SetPosition(0, transform.position);
                ConnectionArt[i].GetComponent<LineRenderer>().SetPosition(1, NextNodes[i].transform.position);
            }
            else{

                ConnectionArt[i].GetComponent<LineRenderer>().enabled = false;

            }

        }

        if (DragingDestroy)
        {

            GetComponent<LineRenderer>().material = DestroyMaterial;

        }
        else
        {

            GetComponent<LineRenderer>().material = DrawMaterial;

        }

        if (DragingWire || DragingDestroy)
        {

            Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ClickRay, out Hit, 1000.0f))
            {

                GetComponent<LineRenderer>().SetPosition(0, transform.position);
                GetComponent<LineRenderer>().SetPosition(1, new Vector3(Hit.point.x, 0.05f, Hit.point.z));
                GetComponent<LineRenderer>().enabled = true;

            }

        }
        else
        {

            GetComponent<LineRenderer>().enabled = false;

        }

    }

    private void Rout()
    {

        int SparkType = God.GetComponent<God_BHV>().BlendSpark(ArrivingSparks);

        for (int i = 0; i < NextNodes.Count; i++)
        {

            GameObject NewSpark = (GameObject)Instantiate(Spark, transform.position + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity);
            NewSpark.GetComponent<Spark_BHV>().SetPath(gameObject, NextNodes[i]);
            NewSpark.GetComponent<Spark_BHV>().God = God;
            NewSpark.GetComponent<Spark_BHV>().SparkColor = SparkType;
            NewSpark.GetComponent<MeshRenderer>().material = God.GetComponent<God_BHV>().GetColorMaterial(SparkType);

        }

        ArrivingSparks.Clear();
    }

    private void MouseConnection()
    {

        //CreatePath
        if (Input.GetMouseButtonDown(0))
        {

            Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ClickRay, out Hit, 1000.0f) && Hit.collider.gameObject == gameObject && !DragingDestroy)
            {

                DragingWire = true;

            }
            else
            {

                DragingWire = false;

            }

        }

        if (Input.GetMouseButtonUp(0))
        {

            Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ClickRay, out Hit, 1000.0f) && DragingWire && Hit.collider.gameObject.tag == "Node")
            {


                if (Hit.collider.gameObject != gameObject &&
                    (Hit.collider.gameObject.transform.position - transform.position).magnitude <= ConnectionRadius &&
                    !IsConnectedTo(Hit.collider.gameObject) &&
                    !Hit.collider.gameObject.GetComponent<Node_BHV>().IsConnectedTo(gameObject) &&
                    CanConnect &&
                    Hit.collider.gameObject.GetComponent<Node_BHV>().CanBeConnected)
                {

                    NextNodes.Add(Hit.collider.gameObject);
                    God.GetComponent<God_BHV>().ResetCompletionScore();

                }


            }

            DragingWire = false;

        }

        //DestroyPath
        if (Input.GetMouseButtonDown(1))
        {

            Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ClickRay, out Hit, 1000.0f) && Hit.collider.gameObject == gameObject && !DragingWire)
            {

                DragingDestroy = true;

            }
            else
            {

                DragingDestroy = false;

            }

        }

        if (Input.GetMouseButtonUp(1))
        {

            Ray ClickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ClickRay, out Hit, 1000.0f) && DragingDestroy && Hit.collider.gameObject.tag == "Node")
            {

                if (Hit.collider.gameObject != gameObject && (Hit.collider.gameObject.transform.position - transform.position).magnitude <= ConnectionRadius)
                {


                    if (CanDisconnect && Hit.collider.gameObject.GetComponent<Node_BHV>().CanBeDisconnected)
                    {
                        NextNodes.Remove(Hit.collider.gameObject);
                        God.GetComponent<God_BHV>().ResetCompletionScore();
                    }
                    if (CanBeDisconnected && Hit.collider.gameObject.GetComponent<Node_BHV>().CanDisconnect)
                    {
                        Hit.collider.gameObject.GetComponent<Node_BHV>().NextNodes.Remove(gameObject);
                        God.GetComponent<God_BHV>().ResetCompletionScore();
                    }

                }

            }

            DragingDestroy = false;

        }

    }

    public void AddColor(int ColorIndex)
    {

        ArrivingSparks.Add(ColorIndex);

    }

    public void RemoveColor(int ColorIndex)
    {

        List<int> FilteredList = new List<int>();

        for (int i = 0; i < ArrivingSparks.Count; i++)
        {

            int FilteringColor = God.GetComponent<God_BHV>().FilterColor(ColorIndex, ArrivingSparks[i]);

            if (FilteringColor != -1)
            {

                FilteredList.Add(FilteringColor);

            }

        }

        ArrivingSparks = FilteredList;

    }

	public void RemoveEntry (int ColorIndex){

		ArrivingSparks.Remove (ColorIndex);

	}

    public int GetColor()
    {

        return God.GetComponent<God_BHV>().BlendSpark(ArrivingSparks);

    }

    public void ConnectTo(GameObject NodeToConnect)
    {

        NextNodes.Add(NodeToConnect);
        //God.GetComponent <God_BHV> ().ResetCompletionScore ();

    }

    public void DisconnectFrom(GameObject NodeToDisonnect)
    {

        print("Disconnected");
        NextNodes.Remove(NodeToDisonnect);
        //God.GetComponent <God_BHV> ().ResetCompletionScore ();

    }

    public bool IsConnectedTo(GameObject ConnectedNode)
    {

        return NextNodes.Contains(ConnectedNode);

    }

    public bool HasArrivingConnections()
    {

        return false;

    }

    public void DisconnectFromAll()
    {

        NextNodes.Clear();

    }
    // Start
    void Start()
    {

        //NextNodes = new List <GameObject> ();
        ArrivingSparks = new List<int>();

    }

    // Update is called once per frame
    void Update()
    {

        RefreshArt();
        MouseConnection();
        //OnBeat
        if (God.GetComponent<God_BHV>().GetSemipathTrigger(1) && ArrivingSparks.Count != 0)
        {

            Rout();

        }

    }

}

