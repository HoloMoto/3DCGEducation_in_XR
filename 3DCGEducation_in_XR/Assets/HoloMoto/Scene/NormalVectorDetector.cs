using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalVectorDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // ���C���΂����������|���S���̖@���x�N�g�����擾���ă��O�ɏo��
    void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return;

            Mesh mesh = meshCollider.sharedMesh;
            Vector3[] normals = mesh.normals;
            int[] triangles = mesh.triangles;
            Vector3 normal = Vector3.zero;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                if (triangles[i] == hit.triangleIndex)
                {
                    normal = normals[triangles[i]];
                    break;
                }
            }
            Debug.Log("normal: " + normal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�̍��N���b�N��OnMouseDown���Ăяo��
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }

    }
}
