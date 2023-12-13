using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] vFXPools;
    [SerializeField] Pool[] iTems;

    static Dictionary<GameObject, Pool> dictionary; // �e�v���n�u�Ƃ���Ɋ֘A����I�u�W�F�N�g�v�[�����֘A�t���邽�߂̎����B

    void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(vFXPools);
        Initialize(iTems);
    }

    // UNITY_EDITOR�f�B���N�e�B�u�́AUnity�G�f�B�^�����ł̂݃R�[�h�����s���邽�߂̂��́B
    // ���ۂ̃Q�[���v���C�ł͎��s����Ȃ��B
#if UNITY_EDITOR
    void OnDestroy()
    {
        //�v�[���T�C�Y�������������`�F�b�N����
        CheckPoolSize(vFXPools);
        CheckPoolSize(iTems);
    }
#endif

    // �e�v�[���̃����^�C�����̃T�C�Y���m�F���A�����ݒ�����傫���ꍇ�͌x����\������B
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(
                    string.Format("Pool:{0}has a runtime size {1} bigger than its initial size{2}!",
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
            }
        }
    }

    // �v�[�������������A���ꂼ��̃v�[���������ɒǉ�����B
    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR    
            if (dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in multiple pools! prefab:" + pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);

            // Hierarchy�r���[�Ō��₷�����邽�߂ɐV����GameObject���쐬���āA���̎q�Ƃ��ăv�[���I�u�W�F�N�g�����B
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;

            pool.Initialize(poolParent);
        }
    }


    // �ȉ���Release�֐��Q�́A�w�肳�ꂽ�v���n�u�Ɋ�Â��ăv�[������I�u�W�F�N�g���擾���邽�߂̃I�[�o�[���[�h���ꂽ�֐��B
    // �����v�[�������݂��Ȃ��A�܂��̓v�[������̏ꍇ�A�V�����I�u�W�F�N�g���쐬�����B

    /// <summary>
    /// <para>�v�[�����Ɏw�肳�ꂽ<paramref name="prefab"></paramref>���Q�[���I�u�W�F�N�g�ɕԂ��B</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>�w�肳�ꂽ�v���n�u</para>
    /// </param>
    /// <returns>
    /// <para>�v�[�����ɏ����ł����Q�[���I�u�W�F�N�g</para>
    /// </returns>
    public static GameObject Release(GameObject prefab)
    {
        #if UNITY_EDITOR
        if(!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].preparedObject();
    }

    /// <summary>
    /// <para>�v�[�����Ɏw�肳�ꂽ<paramref name="prefab"></paramref>���Q�[���I�u�W�F�N�g�ɕԂ��B</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>�w�肳�ꂽ�v���n�u</para>
    /// </param>
    /// <param name="position">
    /// <para>�w�肳�ꂽ�����ʒu</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab,Vector3 position)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].preparedObject(position);
    }

    /// <summary>
    /// <para>�v�[�����Ɏw�肳�ꂽ<paramref name="prefab"></paramref>���Q�[���I�u�W�F�N�g�ɕԂ��B</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>�w�肳�ꂽ�v���n�u</para>
    /// </param>
    /// <param name="position">
    /// <para>�w�肳�ꂽ�����ʒu</para>
    /// </param>
    /// <param name="rotation">
    /// <para>�w�肳�ꂽ��]</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].preparedObject(position,rotation);
    }

    /// <summary>
    /// <para>�v�[�����Ɏw�肳�ꂽ<paramref name="prefab"></paramref>���Q�[���I�u�W�F�N�g�ɕԂ��B</para>
    /// </summary>
    /// <param name="prefab">
    /// <para>�w�肳�ꂽ�v���n�u</para>
    /// </param>
    /// <param name="position">
    /// <para>�w�肳�ꂽ�����ʒu</para>
    /// </param>
    /// <param name="rotation">
    /// <para>�w�肳�ꂽ��]</para>
    /// </param>
    /// <param name="localScale">
    /// <para>�w�肳�ꂽ�g��E�k��</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation,Vector3 localScale)
    {
#if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("pool Manager could NOT find prefab : " + prefab.name);

            return null;
        }
#endif
        return dictionary[prefab].preparedObject(position, rotation,localScale);
    }
}
