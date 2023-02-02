using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YA7GI
{
    [AddComponentMenu("UI/VANI/RadarGraph")]
    [RequireComponent(typeof(CanvasRenderer))]
    [RequireComponent(typeof(RectTransform))]
    public class RadarUnit : Graphic
    {
        [Range(3, 12)]
        public int vertexCount = 3;
        [Range(0, 1)]
        public List<float> vertexList = new List<float>();

        float graphSize = 0;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            // サイズを rect に合わせて更新
            graphSize = rectTransform.rect.width < rectTransform.rect.height ? rectTransform.rect.width : rectTransform.rect.height;
            if (graphSize > 0) graphSize /= 2;

            if (vertexList.Count != vertexCount)
            {
                vertexList.Clear();
                for (int i = 0; i < vertexCount; i++) vertexList.Add(1);
            }

            vh.Clear();
            for (int i = 0; i < vertexList.Count; i++)
            {
                SetVertex(vh, i);
            }
        }

        private UIVertex[] SetVbo(Vector2[] vertices)
        {
            UIVertex[] vbo = new UIVertex[4];
            for (int i = 0; i < vertices.Length; i++)
            {
                var vert = UIVertex.simpleVert;
                vert.color = color;
                vert.position = vertices[i];
                vbo[i] = vert;
            }
            return vbo;
        }

        private void SetVertex(VertexHelper vh, int num)
        {
            float currVertex = vertexList[num];
            float nextVertex = vertexList[num + 1 >= vertexList.Count ? 0 : num + 1];
            float degrees = 360f / vertexList.Count;
            Vector2[] posData = {
                Vector2.zero,
                new Vector2(Mathf.Sin(degrees*(num+0)*Mathf.PI/180.0f)*graphSize*currVertex, Mathf.Cos(degrees*(num+0)*Mathf.PI/180.0f)*graphSize*currVertex),
                new Vector2(Mathf.Sin(degrees*(num+1)*Mathf.PI/180.0f)*graphSize*nextVertex, Mathf.Cos(degrees*(num+1)*Mathf.PI/180.0f)*graphSize*nextVertex),
            };

            vh.AddUIVertexQuad(SetVbo(posData));
        }
    }
}
