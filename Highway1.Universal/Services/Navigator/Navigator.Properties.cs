namespace Highway1.Universal.Services
{

    partial class Navigator
    {

        /// <summary>Gets or sets the back stack depth.</summary>
        /// <value>The back stack depth.</value>
        public int BackStackDepth => (_frame?.BackStackDepth).GetValueOrDefault();

        /// <summary>
        /// Gets or sets a value indicating whether this instance can go back.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go back; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoBack => (_frame?.CanGoBack).GetValueOrDefault();

        /// <summary>
        /// Gets or sets a value indicating whether this instance can go forward.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can go forward; otherwise, <c>false</c>.
        /// </value>
        public bool CanGoForward => (_frame?.CanGoForward).GetValueOrDefault();

    }

}