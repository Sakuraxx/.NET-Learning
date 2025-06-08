#region Usings

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SimpleToDoList.Models;

#endregion Usings

namespace SimpleToDoList.ViewModels
{
    /// <summary>
    /// This is a ViewModel which represents a <see cref="Models.ToDoItem"/>
    /// </summary>
    public partial class ToDoItemViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the checked status of each item
        /// </summary>
        [ObservableProperty]
        private bool _isChecked;

        /// <summary>
        /// Gets or sets the content of the to-do item
        /// </summary>
        [ObservableProperty]
        private string? _content;

        private readonly Action<ToDoItemViewModel> _onRemove;

        [RelayCommand]
        private void Remove()
        {
            _onRemove?.Invoke(this);
        }

        /// <summary>
        /// Creates a new blank ToDoItemViewModel
        /// </summary>
        public ToDoItemViewModel()
        {
            // empty
        }

        /// <summary>
        /// Creates a new ToDoItemViewModel for the given <see cref="Models.ToDoItem"/>
        /// </summary>
        /// <param name="item">The item to load</param>
        /// <param name="onRemove">The remove action</param>
        public ToDoItemViewModel(ToDoItem item, Action<ToDoItemViewModel> onRemove)
        {
            // Init the properties with the given values
            IsChecked = item.IsChecked;
            Content = item.Content;
            _onRemove = onRemove;
        }

        /// <summary>
        /// Gets a ToDoItem of this ViewModel
        /// </summary>
        /// <returns>The ToDoItem</returns>
        public ToDoItem GetToDoItem()
        {
            return new ToDoItem()
            {
                IsChecked = this.IsChecked,
                Content = this.Content
            };
        }
    }
}