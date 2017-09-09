namespace BlitzDemoGunBoat
{
    using Base.RuntimeChecks;
    using Camera;

    /// <summary>
    /// Represents a "game state" for this demo.
    /// </summary>
    public class DemoGameState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoGameState"/> class.
        /// </summary>
        public DemoGameState(ICameraTransformation cameraTransformation)
        {
            ArgumentChecks.AssertNotNull(cameraTransformation, nameof(cameraTransformation));

            this.CameraTransformation = cameraTransformation;
        }

        /// <summary>
        /// Gets the camera transformation.
        /// </summary>
        public ICameraTransformation CameraTransformation { get; }
    }
}
