using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HSBHandleTracker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private const string TRACK_CURSOR = "TrackCursor";
    private RectTransform RectTransform;
    private Canvas ParentCanvas;

    [SerializeField] private RectTransform Handle;
    public Vector2 Value
        {
            get { return Handle.transform.localPosition; }
            set { Handle.transform.localPosition = value; }
        }
    public Vector2 NormalizedValue
        {
            get { return Normalize(Value); }
            set { Value = DeNormalize(value); }
        }
    public float CanvasWidth 
        { 
            get { return RectTransform != null ? RectTransform.rect.width : 0; } 
        }
    public float CanvasHeight 
        { 
            get { return RectTransform != null ? RectTransform.rect.height : 0; } 
        }
    // Pointer drag on canvas event
    public delegate void OnDrag(Vector2 position);
    public OnDrag DragEvent;

    // Handle moved (consequence of pointer drag) event
    public delegate void OnHandleMove(HSBHandleTracker sender, Vector2 position);
    public OnHandleMove HandleMovedEvent;

    private void Awake()
    {
        RectTransform = transform as RectTransform;
        ParentCanvas = GetComponentInParent<Canvas>();

        DragEvent = OnCanvasDrag;
    }

    /**
    * On cursor drag (triggered by TrackCursor coroutine) move the handle coherently with the cursor and trigger the HandleMoved Event (used from the HSBPicker to trigger the set color procedure)
    */
    private void OnCanvasDrag(Vector2 position)
    {
        Handle.transform.localPosition = position;

        HandleMovedEvent?.Invoke(this, position);
    }

    /**
    * return the position on canvas of the cursor, normalized (means the values will included between 1 at max (right for x and top for y) and 0 at min (left for x and bottom for y))
    */
    public Vector2 Normalize(Vector2 position)
    {
        Vector2 pos = new Vector2(position.x / RectTransform.rect.width, position.y / RectTransform.rect.height);
        return pos;
    }
    /**
    * See above, opposite
    */
    public Vector2 DeNormalize(Vector2 position)
    {
        Vector2 pos = new Vector2(position.x * RectTransform.rect.width, position.y * RectTransform.rect.height);
        return pos;
    }
    /**
    * Reset the the handle to the initial position (center, normal x = .5, normal y = .5)
    */
    public void Reset() {
        Handle.transform.localPosition = new Vector2(CanvasWidth / 2, CanvasHeight / 2);
    }

    /**
    * Start the track cursor coroutine, on cursor click on canvas
    */
    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(TRACK_CURSOR);
    }

    /**
    * Stop the track cursor coroutine, on cursor releas
    */
    public void OnPointerDown(PointerEventData eventData)
    {        
        StartCoroutine(TRACK_CURSOR);
    }

    /**
    * While it's active (cursor still clicked, started on HSB Box canvas), register the cursor position and emit it with the dragEvent
    */
    private IEnumerator TrackCursor()
    {
        while (true)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Input.mousePosition, ParentCanvas.worldCamera, out Vector2 position);

            var rect = RectTransform.rect;
            position.x = Mathf.Clamp(position.x, rect.min.x, rect.max.x);
            position.y = Mathf.Clamp(position.y, rect.min.y, rect.max.y);

            DragEvent?.Invoke(position);

            yield return 0;
        }
    }
}