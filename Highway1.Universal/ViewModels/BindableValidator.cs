namespace Highway1.Universal.ViewModels
{

    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Diagnostics;

    public class ValidationService : ViewModelBase, INotifyDataErrorInfo
    {

        #region Fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConcurrentDictionary<string, IReadOnlyCollection<string>> _errors = new ConcurrentDictionary<string, IReadOnlyCollection<string>>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly object _validatableEntity;

        #endregion

        #region Properties

        /// <summary>Gets a value indicating whether this instance has errors.</summary>
        /// <value>
        /// <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors => GetAllErrors().Any();

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; } = true;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationService"/> class.
        /// </summary>
        /// <param name="validatableEntity">The validatable entity.</param>
        public ValidationService(object validatableEntity)
        {
            Contract.Requires<ArgumentNullException>(validatableEntity != null, nameof(validatableEntity));
            _validatableEntity = validatableEntity;
        }

        /// <summary>Gets all errors.</summary>
        /// <returns></returns>
        [Pure]
        public ReadOnlyDictionary<string, IReadOnlyCollection<string>> GetAllErrors()
        {
            Contract.Ensures(Contract.Result<ReadOnlyDictionary<string, IReadOnlyCollection<string>>>() != null, nameof(GetAllErrors));
            return _errors.ToReadOnlyDictionary();
        }

        /// <summary>Gets the errors.</summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName) 
            => GetAllErrors().Where(x => string.Equals(x.Key, propertyName)).Select(x => x.Value);

        /// <summary>
        /// Called when [errors changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnErrorsChanged([CallerMemberName] string propertyName = null)
            => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        private void RaiseHandlers(string propertyName)
        {
            OnErrorsChanged(propertyName);
            OnPropertyChanged(string.Format(CultureInfo.CurrentCulture, "Item[{0}]", propertyName));
        }

        /// <summary>Sets all errors.</summary>
        /// <param name="entityErrors">The entity errors.</param>
        public void SetAllErrors(IDictionary<string, IReadOnlyCollection<string>> entityErrors)
        {
            Contract.Requires<ArgumentNullException>(entityErrors != null, nameof(entityErrors));
            _errors.Clear();
            entityErrors.Each(pair => SetPropertyErrors(pair.Key, pair.Value));
            OnPropertyChanged("Item[]");
            OnErrorsChanged(string.Empty);
        }

        private bool SetPropertyErrors(string propertyName, IReadOnlyCollection<string> newPropertyErrors)
        {
            Contract.Requires(propertyName != null, nameof(propertyName));
            Contract.Requires(newPropertyErrors != null, nameof(newPropertyErrors));
            var errorsChanged = false;
            if (!_errors.ContainsKey(propertyName) && newPropertyErrors.Any())
            {
                _errors.TryAdd(propertyName, newPropertyErrors);
                errorsChanged = true;
            }
            else if (newPropertyErrors.Count != _errors[propertyName].Count || _errors[propertyName].Intersect(newPropertyErrors).Count() != newPropertyErrors.Count)
            {
                if (newPropertyErrors.Any())
                    _errors[propertyName] = newPropertyErrors;
                else
                {
                    IReadOnlyCollection<string> result;
                    _errors.TryRemove(propertyName, out result);
                }
                errorsChanged = true;
            }
            return errorsChanged;
        }

        private bool TryValidateProperty(PropertyInfo propertyInfo, List<string> propertyErrors)
        {
            Contract.Requires(propertyInfo != null, nameof(propertyInfo));
            Contract.Requires(propertyErrors != null, nameof(propertyErrors));
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_validatableEntity) { MemberName = propertyInfo.Name };
            var propertyValue = propertyInfo.GetValue(_validatableEntity);
            var isValid = Validator.TryValidateProperty(propertyValue, validationContext, validationResults);
            if (validationResults.Any())
                propertyErrors.AddRange(validationResults.Select(x => x.ErrorMessage));
            return isValid;
        }

        /// <summary>Validates the properties.</summary>
        /// <returns></returns>
        public bool ValidateProperties()
        {
            var propertyNames = new HashSet<string>();
            _validatableEntity
                .GetType()
                .GetRuntimeProperties().Where(x => x.GetCustomAttributes<ValidationAttribute>().Any())
                .ToArray()
                .Each(propertyInfo =>
                {
                    var propertyErrors = new List<string>();
                    TryValidateProperty(propertyInfo, propertyErrors);
                    var errorsChanged = SetPropertyErrors(propertyInfo.Name, propertyErrors);
                    if (errorsChanged)
                        propertyNames.Add(propertyInfo.Name);
                });
            propertyNames.Each(RaiseHandlers);
            return !_errors.Values.Any();
        }

        /// <summary>Validates the property.</summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool ValidateProperty(string propertyName)
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);
            var propertyInfo = _validatableEntity.GetType().GetRuntimeProperty(propertyName);
            if (propertyInfo == null)
                throw new InvalidOperationException(propertyName);
            var propertyErrors = new List<string>();
            var isValid = TryValidateProperty(propertyInfo, propertyErrors);
            var errorsChanged = SetPropertyErrors(propertyInfo.Name, propertyErrors);
            if (errorsChanged)
                RaiseHandlers(propertyName);
            return isValid;
        }

        #endregion

        #region Events

        /// <summary>Occurs when [errors changed].</summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

    }

}