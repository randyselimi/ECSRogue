using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue2.Components
{
    public class Camera : Component
    {
        bool CameraFollowPlayer = true;
        public Camera()
        {

        }
        public Camera(Camera camera)
        {
            this.CameraFollowPlayer = camera.CameraFollowPlayer;
        }
        public override object Clone()
        {
            Camera clone = new Camera(this);
            return clone;
        }
    }
}
