using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [System.Serializable]
    public class ChildMaterialScroll
    {
        public Renderer renderer;
        public Vector2 scrollSpeed;
        public Vector2 currentOffset;
    }

    public ChildMaterialScroll[] childrenMaterials;

    [SerializeField] bool flollowPlayer;

    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;

    void Start()
    {
        // �e�q�I�u�W�F�N�g�̏����I�t�Z�b�g���擾
        foreach (var childMaterial in childrenMaterials)
        {
            if (childMaterial.renderer != null)
            {
                // �}�e���A���̌��݂̃I�t�Z�b�g��ۑ�
                childMaterial.currentOffset = childMaterial.renderer.material.GetTextureOffset("_MainTex");
            }
        }
    }

    void Update()
    {
        if (flollowPlayer) transform.position = player.position + offset;
        // �e�q�I�u�W�F�N�g�̃}�e���A���ɑ΂��ăI�t�Z�b�g���X�V
        foreach (var childMaterial in childrenMaterials)
        {
            if (childMaterial.renderer != null)
            {
                // ���݂̃I�t�Z�b�g�ɃX�N���[���X�s�[�h�����Z
                childMaterial.currentOffset += childMaterial.scrollSpeed * Time.deltaTime;

                // �I�t�Z�b�g�����[�v�����邽�߂̏����i�K�v�ɉ����āj
                childMaterial.currentOffset.x = Mathf.Repeat(childMaterial.currentOffset.x, 1);
                childMaterial.currentOffset.y = Mathf.Repeat(childMaterial.currentOffset.y, 1);

                // �}�e���A���̃I�t�Z�b�g���X�V
                childMaterial.renderer.material.SetTextureOffset("_MainTex", childMaterial.currentOffset);
            }
        }
    }

}
