using GeometrySharp.Core;
using System;
using System.Text.Json.Serialization;

namespace GeometrySharp.Geometry
{
    public partial class Transform
    {
        public Matrix Matrix { get; private set; }
        [JsonIgnore]
        public Vector3 Origin => this.Matrix.Translation;

        /// <summary>
        /// The x axis of the transform.
        /// </summary>
        [JsonIgnore]
        public Vector3 XAxis => this.Matrix.XAxis;

        /// <summary>
        /// The y axis of the transform.
        /// </summary>
        [JsonIgnore]
        public Vector3 YAxis => this.Matrix.YAxis;

        /// <summary>
        /// The z axis of the transform.
        /// </summary>
        [JsonIgnore]
        public Vector3 ZAxis => this.Matrix.ZAxis;

        /// <summary>
        /// Create the identity transform.
        /// </summary
        public Transform()
        {
            this.Matrix = new Matrix();
        }


        /// <summary>
        /// Create a transform with a translation.
        /// </summary>
        /// <param name="origin">The origin of the transform.</param>
        /// <param name="rotation">An optional rotation in degrees around the transform's z axis.</param>
        public Transform(Vector3 origin, double rotation = 0.0)
        {
            this.Matrix = new Matrix();
            this.Matrix.SetupTranslation(origin);
            ApplyRotationAndTranslation(rotation, this.Matrix.ZAxis, Vector3.Origin);
        }

        /// <summary>
        /// Create a transform with a translation.
        /// </summary>
        /// <param name="x">The X component of translation.</param>
        /// <param name="y">The Y component of translation.</param>
        /// <param name="z">The Z component of translation.</param>
        /// <param name="rotation">An optional rotation in degrees around the transform's z axis.</param>
        public Transform(double x, double y, double z, double rotation = 0.0)
        {
            this.Matrix = new Matrix();
            this.Matrix.SetupTranslation(new Vector3(x, y, z));
            ApplyRotationAndTranslation(rotation, this.Matrix.ZAxis, Vector3.Origin);
        }
        private void ApplyRotationAndTranslation(double rotation, Vector3 axis, Vector3 translation)
        {
            if (rotation != 0.0)
            {
                this.Rotate(axis, rotation);
            }
            // Apply translation after rotation.
            this.Move(translation);
        }

        public void Move(Vector3 translation)
        {
            var m = new Matrix();
            m.SetupTranslation(translation);
            this.Matrix = this.Matrix * m;
        }

        /// <summary>
        /// Apply a translation to the transform.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Move(double x = 0.0, double y = 0.0, double z = 0.0)
        {
            Move(new Vector3(x, y, z));
        }

        /// <summary>
        /// Apply a rotation to the transform.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in degrees.</param>
        public void Rotate(Vector3 axis, double angle)
        {
            var m = new Matrix();
            m.SetupRotate(axis, angle * (Math.PI / 180.0));
            this.Matrix = this.Matrix * m;
        }

        /// <summary>
        /// Apply a rotation to the transform around the Z axis.
        /// </summary>
        /// <param name="angle">The angle of rotation in degrees.</param>
        public void Rotate(double angle)
        {
            Rotate(Vector3.ZAxis, angle);
        }

    }


}
