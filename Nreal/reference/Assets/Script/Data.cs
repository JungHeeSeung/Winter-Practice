using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRKernal.NRExamples
{
    // 씬 넘어갈 때 필요한 데이터들
    public class Data : MonoBehaviour
    {
        // 평면 트래킹 후, 플레이어가 찍은 pool position
        public List<Vector3> poolPos;
        public Droidpool[] droidpools;

        // Start is called before the first frame update
        void Awake()
        {
            DontDestroyOnLoad(this);
            if (GameObject.Find("Droidpool(Clone)"))
                droidpools = GameObject.Find("Droidpool(Clone)").GetComponents<Droidpool>();
        }
    }
}
