using UnityEngine;
using UnityEngine.UI;

public class FixedAspectRatio : MonoBehaviour
{
    public float targetAspect = 16f / 10f; // Desired aspect ratio
   // public Canvas canvas; // Reference to the Canvas (Screen Space - Overlay)
    public RectTransform imageRect;
    public Image image;

   // public RectTransform canvasRect; // Assign the Canvas's RectTransform

    void Start()
    {
        // Adjust aspect ratio
        AdjustAspectRatio();


        

        // Adjust canvas scaling
        if (image != null)
        {
            // canvasRect = canvas.GetComponent<RectTransform>();
            //AdjustCanvasScale();
            // AdjustCanvasAspectRatio();
            imageRect = image.GetComponent<RectTransform>();
            AdjustImageScaling();
        }
        
        else
        {
            Debug.LogError("image is not assigned. Please assign a image in the inspector.");
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

    /*void AdjustCanvasScale()
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
    }*/
    /*void AdjustCanvasAspectRatio()
    {
        if (canvasRect == null)
        {
            Debug.LogError("Canvas RectTransform is not assigned. Please assign it in the inspector.");
            return;
        }

        float screenAspect = (float)Screen.width / Screen.height;
        float scaleFactor;

        if (screenAspect > targetAspect)
        {
            // Screen is wider than target aspect ratio (pillarboxing)
            scaleFactor = targetAspect / screenAspect;
            canvasRect.localScale = new Vector3(scaleFactor, 1f, 1f);
        }
        else
        {
            // Screen is taller than target aspect ratio (letterboxing)
            scaleFactor = screenAspect / targetAspect;
            canvasRect.localScale = new Vector3(1f, scaleFactor, 1f);
        }

        // Center the canvas
        canvasRect.anchorMin = new Vector2(0.5f, 0.5f);
        canvasRect.anchorMax = new Vector2(0.5f, 0.5f);
        canvasRect.pivot = new Vector2(0.5f, 0.5f);
        canvasRect.anchoredPosition = Vector2.zero;
    }*/
    void AdjustImageScaling()
    {
        if (imageRect == null)
        {
            Debug.LogError("Image RectTransform is not assigned. Please assign it in the Inspector.");
            return;
        }

        // Calculate the screen aspect ratio
        float screenAspect = (float)Screen.width / Screen.height;

        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        float scaleWidth = 1.0f / scaleHeight;

        if (screenAspect < targetAspect)
        {
            // Screen is wider than target aspect ratio - adjust height
            float scaleFactor = targetAspect / screenAspect;
            imageRect.anchorMin = new Vector2(0f, (1f - scaleHeight) / 2f); // Bottom
            imageRect.anchorMax = new Vector2(1f, 1f - (1f - scaleHeight) / 2f); // Top
            imageRect.offsetMin = Vector2.zero; // Reset offsets
            imageRect.offsetMax = Vector2.zero;
        }
        else
        {
            
            
            // Screen is taller than target aspect ratio - adjust width
            float scaleFactor = screenAspect / targetAspect;
            imageRect.anchorMin = new Vector2((1-scaleWidth) / 2f, 0f); // Left
            imageRect.anchorMax = new Vector2(1-(1-scaleWidth) / 2f, 1f); // Right
            imageRect.offsetMin = Vector2.zero; // Reset offsets
            imageRect.offsetMax = Vector2.zero;
        }
    }
}
