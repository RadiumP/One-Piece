using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Bu : MonoBehaviour {

    public GameObject UnderwaterMeshOBJ;
    private Mesh BoatMesh;
    private Vector3[] originalVerticesArray;
    private int[] originalTrianglesArray;
    private Mesh UnderWaterMesh;
    private List<Vector3> underwaterVertices;
    private List<int> underwaterTriangles;
    Rigidbody boatRB;
    private Wavecontrol waveScript;

    public class Distance: IComparable<Distance>
    {
        public float distance;
        public string name;
        public Vector3 verticePos;

        public int CompareTo(Distance other)
        {
            return this.distance.CompareTo(other.distance);
        }
    }


	// Use this for initialization
	void Start () {
        //Get the waveControllerScript
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");

        waveScript = gameController.GetComponent<Wavecontrol>();

        UnderWaterMesh = UnderwaterMeshOBJ.GetComponent<MeshFilter>().mesh;
        BoatMesh = GetComponent<MeshFilter>().mesh;
        originalVerticesArray = BoatMesh.vertices;
        originalTrianglesArray = BoatMesh.triangles;
        boatRB = GetComponent<Rigidbody>();
        boatRB.maxAngularVelocity = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        GenerateUnderwaterMesh();
	}


    void GenerateUnderwaterMesh()
    {
        underwaterVertices = new List<Vector3>();
        underwaterTriangles = new List<int>();

        int i = 0;
        while (i < originalTrianglesArray.Length)
        {
            Vector3 vertice_1_pos = originalVerticesArray[originalTrianglesArray[i]];
            float? distance1 = DistanceToWater(vertice_1_pos);
            i++;

            Vector3 vertice_2_pos = originalVerticesArray[originalTrianglesArray[i]];
            float? distance2 = DistanceToWater(vertice_2_pos);
            i++;

            Vector3 vertice_3_pos = originalVerticesArray[originalTrianglesArray[i]];
            float? distance3 = DistanceToWater(vertice_3_pos);
            i++;

            if (distance1 > 0f && distance2 > 0f && distance3 == null)
            {
                continue;
            }

            if (distance1 == null || distance2 == null || distance3 == null)
            {
                continue;
            }



            Distance distance1OBJ = new Distance();
            Distance distance2OBJ = new Distance();
            Distance distance3OBJ = new Distance();

            distance1OBJ.distance = (float)distance1;
            distance1OBJ.name = "one";
            distance1OBJ.verticePos = vertice_1_pos;

            distance2OBJ.distance = (float)distance2;
            distance2OBJ.name = "two";
            distance2OBJ.verticePos = vertice_2_pos;

            distance3OBJ.distance = (float)distance3;
            distance3OBJ.name = "three";
            distance3OBJ.verticePos = vertice_3_pos;

            List<Distance> allDistancesList = new List<Distance>();
            allDistancesList.Add(distance1OBJ);
            allDistancesList.Add(distance2OBJ);
            allDistancesList.Add(distance3OBJ);

            allDistancesList.Sort();
            allDistancesList.Reverse();

            if (allDistancesList[0].distance < 0f && allDistancesList[1].distance < 0f && allDistancesList[2].distance < 0f)
            {
                AddCoordinateToMesh(distance1OBJ.verticePos);
                AddCoordinateToMesh(distance2OBJ.verticePos);
                AddCoordinateToMesh(distance3OBJ.verticePos);
            }

            //One vertice is above the water, the rest is below
            else if (allDistancesList[0].distance > 0f && allDistancesList[1].distance < 0f && allDistancesList[2].distance < 0f)
            {
                //H is always at position 0
                Vector3 H = allDistancesList[0].verticePos;

                //Left of H is M
                //Right of H is L

                //Find the name of M
                string M_name = "temp";
                if (allDistancesList[0].name == "one")
                {
                    M_name = "three";
                }
                else if (allDistancesList[0].name == "two")
                {
                    M_name = "one";
                }
                else {
                    M_name = "two";
                }

                //We also need the heights to water
                float h_H = allDistancesList[0].distance;
                float h_M = 0f;
                float h_L = 0f;

                Vector3 M = Vector3.zero;
                Vector3 L = Vector3.zero;

                //This means M is at position 1 in the List
                if (allDistancesList[1].name == M_name)
                {
                    M = allDistancesList[1].verticePos;
                    L = allDistancesList[2].verticePos;

                    h_M = allDistancesList[1].distance;
                    h_L = allDistancesList[2].distance;
                }
                else {
                    M = allDistancesList[2].verticePos;
                    L = allDistancesList[1].verticePos;

                    h_M = allDistancesList[2].distance;
                    h_L = allDistancesList[1].distance;
                }


                //Now we can calculate where we should cut the triangle to form 2 new triangles
                //because the resulting area will always form a square

                //Point I_M
                Vector3 MH = H - M;

                float t_M = -h_M / (h_H - h_M);

                Vector3 MI_M = t_M * MH;

                Vector3 I_M = MI_M + M;


                //Point I_L
                Vector3 LH = H - L;

                float t_L = -h_L / (h_H - h_L);

                Vector3 LI_L = t_L * LH;

                Vector3 I_L = LI_L + L;


                //Build the 2 new triangles
                AddCoordinateToMesh(M);
                AddCoordinateToMesh(I_M);
                AddCoordinateToMesh(I_L);

                AddCoordinateToMesh(M);
                AddCoordinateToMesh(I_L);
                AddCoordinateToMesh(L);
            }
            //Two vertices are above the water, the other is below
            else if (allDistancesList[0].distance > 0f && allDistancesList[1].distance > 0f && allDistancesList[2].distance < 0f)
            {
                //H and M are above the water
                //H is after the vertice that's below water, which is L
                //So we know which one is L because it is last in the sorted list
                Vector3 L = allDistancesList[2].verticePos;

                //Find the name of H
                string H_name = "temp";
                if (allDistancesList[2].name == "one")
                {
                    H_name = "two";
                }
                else if (allDistancesList[2].name == "two")
                {
                    H_name = "three";
                }
                else {
                    H_name = "one";
                }


                //We also need the heights to water
                float h_L = allDistancesList[2].distance;
                float h_H = 0f;
                float h_M = 0f;

                Vector3 H = Vector3.zero;
                Vector3 M = Vector3.zero;

                //This means that H is at position 1 in the list
                if (allDistancesList[1].name == H_name)
                {
                    H = allDistancesList[1].verticePos;
                    M = allDistancesList[0].verticePos;

                    h_H = allDistancesList[1].distance;
                    h_M = allDistancesList[0].distance;
                }
                else {
                    H = allDistancesList[0].verticePos;
                    M = allDistancesList[1].verticePos;

                    h_H = allDistancesList[0].distance;
                    h_M = allDistancesList[1].distance;
                }


                //Now we can find where to cut the triangle

                //Point J_M
                Vector3 LM = M - L;

                float t_M = -h_L / (h_M - h_L);

                Vector3 LJ_M = t_M * LM;

                Vector3 J_M = LJ_M + L;


                //Point J_H
                Vector3 LH = H - L;

                float t_H = -h_L / (h_H - h_L);

                Vector3 LJ_H = t_H * LH;

                Vector3 J_H = LJ_H + L;


                //Create the triangle
                AddCoordinateToMesh(L);
                AddCoordinateToMesh(J_H);
                AddCoordinateToMesh(J_M);
            }

        }
        //Gen underwater mesh
        UnderWaterMesh.Clear();
        UnderWaterMesh.name = "Underwater Mesh";
        UnderWaterMesh.vertices = underwaterVertices.ToArray();
        UnderWaterMesh.triangles = underwaterTriangles.ToArray();

        UnderWaterMesh.RecalculateBounds();
        UnderWaterMesh.RecalculateNormals();
        
    }


    float? DistanceToWater(Vector3 position)
    {
        // coord to global space
        Vector3 globalVerticePosition = transform.TransformPoint(position);
        float? y_pos = 0f;
        y_pos += waveScript.GetWaveYPos(globalVerticePosition.x, globalVerticePosition.z);
        return globalVerticePosition.y - y_pos;
    }

    void AddCoordinateToMesh(Vector3 coord)
    {
        underwaterVertices.Add(coord);
        underwaterTriangles.Add(underwaterVertices.Count - 1);

    }

    void FixedUpdate()
    {
        if(underwaterTriangles.Count > 0)
        {
            AddForcesToBoat();
        }
    }

    void AddForcesToBoat()
    {
        int i = 0;
        while(i < underwaterTriangles.Count)
        {
            Vector3 vertice_1_pos = underwaterVertices[underwaterTriangles[i]];
            i++;
            Vector3 vertice_2_pos = underwaterVertices[underwaterTriangles[i]];
            i++;
            Vector3 vertice_3_pos = underwaterVertices[underwaterTriangles[i]];
            i++;
            Vector3 centerPoint = (vertice_1_pos + vertice_2_pos + vertice_3_pos) / 3f;

            float distance_to_surface = Mathf.Abs((float)DistanceToWater(centerPoint));

            centerPoint = transform.TransformPoint(centerPoint);
            vertice_1_pos = transform.TransformPoint(vertice_1_pos);
            vertice_2_pos = transform.TransformPoint(vertice_2_pos);
            vertice_3_pos = transform.TransformPoint(vertice_3_pos);

            Vector3 crossProduct = Vector3.Cross(vertice_2_pos - vertice_1_pos, vertice_3_pos - vertice_1_pos).normalized;

            Debug.DrawRay(centerPoint, crossProduct * 3f);

            float a = Vector3.Distance(vertice_1_pos, vertice_2_pos);
            float c = Vector3.Distance(vertice_3_pos, vertice_1_pos);

            float area_sin = (a * c * Mathf.Sin(Vector3.Angle(vertice_2_pos - vertice_1_pos, vertice_3_pos - vertice_1_pos) * Mathf.Deg2Rad)) / 2f;
            float area = area_sin;

            AddBuoyancy(distance_to_surface, area, crossProduct, centerPoint);




        }

       
    }

    private void AddBuoyancy(float distance_to_surface, float area, Vector3 crossProduct, Vector3 centerPoint)
    {
        float rho_water = 1000f;
        Vector3 F = rho_water * Physics.gravity.y * distance_to_surface * area * crossProduct;

        F = new Vector3(0f, F.y, 0f);
        boatRB.AddForceAtPosition(F, centerPoint);
    }
}
