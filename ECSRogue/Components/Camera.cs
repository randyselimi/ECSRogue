namespace ECSRogue.Components
{
    public class Camera : Component
    {
        private readonly bool CameraFollowPlayer = true;

        public Camera()
        {
        }

        public Camera(Camera camera)
        {
            CameraFollowPlayer = camera.CameraFollowPlayer;
        }

        public override object Clone()
        {
            var clone = new Camera(this);
            return clone;
        }
    }
}