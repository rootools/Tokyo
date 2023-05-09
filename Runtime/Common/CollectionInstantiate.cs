using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tokyo {

    public static class CollectionInstantiate {

        private static Transform[] Generate(GameObject prefab, int count) {
            Transform[] result = new Transform[count];

            for (int i = 0; i < count; ++i) {
                GameObject view = Object.Instantiate(prefab, prefab.transform.parent);

                view.name = $"{prefab.name}_{i}";

                if (!view.gameObject.activeInHierarchy)
                    view.gameObject.SetActive(true);

                result[i] = view.transform;
            }

            return result;
        }

        public static TV[] Update<TV, TM>(Transform container, IEnumerable<TM> sourceModels, Action<TV, TM> connector = null) where TV : Component {
            int containerChildCount = container.childCount;

            if (containerChildCount == 0)
                throw new ArgumentException($"Container {container.name} is empty. Need at least one child element.");

            List<Transform> childs = container.transform.Cast<Transform>().ToList();

            TM[] models = sourceModels.ToArray();
            TV[] views = new TV[models.Length];

            if (containerChildCount < models.Length) {
                Transform[] newChild = Generate(childs[0].gameObject, models.Length - containerChildCount);
                childs.AddRange(newChild);
            } else {
                for (int i = models.Length; i < containerChildCount; ++i)
                    childs[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < models.Length; ++i) {
                views[i] = childs[i].GetComponent<TV>();

                if (!views[i].gameObject.activeInHierarchy)
                    views[i].gameObject.SetActive(true);

                connector?.Invoke(views[i], models[i]);
            }

            return views;
        }

    }
}