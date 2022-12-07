using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class ShadowCasterGenerator : MonoBehaviour
{

    [SerializeField] bool rendererSilhouette = true;
    [SerializeField] bool castShadows = true;
    [SerializeField] bool selfShadows = false;

    [SerializeField] Transform grid;
    [SerializeField] ShadowCaster2D shadowCasterComponent;
    [SerializeField] List<CompositeCollider2D> tilemapColliders;

    static readonly FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathHashField = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly MethodInfo generateShadowMeshMethod = typeof(ShadowCaster2D)
                                    .Assembly
                                    .GetType("UnityEngine.Rendering.Universal.ShadowUtility")
                                    .GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);
    [ContextMenu("Generate")]
    public void Generate()
    {
        CalculateTilemaps();

        //  Generates ShadowCaster2D on obstacles
        for (int i = 0; i < tilemapColliders.Count; i++)
        {
            for (int j = 0; j < tilemapColliders[i].pathCount; j++)
            {
                Vector2[] pathVertices = new Vector2[tilemapColliders[i].GetPathPointCount(j)];
                tilemapColliders[i].GetPath(j, pathVertices);
                shadowCasterComponent = tilemapColliders[i].gameObject.AddComponent<ShadowCaster2D>();

                shadowCasterComponent.useRendererSilhouette = rendererSilhouette;
                shadowCasterComponent.castsShadows = castShadows;
                shadowCasterComponent.selfShadows = selfShadows;

                Vector3[] testPath = new Vector3[pathVertices.Length];
                for (int k = 0; k < pathVertices.Length; k++)
                {
                    testPath[k] = pathVertices[k];
                }

                shapePathField.SetValue(shadowCasterComponent, testPath);
                shapePathHashField.SetValue(shadowCasterComponent, Random.Range(int.MinValue, int.MaxValue));
                meshField.SetValue(shadowCasterComponent, new Mesh());
                generateShadowMeshMethod.Invoke(shadowCasterComponent, new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent) });
            }
        }
    }
    void CalculateTilemaps()
    {
        tilemapColliders.Clear();

        Transform walls = grid.Find("Walls");

        for (int i = 0; i < walls.childCount; i++)
        {
            if (walls.GetChild(i).GetComponent<ShadowCaster2D>() is ShadowCaster2D shadow)
                DestroyImmediate(shadow);

            tilemapColliders.Add(walls.GetChild(i).GetComponent<CompositeCollider2D>());
        }
    }
}