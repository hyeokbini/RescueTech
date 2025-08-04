using UnityEngine;

namespace OxygenTank
{
    [ExecuteInEditMode]
    public class OxygenTankLiquid : MonoBehaviour
    {

        [Range(0, 1)]
        public float Amount;

        private SkinnedMeshRenderer _liquidRenderer;

        void Start()
        {
            _liquidRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        void Update()
        {
            if (_liquidRenderer == null)
                _liquidRenderer = GetComponent<SkinnedMeshRenderer>();

            if (_liquidRenderer != null)
                _liquidRenderer.SetBlendShapeWeight(0, Amount * 100);
        }
    }
}