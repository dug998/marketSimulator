#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CustomButtonEditor
{
    [MenuItem("GameObject/UI/Custom Button", false, 10)]
    private static void CreateCustomButton(MenuCommand menuCommand)
    {
        // Tạo object với Image + CustomButton
        GameObject go = new GameObject("CustomButton", typeof(RectTransform), typeof(Image), typeof(CustomButton));
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

        // Background
        Image bg = go.GetComponent<Image>();
        bg.color = new Color(1f, 1f, 1f, 1f / 255f); // invisible nhưng bấm được

        // Button
        CustomButton btn = go.GetComponent<CustomButton>();
        btn.targetGraphic = bg;

        // Child ImgView
        GameObject child = new GameObject("ImgView", typeof(RectTransform), typeof(Image));
        child.transform.SetParent(go.transform, false);

        //RectTransform rect = child.GetComponent<RectTransform>();
        //rect.anchorMin = Vector2.zero;
        //rect.anchorMax = Vector2.one;
        //rect.offsetMin = Vector2.zero;
        //rect.offsetMax = Vector2.zero;

        Image imgView = child.GetComponent<Image>();
        imgView.raycastTarget = false;
       
        // Đăng vào hierarchy
        Undo.RegisterCreatedObjectUndo(go, "Create Custom Button");
        Selection.activeGameObject = go;
    }
}
#endif
