using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ScrollingBehaviour : MonoBehaviour
{
    /// <summary>
    /// Scrolling speed
    /// </summary>
    [SerializeField] private Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Moving direction
    /// </summary>
    [SerializeField] private Vector2 direction = new Vector2(-1, 0);

    /// <summary>
    /// Movement should be applied to camera
    /// </summary>
    [SerializeField] private bool isLinkedToCamera = false;

    /// <summary>
    /// Whether elements should be looped
    /// </summary>
    [SerializeField] private bool isLooping = false;

    /// <summary>
    /// List of children with a renderer.
    /// </summary>
    private List<SpriteRenderer> _backgroundPart;

    private void Start()
    {
        if (!isLooping)
        {
            return;
        }

        // Get all the children of the layer with a renderer
        _backgroundPart = new List<SpriteRenderer>();

        for (var i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer r = child.GetComponent<SpriteRenderer>();

            // Add only the visible children
            if (r != null)
            {
                _backgroundPart.Add(r);
            }
        }

        // Sort by position.
        _backgroundPart = _backgroundPart.OrderBy(
            t => t.transform.position.x
        ).ToList();
    }

    private void Update()
    {
        var movement = new Vector3(
            speed.x * direction.x,
            speed.y * direction.y,
            0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping)
        {
            // Get the first object.
            // The list is ordered from left (x position) to right.
            var firstChild = _backgroundPart.FirstOrDefault();

            if (firstChild == null) return;

            // Check if the child is already (partly) before the camera.
            // We test the position first because the IsVisibleFrom
            // method is a bit heavier to execute.
            if (firstChild.transform.position.x >= Camera.main.transform.position.x) return;

            // If the child is already on the left of the camera,
            // we test if it's completely outside and needs to be
            // recycled.
            if (firstChild.IsVisibleFrom(Camera.main)) return;

            // Get the last child position.
            var lastChild = _backgroundPart.LastOrDefault();

            Debug.Log($"Placing {firstChild.gameObject.name} after {lastChild.gameObject.name}");

            var lastPosition = lastChild.transform.position;
            var lastSize = (lastChild.bounds.max - lastChild.bounds.min);

            // Set the position of the recycled one to be AFTER
            // the last child.
            firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y,
                firstChild.transform.position.z);

            // Set the recycled child to the last position
            // of the backgroundPart list.
            _backgroundPart.Remove(firstChild);
            _backgroundPart.Add(firstChild);
        }
    }
}