using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testinstancing : MonoBehaviour
{

    // Kuubit menu taustalla. BackgroundCubes objektilla on tämä scripti. Materials/MenuCubeShader on tämän scription toinen osa.


    // !!! Voit vaihtaa miten monta kuubia on olemassa BackgroundCubes objektissa !!!
    public int instanceCount = 1;

    Vector4[] colors;
    int[] cubeColors;

    public Mesh instanceMesh;
    public Material instanceMaterial;
    public int subMeshIndex = 0;

    private int cachedInstanceCount = -1;
    private int cachedSubMeshIndex = -1;
    private ComputeBuffer positionBuffer;
    private ComputeBuffer argsBuffer;
    private ComputeBuffer colorBuffer;
    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    void Start() {
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        UpdateBuffers();
    }

    void Update() {
        // Update starting position buffer
        if (cachedInstanceCount != instanceCount || cachedSubMeshIndex != subMeshIndex)
            UpdateBuffers();

        updateColor();

        int cubeRefresher = Random.Range(0,5000);
        if(cubeRefresher == 0) {
            instanceCount++;
        }

        // Render
        Graphics.DrawMeshInstancedIndirect(instanceMesh, subMeshIndex, instanceMaterial, new Bounds(new Vector3(0f, 0f, 0),new Vector3(1000.0f, 1000.0f, 1000.0f)), argsBuffer);
    }

    void UpdateBuffers() {
        // Ensure submesh index is in range
        if (instanceMesh != null)
            subMeshIndex = Mathf.Clamp(subMeshIndex, 0, instanceMesh.subMeshCount - 1);

        // Positions
        if (positionBuffer != null)
            positionBuffer.Release();

        if (colorBuffer != null)
            colorBuffer.Release();

        positionBuffer = new ComputeBuffer(instanceCount, 16);
        colorBuffer = new ComputeBuffer(instanceCount, 16);

        Vector4[] positions = new Vector4[instanceCount];
        cubeColors = new int[instanceCount];
        colors = new Vector4[instanceCount];

        // Kaikki erilaiset attributes laitetaan tänne.
        for (int i = 0; i < instanceCount; i++) {

            // Kuubien erilaiset muodot ja position.
            float angle = Random.Range(0.0f, Mathf.PI * 3.6f);
            float distance = Random.Range(20.0f, 5000.0f);
            float height = Random.Range(0.0f, 700.0f);
            float size = Random.Range(0.05f, 70.0f);

            cubeColors[i] = Random.Range(0,2);

            positions[i] = new Vector4(Mathf.Sin(angle) * distance, height, Mathf.Cos(angle) * distance, size);
        }
        positionBuffer.SetData(positions);

        instanceMaterial.SetBuffer("positionBuffer", positionBuffer);

        // Jotai system asioit
        if (instanceMesh != null) {
            args[0] = (uint)instanceMesh.GetIndexCount(subMeshIndex);
            args[1] = (uint)instanceCount;
            args[2] = (uint)instanceMesh.GetIndexStart(subMeshIndex);
            args[3] = (uint)instanceMesh.GetBaseVertex(subMeshIndex);
        }
        else
        {
            args[0] = args[1] = args[2] = args[3] = 0;
        }
        argsBuffer.SetData(args);

        cachedInstanceCount = instanceCount;
        cachedSubMeshIndex = subMeshIndex;
    }

    // 50/50 jos kuubin väri on player 1 tai player 2.
    void updateColor() {
        for (int i = 0; i < instanceCount; i++) {
            if(cubeColors[i] == 0) {
                colors[i] = StateController.player1Color;
            } else {
                colors[i] = StateController.player2Color;
            }
        }
        colorBuffer.SetData(colors);
        instanceMaterial.SetBuffer("colorBuffer", colorBuffer);
    }


    // Pitää release nää GPU bufferit.
    void OnDisable() {
        if (positionBuffer != null)
            positionBuffer.Release();
        positionBuffer = null;

        if (colorBuffer != null)
            colorBuffer.Release();
        colorBuffer = null;

        if (argsBuffer != null)
            argsBuffer.Release();
        argsBuffer = null;
    }
}
