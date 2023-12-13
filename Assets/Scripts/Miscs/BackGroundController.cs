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
        // 各子オブジェクトの初期オフセットを取得
        foreach (var childMaterial in childrenMaterials)
        {
            if (childMaterial.renderer != null)
            {
                // マテリアルの現在のオフセットを保存
                childMaterial.currentOffset = childMaterial.renderer.material.GetTextureOffset("_MainTex");
            }
        }
    }

    void Update()
    {
        if (flollowPlayer) transform.position = player.position + offset;
        // 各子オブジェクトのマテリアルに対してオフセットを更新
        foreach (var childMaterial in childrenMaterials)
        {
            if (childMaterial.renderer != null)
            {
                // 現在のオフセットにスクロールスピードを加算
                childMaterial.currentOffset += childMaterial.scrollSpeed * Time.deltaTime;

                // オフセットをループさせるための処理（必要に応じて）
                childMaterial.currentOffset.x = Mathf.Repeat(childMaterial.currentOffset.x, 1);
                childMaterial.currentOffset.y = Mathf.Repeat(childMaterial.currentOffset.y, 1);

                // マテリアルのオフセットを更新
                childMaterial.renderer.material.SetTextureOffset("_MainTex", childMaterial.currentOffset);
            }
        }
    }

}
