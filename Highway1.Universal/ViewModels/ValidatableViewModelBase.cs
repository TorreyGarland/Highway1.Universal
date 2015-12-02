namespace Highway1.Universal.ViewModels
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ValidatableViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {

        #region Properties

        /// <summary>Gets the errors.</summary>
        /// <value>The errors.</value>
        public ValidationService Errors { get; }

        /// <summary>Gets a value indicating whether this instance has errors.</summary>
        /// <value>
        /// <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors 
            => Errors.HasErrors;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatableViewModelBase"/> class.
        /// </summary>
        protected ValidatableViewModelBase()
        {
            Errors = new ValidationService(this);
        }

        public ReadOnlyDictionary<string, IReadOnlyCollection<string>> GetAllErrors()
        {
            return Errors.GetAllErrors();
        }

        /// <summary>Gets the errors.</summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName) 
            => Errors.GetErrors(propertyName);

        /// <summary>Sets the specified field.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected override bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            var result = base.Set(ref field, value, propertyName);
            if(result && !string.IsNullOrWhiteSpace(propertyName) && Errors.IsEnabled)
                Errors.ValidateProperty(propertyName);
            return result;
        }

        public void SetAllErrors(IDictionary<string, IReadOnlyCollection<string>> errors) 
            => Errors.SetAllErrors(errors);

        /// <summary>Validates the properties.</summary>
        /// <returns></returns>
        public bool ValidateProperties() 
            => Errors.ValidateProperties();

        #endregion

        #region Events

        /// <summary>Occurs when [errors changed].</summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { Errors.ErrorsChanged += value; }
            remove { Errors.ErrorsChanged -= value; }
        }

        #endregion

    }

}