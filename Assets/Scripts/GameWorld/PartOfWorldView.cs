using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Views
{
    public interface IPartOfWorldView
    {

        PartOfWorldView.LineInfo[] LinesInfo { get; }

        Vector3 EndPointPosition { get; }

    }

    public class PartOfWorldView : MonoBehaviour, IPartOfWorldView
    {

        [System.Serializable]
        public class LineInfo
        {

            [SerializeField]
            Transform[] wayPoints;

            public Transform[] WayPoints
            {
                get { return wayPoints; }
            }

        }

        [SerializeField]
        LineInfo[] linesInfo = new LineInfo[GameWorldModel.LinesCount];
        [SerializeField]
        Transform endPoint;

        public LineInfo[] LinesInfo
        {
            get { return linesInfo; }
        }

        public Vector3 EndPointPosition
        {
            get { return endPoint.position; }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log(other);
        }

    }

    
}