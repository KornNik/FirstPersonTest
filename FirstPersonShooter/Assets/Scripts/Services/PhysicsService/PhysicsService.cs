using System.Collections.Generic;
using UnityEngine;


namespace ExampleTemplate
{
    public sealed class PhysicsService : Service
    {
        #region Fields

        private const int COLLIDED_OBJECT_SIZE = 20;

        private readonly Collider[] _collidedObjects;
        private readonly List<Collider> _triggeredObjects;

        #endregion

        
        #region ClassLifeCycles

        public PhysicsService(CameraServices cameraServices) : base()
        {
            _collidedObjects = new Collider[COLLIDED_OBJECT_SIZE];
            _triggeredObjects = new List<Collider>();
        }

        #endregion

        
        #region Methods

        public void SwitchRenderer(GameObject swithcingObject, bool value)
        {
            var tempRenderer = swithcingObject.GetComponent<Renderer>();
            if (tempRenderer)
            {
                tempRenderer.enabled = value;
            }
            if (swithcingObject.transform.childCount <= 0) return;

            foreach(Transform item in swithcingObject.transform)
            {
                tempRenderer = item.GetComponentInChildren<Renderer>();

                if (tempRenderer) 
                { 
                    tempRenderer.enabled = value;
                }
            }

        }

        public bool CheckGround(Vector3 position, float distanceRay, out Vector3 hitPoint, int layerMask = LayerManager.DEFAULT_LAYER)
        {
            hitPoint = Vector2.zero;

            var hit = Physics2D.Raycast(position, Vector3.down, distanceRay, layerMask);
            if (hit.collider == null)
            {
                return false;
            }

            hitPoint = hit.point;
            return true;
        }
        
        public List<Collider> GetObjectsInRadius(Vector3 position, float radius, int layerMask = LayerManager.DEFAULT_LAYER)
        {
            _triggeredObjects.Clear();
            Collider trigger;

            var collidersCount = Physics.OverlapSphereNonAlloc(position, radius, _collidedObjects, layerMask);
            
            for (var i = 0; i < collidersCount; i++)
            {
                trigger = _collidedObjects[i];

                if (trigger != null && !_triggeredObjects.Contains(trigger))
                {
                    _triggeredObjects.Add(trigger);
                }
            }

            return _triggeredObjects;
        }
        
        public Collider2D GetNearestObject(Vector3 targetPosition, HashSet<Collider2D> objectBuffer)
        {
            var nearestDistance = Mathf.Infinity;
            Collider2D result = null;

            foreach (var trigger in objectBuffer)
            {
                var objectDistance = (targetPosition - trigger.transform.position).sqrMagnitude;
                if (objectDistance >= nearestDistance)
                {
                    continue;
                }

                nearestDistance = objectDistance;
                result = trigger;
            }

            return result;
        }

        public int GetIdObject(Vector2 position)
        {
            var raycastHit2D = Physics2D.Raycast(position, Vector2.zero, 0f);
            if (raycastHit2D.collider)
            {
                return raycastHit2D.collider.gameObject.GetInstanceID();
            }

            return -1;
        }

        #endregion
    }
}
