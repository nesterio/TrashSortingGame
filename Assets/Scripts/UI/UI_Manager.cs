using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UI
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance;

        [Serializable]
        public struct PrefabDictItem {
            public string name;
            public GameObject prefab;
        }

        public GameObject FindObjByName(string requestName)
        {
            foreach (var prefabItem in Objects)
            {
                var itemName = prefabItem.name.Trim().Replace(" ", "");
                var searchName = requestName.Trim().Replace(" ", "");

                if (StringComparer.InvariantCultureIgnoreCase.Equals(itemName, searchName))
                {
                    return prefabItem.prefab;
                }
            }
            return null;
        }

        public PrefabDictItem[] Objects;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }

}
