using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponData))]
public class WeaponEditor : Editor
{
    private WeaponData weaponData;

    private void OnEnable()
    {
        weaponData = target as WeaponData;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (weaponData.Sprite == null) return;

        Texture2D sprite = AssetPreview.GetAssetPreview(weaponData.Sprite);

        GUILayout.Label("", GUILayout.Height(120), GUILayout.Width(120));

        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), sprite);
    }
}
