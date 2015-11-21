namespace Highway1.Universal.ViewModels
{

    using System;

    /// <summary>Version view model class.</summary>
    public sealed class VersionViewModel
    {

        #region Properties

        /// <summary>Gets the major.</summary>
        /// <value>The major.</value>
        public ViewModel<int> Major { get; }

        /// <summary>Gets the minor.</summary>
        /// <value>The minor.</value>
        public ViewModel<int> Minor { get; }

        /// <summary>Gets the build.</summary>
        /// <value>The build.</value>
        public ViewModel<int> Build { get; }

        /// <summary>Gets the revision.</summary>
        /// <value>The revision.</value>
        public ViewModel<int> Revision { get; }

        /// <summary>Gets or sets the version.</summary>
        /// <value>The version.</value>
        public Version Version => (Version)this;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionViewModel" /> class.
        /// </summary>
        /// <param name="major">The major.</param>
        /// <param name="minor">The minor.</param>
        /// <param name="build">The build.</param>
        /// <param name="revision">The revision.</param>
        public VersionViewModel(int major = 0, int minor = 0, int build = 0, int revision = 0)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Version" /> to <see cref="VersionViewModel" />.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator VersionViewModel(Version version)
            => version == null ? null : new VersionViewModel(version.Major, version.Minor, version.Build, version.Revision);

        /// <summary>
        /// Performs an explicit conversion from <see cref="VersionViewModel" /> to <see cref="Version" />.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Version(VersionViewModel viewModel)
            => viewModel == null ? null : new Version(
                viewModel.Major.Value < 0 ? 0 : viewModel.Major.Value,
                viewModel.Minor.Value < 0 ? 0 : viewModel.Minor.Value,
                viewModel.Build.Value < 0 ? 0 : viewModel.Build.Value,
                viewModel.Revision.Value < 0 ? 0 : viewModel.Revision.Value);

        #endregion

    }

}