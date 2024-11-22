using UnityEngine;
using UnityEngine.UI;

public class FixedAspectRatio : MonoBehaviour
{
    public float targetAspect = 16f / 9f; // Desired aspect ratio
    public Canvas canvas; // Reference to the Canvas (Screen Space - Overlay)

    void Start()
    {
        // Adjust aspect ratio
        AdjustAspectRatio();

        // Adjust canvas scaling
        if (canvas != null)
        {
            AdjustCanvasScale();
        }
        else
        {
            Debug.LogError("Canvas is not assigned. Please assign a Canvas in the inspector.");
        }
    }

    void AdjustAspectRatio()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Add letterbox (black bars at the top and bottom)
            Camera.main.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else
        {
            // Add pillarbox (black bars on the sides)
            float scaleWidth = 1.0f / scaleHeight;
            Camera.main.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }

    void AdjustCanvasScale()
    {
        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
        if (canvasScaler == null)
        {
            Debug.LogError("Canvas Scaler component is missing on the Canvas.");
            return;
        }

        // Set scaling mode and reference resolution
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080); // Fixed reference resolution
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

        // Dynamically adjust match based on aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;
        if (windowAspect > targetAspect)
        {
            // Wider screen - match width
            canvasScaler.matchWidthOrHeight = 0f;
        }
        else
        {
            // Taller screen - match height
            canvasScaler.matchWidthOrHeight = 1f;
        }
    }
}
