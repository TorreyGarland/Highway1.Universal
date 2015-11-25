namespace Highway1.Universal.ViewModels
{

    using System;
    using System.ComponentModel;
    using System.Diagnostics.Contracts;

    /// <summary>Version view model class.</summary>
    public sealed class VersionViewModel : ViewModelBase, IComparable<VersionViewModel>, IEquatable<VersionViewModel>
    {

        #region Properties

        /// <summary>Gets the major.</summary>
        /// <value>The major.</value>
        public ViewModel<int> Major { get; }

        /// <summary>Gets or sets the major revision.</summary>
        /// <value>The major revision.</value>
        public short MajorRevision => Value.MajorRevision;

        /// <summary>Gets the minor.</summary>
        /// <value>The minor.</value>
        public ViewModel<int> Minor { get; }

        /// <summary>Gets or sets the minor revision.</summary>
        /// <value>The minor revision.</value>
        public short MinorRevision => Value.MinorRevision;

        /// <summary>Gets the build.</summary>
        /// <value>The build.</value>
        public ViewModel<int> Build { get; }

        /// <summary>Gets the revision.</summary>
        /// <value>The revision.</value>
        public ViewModel<int> Revision { get; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The version.</value>
        public Version Value => (Version)this;

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
            Contract.Requires<ArgumentOutOfRangeException>(major >= 0, nameof(major));
            Contract.Requires<ArgumentOutOfRangeException>(minor >= 0, nameof(minor));
            Contract.Requires<ArgumentOutOfRangeException>(build >= 0, nameof(build));
            Contract.Requires<ArgumentOutOfRangeException>(revision >= 0, nameof(revision));
            Major = new ViewModel<int>(major);
            Minor = new ViewModel<int>(minor);
            Build = new ViewModel<int>(build);
            Revision = new ViewModel<int>(revision);
            Major.PropertyChanged += OnPropertyChanged;
            Major.ValueChanging += ValueChanging;
            Minor.PropertyChanged += OnPropertyChanged;
            Minor.ValueChanging += ValueChanging;
            Build.PropertyChanged += OnPropertyChanged;
            Build.ValueChanging += ValueChanging;
            Revision.PropertyChanged += OnPropertyChanged;
            Revision.ValueChanging += ValueChanging;
        }

        /// <summary>Compares to.</summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(VersionViewModel other)
            => Value.CompareTo(other?.Value);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
            => Equals(obj as VersionViewModel);

        /// <summary>Equalses the specified other.</summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(VersionViewModel other)
            => ReferenceEquals(this, other) || (other != null && Value.Equals(other.Value));

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
            => (Value?.GetHashCode()).GetValueOrDefault();

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
            => RaisePropertyChanged(nameof(Value), nameof(MajorRevision), nameof(MinorRevision));

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString() => Value.ToString();

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="fieldCount">The field count.</param>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        [Pure]
        public string ToString(int fieldCount)
        {
            Contract.Requires<ArgumentOutOfRangeException>(fieldCount >= 0, nameof(fieldCount));
            Contract.Requires<ArgumentOutOfRangeException>(fieldCount <= 4, nameof(fieldCount));
            Contract.Ensures(Contract.Result<string>() != null, nameof(ToString));
            return Value.ToString(fieldCount);
        }

        private void ValueChanging(object sender, CancelEventArgs e) 
            => e.Cancel = ((sender as ViewModel<int>)?.Value).GetValueOrDefault() < 0;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Value" /> to <see cref="VersionViewModel" />.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator VersionViewModel(Version version)
            => version == null ? null : new VersionViewModel(version.Major, version.Minor, version.Build, version.Revision);

        /// <summary>Tries the parse.</summary>
        /// <param name="input">The input.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        [Pure]
        public static bool TryParse(string input, out VersionViewModel result)
        {
            result = (VersionViewModel)input.ToVersion();
            return result != null;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="VersionViewModel" /> to <see cref="Value" />.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Version(VersionViewModel viewModel)
            => viewModel == null ? null : new Version(
                viewModel.Major.Value < 0 ? 0 : viewModel.Major.Value,
                viewModel.Minor.Value < 0 ? 0 : viewModel.Minor.Value,
                viewModel.Build.Value < 0 ? 0 : viewModel.Build.Value,
                viewModel.Revision.Value < 0 ? 0 : viewModel.Revision.Value);

        /// <summary>Implements the operator ==.</summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(VersionViewModel a, VersionViewModel b)
            => (a?.Equals(b)).GetValueOrDefault();

        /// <summary>Implements the operator !=.</summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(VersionViewModel a, VersionViewModel b)
            => !(a == b);

        /// <summary>Implements the operator &lt;.</summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(VersionViewModel a, VersionViewModel b)
            => a?.Value < b?.Value;

        /// <summary>Implements the operator &gt;.</summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(VersionViewModel a, VersionViewModel b)
            => a?.Value > b?.Value;

        /// <summary>Implements the operator &lt;=.</summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(VersionViewModel a, VersionViewModel b)
            => a?.Value <= b?.Value;

        /// <summary>Implements the operator &gt;=.</summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(VersionViewModel a, VersionViewModel b)
            => a?.Value >= b?.Value;

        #endregion

    }

}