using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nuwn
{
    namespace Extensions
    {
        public static class Extensions
        {
            /// <summary>
            /// How to use: transform.position = transform.SetPosition()
            /// just to show how to build extentions
            /// </summary>
            /// <param name="trans"></param>
            /// <returns></returns>
            public static Vector3 SetPosition(this Transform trans)
            {
                return trans.position = Vector3.zero;
            }

        }
        public static class TransformExtensions
        {
            /// <summary>
            /// Checks whether or not the transform is in view of main camera
            /// </summary>
            /// <param name="transform"></param>
            /// <returns></returns>
            public static bool IsInView(this Transform transform)
            {
                return Essentials.Essentials.IsInView(transform.position, Camera.main);
            }
            /// <summary>
            /// Checks if transform is in view of the targeted camera
            /// </summary>
            /// <param name="transform"></param>
            /// <param name="cam"></param>
            /// <returns></returns>
            public static bool IsInView(this Transform transform, Camera cam)
            {
                return Essentials.Essentials.IsInView(transform.position, cam);
            }
            /// <summary>
            /// returns which camera is viewing the target, 
            /// asking for a list of all cameras you wish too look up,
            /// so it dont have to search for it and that's slow to do.
            /// </summary>
            /// <param name="transform"></param>
            /// <param name="cameras"></param>
            /// <returns></returns>
            public static List<Camera> IsInView(this Transform transform, List<Camera> cameras)
            {
                List<Camera> camerasViewing = new List<Camera>();

                foreach (var cam in cameras)
                {
                    var res = Essentials.Essentials.IsInView(transform.position, cam);
                    if (res)
                        camerasViewing.Add(cam);
                }
                return camerasViewing;
            }
        }
        public static class MonoBehaviourExtentions
        {
            /// <summary>
            /// Waits n time before activating the debugger.
            /// usage : this.setTimeout(1, (result) => { Debug.Log("debug"); } );
            /// </summary>
            /// <param name="instance"></param>
            /// <param name="waitTime"></param>
            /// <param name="Callback"></param>
            public static void SetTimeout(this MonoBehaviour instance, Action Callback, float waitTime)
            {
                instance.StartCoroutine(Wait((res) => Callback?.Invoke(), waitTime));
            }
            public static void SetTimeout(this MonoBehaviour instance, Action<object> Callback, float waitTime)
            {
                instance.StartCoroutine(Wait( (res) => Callback?.Invoke(true), waitTime));
            }
            /// <summary>
            /// Continues interval with callback, use stopinterval to stop it.
            /// </summary>
            /// <param name="instance"></param>
            /// <param name="Callback"></param>
            /// <param name="intervalTime"></param>
            /// <returns></returns>
            public static Coroutine SetInterval(this MonoBehaviour instance, Action<object> Callback, float intervalTime)
            {
                return instance.StartCoroutine(RepeatingWait((res) => Callback?.Invoke(true), intervalTime));
            }
            public static Coroutine SetInterval(this MonoBehaviour instance, Action Callback, float intervalTime)
            {
                return instance.StartCoroutine(RepeatingWait((res) => Callback?.Invoke(), intervalTime));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="instance"></param>
            /// <param name="coroutine">The interval to stop, store it as a var</param>
            public static void StopInterval(this MonoBehaviour instance, Coroutine coroutine)
            {
                instance.StopCoroutine(coroutine);
            }
            static IEnumerator Wait(Action<bool> Callback, float duration)
            {
                yield return new WaitForSeconds(duration / 1000);
                Callback.Invoke(true);
            }
            static IEnumerator RepeatingWait( Action<bool> Callback, float waitTime)
            {
                while (true)
                {
                    yield return new WaitForSeconds(waitTime / 1000);
                    Callback.Invoke(true);
                }
            }
        }
    }
}
