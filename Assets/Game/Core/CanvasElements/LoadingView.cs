using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Core.CanvasElements
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 180f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }

        public void Set(bool isLoading)
        {
            gameObject.SetActive(isLoading);
        }
    }
}