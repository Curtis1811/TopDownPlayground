using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController
{
    private IMoveable _cameraEntity;
    private Camera camera;
    
    public float offset = 0f;

    public CameraController(IMoveable imoveable)
    {
        this._cameraEntity = imoveable;

        if(camera == null)
        {
            camera = Object.Instantiate(Camera.current);
        }

        camera.name = $"{imoveable.GameObject.name} Cam";
        camera.orthographic = true;
        camera.orthographicSize = 2;
        camera.enabled = true;
        camera.transform.position = new Vector3(imoveable.GameObject.transform.position.x, imoveable.GameObject.transform.position.y, 0);
    }

    public void FollowEntity()
    {
        var viewPortWidth = camera.rect.width * 0.2f;
        var viewportHeight = camera.rect.width * 0.2f;
        var cameraSpeedOffset = 4;

        Debug.DrawLine(camera.transform.position - new Vector3(0, viewportHeight, 0),camera.transform.position + new Vector3(0, viewportHeight, 0));
        Debug.DrawLine(camera.transform.position - new Vector3(viewPortWidth, 0, 0),camera.transform.position + new Vector3(viewPortWidth,0, 0));

        Vector3 entityPosition = new Vector3(_cameraEntity.GameObject.transform.position.x, _cameraEntity.GameObject.transform.position.y + offset, 1);

        if(Vector3.Distance(new Vector3(entityPosition.x, entityPosition.y , entityPosition.z), camera.transform.position) > 3)
        {
            cameraSpeedOffset = 10;
        }

        var cameraSpeed = Vector3.Distance(new Vector3(entityPosition.x, entityPosition.y , entityPosition.z), camera.transform.position) * cameraSpeedOffset;

        Vector3 direction = (entityPosition - camera.transform.position).normalized * Time.deltaTime * 8;
        camera.transform.position += new Vector3(direction.x , direction.y, camera.transform.position.z);
    }
}