using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace MvvmBasic.ViewModels;

/// <summary>
/// Parent ViewModel that all ViewModel implementations can inherit from.
/// Incoporates all logic and properties that all ViewModels may need to use.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
{
    public event PropertyChangedEventHandler? PropertyChanged;

    #region Notify Property Changed

    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    public void VerifyPropertyName(string? propertyName)
    {
        // Verify that the property name matches a real,
        // public, instance property on this object.
        if (propertyName != null && TypeDescriptor.GetProperties(this)[propertyName] != null)
            return;

        string msg = "Invalid property name: " + propertyName;
        throw new ArgumentNullException(msg);
    }

    /// <summary>
    /// Inform the observers that the property has updated
    /// </summary>
    /// <param name="propertyName">The name of the property that has been updated</param>
    protected void OnPropertyChanged(string propertyName)
        => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        VerifyPropertyName(e.PropertyName);
        PropertyChanged?.Invoke(this, e);
    }

    #endregion Notify Property Changed

    /*
     * This section is for adding notification errors.
     * Each time a property changes you can add an error which will be displayed on the UI.
     * This can be removed if you do not require this feature.
     */

    #region Error Notification

    private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => _errorsByPropertyName.Any();

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
            return _errorsByPropertyName.SelectMany(x => x.Value.ToList());
        if (_errorsByPropertyName.ContainsKey(propertyName) && _errorsByPropertyName[propertyName].Count > 0)
            return _errorsByPropertyName[propertyName].ToList();
        return new List<string>();
    }

    /// <summary>
    /// Add a new error for the property
    /// </summary>
    /// <param name="propertyName">Name of the Property to add an error for</param>
    /// <param name="error">The error description to add</param>
    protected void AddErrors(string propertyName, string error)
    {
        if (!_errorsByPropertyName.ContainsKey(propertyName))
            _errorsByPropertyName[propertyName] = new List<string>();

        if (_errorsByPropertyName[propertyName].Contains(error)) return;

        _errorsByPropertyName[propertyName].Add(error);
        OnErrorsChanged(propertyName);
    }

    /// <summary>
    /// Clear all the errors
    /// </summary>
    /// <param name="propertyName">The errors to clear for</param>
    protected void ClearErrors(string propertyName)
    {
        if (!_errorsByPropertyName.ContainsKey(propertyName)) return;
        _errorsByPropertyName.Remove(propertyName);
        OnErrorsChanged(propertyName);
    }

    private void OnErrorsChanged(string propertyName)
                => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    #endregion Error Notification
}