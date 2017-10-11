namespace Apex.Mecanim
{
    using UnityEngine;
    using System.Collections;
    //using Apex.Steering;

    public class MecanimMoverComponentComplete : MonoBehaviour //, IMoveUnits
    {
        public float animatorSpeed = 1f;
        private Transform _transform;
        private Animator _animator;
        private int _speedId;
        private int _angleId;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _transform = transform;
            _animator.speed = animatorSpeed;

            _speedId = Animator.StringToHash("Speed");
            _angleId = Animator.StringToHash("Angle");
        }

        public void Move(Vector3 velocity, float deltaTime)
        {
            float speed = velocity.magnitude;
            _animator.SetFloat(_speedId, speed);

            if (speed < 0.2f)
            {
                _animator.SetFloat(_speedId, 0f);
                _animator.SetFloat(_angleId, 0f);
                return;
            }

            _animator.SetFloat(_speedId, speed);

            float angleDirection = TurnDir(_transform.forward, velocity);
            float angle = Vector3.Angle(_transform.forward, velocity) * angleDirection;

            _animator.SetFloat(_angleId, angle);
        }

        public void Rotate(Vector3 targetOrientation, float angularSpeed, float deltaTime)
        {
            /* NOT USED */
        }

        public void Stop()
        {
            _animator.SetFloat(_speedId, 0f);
        }

        private static float TurnDir(Vector3 p1, Vector3 p2)
        {
            return Mathf.Sign((p1.z * p2.x) - (p1.x * p2.z));
        }
    }
}