﻿using System;
using System.Windows.Input;

namespace WpfUpdateListInList.Command
{
    /// <summary>
    /// Non-generic class that implements ICommand. This class takes a action without parameter that
    /// can be executed if a condition is met.
    /// </summary>
    public class CommandHandler : ICommand
    {
        /// <summary>Action to execute in case the command is executable</summary>
        private Action MethodToExecute = null;

        /// <summary>Method to determine whether the command is executable or not</summary>
        private Func<bool> MethodToDetermineCanExecute = null;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="methodToExecute">Method to execute in case the command is executable</param>
        /// <param name="methodToDetermineCanExecute">Method to determine whether the command is executable or not</param>
        public CommandHandler(Action methodToExecute, Func<bool> methodToDetermineCanExecute)
        {
            this.MethodToExecute = methodToExecute;
            this.MethodToDetermineCanExecute = methodToDetermineCanExecute;
        }

        /// <summary>
        /// Determine whether the command is executable or not
        /// </summary>
        /// <param name="parameter">Optional parameters</param>
        /// <returns>True if the method can be executed, false otherwise</returns>
        public bool CanExecute(object parameter)
        {
            if (MethodToDetermineCanExecute == null)
            {
                return true;
            }
            else
            {
                return MethodToDetermineCanExecute();
            }
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">optional parameters</param>
        public void Execute(object parameter)
        {
            MethodToExecute();
        }

        /// <summary>
        /// Event handler to raise when the method to determine whether the command
        /// is executable or not changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raise the event indicating that something changed and we need
        /// to check if the command is now executable or not
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
