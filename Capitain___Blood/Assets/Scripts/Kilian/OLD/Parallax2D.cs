
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RetroJam.CaptainBlood
{

	//[ExecuteInEditMode]
	public class Parallax2D : MonoBehaviour
    {

		[System.Serializable]
		public class TwoDeeLayer
        {

			[Range(0.0f, 1.0f)]
			public float move_multipler;
			public float zPos;
			public Transform layer;
            public Vector3 layerScale;

            public TwoDeeLayer(Transform _layer, float _multiplier, float _zPos)
            {
                layer = _layer;
                move_multipler = _multiplier;
                zPos = _zPos;
            }

		}
        [SerializeField] GameObject spawnLayer;
        [SerializeField] Transform poubelle;
        public Transform     _targetCam;
		public Vector2       _offset = Vector2.zero;
		public List<TwoDeeLayer> _layers = new List<TwoDeeLayer>();


		void Reset()////////////////////////////////////UPDATE
        {

            _layers.Clear();

            for (int i=0; i< transform.childCount; i++)
            {
                _layers.Add(new TwoDeeLayer(transform.GetChild(i), (float)(i + 1) / ((float)transform.childCount + 1.0f), (i + 1) * 1.0f));

            }

        }

        // Use this for initialization
        void Start ()
        {

			if( !_targetCam)
            {
				_targetCam = Camera.main.transform;
			}

		}

        private void Update()
        {
            BigSpawn();

        }

        void LateUpdate()
        {

			if( !_targetCam)
            {
				_targetCam = Camera.main.transform;
				return;
			}

			AdjustLayers(_targetCam.position);



		}


        void BigSpawn()
        {
            for (int i = 8; i >= 0; i--)
            {

                _layers[i].layerScale = transform.GetChild(i).localScale;

                Vector3 Scale = transform.GetChild(i).localScale;

                Scale = new Vector3(Scale.x + (Scale.x*0.07f), Scale.y + (Scale.y * 0.07f));

                transform.GetChild(i).localScale = Scale;

                if (Scale.x > 2)
                {
                    transform.GetChild(i).transform.SetParent(poubelle);
                    Instantiate(spawnLayer, transform);
                    Reset();
                }


            }
        }


//		public void OnWillRenderObject()
//		{
//			if(!enabled)
//				return;
//
//			Camera cam = Camera.current;
//			if( !cam )
//				return;
//
//			AdjustLayers(cam.transform.position);
//
//		}

		void AdjustLayers(Vector3 viewPoint)
        {

			viewPoint += (Vector3) _offset;

			Vector3 displacement = viewPoint - transform.position;
			Vector3 layerSpot = Vector3.zero;

			for(int i=0; i<_layers.Count; i++)
            {

				layerSpot = displacement * _layers[i].move_multipler;
				layerSpot.z = _layers[i].zPos;

				_layers[i].layer.localPosition = layerSpot;

			}

		}
	}

}