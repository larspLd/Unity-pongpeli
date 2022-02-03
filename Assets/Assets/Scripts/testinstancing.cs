using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testinstancing : MonoBehaviour
{

    // Kuubit menu taustalla. BackgroundCubes objektilla on tämä scripti. Materials/MenuCubeShader on tämän scription toinen osa.


    // !!! Voit vaihtaa miten monta kuubia on olemassa BackgroundCubes objektissa !!!
    public int instanceCount = 1;


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
        positionBuffer = new ComputeBuffer(instanceCount, 16);
        colorBuffer = new ComputeBuffer(instanceCount, 16);

        Vector4[] positions = new Vector4[instanceCount];
        Vector4[] colors = new Vector4[instanceCount];

        // Kaikki erilaiset attributes laitetaan tänne.
        for (int i = 0; i < instanceCount; i++) {

            // Kuubien erilaiset muodot ja position.
            float angle = Random.Range(0.0f, Mathf.PI * 3.6f);
            float distance = Random.Range(20.0f, 5000.0f);
            float height = Random.Range(0.0f, 700.0f);
            float size = Random.Range(0.05f, 70.0f);
            positions[i] = new Vector4(Mathf.Sin(angle) * distance, height, Mathf.Cos(angle) * distance, size);

            // Kuubien värit.
            float r = Random.Range(0.00f, 1f);
            float g = Random.Range(0.00f, 1f);
            float b = Random.Range(0.00f, 1f);
            float a = Random.Range(0.80f, 1f);
            colors[i] = new Vector4(r, g, b, a);
        }
        positionBuffer.SetData(positions);
        colorBuffer.SetData(colors);

        instanceMaterial.SetBuffer("positionBuffer", positionBuffer);
        instanceMaterial.SetBuffer("colorBuffer", colorBuffer);

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
